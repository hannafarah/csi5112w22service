using csi5112lec4b.models;

namespace csi5112lec4b.services;
public class TodoService {
    // Data placeholder
    private List<Todo> todos = new List<Todo> () {
        new Todo("1", "Complete service project", "Work 2 days to finish this project", true),
        new Todo("2", "Bing watch series", "Finish series ... over the weekend", true),
        new Todo("3", "Exercise", "Finish 30 min of exercise", true)
    };

    public TodoService() {
    }

    public async Task CreateAsync(Todo newTodo) {
        todos.Add(newTodo);
    }

    public async Task<List<Todo>> GetAsync() {
        return todos;
    }

    public async Task<Todo> GetAsync(string Id) {
        return todos.Find(x => x.Id == Id);
    }

    public async Task<bool> UpdateAsync(string Id, Todo updatedTodo) {
        bool result = false;
        int index = todos.FindIndex(x => x.Id == Id);
        if (index != -1) {
            updatedTodo.Id = Id;
            todos[index] = updatedTodo;
            result = true;
        }
        return result;
    }

    public async Task<bool> DeleteAsync(string Id) {
        bool deleted = false;
        int index = todos.FindIndex(x => x.Id == Id);
        if (index != -1) {
            todos.RemoveAt(index);
            deleted = true;
        }
        return deleted;
    }
}
