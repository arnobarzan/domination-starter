using System;
using Xunit;

namespace DominationGame.Tests
{
    // Board creates Block (WPF Rectangle) objects and writes to domination.txt — all tests
    // must run on an STA thread and must not run in parallel with other Board/Block tests.
    [Collection("Sequential")]
    public class BoardTests
    {
        // ── HasMoveLeftFor ──────────────────────────────────────────────────────────────

        [StaFact]
        public void HasMoveLeftFor_PlayerNone_ThrowsArgumentException()
        {
            using var board = new Board(8);

            Assert.Throws<ArgumentException>(() => board.HasMoveLeftFor(Player.None));
        }

        [StaFact]
        public void HasMoveLeftFor_OnNewBoard_RedCanMove()
        {
            using var board = new Board(8);

            Assert.True(board.HasMoveLeftFor(Player.Red));
        }

        [StaFact]
        public void HasMoveLeftFor_OnNewBoard_BlueCanMove()
        {
            using var board = new Board(8);

            Assert.True(board.HasMoveLeftFor(Player.Blue));
        }

        [StaFact]
        public void HasMoveLeftFor_AfterOneRedMove_BlueCanStillMove()
        {
            using var board = new Board(8);
            board.ClaimBlocks(0, 0, Player.Red);    // occupies (0,0) and (1,0)

            Assert.True(board.HasMoveLeftFor(Player.Blue));
        }

        [StaFact]
        public void HasMoveLeftFor_BoardFilledWithRedDominoes_BlueCannotMove()
        {
            // 2×2 board: Red fills it vertically — (0,0)+(1,0) then (0,1)+(1,1).
            // No two horizontally adjacent empty cells remain, so Blue has no valid move.
            using var board = new Board(2);
            board.ClaimBlocks(0, 0, Player.Red);
            board.ClaimBlocks(0, 1, Player.Red);

            Assert.False(board.HasMoveLeftFor(Player.Blue));
        }

        [StaFact]
        public void HasMoveLeftFor_BoardFilledWithBlueDominoes_RedCannotMove()
        {
            // 2×2 board: Blue fills it horizontally — (0,0)+(0,1) then (1,0)+(1,1).
            // No two vertically adjacent empty cells remain, so Red has no valid move.
            using var board = new Board(2);
            board.ClaimBlocks(0, 0, Player.Blue);
            board.ClaimBlocks(1, 0, Player.Blue);

            Assert.False(board.HasMoveLeftFor(Player.Red));
        }

        // ── ClaimBlocks — argument validation ──────────────────────────────────────────

        [StaFact]
        public void ClaimBlocks_PlayerNone_ThrowsArgumentException()
        {
            using var board = new Board(8);

            Assert.Throws<ArgumentException>(() => board.ClaimBlocks(0, 0, Player.None));
        }

        [StaFact]
        public void ClaimBlocks_Red_ValidMove_DoesNotThrow()
        {
            using var board = new Board(8);

            var ex = Record.Exception(() => board.ClaimBlocks(0, 0, Player.Red));

            Assert.Null(ex);
        }

        [StaFact]
        public void ClaimBlocks_Blue_ValidMove_DoesNotThrow()
        {
            using var board = new Board(8);

            var ex = Record.Exception(() => board.ClaimBlocks(0, 0, Player.Blue));

            Assert.Null(ex);
        }

        // ── ClaimBlocks — Red places vertically ────────────────────────────────────────

        [StaFact]
        public void ClaimBlocks_Red_PlacesVertically_FirstCellBecomesOccupied()
        {
            using var board = new Board(8);
            board.ClaimBlocks(0, 0, Player.Red);    // occupies (0,0) and (1,0)

            // (0,0) is now taken — any player trying to start there must be refused.
            Assert.Throws<DominationException>(() => board.ClaimBlocks(0, 0, Player.Blue));
        }

        [StaFact]
        public void ClaimBlocks_Red_PlacesVertically_SecondCellBecomesOccupied()
        {
            using var board = new Board(8);
            board.ClaimBlocks(0, 0, Player.Red);    // occupies (0,0) and (1,0)

            // (1,0) is now taken.
            Assert.Throws<DominationException>(() => board.ClaimBlocks(1, 0, Player.Blue));
        }

        [StaFact]
        public void ClaimBlocks_Red_AtLastRow_ThrowsDominationException()
        {
            // Row 7 is the last row; the second cell (row 8) is out of bounds.
            using var board = new Board(8);

            Assert.Throws<DominationException>(() => board.ClaimBlocks(7, 0, Player.Red));
        }

        [StaFact]
        public void ClaimBlocks_Red_SecondCellAlreadyOccupied_ThrowsDominationException()
        {
            using var board = new Board(8);
            board.ClaimBlocks(1, 0, Player.Blue);   // Blue occupies (1,0) and (1,1)

            // Red at (0,0) needs (0,0) and (1,0); first cell is free but second is taken.
            Assert.Throws<DominationException>(() => board.ClaimBlocks(0, 0, Player.Red));
        }

        // ── ClaimBlocks — Blue places horizontally ─────────────────────────────────────

        [StaFact]
        public void ClaimBlocks_Blue_PlacesHorizontally_FirstCellBecomesOccupied()
        {
            using var board = new Board(8);
            board.ClaimBlocks(0, 0, Player.Blue);   // occupies (0,0) and (0,1)

            Assert.Throws<DominationException>(() => board.ClaimBlocks(0, 0, Player.Red));
        }

        [StaFact]
        public void ClaimBlocks_Blue_PlacesHorizontally_SecondCellBecomesOccupied()
        {
            using var board = new Board(8);
            board.ClaimBlocks(0, 0, Player.Blue);   // occupies (0,0) and (0,1)

            Assert.Throws<DominationException>(() => board.ClaimBlocks(0, 1, Player.Red));
        }

        [StaFact]
        public void ClaimBlocks_Blue_AtLastColumn_ThrowsDominationException()
        {
            // Column 7 is the last column; the second cell (column 8) is out of bounds.
            using var board = new Board(8);

            Assert.Throws<DominationException>(() => board.ClaimBlocks(0, 7, Player.Blue));
        }

        [StaFact]
        public void ClaimBlocks_Blue_SecondCellAlreadyOccupied_ThrowsDominationException()
        {
            using var board = new Board(8);
            board.ClaimBlocks(0, 1, Player.Red);    // Red occupies (0,1) and (1,1)

            // Blue at (0,0) needs (0,0) and (0,1); first cell is free but second is taken.
            Assert.Throws<DominationException>(() => board.ClaimBlocks(0, 0, Player.Blue));
        }

        // ── ClaimBlocks — exception message ────────────────────────────────────────────

        [StaFact]
        public void ClaimBlocks_InvalidMove_DominationExceptionMessageIsCorrect()
        {
            using var board = new Board(8);

            var ex = Assert.Throws<DominationException>(() => board.ClaimBlocks(7, 0, Player.Red));

            Assert.Equal("Move not possible.", ex.Message);
        }
    }
}
