import test from "node:test";
import StudentDashboard from "./components/student-dashboard";
import TeacherDashboard from "./components/teacher-dashboard";
import useAuth from "~/hooks/useAuth";

export default function Dashboard() {
	const { user } = useAuth();

	return (
		<div className="w-full h-full flex flex-col p-2">
			{user?.user_metadata.role === "student" ? (
				<StudentDashboard />
			) : (
				<TeacherDashboard />
			)}
		</div>
	);
}
