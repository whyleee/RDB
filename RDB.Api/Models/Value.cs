using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace RDB.Api.Models
{
    /// <summary>
    /// Represents a text value with ID.
    /// </summary>
    public class Value
    {
        /// <summary>
        /// Value ID.
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        /// <summary>
        /// Value text.
        /// </summary>
        public string Text { get; set; }
    }
}
