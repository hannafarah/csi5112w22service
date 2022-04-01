using csi5112lec4b.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace csi5112lec4b.services;
public class TodoService {

    private readonly IMongoCollection<Todo> _todos;


    public TodoService(IOptions<TodoDatabaseSettings> todoDatabaseSettings, IConfiguration configuration) {
        string connection_string = configuration.GetValue<string>("CONNECTION_STRING");
        if (string.IsNullOrEmpty(connection_string)) {
            // default - should not be used
            connection_string = todoDatabaseSettings.Value.ConnectionString;
        }
        var settings = MongoClientSettings.FromConnectionString(connection_string);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase(todoDatabaseSettings.Value.DatabaseName);
        _todos = database.GetCollection<Todo>(todoDatabaseSettings.Value.TodoCollectionName);
    }

    public async Task CreateAsync(Todo newTodo) {
        newTodo.Id = null; // will be set by Mongo
        await _todos.InsertOneAsync(newTodo);
    }

    // Get all Todos
    public async Task<List<Todo>> GetAsync() {
        return await _todos.Find(_ => true).ToListAsync();
    }

    // Get a Todo with a specified Id
    public async Task<Todo> GetAsync(string Id) {
        return await _todos.Find<Todo>(todo => todo.Id == Id).FirstOrDefaultAsync();
    }

    // Update a Todo with a specified Id
    public async Task<bool> UpdateAsync(string Id, Todo updatedTodo) {
        ReplaceOneResult r = await _todos.ReplaceOneAsync(todo => todo.Id == updatedTodo.Id, updatedTodo);
        return r.IsModifiedCountAvailable && r.ModifiedCount == 1;
    }

    // Delete a Todo with a specidied Id
    public async Task<bool> DeleteAsync(string Id) {
        DeleteResult r = await _todos.DeleteOneAsync(todo => todo.Id == Id);
        return r.DeletedCount == 1;
    }

}
