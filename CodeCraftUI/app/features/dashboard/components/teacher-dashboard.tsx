import useAuth from "~/hooks/useAuth";
import { useEffect, useState } from "react";
import type { Group } from "../../../types/types";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../../../components/ui/card";

export default function TeacherDashboard() {
  const { user } = useAuth();
  const [groups, setGroups] = useState<Group[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchGroups() {
      try {
        const res = await fetch("https://localhost:7060/groups");
        const data = await res.json();

        setGroups(data.groups || []);
      } catch (err) {
        console.error("Failed to fetch groups:", err);
      } finally {
        setLoading(false);
      }
    }
    fetchGroups();
  }, []);

  return (
    <div>
      <p className="text-2xl tracking-widest">Teacher Dashboard</p>
      <p className="text-center mt-4 opacity-80">
        {user?.user_metadata.user_name}
      </p>
      <p className="text-center opacity-80">{user?.email}</p>
      <p className="text-center opacity-80">{user?.user_metadata.role} </p>

      <h2 className="text-xl font-semibold">Your Groups</h2>
      <div className="grid grid-cols-2 mt-10 ">
        {loading ? (
          <p>Loading...</p>
        ) : groups && Array.isArray(groups) && groups.length > 0 ? (
          groups.map((group) => (
            <div className="w-1/3 p-4" key={group.id}>
              <Card className="w-[350px] " key={group.id}>
                <CardHeader>
                  <CardTitle>{group.name}</CardTitle>
                  <CardDescription>
                    {group.members && group.members.length} members
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  {group.members &&
                    group.members.map((member) => <p key={member}>{member}</p>)}
                </CardContent>
                //test
                <CardFooter>{/* Add any footer content here */}</CardFooter>
              </Card>
            </div>
          ))
        ) : (
          <p>No groups found</p>
        )}
      </div>
    </div>
  );
}
