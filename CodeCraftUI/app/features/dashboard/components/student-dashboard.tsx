import useAuth from "~/hooks/useAuth";

export default function StudentDashboard() {
	const { user } = useAuth();

	return (
		<div>
			<p className="text-2xl tracking-widest">Student Dashboard</p>
			<p className="text-center mt-4 opacity-80">
				{user?.user_metadata.user_name}
			</p>
			<p className="text-center opacity-80">{user?.email}</p>
			<p className="text-center opacity-80">{user?.user_metadata.role}</p>
		</div>
	);
}
