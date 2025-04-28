import useAuth from "~/hooks/useAuth";
import { useEffect, useState } from "react";
import type { Group } from "../../../types/types";
import { useGroupStore } from "../../../stores/groupStore";
import { useGetGroups } from "~/hooks/useGetGroups";
import GroupCard from "./group-card";

export default function TeacherDashboard() {
	const { groups } = useGroupStore();
	const { user } = useAuth();
	const { loading } = useGetGroups();

	return (
		<div>
			<p className="text-2xl tracking-widest">Teacher Dashboard</p>
			<p className="text-center mt-4 opacity-80">
				{user?.user_metadata.user_name}
			</p>
			<p className="text-center opacity-80">{user?.email}</p>
			<p className="text-center opacity-80">{user?.user_metadata.role} </p>

			<h2 className="text-xl font-semibold">Your Groups</h2>
			<div className="grid grid-cols-2 mt-10 ">
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
