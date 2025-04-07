import { type RouteConfig, layout, route } from "@react-router/dev/routes";

export default [
	layout("./features/auth/layout.tsx", [
		route("/signup", "./features/auth/components/signup.tsx"),
		route("/login", "./features/auth/components/login.tsx"),
	]),
] satisfies RouteConfig;
