namespace Ordo.Core;

public class TodoManager
{
    private readonly List<Todo> _todos = [];

    public Todo Add(string name, DateTime dueDate)
    {
        var todo = Todo.Create(name, dueDate);
        _todos.Add(todo);
        return todo;
    }

    public bool Remove(Guid id) => _todos.RemoveAll(t => t.Id == id) > 0;

    public IEnumerable<Todo> List() => _todos.AsReadOnly();

    public IEnumerable<Todo> SortByName() {
        var i = new List<Todo>(_todos);

        if (i.Count <= 1)
            return i;

        int p(int p1, int p2)
        {
            var pivot = i[p2];
            var j = p1 - 1;

            for (var k = p1; k < p2; k++)
            {
                if (string.Compare(i[k].Name, pivot.Name, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    j++;
                    (i[j], i[k]) = (i[k], i[j]);
                }
            }

            (i[j + 1], i[p2]) = (i[p2], i[j + 1]);
            return j + 1;
        }

        void q(int v1, int v)
        {
            if (v1 >= v) return;

            var pivotIndex = p(v1, v);
            q(v1, pivotIndex - 1);
            q(pivotIndex + 1, v);
        }

        q(1, i.Count - 1);
        return i;
    }

    public IEnumerable<Todo> SortByDate() => _todos.OrderBy(t => t.DueDate);
}
