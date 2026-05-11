using System;
using System.Windows;
using System.Windows.Input;

namespace DominationGame
{
    public partial class MainWindow : Window
    {
        private Board _board;
        private Player _currentPlayer = Player.Red;
        private Player _nextPlayer = Player.Blue;
        private bool _gameStarted = false;

        public MainWindow()
        {
            InitializeComponent();

            _board = new Board(8);
            _board.DisplayOn(gameCanvas);
        }

        private void gameCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // TODO: Handle a click on the game canvas.
            // 1. Return immediately when _gameStarted is false.
            // 2. Get the mouse position via e.GetPosition(gameCanvas).
            // 3. Display the coordinates in xCoordinateLabel / yCoordinateLabel.
            // 4. Convert pixel → grid index (cast to int):
            //      rowIndex    = (Y - Board.Margin) / Block.Size
            //      columnIndex = (X - Board.Margin) / Block.Size
            // 5. Show "(rowIndex, columnIndex)" in cellLabel.
            // 6. Call _board.ClaimBlocks(rowIndex, columnIndex, _currentPlayer):
            //    On success:
            //      a. If _board.HasMoveLeftFor(_nextPlayer) is true
            //             → update playerLabel and call SwitchTurn().
            //      b. Otherwise
            //             → show MessageBox "No moves possible. {_currentPlayer} player wins.",
            //               set _gameStarted = false, call _board.EndGame(), enable moves_Menu.
            //               (The losing player had no moves left, so the current player wins.)
            //    On DominationException → show its Message in a MessageBox (do NOT switch turns).
            throw new NotImplementedException();
        }

        private void SwitchTurn()
        {
            // TODO: Swap _currentPlayer and _nextPlayer so play alternates between Red and Blue.
            throw new NotImplementedException();
        }

        private void MenuItemStartGame_Click(object sender, RoutedEventArgs e)
        {
            _gameStarted = true;
            _board.Restart();
            _currentPlayer = Player.Red;
            _nextPlayer = Player.Blue;
            playerLabel.Content = $"{_currentPlayer} player's turn.";
            xCoordinateLabel.Content = "X Coordinate ";
            yCoordinateLabel.Content = "Y Coordinate ";
            cellLabel.Content = "";
            moves_Menu.IsEnabled = false;
        }

        private void MenuItemMoves_Click(object sender, RoutedEventArgs e)
        {
            MovesWindow window = new MovesWindow();
            window.Show();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
