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
import type { CodeSubmit, Group } from "~/types/types";
import useGroups from "~/hooks/useGroups";
import useAuth from "~/hooks/useAuth";
import { useNavigate } from "react-router";
import { useSubmitStore } from "~/stores/submitStore";
import { useEffect, useState } from "react";

export default function GroupCard({ group }: { group: Group }) {
	const { loading, getSubmissionsByGroup } = useGroups();
	const { user } = useAuth();
	const isMember = group.members?.some((member) => member.id === user?.id);
	const navigate = useNavigate();
	const { submissions, setGroupSubmissions } = useSubmitStore();

	function getFailedAttemptsSinceLastSuccess(
		submissions: CodeSubmit[]
	): number {
		if (!submissions || submissions.length === 0) return 0;

		submissions = submissions.sort((a, b) => {
			return new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime();
		});

		let failedCount = 0;
		for (let i = submissions.length - 1; i >= 0; i--) {
			if (submissions[i].isSuccess) {
				break;
			}
			failedCount++;
		}
		return failedCount;
	}

	useEffect(() => {
		async function fetchSubmissions() {
			if (group.id) {
				const submissions = await getSubmissionsByGroup(group.id);
				console.log("Submissions: ", submissions);
				setGroupSubmissions(group.id, submissions);
			}
		}
		fetchSubmissions();
	}, [group.id]);

	return (
		<Card className="w-[300px] relative">
			{submissions[group.id] &&
				getFailedAttemptsSinceLastSuccess(submissions[group.id]) !== 0 && (
					<Badge className="absolute -top-2 -right-2" variant={"destructive"}>
						Failed attempts:{" "}
						<span>
							{getFailedAttemptsSinceLastSuccess(submissions[group.id])}
						</span>
					</Badge>
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
							// addToGroup({ groupName: group.name });
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
