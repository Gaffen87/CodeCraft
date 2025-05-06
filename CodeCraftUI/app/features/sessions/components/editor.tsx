import { useEffect, useRef, useState } from "react";
import editorWorker from "monaco-editor/esm/vs/editor/editor.worker?worker";
import * as monaco from "monaco-editor";
import { useCodeStore } from "~/stores/codeStore";
import { useEditorStore } from "~/stores/editorStore";
import useSignalR from "~/hooks/useSignalR";
import useGroups from "~/hooks/useGroups";

export default function Editor({
	className,
	groupName,
}: {
	className?: string;
	groupName: string | undefined;
}) {
	const [editor, setEditor] =
		useState<monaco.editor.IStandaloneCodeEditor | null>(null);
	const monacoRef = useRef(null);
	const isRemoteEdit = useRef(false);
	const editorTheme = useEditorStore((state) => state.editorTheme);
	const { code, setCode } = useCodeStore();
	const { codeChange } = useGroups();
	const { connection } = useSignalR();

	useEffect(() => {
		window.MonacoEnvironment = {
			getWorker(workerId: string, label: string) {
				return new editorWorker();
			},
		};

		if (monacoRef) {
			setEditor((editor) => {
				if (editor) return editor;

				return monaco.editor.create(monacoRef.current!, {
					language: "csharp",
					value: code,
					theme: editorTheme,
					automaticLayout: true,
					minimap: {
						enabled: false,
					},
					scrollBeyondLastLine: false,
					overviewRulerBorder: false,
					overviewRulerLanes: 0,
				});
			});
		}

		editor?.onDidChangeModelContent((e) => {
			const newCode = editor?.getValue() || "";
			setCode(newCode);

			if (isRemoteEdit.current) return;

			codeChange({ changes: e.changes, groupName: groupName! });
		});

		return () => editor?.dispose();
	}, [monacoRef.current]);

	useEffect(() => {
		if (editor) {
			connection?.on("ReceiveEditorMessage", (message) => {
				isRemoteEdit.current = true;

				console.log("Received code message:", message);
				editor?.executeEdits("signalR", message);

				isRemoteEdit.current = false;
			});
		}

		return () => {
			connection?.off("ReceiveEditorMessage");
		};
	}, [editor]);

	useEffect(() => {
		if (editor) {
			editor.updateOptions({
				theme: editorTheme,
			});
		}
	}, [editorTheme]);

	return <div className={className} ref={monacoRef}></div>;
}
