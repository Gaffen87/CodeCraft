import { useState } from "react";
import { useGroupStore } from "~/stores/groupStore";
import type { AddToGroupPayload } from "~/types/types";
import useSignalR from "~/hooks/useSignalR";

export function useGroups() {
	const { setGroups } = useGroupStore();
	const { connection } = useSignalR();
	const [loading, setLoading] = useState(true);

	async function fetchGroups() {
		try {
			const res = await fetch("https://localhost:7060/groups");
			const data = await res.json();
			console.log("Fetched groups:", data);
			if (res.ok) {
				var groups = data.groups.map((group: any) => {
					return {
						id: group.id,
						name: group.name,
						members: group.members.map((member: any) => ({
							id: member.id,
							name: member.userName,
						})),
					};
				});
				setGroups(groups);
			}
		} catch (err) {
			console.error("Failed to fetch groups:", err);
		} finally {
			setLoading(false);
		}
	}

	async function addToGroup(payload: AddToGroupPayload) {
		await connection?.invoke("InvokeMethod", "AddToGroup", payload);
	}

	return { loading, fetchGroups, addToGroup };
}
