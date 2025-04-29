import { useContext } from "react";
import { SignalRContext } from "~/contexts/signalrContext";

export default function useSignalR() {
	return useContext(SignalRContext);
}
