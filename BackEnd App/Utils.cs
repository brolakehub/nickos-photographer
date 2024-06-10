using Microsoft.EntityFrameworkCore;

namespace BackEnd_App
{
    public static class Utils
    {
        public static IQueryable<T> GetMultipleElementsByValue<T>(
            DbSet<T> context,
            int number,
            int size
        )
            where T : class =>
            (
                number == 0 || size == 0
                    ? context
                    : context.Take(size).Skip(size * (number - 1)).Take(size)
            );
    }
}
