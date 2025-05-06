import { NavLink } from "react-router";
import Logo from "~/assets/code.svg";
import useAuth from "~/hooks/useAuth";
import { Button } from "~/components/ui/button";
import { ModeToggle } from "./theme-toggle";
import { Avatar, AvatarFallback, AvatarImage } from "~/components/ui/avatar";
import {
	Popover,
	PopoverContent,
	PopoverTrigger,
} from "~/components/ui/popover";
import {
	Tooltip,
	TooltipContent,
	TooltipProvider,
	TooltipTrigger,
} from "~/components/ui/tooltip";
import useSignalR from "~/hooks/useSignalR";
import { SidebarTrigger } from "~/components/ui/sidebar";

export default function Navbar() {
	const { user, signOut } = useAuth();
	const { isConnected } = useSignalR();

	return (
		<div className="bg-sidebar px-4 py-4 flex items-center border-b-foreground/25 border-b w-full flex-none sticky top-0 z-50">
			<SidebarTrigger />
			<img src={Logo} alt="Logo" className="h-10 w-10 inline-block mr-2" />
			<span className="mr-7 text-2xl text-foreground">CodeCraft</span>
			<div className="w-0.5 h-full bg-foreground/25" />
			<nav className="space ml-4">
				<NavLink
					to="/"
					end
					className={({ isActive }) =>
						isActive ? "p-4 underline" : "p-4 transition-all hover:opacity-60"
					}
				>
					Home
				</NavLink>
				<NavLink
					to="/exercises"
					className={({ isActive }) =>
						isActive ? "p-4 underline" : "p-4 transition-all hover:opacity-60"
					}
				>
					Exercises
				</NavLink>
			</nav>
			<div className="ml-auto flex items-center space-x-4 relative">
				<ModeToggle />
				<Popover>
					<PopoverTrigger>
						<div className="relative">
							<Avatar className="w-10 h-10">
								<AvatarImage src="https://static.vecteezy.com/system/resources/thumbnails/009/292/244/small/default-avatar-icon-of-social-media-user-vector.jpg" />
								<AvatarFallback>CC</AvatarFallback>
							</Avatar>
						</div>
					</PopoverTrigger>
					<PopoverContent className="w-48 mr-3 flex flex-col">
						<p>{user?.user_metadata.user_name}</p>
						<p className="text-muted-foreground">{user?.email}</p>
						<Button onClick={signOut} className="mt-4" variant={"destructive"}>
							Log out
						</Button>
					</PopoverContent>
				</Popover>
				<TooltipProvider>
					<Tooltip>
						<TooltipTrigger asChild>
							<div
								className={`rounded-full h-3 w-3 z-10 absolute top-0 right-3 ${
									isConnected ? "bg-green-800" : "bg-red-600 animate-bounce"
								}`}
							/>
						</TooltipTrigger>
						<TooltipContent>
							<p>{isConnected ? "Connected" : "Disconnected"}</p>
						</TooltipContent>
					</Tooltip>
				</TooltipProvider>
			</div>
		</div>
	);
}
