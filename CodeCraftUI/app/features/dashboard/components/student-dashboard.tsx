export default function StudentDashboard({ user }) {
	return (
		<div>
			<p className="text-2xl">Student Dashboard</p>
			<p className="text-center mt-4">{user?.user_metadata.user_name}</p>
			<p className="text-center">{user?.email}</p>
			<p className="text-center">{user?.user_metadata.role}</p>
		</div>
	);
}
