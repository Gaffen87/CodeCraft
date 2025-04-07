import { Outlet } from "react-router";

export default function AuthLayout() {
	return (
		<div className="w-full h-dvh flex items-center justify-center">
			<div className="lg:w-2/6 md:w-1/2 bg-gray-800 bg-opacity-50 rounded-lg p-8 flex flex-col mx-auto w-full mt-10 md:mt-0">
				<Outlet />
			</div>
		</div>
	);
}
