import { useState } from "react";
import { useGroupStore } from "~/stores/groupStore";
import type { AddToGroupPayload } from "~/types/types";
import useSignalR from "~/hooks/useSignalR";

export default function useGroups() {
	const { setGroups } = useGroupStore();
	const { connection } = useSignalR();
	const [loading, setLoading] = useState(false);

	async function fetchGroups() {
		setLoading(true);
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
		setLoading(true);
		await connection?.invoke("InvokeMethod", "AddToGroup", payload);
		setLoading(false);
	}

	return { loading, fetchGroups, addToGroup };
}
