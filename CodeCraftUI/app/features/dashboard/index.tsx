import useUserStore from "~/stores/userstore";
import StudentDashboard from "./components/student-dashboard";
import TeacherDashboard from "./components/teacher-dashboard";
import { useNavigate } from "react-router";
import { useEffect } from "react";
import supabase from "~/lib/supabase";

export default function DashboardLayout() {
	const user = useUserStore((state) => state.user);
	const clearUser = useUserStore((state) => state.clearUser);
	const navigate = useNavigate();

	useEffect(() => {
		if (!user) {
			navigate("/login");
		}
	}, [user, navigate]);

	return (
		<div className="w-full h-dvh flex flex-col items-center justify-center">
			{user?.user_metadata.role === "student" ? (
				<StudentDashboard user={user} />
			) : (
				<TeacherDashboard />
			)}
			<button
				className="bg-blue-950 px-4 py-2 mt-4 hover:cursor-pointer rounded-lg active:bg-blue-800"
				onClick={async () => {
					await supabase.auth.signOut();
					clearUser();
				}}
			>
				Log ud
			</button>
		</div>
	);
}
