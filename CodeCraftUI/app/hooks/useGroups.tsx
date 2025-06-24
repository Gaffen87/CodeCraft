import { useState } from "react";
import { useGroupStore } from "~/stores/groupStore";
import type { AddToGroupPayload, RemoveFromGroupPayload } from "~/types/types";
import useSignalR from "~/hooks/useSignalR";
import * as monaco from "monaco-editor";

export default function useGroups() {
	const { setGroups } = useGroupStore();
	const { connection } = useSignalR();
	const [loading, setLoading] = useState(false);

	async function fetchGroups() {
		setLoading(true);
		try {
			const res = await fetch(import.meta.env.VITE_API_URL + "/groups");
			const data = await res.json();
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
				console.log("Fetched groups:", groups);
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

	async function removeFromGroup(payload: RemoveFromGroupPayload) {
		setLoading(true);
		await connection?.invoke("InvokeMethod", "RemoveFromGroup", payload);
		setLoading(false);
	}

	async function codeChange(payload: {
		changes: monaco.editor.IModelContentChange[];
		groupName: string;
	}) {
		await connection?.invoke("InvokeMethod", "EditorChanged", payload);
	}

	async function getSubmissionsByGroup(groupId: string) {
		setLoading(true);
		try {
			const res = await fetch(
				import.meta.env.VITE_API_URL + `/code/submissions/${groupId}`
			);
			const data = await res.json();
			return data.submissions;
		} catch (err) {
			console.error("Failed to fetch submissions:", err);
		} finally {
			setLoading(false);
		}
	}

	return {
		loading,
		fetchGroups,
		addToGroup,
		removeFromGroup,
		getSubmissionsByGroup,
		codeChange,
	};
}
