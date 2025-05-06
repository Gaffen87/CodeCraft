import { Button } from "~/components/ui/button";
import Editor from "./editor";
import useSubmitCode from "~/hooks/useSubmitCode";
import Console from "./console";
import { useNavigate } from "react-router";
import { useGroupStore } from "~/stores/groupStore";
import useAuth from "~/hooks/useAuth";
import useGroups from "~/hooks/useGroups";
import { ModeToggle } from "~/shared/theme-toggle";
import { useEffect } from "react";

export default function GroupRoom({ groupId }: { groupId: string }) {
	const { Submit, loading } = useSubmitCode();
	const navigate = useNavigate();
	const { removeMember, getGroupName } = useGroupStore();
	const { removeFromGroup, addToGroup } = useGroups();
	const { user } = useAuth();

	useEffect(() => {
		addToGroup({ groupName: getGroupName(groupId)! });
		console.log("Added to group: ", getGroupName(groupId));

		return () => {
			removeFromGroup({ groupName: getGroupName(groupId)! });
			removeMember(groupId, user!.id);
		};
	}, [groupId]);

	return (
		<div className="flex flex-col w-full h-full mt-5">
			<Editor className="h-3/5 w-full" groupName={getGroupName(groupId)} />
			<div className="ml-10 flex items-center gap-4">
				<ModeToggle />
				<Button
					onClick={() => Submit(groupId)}
					disabled={loading}
					variant={"outline"}
				>
					{loading ? "Loading..." : "Run Code"}
				</Button>
				<Button
					variant={"destructive"}
					onClick={() => {
						navigate("/");
					}}
				>
					Leave room
				</Button>
			</div>
			<Console className="p-2" />
		</div>
	);
}
