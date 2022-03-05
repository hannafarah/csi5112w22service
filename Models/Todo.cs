using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace csi5112lec4b.models;
public class Todo {
    [BsonId][BsonRepresentation(BsonType.ObjectId)][JsonPropertyName("id")]
    public string? Id { get; set; }

    [BsonElement("AnotherName")][JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("isComplete")]
    public bool IsComplete { get; set; }

    public Todo(string Id, string Name, string Description, bool IsComplete) {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.IsComplete = IsComplete;
    }
}