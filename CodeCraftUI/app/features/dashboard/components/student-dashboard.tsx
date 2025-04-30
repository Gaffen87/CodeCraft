import { useEffect } from "react";
import { useGroupStore } from "../../../stores/groupStore";
import useGroups from "~/hooks/useGroups";
import GroupCard from "./group-card";
import { Button } from "~/components/ui/button";

export default function StudentDashboard() {
	const { groups } = useGroupStore();
	const { loading, fetchGroups, addToGroup } = useGroups();

	useEffect(() => {
		fetchGroups();
	}, []);

	return (
		<div>
			<p className="text-2xl tracking-widest">Teacher Dashboard</p>
			<h2 className="text-xl font-semibold">Groups</h2>
			<Button
				variant={"default"}
				onClick={() =>
					addToGroup({
						groupName: "Gruppe " + Math.floor(Math.random() * 100).toString(),
					})
				}
			>
				Create Group
			</Button>
			<div className="grid grid-cols-3 mt-10 ">
				{loading ? (
					<p>Loading...</p>
				) : groups && Array.isArray(groups) && groups.length > 0 ? (
					groups.map((group) => (
						<div className="w-1/3 p-4" key={group.id}>
							<GroupCard group={group} />
						</div>
					))
				) : (
					<p>No groups found</p>
				)}
			</div>
		</div>
	);
}
