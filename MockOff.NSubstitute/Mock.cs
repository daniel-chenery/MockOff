using NSubstitute;
using System.Linq.Expressions;
using System.Reflection;

namespace MockOff.NSubstitute;

public class Mock<TMock>
    where TMock : class
{
    private object? _current;

    public Mock()
    {
        Object = Substitute.For<TMock>();
    }

    public Mock<TMock> Setup<TReturn>(Expression<Func<TMock, TReturn>> expression)
    {
        _current = GetValueFromExpression(expression.Body);

        return this;
    }

    public Mock<TMock> Returns<TReturn>(TReturn returnValue)
    {
        _current.Returns(returnValue);

        return this;
    }

    public Mock<TMock> ReturnsAsync<TReturn>(TReturn returnValue)
    {
        _current.Returns(Task.FromResult(returnValue));

        return this;
    }

    public TMock Object { get; }

    private object? GetValueFromExpression(Expression? expression)
    {
        if (expression is UnaryExpression unaryExpression)
        {
            return unaryExpression.Operand;
        }

        if (expression is MethodCallExpression methodCallExpression)
        {
            var methodArguments = methodCallExpression.Arguments.Select(GetValueFromExpression).ToArray();

            return methodCallExpression.Method.Invoke(Object, methodArguments);
        }

        if (expression is MemberExpression memberExpression && memberExpression.Member is PropertyInfo property)
        {
            return property.GetGetMethod()?.Invoke(Object, Array.Empty<object>());
        }

        if (expression is ConstantExpression constantExpression)
        {
            return constantExpression.Value;
        }

        return null;
    }
}
