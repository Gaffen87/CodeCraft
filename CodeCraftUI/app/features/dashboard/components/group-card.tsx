import { Button } from "~/components/ui/button";
import {
	Card,
	CardContent,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";
import { Badge } from "~/components/ui/badge";
import type { Group } from "~/types/types";
import useGroups from "~/hooks/useGroups";
import useAuth from "~/hooks/useAuth";
import { useNavigate } from "react-router";
import { useEffect, useState } from "react";

export default function GroupCard({ group }: { group: Group }) {
	const { addToGroup, getSubmissions, loading } = useGroups();
	const { user } = useAuth();
	const [submissions, setSubmissions] = useState([]);
	const isMember = group.members?.some((member) => member.id === user?.id);
	const navigate = useNavigate();

	useEffect(() => {
		const fetchSubmissions = async () => {
			const submissions = await getSubmissions(group.id);
			setSubmissions(submissions);
		};
		fetchSubmissions();
	}, []);

	return (
		<Card className="w-[350px] relative">
			{submissions && submissions.length > 0 && (
				<>
					<Badge className="absolute -top-2 -right-2">
						Submissions: {submissions.length}
					</Badge>
				</>
			)}
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
							navigate("/session/" + group.id);
						}}
					>
						Join
					</Button>
				)}
			</CardFooter>
		</Card>
	);
}
