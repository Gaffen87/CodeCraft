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
			layout("./features/exercises/index.tsx", [
				index("./features/exercises/components/exercise-dashboard.tsx"),
				route("create", "./features/exercises/components/exercise-form.tsx"),
				route("view", "./features/exercises/components/exercise-overview.tsx"),
				route(
					"details/:exerciseId",
					"./features/exercises/components/exercise-details.tsx"
				),
				route("edit/:exerciseId", "./features/exercises/components/exercise-update-form.tsx")
			]),
		]),
	]),
	layout("./shared/protected-layout.tsx", [
		...prefix("/session", [route(":groupId", "./features/sessions/index.tsx")]),
	]),
	...prefix("/auth", [index("./features/auth/layout.tsx")]),
] satisfies RouteConfig;
