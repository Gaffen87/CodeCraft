import { HubConnection } from "@microsoft/signalr";

export async function AddToGroup(
	connection: HubConnection | null,
	groupName: string
) {
	await connection?.invoke("AddToGroup", groupName);
}
