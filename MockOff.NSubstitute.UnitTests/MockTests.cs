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

    public interface ITest
    {
        int ReturnInt();

        int ReturnIntWithArgument(int anything);

        int AnInt { get; }

        Task<int> ReturnsIntAsync();
    }
}