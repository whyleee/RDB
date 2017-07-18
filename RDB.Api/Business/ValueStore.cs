using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using RDB.Api.Models;

namespace RDB.Api.Business
{
    public class ValueStore
    {
        private readonly IMongoDatabase _mongo;

        public ValueStore(IMongoDatabase mongo)
        {
            _mongo = mongo;
        }

        private IMongoCollection<Value> GetCollection()
        {
            return _mongo.GetCollection<Value>("values");
        }

        public async Task<IList<Value>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            var cursor = await GetCollection().FindAsync(val => true, cancellationToken: ct);
            return await cursor.ToListAsync(ct);
        }

        public async Task<Value> GetByIdAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            var cursor = await GetCollection().FindAsync(val => val.Id == id, cancellationToken: ct);
            return await cursor.FirstOrDefaultAsync(ct);
        }

        public Task InsertAsync(Value value, CancellationToken ct = default(CancellationToken))
        {
            return GetCollection().InsertOneAsync(value, cancellationToken: ct);
        }

        public Task<ReplaceOneResult> UpdateAsync(Value value, CancellationToken ct = default(CancellationToken))
        {
            return GetCollection().ReplaceOneAsync(val => val.Id == value.Id, value, cancellationToken: ct);
        }

        public Task<DeleteResult> DeleteAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            return GetCollection().DeleteOneAsync(val => val.Id == id, ct);
        }
    }
}
