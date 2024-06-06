using BackEnd_App.Dao;
using BackEnd_App.Data;
using BackEnd_App.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Route("/test")]
    public class TestController
    {
        private readonly TestEntityDao _testEntity;
        public TestController(DatabaseContext db) => _testEntity = new TestEntityDao(db);

        [HttpPost("add")]
        public async Task Add([FromBody] TestEntity testEntity) => await _testEntity.Add(testEntity);

        [HttpGet("all")]
        public List<TestEntity> GetAll() => _testEntity.GetAll();

        [HttpGet("{id}")]
        public TestEntity GetById(string id) => _testEntity.GetById(int.Parse(id));

        [HttpPut("update")]
        public async Task Update([FromBody] TestEntity testEntity) => await _testEntity.Update(testEntity);

        [HttpDelete("remove/{id}")]
        public async Task Remove(string id) => await _testEntity.RemoveById(int.Parse(id));

    }
}
