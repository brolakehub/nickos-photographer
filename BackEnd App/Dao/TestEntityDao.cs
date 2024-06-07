using BackEnd_App.Data;
using BackEnd_App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_App.Dao
{
    public class TestEntityDao : IDao<TestEntity>
    {
        private readonly DatabaseContext _context;
        public TestEntityDao(DatabaseContext context) => _context = context;

        public async Task<int> Add(TestEntity testEntity)
        {
            await _context.TestEntities.AddAsync(testEntity);
            await _context.SaveChangesAsync();

            return testEntity.Id;
        }

        public TestEntity GetById(int id) => _context.TestEntities.FirstOrDefaultAsync(testEntity => testEntity.Id == id).Result;

        public List<TestEntity> GetAll() => _context.TestEntities.ToListAsync().Result;

        public async Task RemoveById(int id)
        {
            _context.TestEntities.Remove(GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task Update(TestEntity testEntity)
        {
            _context.TestEntities.Update(testEntity);
            await _context.SaveChangesAsync();
        }
    }
}
