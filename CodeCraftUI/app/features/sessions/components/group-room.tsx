import Editor from "./editor";
import Console from "./console";
import { useGroupStore } from "~/stores/groupStore";
import useAuth from "~/hooks/useAuth";
import useGroups from "~/hooks/useGroups";
import { useEffect } from "react";

import Header from "./header";
import ButtonPanel from "./button-panel";

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
			<Editor className="h-3/5 w-full" groupName={getGroupName(groupId)} />
			<ButtonPanel groupId={groupId} />
			<Console className="p-2" />
		</div>
	);
}
