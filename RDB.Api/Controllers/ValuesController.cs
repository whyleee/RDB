using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RDB.Api.Business;
using RDB.Api.Models;

namespace RDB.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ValueStore _valueStore;

        public ValuesController(ValueStore valueStore)
        {
            _valueStore = valueStore;
        }

        // GET api/values
        [HttpGet]
        public Task<IList<Value>> Get(CancellationToken ct)
        {
            return _valueStore.GetAllAsync(ct);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken ct)
        {
            var value = await _valueStore.GetByIdAsync(id, ct);

            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required]string text, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var value = new Value { Text = text };
            await _valueStore.InsertAsync(value, ct);
            
            return Ok(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody][Required]string text, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var value = new Value { Id = id, Text = text };
            var result = await _valueStore.UpdateAsync(value, ct);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id, CancellationToken ct)
        {
            var result = await _valueStore.DeleteAsync(id, ct);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}