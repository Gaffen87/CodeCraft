import { HubConnection } from "@microsoft/signalr";
import type { AddToGroupPayload, RemoveFromGroupPayload } from "~/types/types";

export async function AddToGroup(
	connection: HubConnection | null,
	payload: AddToGroupPayload
) {
	await connection?.invoke("InvokeMethod", "AddToGroup", payload);
}

export async function RemoveFromGroup(
	connection: HubConnection | null,
	payload: RemoveFromGroupPayload
) {
	await connection?.invoke("InvokeMethod", "RemoveFromGroup", payload);
}
