import {
	type RouteConfig,
	index,
	layout,
	prefix,
	route,
} from "@react-router/dev/routes";

export default [
	layout("./shared/protected-layout-navbar.tsx", [
		index("./features/dashboard/index.tsx"),
		...prefix("/exercises", [
			layout("./features/exercises/layout.tsx", [
				index("./features/exercises/components/exercise-dashboard.tsx"),
			]),
		]),
	]),
	layout("./shared/protected-layout.tsx", [
		...prefix("/session", [route(":groupId", "./features/sessions/index.tsx")]),
	]),
	...prefix("/auth", [index("./features/auth/layout.tsx")]),
] satisfies RouteConfig;
