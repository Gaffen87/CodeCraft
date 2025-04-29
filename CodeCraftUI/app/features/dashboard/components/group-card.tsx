import { Button } from "~/components/ui/button";
import {
	Card,
	CardContent,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";
import type { Group } from "~/types/types";
import useGroups from "~/hooks/useGroups";
import useAuth from "~/hooks/useAuth";

export default function GroupCard({ group }: { group: Group }) {
	const { addToGroup, loading } = useGroups();
	const { user } = useAuth();
	const isMember = group.members?.some((member) => member.id === user?.id);
	console.log("isMember", isMember);

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
			<CardFooter>
				{!isMember && (
					<Button
						variant={"default"}
						disabled={loading}
						onClick={() => {
							addToGroup({ groupName: group.name });
						}}
					>
						Join
					</Button>
				)}
			</CardFooter>
		</Card>
	);
}
