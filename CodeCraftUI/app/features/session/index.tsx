import GroupRoom from "./components/group-room";
import type { Route } from "./+types/index";

export default function Session({ params }: Route.ComponentProps) {
	return (
		<>
			<GroupRoom groupId={params.groupId} />
		</>
	);
}
