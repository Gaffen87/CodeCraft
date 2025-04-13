namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class Data
{
    public static async Task<ExerciseItem?> GetExerciseItemsAsync(AppDbContext db, Guid id)
    {
        return await db.ExerciseItem
            .FirstOrDefaultAsync(x => x.Id == id);
    } 
    
}