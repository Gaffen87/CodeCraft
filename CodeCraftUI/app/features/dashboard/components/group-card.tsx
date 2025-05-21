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
		<Card className="min-h-[250px] relative shadow-lg border border-gray-200 rounded-2xl bg-white hover:shadow-xl transition-all">
			{user?.user_metadata.role === "teacher" && (
				<SubmissionBadge submissions={submissions[group.id]} />
			)}
			<CardHeader className="space-y-1">
				<CardTitle className="text-xl font-semibold text-gray-900 border-b border-gray-200 pb-2">
					{group.name}
				</CardTitle>
				<CardDescription className="text-sm text-gray-500">
					<div className="space-y-1">
						{group.members.map((member) => (
							<Member key={member.id} member={member} />
						))}
					</div>
				</CardDescription>
			</CardHeader>
			<CardFooter className="mt-auto">
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
