import useSignalR from "~/hooks/useSignalR";
import { AddToGroup } from "./services/signalr-service";
import { Button } from "~/components/ui/button";
import { useEffect } from "react";
import { useMessageStore } from "~/stores/messageStore";
import type { Message } from "~/types/types";

export default function Test() {
	const { connection, isConnected } = useSignalR();
	const { messages } = useMessageStore();

	async function test() {
		await AddToGroup(connection, "test-group 2");
	}

	useEffect(() => {}, []);

	return (
		<div>
			<h1>Test</h1>
			<p>This is a test component.</p>
			<Button onClick={test}>Test</Button>
			<p>Connection Status: {isConnected ? "Connected" : "Disconnected"}</p>
		</div>
	);
}
