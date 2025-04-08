import {
	type RouteConfig,
	index,
	layout,
	route,
} from "@react-router/dev/routes";

export default [
	index("./features/dashboard/index.tsx"),
	layout("./features/auth/layout.tsx", [
		route("/signup", "./features/auth/components/signup.tsx"),
		route("/login", "./features/auth/components/login.tsx"),
	]),
] satisfies RouteConfig;
