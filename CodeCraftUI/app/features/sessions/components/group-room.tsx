import Editor from "./editor";
import Console from "./console";
import { useGroupStore } from "~/stores/groupStore";
import useAuth from "~/hooks/useAuth";
import useGroups from "~/hooks/useGroups";
import { useEffect } from "react";

import Header from "./header";
import ButtonPanel from "./button-panel";
import ExercisePanel from "./exercise-panel";

export default function GroupRoom({ groupId }: { groupId: string }) {
	const { removeMember, getGroupName, groups } = useGroupStore();
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
			<Header groups={groups} groupId={groupId} />
			<div className="flex h-full w-full">
				<div className="w-1/4 h-full">
					<ExercisePanel className="w-full h-full p-2 bg-secondary" />
				</div>
				<div className="w-3/4 h-full">
					<Editor className="h-3/5 w-full" groupName={getGroupName(groupId)} />
					<ButtonPanel groupId={groupId} />
					<Console className="p-2" />
				</div>
			</div>
		</div>
	);
}
