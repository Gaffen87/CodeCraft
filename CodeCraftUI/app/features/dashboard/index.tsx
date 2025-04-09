import StudentDashboard from "./components/student-dashboard";
import TeacherDashboard from "./components/teacher-dashboard";
import useAuth from "~/hooks/useAuth";

export default function Dashboard() {
	const { user, signOut } = useAuth();

	return (
		<div className="w-full h-full flex flex-col items-center justify-center">
			{user?.user_metadata.role === "student" ? (
				<StudentDashboard />
			) : (
				<TeacherDashboard />
			)}
		</div>
	);
}
