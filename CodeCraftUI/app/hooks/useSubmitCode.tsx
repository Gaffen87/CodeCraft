import { useState } from "react";
import { useCodeStore } from "~/stores/codeStore";

export default function useSubmitCode() {
	const { code, setConsole } = useCodeStore();
	const [loading, setLoading] = useState(false);

	async function Submit(groupId: string) {
		setLoading(true);
		const response = await fetch(
			import.meta.env.VITE_API_URL + "/code/submissions",
			{
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify({
					files: [
						{
							fileName: "Program.cs",
							content: code,
						},
					],
					submittedBy: groupId,
					exerciseStep: "83dfe1be-baf5-47db-b797-b403089fa1a9",
				}),
			}
		);
		const data = await response.json();
		setConsole(data.result);
		setLoading(false);
	}

	return { Submit, loading };
}
