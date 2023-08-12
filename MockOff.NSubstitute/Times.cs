namespace MockOff.NSubstitute;

public readonly struct Times
{
    private Times(int count)
    {
        Count = count;
    }

    internal readonly int Count { get; }

    public static Times Once() => new(1);

    public static Times Exactly(int exactly) => new(exactly);
}
