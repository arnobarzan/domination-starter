using System;
using Xunit;

namespace DominationGame.Tests
{
    public class DominationExceptionTests
    {
        [Fact]
        public void Constructor_WithMessage_SetsMessageProperty()
        {
            var ex = new DominationException("Move not possible.");

            Assert.Equal("Move not possible.", ex.Message);
        }

        [Fact]
        public void DominationException_InheritsFromException()
        {
            var ex = new DominationException("test");

            Assert.IsAssignableFrom<Exception>(ex);
        }
    }
}
