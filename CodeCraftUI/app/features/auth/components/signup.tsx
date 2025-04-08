import type { Route } from "./+types/signup";
import supabase from "../../../lib/supabase";
import useUserStore from "~/stores/userstore";
import { useNavigate } from "react-router";

export default function SignUp() {
	const setUser = useUserStore((state) => state.setUser);
	const navigate = useNavigate();

	const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const formData = new FormData(event.currentTarget);
		const email = formData.get("email") as string;
		const password = formData.get("password") as string;
		const fullName = formData.get("full-name") as string;

		const { data, error } = await supabase.auth.signUp({
			email,
			password,
			options: {
				data: {
					user_name: fullName,
					role: "student",
				},
			},
		});

		if (error) {
			console.error("Error signing up:", error.message);
			return;
		}

		if (data.user) {
			setUser(data.user);
			navigate("/");
		}
	};

	return (
		<form onSubmit={handleSubmit}>
			<h2 className="text-white text-lg font-medium title-font mb-5 text-center">
				Sign Up
			</h2>
			<div className="relative mb-4">
				<label htmlFor="full-name" className="leading-7 text-sm text-gray-400">
					Name
				</label>
				<input
					type="text"
					id="full-name"
					name="full-name"
					className="w-full bg-gray-600 bg-opacity-20 focus:bg-transparent focus:ring-2 focus:ring-indigo-900 rounded border border-gray-600 focus:border-indigo-500 text-base outline-none text-gray-100 py-1 px-3 leading-8 transition-colors duration-200 ease-in-out"
				/>
			</div>
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
			<button className="text-white bg-indigo-500 border-0 py-2 px-8 active:bg-indigo-700 focus:outline-none hover:bg-indigo-600 hover:cursor-pointer rounded text-lg">
				Sign Up
			</button>
			<p className="text-xs mt-3">
				Already signed up?{" "}
				<a href="/login" className="text-indigo-400 hover:text-indigo-600">
					Log In
				</a>
			</p>
		</form>
	);
}
