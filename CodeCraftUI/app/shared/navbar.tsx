import { NavLink } from "react-router";
import Logo from "~/assets/code.svg";
import useAuth from "~/hooks/useAuth";

export default function Navbar() {
	const { signOut } = useAuth();

	return (
		<div className="bg-blue-950 px-4 py-4 flex items-center">
			<img src={Logo} alt="Logo" className="h-10 w-10 inline-block mr-2" />
			<span className="mr-7 text-2xl text-shadow-lg/80 text-white">
				CodeCraft
			</span>
			<div className="w-0.5 h-full bg-white" />
			<nav className="space ml-4">
				<NavLink
					to="/"
					end
					className={({ isActive }) =>
						isActive
							? "p-4 text-white underline"
							: "p-4 text-white transition-all hover:opacity-60"
					}
				>
					Home
				</NavLink>
				<NavLink
					to="/exercises"
					className={({ isActive }) =>
						isActive
							? "p-4 text-white underline"
							: "p-4 text-white transition-all hover:opacity-60"
					}
				>
					Exercises
				</NavLink>
			</nav>
			<button
				className="px-4 py-2 text-white ml-auto inset-ring inset-ring-white rounded-lg hover:inset-ring-2 hover:cursor-pointer transition-all"
				onClick={signOut}
			>
				Log out
			</button>
		</div>
	);
}
