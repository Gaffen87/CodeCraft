import { create } from "zustand";
import type { User } from "@supabase/supabase-js";
import supabase from "~/lib/supabase";

interface UserState {
	user: User | null;
	setUser: (newUser: User | null) => void;
	clearUser: () => void;
}

const setUserFromLocalStorage = () => {
	const storedToken =
		JSON.parse(localStorage.getItem(localStorage.key(0)!)!) ?? null;
	if (storedToken) {
		const storedUser = storedToken.user as User;
		const expireDate = JSON.parse(
			localStorage.getItem(localStorage.key(0)!)!
		).expires_at;
		const currentDate = new Date().getTime() / 1000;
		if (expireDate > currentDate) {
			return storedUser;
		} else {
			return null;
		}
	}
	return null;
};

const useUserStore = create<UserState>()((set) => ({
	user: setUserFromLocalStorage(),
	setUser: (newUser) => {
		set({ user: newUser });
		console.log("User set:", newUser);
	},
	clearUser: () => set({ user: null }),
}));

export default useUserStore;
