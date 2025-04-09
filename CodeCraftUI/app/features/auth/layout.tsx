import { Navigate, Outlet } from "react-router";
import useAuth from "~/hooks/useAuth";

export default function AuthLayout() {
	const { user } = useAuth();

	if (user) {
		return <Navigate to="/" replace />;
	}

	return (
		<div className="w-full h-dvh flex items-center justify-center">
			<div className="lg:w-2/6 md:w-1/2 bg-gray-800 bg-opacity-50 rounded-lg p-8 flex flex-col mx-auto w-full mt-10 md:mt-0">
				<Outlet />
			</div>
		</div>
	);
}
