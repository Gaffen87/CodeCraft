import useAuth from "~/hooks/useAuth";

export default function TeacherDashboard() {
  const { user } = useAuth();

  return (
    <div>
      <p className="text-2xl tracking-widest">Teacher Dashboard</p>
      <p className="text-center mt-4 opacity-80">
        {user?.user_metadata.user_name}
      </p>
      <p className="text-center opacity-80">{user?.email}</p>
      <p className="text-center opacity-80">{user?.user_metadata.role} </p>
    </div>
  );
}
