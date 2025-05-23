import { Navigate, Outlet } from "react-router";
import useAuth from "~/hooks/useAuth";

export default function DashboardLayout() {
	const { user } = useAuth();

	if (!user) {
		return <Navigate to="/auth" replace />;
	}

	return (
		<div className="h-screen flex flex-col">
			<div className="flex-1">
				<Outlet />
			</div>
		</div>
	);
}
