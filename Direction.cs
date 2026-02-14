class Direction
{
    public string Name;
    public string Description;
    public Dictionary<string, Direction> Choices = new Dictionary<string, Direction>();
    public Action? OnEnter;

    public Direction(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddChoice(string choiceName, Direction direction)
    {
        Choices[choiceName] = direction;
    }
}