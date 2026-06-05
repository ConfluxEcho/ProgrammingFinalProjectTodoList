using System.Reflection.Metadata.Ecma335;

namespace TodoList.Services;

public class TaskDefinition
{
    public string Name;
    public short Priority;
    public string Deadline;
    public bool Finished;
    public string Color;

    public TaskDefinition(string name, short priority, string deadline, bool finished, string color)
    {
        Name = name;
        Priority = priority;
        Deadline = deadline;
        Finished = finished;
        Color = color;
    }
}