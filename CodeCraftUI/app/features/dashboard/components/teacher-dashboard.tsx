import { useEffect } from "react";
import { useGroupStore } from "../../../stores/groupStore";
import useGroups from "~/hooks/useGroups";
import GroupCard from "./group-card";
import { Button } from "~/components/ui/button";
import useAuth from "~/hooks/useAuth";
import { CgSpinner } from "react-icons/cg";

export default function TeacherDashboard() {
	const { groups, clearMember } = useGroupStore();
	const { loading, fetchGroups, addToGroup } = useGroups();
	const { user } = useAuth();

	useEffect(() => {
		clearMember(user!.id);
		fetchGroups();
	}, []);

	return (
		<div>
			<h2 className="text-xl font-semibold">Groups</h2>
			<Button
				variant={"default"}
				onClick={() =>
					addToGroup({
						groupName: "Gruppe " + Math.floor(Math.random() * 100).toString(),
					})
				}
				disabled={loading}
			>
				Create Group
			</Button>
			<div className="grid xl:grid-cols-4 lg:grid-cols-3 md:grid-cols-2 grid-cols-1 mt-10">
				{loading ? (
					<div className="absolute top-[50%] right-[50%] translate-x-[50%] translate-y-[-50%] flex items-center gap-2">
						<span>Loading groups</span>
						<CgSpinner className="animate-spin" />
					</div>
				) : groups && Array.isArray(groups) && groups.length > 0 ? (
					groups.map((group) => (
						<div className="p-4" key={group.id}>
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
