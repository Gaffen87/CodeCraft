import { useEffect, useState } from "react";

export default function useExercise({ exerciseId }) {
	const [exercise, setExercise] = useState();
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);

	useEffect(() => {
		const fetchExercise = async () => {
			try {
				const response = await fetch(
					import.meta.env.VITE_API_URL + "/api/exercise/1",
					{
						method: "GET",
						headers: {
							"Content-Type": "application/json",
						},
					}
				);
				if (!response.ok) {
					throw new Error("Network response was not ok");
				}
				const data = await response.json();
				setExercise(data);
			} catch (error) {
				console.error("Error fetching exercise:", error);
			}
		};

		fetchExercise();
	}, []);

	return { exercise, loading, error };
}
