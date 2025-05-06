import useSignalR from "~/hooks/useSignalR";
import { Button } from "~/components/ui/button";
import { useEffect } from "react";
import { useMessageStore } from "~/stores/messageStore";
import type { Message } from "~/types/types";

export default function Test() {
	const { connection, isConnected } = useSignalR();
	const { messages } = useMessageStore();

	return <div></div>;
}
