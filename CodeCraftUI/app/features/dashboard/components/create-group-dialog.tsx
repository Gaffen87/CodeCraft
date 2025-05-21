import { useState } from "react";
import { Button } from "~/components/ui/button";
import {
	Dialog,
	DialogClose,
	DialogContent,
	DialogFooter,
	DialogHeader,
	DialogTitle,
	DialogTrigger,
} from "~/components/ui/dialog";
import { Input } from "~/components/ui/input";
import { Label } from "~/components/ui/label";
import useGroups from "~/hooks/useGroups";

export default function CreateGroupDialog() {
	const { addToGroup } = useGroups();
	const [groupName, setGroupName] = useState<string>("");

	return (
		<Dialog>
			<DialogTrigger asChild>
				<Button>Create Group</Button>
			</DialogTrigger>
			<DialogContent>
				<DialogHeader>
					<DialogTitle>Create new group</DialogTitle>
				</DialogHeader>
				<div className="grid gap-4 py-4">
					<div className="grid grid-cols-4 items-center gap-4">
						<Label htmlFor="name" className="text-right">
							Group Name
						</Label>
						<Input
							id="name"
							value={groupName}
							onChange={(e) => setGroupName(e.target.value)}
							type="text"
							placeholder="Give the group a name"
							className="col-span-3"
						/>
					</div>
				</div>
				<DialogFooter>
					<DialogClose asChild>
						<Button
							type="submit"
							onClick={() =>
								addToGroup({
									groupName,
								})
							}
						>
							Create
						</Button>
					</DialogClose>
					<DialogClose asChild>
						<Button type="button" variant="secondary">
							Close
						</Button>
					</DialogClose>
				</DialogFooter>
			</DialogContent>
		</Dialog>
	);
}
