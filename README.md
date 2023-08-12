# MockOff

A lightweight Moq-syntax wrapper for NSubstitute to aid getting Mock off of your project.

This is **not** intended to replace Moq. This is **not** a fully-fledged wrapper. Its case is to bridge-the-gap whilst transitioning testing frameworks. Support is not exhaustive. Please **do** support Moq where you can.

## Usage
Install, do a global find & replace on your namespaces replacing `using Moq;` with `using MockOff.NSubstitute;`. Run your tests, hope for the best.

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
:heavy_check_mark: `.ReturnsAsync(TValue)`  

### Exceptions
:x: `.Throws(Exception)`  
:x: `.Throws<TException>(Exception)`  
:x: `.ThrowsAsync(Exception)`  
:x: `.ThrowsAsync<TException>()`  

### Verification
:x: `.Verify()`  
:heavy_check_mark: `.Verify(Times)`  
:x: `.Verify(Times.AtLeast)`  
:x: `.Verify(Times.AtMost)`  
:x: `.Verify(Times.Never)`  
