using Microsoft.EntityFrameworkCore;

namespace BackEnd_App
{
    public static class Utils
    {
        public static async Task<List<T>> GetMultipleElementsByValue<T>(
            DbSet<T> context,
            int number,
            int size
        )
            where T : class =>
            (
                number == 0 || size == 0
                    ? await context.ToListAsync()
                    : await context.Take(size).Skip(size * (number - 1)).Take(size).ToListAsync()
            );
    }
}
