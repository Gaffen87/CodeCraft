import { create } from "zustand";
import { createJSONStorage, persist } from "zustand/middleware";
import type { Group, User } from "~/types/types";

type GroupState = {
	groups: Group[];
	setGroups: (groups: Group[]) => void;
	getGroupId: (groupName: string) => string | undefined;
	getGroupName: (groupId: string) => string | undefined;
	addGroup: (group: Group) => void;
	removeGroup: (groupId: string) => void;
	addMember: (groupId: string, user: User) => void;
	removeMember: (groupId: string, userId: string) => void;
	clearMember: (userId: string) => void;
	setGroupExercise: (groupId: string, newExerciseInfo: { exerciseTitle: string, subExerciseNumber: number, stepIndex: number } | null) => void;
};

export const useGroupStore = create<GroupState>()(
	persist(
		(set, get) => ({
			groups: [],
			setGroups: (groups) =>
				set((state) => {
					const newGroupIds = new Set(groups.map((g) => g.id));
					const filteredGroups = state.groups.filter((group) => newGroupIds.has(group.id));
					const mergedGroups = groups.map((newGroup) => {
						const existing = filteredGroups.find((g) => g.id === newGroup.id);
						if (existing && existing.exerciseInfo != null) {
							return { ...newGroup, exerciseInfo: existing.exerciseInfo };
						}
						return newGroup;
					});
					return { groups: mergedGroups };
				}),
			getGroupId: (groupName) => {
				const group = get().groups.find((group) => group.name === groupName);
				if (group) {
					return group.id;
				}
				return undefined;
			},
			getGroupName: (groupId) => {
				const group = get().groups.find((group) => group.id === groupId);
				if (group) {
					return group.name;
				}
				return undefined;
			},
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
					const updatedGroups = state.groups.map((group) => {
						if (group.id !== groupId) return group;
						return {
							...group,
							members: group.members.filter((member) => member.id !== userId),
						};
					});
					return {
						groups: updatedGroups,
					};
				}),
			clearMember: (userId) =>
				set((state) => {
					const updatedGroups = state.groups.map((group) => {
						return {
							...group,
							members: group.members.filter((member) => member.id !== userId),
						};
					});
					return {
						groups: updatedGroups,
					};
				}),
				setGroupExercise: (groupId, newExerciseInfo) => {
					set((state) => {
						const updatedGroups = state.groups.map((group) => {
							if (group.id !== groupId) return group;
							return {
								...group,
								exerciseInfo: newExerciseInfo,
							};
						})
						return {
							groups: updatedGroups,
						};
					})
				}
		}),
		{
			name: "group-store",
			storage: createJSONStorage(() => localStorage),
		}
	)
);
