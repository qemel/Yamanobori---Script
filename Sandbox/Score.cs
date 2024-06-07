public class Score
{
    public int Value { get; private set; }

    public void Add(int value)
    {
        Value += value;
    }

    public void Subtract(int value)
    {
        Value -= value;
    }
}