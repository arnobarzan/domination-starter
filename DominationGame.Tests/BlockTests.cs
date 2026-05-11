using Xunit;

namespace DominationGame.Tests
{
    // Block creates WPF Rectangle objects — all tests must run on an STA thread.
    [Collection("Sequential")]
    public class BlockTests
    {
        [StaFact]
        public void NewBlock_IsEmpty()
        {
            var block = new Block(0, 0);

            Assert.True(block.IsEmpty());
        }

        [StaFact]
        public void Owner_SetToRed_GetterReturnsRed()
        {
            var block = new Block(0, 0);

            block.Owner = Player.Red;

            Assert.Equal(Player.Red, block.Owner);
        }

        [StaFact]
        public void Owner_SetToBlue_GetterReturnsBlue()
        {
            var block = new Block(0, 0);

            block.Owner = Player.Blue;

            Assert.Equal(Player.Blue, block.Owner);
        }

        [StaFact]
        public void Owner_SetToRed_BlockIsNotEmpty()
        {
            var block = new Block(0, 0);

            block.Owner = Player.Red;

            Assert.False(block.IsEmpty());
        }

        [StaFact]
        public void Owner_SetToBlue_BlockIsNotEmpty()
        {
            var block = new Block(0, 0);

            block.Owner = Player.Blue;

            Assert.False(block.IsEmpty());
        }

        [StaFact]
        public void Owner_ResetToNone_AfterBeingRed_BlockIsEmptyAgain()
        {
            var block = new Block(0, 0);
            block.Owner = Player.Red;

            block.Owner = Player.None;

            Assert.True(block.IsEmpty());
        }

        [StaFact]
        public void ToString_ReturnsRowAndColumnInParentheses()
        {
            var block = new Block(3, 5);

            Assert.Equal("(3,5)", block.ToString());
        }
    }
}
