import useSignalR from "~/hooks/useSignalR";
import { AddToGroup } from "./services/signalr-service";
import { Button } from "~/components/ui/button";
import { useEffect } from "react";

export default function Test() {
	const { connection, isConnected } = useSignalR();

	async function test() {
		await AddToGroup(connection, "test-group");
	}

	return (
		<div>
			<h1>Test</h1>
			<p>This is a test component.</p>
			<Button onClick={test}>Test</Button>
			<p>Connection Status: {isConnected ? "Connected" : "Disconnected"}</p>
		</div>
	);
}
