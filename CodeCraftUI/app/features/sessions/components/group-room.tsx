import { Button } from "~/components/ui/button";
import Editor from "./editor";
import useSubmitCode from "~/hooks/useSubmitCode";
import Console from "./console";
import { useNavigate } from "react-router";
import { useGroupStore } from "~/stores/groupStore";
import useAuth from "~/hooks/useAuth";
import useGroups from "~/hooks/useGroups";
import { ModeToggle } from "~/shared/theme-toggle";
import { useEffect } from "react";
import { Avatar, AvatarFallback, AvatarImage } from "~/components/ui/avatar";
import {
	Tooltip,
	TooltipContent,
	TooltipProvider,
	TooltipTrigger,
} from "~/components/ui/tooltip";

export default function GroupRoom({ groupId }: { groupId: string }) {
	const { Submit, loading } = useSubmitCode();
	const navigate = useNavigate();
	const { removeMember, getGroupName, groups } = useGroupStore();
	const { removeFromGroup, addToGroup } = useGroups();
	const { user } = useAuth();

	useEffect(() => {
		addToGroup({ groupName: getGroupName(groupId)! });
		console.log("Added to group: ", getGroupName(groupId));

		return () => {
			removeFromGroup({ groupName: getGroupName(groupId)! });
			removeMember(groupId, user!.id);
		};
	}, [groupId]);

	return (
		<div className="flex flex-col w-full h-full mt-5">
			<div className="flex items-center mx-auto gap-4 mb-2">
				{groups.map((group) => {
					if (group.id === groupId) {
						return (
							<div
								key={group.id}
								className="flex items-center gap-2 bg-muted p-2 rounded-md"
							>
								<p className="text-muted-foreground">You are in:</p>
								<p className="text-muted-foreground">{group.name}</p>
								{group.members.map((member) => {
									if (member.id !== user?.id) {
										return (
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
										);
									}
									return null;
								})}
							</div>
						);
					}
				})}
			</div>
			<Editor className="h-3/5 w-full" groupName={getGroupName(groupId)} />
			<div className="ml-10 flex items-center gap-4">
				<ModeToggle />
				<Button
					onClick={() => Submit(groupId)}
					disabled={loading}
					variant={"outline"}
				>
					{loading ? "Loading..." : "Run Code"}
				</Button>
				<Button
					variant={"destructive"}
					onClick={() => {
						navigate("/");
					}}
				>
					Leave room
				</Button>
			</div>
			<Console className="p-2" />
		</div>
	);
}
