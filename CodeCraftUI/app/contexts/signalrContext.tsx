import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { createContext, useEffect, useState } from "react";
import useAuth from "../hooks/useAuth";
import { useMessageStore } from "~/stores/messageStore";
import { useGroupStore } from "~/stores/groupStore";
import type { Group, GroupMessage, Message } from "~/types/types";

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
	const { addGroup, removeGroup, addMember, removeMember } = useGroupStore();
	const { session } = useAuth();

	useEffect(() => {
		if (!session?.access_token) return;

		const newConnection = new HubConnectionBuilder()
			.withUrl("https://localhost:7060/hub", {
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

		newConnection.on("ReceiveGroupMessage", (message) => {
			console.log("Received group message:", message);
			if (message.type === "created") {
				addGroup({ id: message.groupId, name: message.groupName, members: [] });
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
				removeMember(message.groupId, message.user[0].id);
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
