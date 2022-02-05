namespace csi5112lec4b.models;
public class Todo {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }

    public Todo(string Id, string Name, string Description, bool IsComplete) {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.IsComplete = IsComplete;
    }
}