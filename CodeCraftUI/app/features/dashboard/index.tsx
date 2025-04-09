import StudentDashboard from "./components/student-dashboard";
import TeacherDashboard from "./components/teacher-dashboard";
import useAuth from "~/hooks/useAuth";

export default function DashboardIndex() {
	const { user, signOut } = useAuth();

	return (
		<div className="w-full h-dvh flex flex-col items-center justify-center">
			{user?.user_metadata.role === "student" ? (
				<StudentDashboard />
			) : (
				<TeacherDashboard />
			)}
			<button
				className="px-8 py-2 mt-6 bg-gray-900 outline-white outline-2 hover:bg-gray-800 hover:cursor-pointer rounded-lg active:opacity-90"
				onClick={() => signOut()}
			>
				Log out
			</button>
		</div>
	);
}
