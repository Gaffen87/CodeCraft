﻿namespace CodeCraftApi.Domain.Entities;

public class User
{
	public Guid Id { get; set; }
	public Role Role { get; set; }
	public Status Status { get; set; }
	public string UserName { get; set; }
	public List<Session> Sessions { get; set; } = [];
	public List<UserExerciseProgress> ExerciseProgress { get; set; } = [];
	public List<UserStepProgress> StepProgress { get; set; } = [];
}

public enum Role
{
	Unassigned,
	Student,
	Teacher
}

public enum Status
{
	Active,
	Passive
}

