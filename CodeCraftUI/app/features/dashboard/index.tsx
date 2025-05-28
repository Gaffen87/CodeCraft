import Dashboard from "./components/dashboard";
import useAuth from "~/hooks/useAuth";
import { Toaster } from "~/components/ui/sonner";

export default function Index() {
	const { user } = useAuth();

	return (
		<div className="w-full h-full flex flex-col p-2">
			<Dashboard />
			{user?.user_metadata.role === "teacher" && (
				<Toaster
					toastOptions={{
						classNames: {
							description: "!text-sm !text-muted-foreground",
						},
					}}
				/>
			)}
		</div>
	);
}
