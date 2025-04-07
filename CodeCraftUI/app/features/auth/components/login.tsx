import type { Route } from "./+types/login";
import { Form } from "react-router";

export default function Login() {
	return (
		<Form>
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
			<button className="text-white bg-indigo-500 border-0 py-2 px-8 active:bg-indigo-700 focus:outline-none hover:bg-indigo-600 hover:cursor-pointer rounded text-lg">
				Log In
			</button>
			<p className="text-xs mt-3">
				Not signed up?{" "}
				<a href="/signup" className="text-indigo-400 hover:text-indigo-600">
					Create account
				</a>
			</p>
		</Form>
	);
}
