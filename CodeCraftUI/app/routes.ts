import {
	type RouteConfig,
	index,
	layout,
	prefix,
	route,
} from "@react-router/dev/routes";

export default [
	layout("./shared/protected-layout.tsx", [
		index("./features/dashboard/index.tsx"),

		...prefix("/exercises", [
			layout("./features/exercises/layout.tsx", [
				index("./features/exercises/components/exercise-dashboard.tsx"),
			]),
		]),
	]),

	layout("./features/auth/layout.tsx", [
		route("/signup", "./features/auth/components/signup.tsx"),
		route("/login", "./features/auth/components/login.tsx"),
	]),
] satisfies RouteConfig;
