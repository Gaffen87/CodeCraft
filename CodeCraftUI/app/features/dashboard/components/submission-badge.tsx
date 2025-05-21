import { Badge } from "~/components/ui/badge";
import type { CodeSubmit } from "~/types/types";

export default function SubmissionBadge({
	submissions,
}: {
	submissions: CodeSubmit[];
}) {
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

	return (
		getFailedAttemptsSinceLastSuccess(submissions) !== 0 && (
			<Badge className="absolute top-3 right-3" variant={"destructive"}>
				{getFailedAttemptsSinceLastSuccess(submissions)} Failed
			</Badge>
		)
	);
}
