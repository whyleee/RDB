using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace RDB.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMongoDatabase _mongo;

        public ValuesController(IMongoDatabase mongo)
        {
            _mongo = mongo;
        }

        private IMongoCollection<Value> GetCollection()
        {
            return _mongo.GetCollection<Value>("values");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            return GetCollection().Find(val => true).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var value = GetCollection().Find(val => val.Id == id).FirstOrDefault();
            return value != null ? (IActionResult) Ok(value) : NotFound();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            Console.WriteLine("POST value: " + value);
            GetCollection().InsertOne(new Value { Text = value });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]string value)
        {
            var existingValue = GetCollection().Find(val => val.Id == id).FirstOrDefault();

            if (existingValue == null)
            {
                return NotFound();
            }

            existingValue.Text = value;
            GetCollection().ReplaceOne(val => val.Id == id, existingValue);

            return Ok(existingValue);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = GetCollection().DeleteOne(val => val.Id == id);
            return result.DeletedCount > 0 ? (IActionResult) Ok() : NotFound();
        }
    }

    public class Value
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
