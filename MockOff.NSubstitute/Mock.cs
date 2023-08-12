using NSubstitute;
using NSubstitute.Exceptions;
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
        _current = GetValueFromExpression(expression.Body, Object);

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

    public void Verify(Expression<Action<TMock>> expression, Times times) => Verify(expression.Body, times);

    public void Verify(Expression<Action<TMock>> expression, Func<Times> times) => Verify(expression.Body, times());

    public void Verify<TReturn>(Expression<Func<TMock, TReturn>> expression, Times times) => Verify(expression.Body, times);

    public void Verify<TReturn>(Expression<Func<TMock, TReturn>> expression, Func<Times> times) => Verify(expression, times());

    private void Verify(Expression expression, Times times)
    {
        var target = Object.Received(times.Count);

        try
        {
            GetValueFromExpression(expression, target);
        }
        catch (TargetInvocationException ex) when (ex.InnerException is ReceivedCallsException)
        {
            throw ex.InnerException;
        }
    }

    public TMock Object { get; }

    private object? GetValueFromExpression(Expression? expression, TMock target)
    {
        if (expression is UnaryExpression unaryExpression)
        {
            return unaryExpression.Operand;
        }

        if (expression is MethodCallExpression methodCallExpression)
        {
            var methodArguments = methodCallExpression.Arguments.Select(a => GetValueFromExpression(a, target)).ToArray();

            return methodCallExpression.Method.Invoke(target, methodArguments);
        }

        if (expression is MemberExpression memberExpression && memberExpression.Member is PropertyInfo property)
        {
            return property.GetGetMethod()?.Invoke(target, Array.Empty<object>());
        }

        if (expression is ConstantExpression constantExpression)
        {
            return constantExpression.Value;
        }

        return null;
    }
}
