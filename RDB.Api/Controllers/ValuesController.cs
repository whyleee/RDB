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
    /// <summary>
    /// Manages and queries values.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ValuesController : Controller
    {
        private readonly ValueStore _valueStore;

        public ValuesController(ValueStore valueStore)
        {
            _valueStore = valueStore;
        }

        /// <summary>
        /// Get all values.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        [HttpGet]
        [ProducesResponseType(typeof(IList<Value>), 200)]
        public Task<IList<Value>> Get(CancellationToken ct)
        {
            return _valueStore.GetAllAsync(ct);
        }

        /// <summary>
        /// Get value by ID.
        /// </summary>
        /// <param name="id">ID of value to return.</param>
        /// <param name="ct">Cancellation token.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Value), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string id, CancellationToken ct)
        {
            var value = await _valueStore.GetByIdAsync(id, ct);

            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        /// <summary>
        /// Create new value.
        /// </summary>
        /// <param name="text">New value text.</param>
        /// <param name="ct">Cancellation token.</param>
        [HttpPost]
        [ProducesResponseType(typeof(Value), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IList<string>>), 400)]
        public async Task<IActionResult> Post([FromBody][Required]string text, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var value = new Value { Text = text };
            await _valueStore.InsertAsync(value, ct);
            
            return Ok(value);
        }

        /// <summary>
        /// Update existing value text.
        /// </summary>
        /// <param name="id">ID of value to update.</param>
        /// <param name="text">New value text.</param>
        /// <param name="ct">Cancellation token.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Value), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IList<string>>), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(string id, [FromBody][Required]string text, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var value = new Value { Id = id, Text = text };
            var result = await _valueStore.UpdateAsync(value, ct);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return Ok(value);
        }

        /// <summary>
        /// Delete value by ID.
        /// </summary>
        /// <param name="id">ID of value to delete.</param>
        /// <param name="ct">Cancellation token.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Value), 200)]
        [ProducesResponseType(404)]
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
