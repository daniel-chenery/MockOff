using NSubstitute;
using NSubstitute.Exceptions;

namespace MockOff.NSubstitute.UnitTests;

public class MockTests
{
    [Fact]
    public void MockMethodSetup_ReturnsMockedValue()
    {
        var mock = new Mock<ITest>();

        mock.Setup(m => m.ReturnInt()).Returns(1);

        Assert.Equal(1, mock.Object.ReturnInt());
    }

    [Fact]
    public void MockMethodSetup_AnyArgument_ReturnsMockedValue()
    {
        var mock = new Mock<ITest>();

        mock.Setup(m => m.ReturnIntWithArgument(It.IsAny<int>())).Returns(1);

        Assert.Equal(1, mock.Object.ReturnIntWithArgument(1));
        Assert.Equal(1, mock.Object.ReturnIntWithArgument(2));
        Assert.Equal(1, mock.Object.ReturnIntWithArgument(3));
        Assert.Equal(1, mock.Object.ReturnIntWithArgument(4));
    }

    [Fact]
    public void MockMethodSetup_SpecificArgument_ReturnsMockedValue()
    {
        var mock = new Mock<ITest>();

        mock.Setup(m => m.ReturnIntWithArgument(1)).Returns(1);

        Assert.Equal(1, mock.Object.ReturnIntWithArgument(1));
        Assert.Equal(0, mock.Object.ReturnIntWithArgument(2));
        Assert.Equal(0, mock.Object.ReturnIntWithArgument(3));
        Assert.Equal(0, mock.Object.ReturnIntWithArgument(4));
    }

    [Fact]
    public void MockMethodSetup_SpecificArgArgument_ReturnsMockedValue()
    {
        var mock = new Mock<ITest>();

        mock.Setup(m => m.ReturnIntWithArgument(It.Is(1))).Returns(1);

        Assert.Equal(1, mock.Object.ReturnIntWithArgument(1));
        Assert.Equal(0, mock.Object.ReturnIntWithArgument(2));
        Assert.Equal(0, mock.Object.ReturnIntWithArgument(3));
        Assert.Equal(0, mock.Object.ReturnIntWithArgument(4));
    }

    [Fact]
    public void MockMethodSetup_ArgPredicate_ReturnsMockedValue()
    {
        var mock = new Mock<ITest>();

        mock.Setup(m => m.ReturnIntWithArgument(It.Is<int>(i => i % 2 == 0))).Returns(1);

        Assert.Equal(0, mock.Object.ReturnIntWithArgument(1));
        Assert.Equal(1, mock.Object.ReturnIntWithArgument(2));
        Assert.Equal(0, mock.Object.ReturnIntWithArgument(3));
        Assert.Equal(1, mock.Object.ReturnIntWithArgument(4));
    }

    [Fact]
    public void MockPropertySetup_ReturnsMockedValue()
    {
        var mock = new Mock<ITest>();

        mock.Setup(m => m.AnInt).Returns(1);

        Assert.Equal(1, mock.Object.AnInt);
    }

    [Fact]
    public async Task MockAsyncSetup_ReturnsMockedValue()
    {
        var mock = new Mock<ITest>();

        mock.Setup(m => m.ReturnsIntAsync()).ReturnsAsync(1);

        Assert.Equal(1, await mock.Object.ReturnsIntAsync());
    }

    [Fact]
    public void MockVerify_Times_ThrowsForNoReceivedCalls()
    {
        var mock = new Mock<ITest>();

        Assert.Throws<ReceivedCallsException>(() => mock.Verify(m => m.DoSomething(), Times.Once()));
        Assert.Throws<ReceivedCallsException>(() => mock.Object.Received(1).ReturnInt());
    }

    [Fact]
    public void MockVerify_TimesFunc_ThrowsForNoReceivedCalls()
    {
        var mock = new Mock<ITest>();

        Assert.Throws<ReceivedCallsException>(() => mock.Verify(m => m.DoSomething(), Times.Once));
    }

    [Fact]
    public void MockVerify_WithReturn_Times_ThrowsForNoReceivedCalls()
    {
        var mock = new Mock<ITest>();

        Assert.Throws<ReceivedCallsException>(() => mock.Verify(m => m.ReturnInt(), Times.Once()));
    }

    [Fact]
    public void MockVerify_WithReturn_TimesFunc_ThrowsForNoReceivedCalls()
    {
        var mock = new Mock<ITest>();

        Assert.Throws<ReceivedCallsException>(() => mock.Verify(m => m.ReturnInt(), Times.Once));
    }

    [Fact]
    public void MockVerify_WithReturn_DoesNotThrowForReceivedCalls()
    {
        var mock = new Mock<ITest>();

        _ = mock.Object.ReturnInt();

        mock.Verify(m => m.ReturnInt(), Times.Once());
    }

    [Fact]
    public void MockVerify_WithReturn_ThrowsForTooManyReceivedCalls()
    {
        var mock = new Mock<ITest>();

        _ = mock.Object.ReturnInt();
        _ = mock.Object.ReturnInt();

        Assert.Throws<ReceivedCallsException>(() => mock.Verify(m => m.ReturnInt(), Times.Once()));
    }

    [Fact]
    public void MockVerify_WithReturn_DoesNotThrowForExactlyReceivedCalls()
    {
        var mock = new Mock<ITest>();

        _ = mock.Object.ReturnInt();
        _ = mock.Object.ReturnInt();

        mock.Verify(m => m.ReturnInt(), Times.Exactly(2));
    }

    public interface ITest
    {
        void DoSomething();

        int ReturnInt();

        int ReturnIntWithArgument(int anything);

        int AnInt { get; }

        Task<int> ReturnsIntAsync();
    }
}