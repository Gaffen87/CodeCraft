import { create } from "zustand";
import { createJSONStorage, persist } from "zustand/middleware";
import type { Group, User } from "~/types/types";

type GroupState = {
	groups: Group[];
	setGroups: (groups: Group[]) => void;
	addGroup: (group: Group) => void;
	removeGroup: (groupId: string) => void;
	addMember: (groupId: string, user: User) => void;
	removeMember: (groupId: string, userId: string) => void;
};

export const useGroupStore = create<GroupState>()(
	persist(
		(set) => ({
			groups: [],
			setGroups: (groups) => set({ groups }),
			addGroup: (newGroup) =>
				set((state) => {
					if (state.groups.some((group) => group.id === newGroup.id)) {
						return {
							groups: state.groups,
						};
					}
					return {
						groups: [...state.groups, newGroup],
					};
				}),
			removeGroup: (groupId) =>
				set((state) => ({
					groups: state.groups.filter((group) => group.id !== groupId),
				})),
			addMember: (groupId, user) =>
				set((state) => {
					const targetGroup = state.groups.find((g) => g.id === groupId);
					if (targetGroup) {
						if (targetGroup.members.some((member) => member.id === user.id)) {
							return {
								groups: state.groups,
							};
						}
						targetGroup.members.push(user);
						return {
							groups: state.groups.map((currentGroup) =>
								currentGroup.id === groupId ? targetGroup : currentGroup
							),
						};
					}
					return {
						groups: state.groups,
					};
				}),
			removeMember: (groupId, userId) =>
				set((state) => {
					const targetGroup = state.groups.find((g) => g.id === groupId);
					if (targetGroup) {
						targetGroup.members = targetGroup.members.filter(
							(member) => member.id !== userId
						);
						return {
							groups: state.groups.map((currentGroup) =>
								currentGroup.id === groupId ? targetGroup : currentGroup
							),
						};
					}
					return {
						groups: state.groups,
					};
				}),
		}),
		{
			name: "group-store",
			storage: createJSONStorage(() => sessionStorage),
		}
	)
);
