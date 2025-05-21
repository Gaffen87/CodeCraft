export default function Member({
	member,
}: {
	member: { id: string; name: string };
}) {
	return (
		<div
			key={member.id}
			className="text-sm text-gray-700 flex items-center gap-2"
		>
			<div className="w-6 h-6 bg-blue-100 text-blue-800 rounded-full flex items-center justify-center text-xs font-medium uppercase">
				{member.name[0]}
			</div>
			<span>{member.name}</span>
		</div>
	);
}
