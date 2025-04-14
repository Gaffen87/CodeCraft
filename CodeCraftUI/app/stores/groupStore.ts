import { create } from "zustand";
import type { Group } from "~/types/types";

type GroupState = {
	groups: Group[];
	addGroup: (group: Group) => void;
	removeGroup: (groupId: string) => void;
	addMember: (groupId: string, memberId: string) => void;
	removeMember: (groupId: string, memberId: string) => void;
};

export const useGroupStore = create<GroupState>((set) => ({
	groups: [],
	addGroup: (newGroup) =>
		set((state) => ({ groups: [...state.groups, newGroup] })),
	removeGroup: (groupId) =>
		set((state) => ({
			groups: state.groups.filter((group) => group.id !== groupId),
		})),
	addMember: (groupId, memberId) =>
		set((state) => {
			const targetGroup = state.groups.find((g) => g.id === groupId);
			if (targetGroup) {
				targetGroup.members.push(memberId);
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
	removeMember: (groupId, memberId) =>
		set((state) => {
			const targetGroup = state.groups.find((g) => g.id === groupId);
			if (targetGroup) {
				targetGroup.members = targetGroup.members.filter(
					(member) => member !== memberId
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
}));
