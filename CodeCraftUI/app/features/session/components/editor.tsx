import { useEffect, useRef, useState } from "react";
import editorWorker from "monaco-editor/esm/vs/editor/editor.worker?worker";
import * as monaco from "monaco-editor";
import { useCodeStore } from "~/stores/codeStore";

export default function Editor({ className }: { className?: string }) {
	const [editor, setEditor] =
		useState<monaco.editor.IStandaloneCodeEditor | null>(null);
	const monacoRef = useRef(null);
	const { code, setCode } = useCodeStore();

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
				});
			});
		}

		editor?.onDidChangeModelContent(() => {
			const newCode = editor?.getValue() || "";
			setCode(newCode);
		});

		return () => editor?.dispose();
	}, [monacoRef.current]);

	return <div className={className} ref={monacoRef}></div>;
}
