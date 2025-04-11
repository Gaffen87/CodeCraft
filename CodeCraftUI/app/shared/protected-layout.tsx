import { Navigate, Outlet } from "react-router";
import useAuth from "~/hooks/useAuth";
import Navbar from "./navbar";

export default function DashboardLayout() {
	const { user } = useAuth();

	if (!user) {
		return <Navigate to="/auth" replace />;
	}

	return (
		<div className="h-screen flex flex-col">
			<Navbar />
			<div className="flex-1">
				<Outlet />
			</div>
		</div>
	);
}
