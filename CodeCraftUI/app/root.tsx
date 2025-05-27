import {
	isRouteErrorResponse,
	Links,
	Meta,
	Outlet,
	Scripts,
	ScrollRestoration,
} from "react-router";

import type { Route } from "./+types/root";
import "./app.css";
import AuthProvider from "./contexts/authContext";
import { ThemeProvider } from "./contexts/themeContext";
import SignalRProvider from "./contexts/signalrContext";
import { SidebarProvider, SidebarTrigger } from "./components/ui/sidebar";
import { ImSpinner3 } from "react-icons/im";

export function HydrateFallback() {
	return (
		<div className="h-screen w-screen flex items-center justify-center">
			<ImSpinner3 className="animate-spin text-4xl" />
		</div>
	);
}

export const links: Route.LinksFunction = () => [
	{ rel: "preconnect", href: "https://fonts.googleapis.com" },
	{
		rel: "preconnect",
		href: "https://fonts.gstatic.com",
		crossOrigin: "anonymous",
	},
	{
		rel: "stylesheet",
		href: "https://fonts.googleapis.com/css2?family=Inter:ital,opsz,wght@0,14..32,100..900;1,14..32,100..900&display=swap",
	},
];

export function Layout({ children }: { children: React.ReactNode }) {
	return (
		<html lang="en">
			<head>
				<meta charSet="utf-8" />
				<meta name="viewport" content="width=device-width, initial-scale=1" />
				<Meta />
				<Links />
			</head>
			<body>
				<SidebarProvider>
					<div className="flex flex-1/2 min-h-screen w-screen">
						{/* <AppSidebar /> */}
						<main className="flex-1 ">{children}</main>
					</div>
				</SidebarProvider>
				<ScrollRestoration />
				<Scripts />
			</body>
		</html>
	);
}

export default function App() {
	return (
		<AuthProvider>
			<SignalRProvider>
				<ThemeProvider defaultTheme="light" storageKey="vite-ui-theme">
					<Outlet />
				</ThemeProvider>
			</SignalRProvider>
		</AuthProvider>
	);
}

export function ErrorBoundary({ error }: Route.ErrorBoundaryProps) {
	let message = "Oops!";
	let details = "An unexpected error occurred.";
	let stack: string | undefined;

	if (isRouteErrorResponse(error)) {
		message = error.status === 404 ? "404" : "Error";
		details =
			error.status === 404
				? "The requested page could not be found."
				: error.statusText || details;
	} else if (import.meta.env.DEV && error && error instanceof Error) {
		details = error.message;
		stack = error.stack;
	}

	return (
		<main className="pt-16 p-4 container mx-auto">
			<h1>{message}</h1>
			<p>{details}</p>
			{stack && (
				<pre className="w-full p-4 overflow-x-auto">
					<code>{stack}</code>
				</pre>
			)}
		</main>
	);
}
