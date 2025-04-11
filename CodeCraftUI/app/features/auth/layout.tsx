import { Navigate, Outlet } from "react-router";
import useAuth from "~/hooks/useAuth";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "~/components/ui/tabs";
import {
	Card,
	CardContent,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";
import Login from "./components/login";
import { Car } from "lucide-react";
import { ModeToggle } from "~/shared/theme-toggle";
import SignUp from "./components/signup";

export default function AuthLayout() {
	const { user } = useAuth();

	if (user) {
		return <Navigate to="/" replace />;
	}

	return (
		<div className="w-full h-dvh flex justify-center">
			<Tabs defaultValue="login" className="lg:w-1/4 w-1/2 mt-20">
				<ModeToggle />
				<TabsList className="grid w-full grid-cols-2">
					<TabsTrigger value="login">Login</TabsTrigger>
					<TabsTrigger value="signup">Sign up</TabsTrigger>
				</TabsList>
				<TabsContent value="login">
					<Card>
						<CardHeader>
							<CardTitle>Login</CardTitle>
							<CardDescription>Login with an existing account</CardDescription>
						</CardHeader>
						<CardContent>
							<Login />
						</CardContent>
					</Card>
				</TabsContent>
				<TabsContent value="signup">
					<Card>
						<CardHeader>
							<CardTitle>Sign up</CardTitle>
							<CardDescription>Create a new account</CardDescription>
						</CardHeader>
						<CardContent>
							<SignUp />
						</CardContent>
					</Card>
				</TabsContent>
			</Tabs>
		</div>
	);
}
