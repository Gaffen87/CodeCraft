import CreateGroupDialog from "./create-group-dialog";

export default function Header() {
	return (
		<div className="flex items-center justify-between mb-6">
			<h2 className="text-2xl font-bold">Groups</h2>
			<CreateGroupDialog />
		</div>
	);
}
