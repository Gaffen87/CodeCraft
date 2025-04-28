import {
	Card,
	CardContent,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";
import type { Group } from "~/types/types";

export default function GroupCard({ group }: { group: Group }) {
	return (
		<Card className="w-[350px]">
			<CardHeader>
				<CardTitle>{group.name}</CardTitle>
				<CardDescription>members</CardDescription>
			</CardHeader>
			<CardContent>
				{group.members &&
					group.members.map((member) => <p key={member.id}>{member.name}</p>)}
			</CardContent>
			<CardFooter>{/* Add any footer content here */}</CardFooter>
		</Card>
	);
}
