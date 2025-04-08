import supabase from "~/lib/supabase";
import { useEffect } from "react";
import type { Route } from "./+types/login";
import useUserStore from "~/stores/userstore";
import { useNavigate } from "react-router";

export default function Login() {
	const user = useUserStore((state) => state.user);
	const setUser = useUserStore((state) => state.setUser);
	const navigate = useNavigate();

	const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const formData = new FormData(event.currentTarget);
		const email = formData.get("email") as string;
		const password = formData.get("password") as string;

		const { data, error } = await supabase.auth.signInWithPassword({
			email,
			password,
		});

		if (error) {
			console.error("Error logging in:", error.message);
			return;
		}
		if (data.user) {
			setUser(data.user);
			navigate("/");
		}
	};

	return (
		<form onSubmit={handleSubmit}>
			<h1>{user?.user_metadata.user_name}</h1>
			<h2 className="text-white text-lg font-medium title-font mb-5 text-center">
				Log in
			</h2>
			<div className="relative mb-4">
				<label htmlFor="email" className="leading-7 text-sm text-gray-400">
					Email
				</label>
				<input
					type="email"
					id="email"
					name="email"
					className="w-full bg-gray-600 bg-opacity-20 focus:bg-transparent focus:ring-2 focus:ring-indigo-900 rounded border border-gray-600 focus:border-indigo-500 text-base outline-none text-gray-100 py-1 px-3 leading-8 transition-colors duration-200 ease-in-out"
				/>
			</div>
			<div className="relative mb-4">
				<label htmlFor="password" className="leading-7 text-sm text-gray-400">
					Password
				</label>
				<input
					type="password"
					id="password"
					name="password"
					className="w-full bg-gray-600 bg-opacity-20 focus:bg-transparent focus:ring-2 focus:ring-indigo-900 rounded border border-gray-600 focus:border-indigo-500 text-base outline-none text-gray-100 py-1 px-3 leading-8 transition-colors duration-200 ease-in-out"
				/>
			</div>
			<button
				type="submit"
				className="text-white bg-indigo-500 border-0 py-2 px-8 active:bg-indigo-700 focus:outline-none hover:bg-indigo-600 hover:cursor-pointer rounded text-lg"
			>
				Log In
			</button>
			<p className="text-xs mt-3">
				No account yet?{" "}
				<a href="/signup" className="text-indigo-400 hover:text-indigo-600">
					Sign up here
				</a>
			</p>
		</form>
	);
}
