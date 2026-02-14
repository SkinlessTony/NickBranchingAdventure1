class Character
{
    public string Name;
    public string Class;
    public int Health = 20;

    public Character(string name, string characterClass)
    {
        Name = name;
        Class = characterClass.ToLower();
    }

    public void DisplayStoryBasedOnHealth()
    {
        if (Health <= 5 && Health > 0)
            Console.WriteLine("\nYour wounds are severe. You need to be careful!");
    }
}