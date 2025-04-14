import type { Group } from "~/types/types";

type GroupsIndexProps = {
	groups: Group[];
};

export default function GroupsIndex({ groups }: GroupsIndexProps) {
	return (
		<div>
			<p>Group</p>
		</div>
	);
}
