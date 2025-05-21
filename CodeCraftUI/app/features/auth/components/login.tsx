import supabase from "~/lib/supabase";
import { useNavigate } from "react-router";
import { Input } from "~/components/ui/input";
import { Button } from "~/components/ui/button";
import { Label } from "~/components/ui/label";
import {
	Card,
	CardContent,
	CardDescription,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";

export default function Login() {
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
			navigate("/");
		}
	};

	return (
		<Card>
			<CardHeader>
				<CardTitle>Login</CardTitle>
				<CardDescription>Login with an existing account</CardDescription>
			</CardHeader>
			<CardContent>
				<form onSubmit={handleSubmit}>
					<div className="mb-4 space-y-1">
						<Label htmlFor="email">Email</Label>
						<Input
							type="email"
							id="email"
							name="email"
							autoComplete="username"
						/>
					</div>
					<div className="mb-4 space-y-1">
						<Label htmlFor="password">Password</Label>
						<Input
							type="password"
							id="password"
							name="password"
							autoComplete="current-password"
						/>
					</div>
					<Button className="mt-4" type="submit">
						Log In
					</Button>
				</form>
			</CardContent>
		</Card>
	);
}
