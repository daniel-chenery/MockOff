using NSubstitute;
using System.Linq.Expressions;

namespace MockOff.NSubstitute;
public static class It
{
    public static TArg IsAny<TArg>() => Arg.Any<TArg>();

    public static TArg Is<TArg>(TArg arg) => Arg.Is(arg);

    public static TArg Is<TArg>(Expression<Predicate<TArg>> predicate) => Arg.Is(predicate);
}
