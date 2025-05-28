import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { createContext, useEffect, useState } from "react";
import useAuth from "../hooks/useAuth";
import { useMessageStore } from "~/stores/messageStore";
import { useGroupStore } from "~/stores/groupStore";
import type { Message } from "~/types/types";
import { toast } from "sonner";
import { useNavigate } from "react-router";
import { useSubmitStore } from "~/stores/submitStore";

interface SignalRContextType {
	connection: HubConnection | null;
	isConnected: boolean;
}

export const SignalRContext = createContext<SignalRContextType>({
	connection: null,
	isConnected: false,
});

export default function SignalRProvider({
	children,
}: {
	children: React.ReactNode;
}) {
	const [connection, setConnection] = useState<HubConnection | null>(null);
	const [isConnected, setIsConnected] = useState(false);
	const { addMessage } = useMessageStore();
	const { addGroup, removeGroup, addMember, removeMember, setGroupExercise } =
		useGroupStore();
	const { addCodeSubmit } = useSubmitStore();
	const { session } = useAuth();
	const navigate = useNavigate();

	useEffect(() => {
		if (!session?.access_token) return;

		const newConnection = new HubConnectionBuilder()
			.withUrl(import.meta.env.VITE_API_URL + "/hub", {
				accessTokenFactory: () => session.access_token,
			})
			.withAutomaticReconnect()
			.build();

		setConnection(newConnection);

		newConnection
			.start()
			.then(() => setIsConnected(true))
			.catch((err) => {
				console.error("SignalR Connection Error:", err);
				setIsConnected(false);
			});

		newConnection.on("ReceiveMessage", (message: Message) => {
			addMessage(message);
			console.log("Received message:", message);
		});

		newConnection.on("ReceiveCodeMessage", (message) => {
			addCodeSubmit(message.groupId, {
				groupName: message.groupName,
				content: message.codeResult,
				isSuccess: message.isSuccess,
				timestamp: message.timeStamp,
			});
			toast.info(`Code submitted by ${message.groupName}`, {
				description: `Result: ${message.codeResult}`,
				duration: 10000,
				action: {
					label: "Go to",
					onClick: () => {
						navigate("/session/" + message.groupId);
					},
				},
			});
		});

		newConnection.on(
			"ReceiveGroupExerciseMessage",
			(groupId, exerciseTitle, exerciseItemNumber, stepIndex) => {
				console.log("Received group exercise message:", {
					groupId,
					exerciseTitle,
					exerciseItemNumber,
					stepIndex,
				});
				if (exerciseTitle === null) {
					setGroupExercise(groupId, null);
				} else {
					setGroupExercise(groupId, {
						exerciseTitle: exerciseTitle,
						subExerciseNumber: exerciseItemNumber,
						stepIndex: stepIndex,
					});
				}
			}
		);

		newConnection.on("ReceiveGroupMessage", (message) => {
			if (message.type === "created") {
				addGroup({
					id: message.groupId,
					name: message.groupName,
					members: [],
					exerciseInfo: null,
				});
			}
			if (message.type === "deleted") {
				removeGroup(message.groupId);
			}
			if (message.type === "joined") {
				addMember(message.groupId, {
					id: message.user[0].id,
					name: message.user[0].userName,
				});
			}
			if (message.type === "left") {
				removeMember(message.groupId, message.userId);
			}
		});

		newConnection.onclose(() => setIsConnected(false));
		newConnection.onreconnected(() => setIsConnected(true));

		return () => {
			newConnection
				.stop()
				.catch((err) => console.error("SignalR Stop Error:", err));
		};
	}, [session?.access_token]);

	return (
		<SignalRContext.Provider value={{ connection, isConnected }}>
			{children}
		</SignalRContext.Provider>
	);
}
