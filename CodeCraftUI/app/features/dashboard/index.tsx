import StudentDashboard from "./components/student-dashboard";
import TeacherDashboard from "./components/teacher-dashboard";
import useAuth from "~/hooks/useAuth";
import { Toaster } from "~/components/ui/sonner";

export default function Dashboard() {
	const { user } = useAuth();

	return (
		<div className="w-full h-full flex flex-col p-2">
			{/* {user?.user_metadata.role === "student" ? (
				<StudentDashboard />
			) : (
				<TeacherDashboard />
			)} */}
			<TeacherDashboard />
			<Toaster
				toastOptions={{
					classNames: {
						description: "!text-sm !text-muted-foreground",
					},
				}}
			/>
		</div>
	);
}
