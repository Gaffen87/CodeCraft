import { Avatar, AvatarFallback, AvatarImage } from "~/components/ui/avatar";
import {
	Tooltip,
	TooltipContent,
	TooltipProvider,
	TooltipTrigger,
} from "~/components/ui/tooltip";
import type { Group } from "~/types/types";
import useAuth from "~/hooks/useAuth";

export default function Header({
	groups,
	groupId,
}: {
	groups: Group[];
	groupId: string;
}) {
	const { user } = useAuth();

	return (
		<div className="flex items-center mx-auto gap-4 mb-2">
			{groups.map((group) => {
				if (group.id === groupId) {
					return (
						<div
							key={group.id}
							className="flex flex-col items-center gap-2 bg-muted p-2 rounded-md"
						>
							<div className="flex items-center gap-2">
								<p className="text-muted-foreground">You are in:</p>
								<p className="text-muted-foreground">{group.name}</p>
							</div>
							<div className="flex items-center gap-2">
								{group.members.map((member) => (
									<div key={member.id} className="text-muted-foreground">
										<TooltipProvider>
											<Tooltip>
												<TooltipTrigger asChild>
													<Avatar className="w-7 h-7">
														<AvatarImage src="https://static.vecteezy.com/system/resources/thumbnails/009/292/244/small/default-avatar-icon-of-social-media-user-vector.jpg" />
														<AvatarFallback>CC</AvatarFallback>
													</Avatar>
												</TooltipTrigger>
												<TooltipContent>
													<p>{member.name}</p>
												</TooltipContent>
											</Tooltip>
										</TooltipProvider>
									</div>
								))}
							</div>
						</div>
					);
				}
			})}
		</div>
	);
}
