import { Input } from "~/components/ui/input";
import supabase from "../../../lib/supabase";
import { useNavigate } from "react-router";
import { Button } from "~/components/ui/button";
import { Label } from "~/components/ui/label";
import {
	Card,
	CardContent,
	CardDescription,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";

export default function SignUp() {
	const navigate = useNavigate();

	const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const formData = new FormData(event.currentTarget);
		const email = formData.get("email") as string;
		const password = formData.get("password") as string;
		const fullName = formData.get("name") as string;

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
			navigate("/");
		}
	};

	return (
		<Card>
			<CardHeader>
				<CardTitle>Sign up</CardTitle>
				<CardDescription>Create a new account</CardDescription>
			</CardHeader>
			<CardContent>
				<form onSubmit={handleSubmit}>
					<div className="mb-4 space-y-1">
						<Label htmlFor="name">Name</Label>
						<Input type="text" id="name" name="name" />
					</div>
					<div className="mb-4 space-y-1">
						<Label htmlFor="email">Email</Label>
						<Input type="email" id="email" name="email" autoComplete="email" />
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
						Sign up
					</Button>
				</form>
			</CardContent>
		</Card>
	);
}
