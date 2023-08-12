# MockOff

A Moq-syntax wrapper for NSubstitute to aid getting Mock off of your project.

This is **not** intended to replace Moq. This is **not** a fully-fledged wrapper. Its case is to bridge-the-gap whilst transitioning testing frameworks. Support is not exhaustive. Please **do** support Moq where you can.

## Supported Methods
If it's not listed, it's probably not supported.

### Creation
:heavy_check_mark: `new Mock<TMock>()`  
:x: `Mock.Of<TMock>()`  

### Setup
:heavy_check_mark: `.Setup()`  
:heavy_check_mark: `.Setup(TArg)`  
:heavy_check_mark: `.Setup(It.IsAny<TArg>())`  
:heavy_check_mark: `.Setup(It.Is(TArg))`  
:x: `.SetupSequence()`

### Return
:heavy_check_mark: `.Returns(TReturn)`  
:x: `.Returns(Callback)`  
:x: `.ReturnsAsync(TValue)`  

### Exceptions
:x: `.Throws(Exception)`  
:x: `.Throws<TException>(Exception)`  
:x: `.ThrowsAsync(Exception)`  
:x: `.ThrowsAsync<TException>()`  

### Verification
:x: `.Verify()`  
:x: `.Verify(int)`  
:x: `.Verify(Times)`  
