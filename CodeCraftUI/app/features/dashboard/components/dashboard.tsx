import { useEffect } from "react";
import { CgSpinner } from "react-icons/cg";
import useAuth from "~/hooks/useAuth";
import useGroups from "~/hooks/useGroups";
import { useGroupStore } from "~/stores/groupStore";
import GroupCard from "./group-card";
import Header from "./header";

export default function Dashboard() {
	const { groups, clearMember } = useGroupStore();
	const { loading, fetchGroups } = useGroups();
	const { user } = useAuth();

	useEffect(() => {
		clearMember(user!.id);
		fetchGroups();
	}, []);

	return (
		<div className="p-6">
			<Header />

			{loading ? (
				<div className="flex justify-center items-center h-[300px] gap-2">
					<CgSpinner className="animate-spin text-2xl" />
					<span className="text-lg">Loading groups...</span>
				</div>
			) : groups && groups.length > 0 ? (
				<div className="grid gap-6 xl:grid-cols-4 lg:grid-cols-3 md:grid-cols-2 grid-cols-1">
					{groups.map((group) => (
						<GroupCard key={group.id} group={group} />
					))}
				</div>
			) : (
				<div className="text-center text-gray-500 text-lg mt-12">
					No groups found. Create one to get started!
				</div>
			)}
		</div>
	);
}
