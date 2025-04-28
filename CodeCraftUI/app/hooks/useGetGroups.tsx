import { useEffect, useState } from "react";
import { useGroupStore } from "~/stores/groupStore";

export function useGetGroups() {
	const { addGroup } = useGroupStore();
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		async function fetchGroups() {
			try {
				const res = await fetch("https://localhost:7060/groups");
				const data = await res.json();
				console.log("Fetched groups:", data);
				if (res.ok) {
					data.groups.forEach((group: any) => {
						addGroup({
							id: group.id,
							name: group.name,
							members: group.members.map((member: any) => ({
								id: member.id,
								name: member.userName,
							})),
						});
					});
				}
			} catch (err) {
				console.error("Failed to fetch groups:", err);
			} finally {
				setLoading(false);
			}
		}
		fetchGroups();
	}, []);

	return { loading };
}
