import { Button } from "~/components/ui/button";
import {
	Card,
	CardContent,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";
import type { CodeSubmit, Group } from "~/types/types";
import useGroups from "~/hooks/useGroups";
import useAuth from "~/hooks/useAuth";
import { useNavigate } from "react-router";
import { useSubmitStore } from "~/stores/submitStore";
import { useEffect } from "react";
import SubmissionBadge from "./submission-badge";
import Member from "./member";
import { Badge } from "~/components/ui/badge";
import { Car } from "lucide-react";

export default function GroupCard({ group }: { group: Group }) {
	const { loading, getSubmissionsByGroup } = useGroups();
	const { user } = useAuth();
	const isMember = group.members?.some((member) => member.id === user?.id);
	const navigate = useNavigate();
	const { submissions, setGroupSubmissions } = useSubmitStore();

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
		<Card className="min-h-[250px] relative shadow-lg border border-gray-200 rounded-2xl bg-secondary/50 hover:shadow-xl transition-all">
			{user?.user_metadata.role === "teacher" && (
				<SubmissionBadge submissions={submissions[group.id]} />
			)}
			<CardHeader className="space-y-1">
				<CardTitle className="text-xl font-semibold text-foreground border-b border-foreground/10 pb-2">
					{group.name}
				</CardTitle>
				<CardDescription className="text-sm text-foreground/80">
					Working on:
					<span className="font-mono"> Hello, World! - 1.1</span>
				</CardDescription>
			</CardHeader>
			<CardContent className="flex flex-col gap-2">
				<div className="space-y-1">
					{group.members.map((member) => (
						<Member key={member.id} member={member} />
					))}
				</div>
			</CardContent>
			<CardFooter className="mt-auto flex flex-col">
				{!isMember && (
					<Button
						variant="default"
						disabled={loading}
						className="w-full"
						onClick={() => navigate("/session/" + group.id)}
					>
						Join Group
					</Button>
				)}
			</CardFooter>
		</Card>
	);
}
