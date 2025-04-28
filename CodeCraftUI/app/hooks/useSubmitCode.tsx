import { useEffect, useState } from "react";
import { useCodeStore } from "~/stores/codeStore";

export default function useSubmitCode() {
	const { code, setConsole } = useCodeStore();
	const [loading, setLoading] = useState(false);

	async function Submit() {
		setLoading(true);
		const response = await fetch("https://localhost:7060/code/submissions", {
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
				submittedBy: "a2e663d4-373c-4f65-b814-908a83c16e2c",
				exerciseStep: "ae487a06-a508-4191-9f22-faae7f9463ba",
			}),
		});
		const data = await response.json();
		setConsole(data.result);
		setLoading(false);
	}

	return { Submit, loading };
}
