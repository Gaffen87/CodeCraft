import useSignalR from "~/hooks/useSignalR";
import { AddToGroup, RemoveFromGroup } from "../../services/signalr-service";
import { Button } from "~/components/ui/button";
import { useEffect } from "react";
import { useMessageStore } from "~/stores/messageStore";
import type { Message } from "~/types/types";

export default function Test() {
	const { connection, isConnected } = useSignalR();
	const { messages } = useMessageStore();

	async function Add() {
		await AddToGroup(connection, { groupName: "TestGruppe" });
	}

	async function Remove() {
		await RemoveFromGroup(connection, { groupName: "TestGroup" });
	}

	return (
		<div>
			<h1>Test</h1>
			<p>This is a test component.</p>
			<Button onClick={Add}>Add</Button>
			<Button onClick={Remove}>Remove</Button>
			<p>Connection Status: {isConnected ? "Connected" : "Disconnected"}</p>
		</div>
	);
}
