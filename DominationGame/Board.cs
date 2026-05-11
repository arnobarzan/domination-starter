using System;
using System.IO;
using System.Windows.Controls;

namespace DominationGame
{
    public class Board : IDisposable
    {
        private int _size;
        private Block[,] _blocks;
        private StreamWriter _writer;

        public const int Margin = 10;

        public Board(int size)
        {
            _size = size;
            _blocks = new Block[size, size];
            InitializeBlocks();

            string moveLogFilePath = GetMoveLogFilePath();

            _writer = File.CreateText(moveLogFilePath);
        }

        public void Restart()
        {
            ClearMoveLogFile();
            ClearBlocks();
        }

        public void DisplayOn(Canvas canvas)
        {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++)
                {
                    _blocks[rowIndex, columnIndex].DisplayOn(canvas);
                }
            }
        }

        public bool HasMoveLeftFor(Player player)
        {
            // TODO: Return true if 'player' can still place at least one domino on the board.
            // - Throw ArgumentException if player == Player.None.
            // - Scan every cell; for each empty cell, check whether the second cell of the domino
            //   is also empty and within the board bounds:
            //     Red  (vertical):    second cell → (rowIndex + 1, columnIndex)
            //     Blue (horizontal):  second cell → (rowIndex,     columnIndex + 1)
            // - Return true as soon as a valid placement is found; return false if none exists.
            throw new NotImplementedException();
        }

        public void ClaimBlocks(int rowIndex, int columnIndex, Player player)
        {
            // TODO: Place a domino for 'player' starting at (rowIndex, columnIndex).
            // - Throw ArgumentException if player == Player.None.
            // - Determine the index of the second cell:
            //     Red  (vertical):    (rowIndex + 1, columnIndex)
            //     Blue (horizontal):  (rowIndex,     columnIndex + 1)
            // - Throw DominationException("Move not possible.") when ANY of the following is true:
            //     * the second row index is >= _size, OR
            //     * the second column index is >= _size, OR
            //     * the first cell is not empty, OR
            //     * the second cell is not empty.
            // - Assign 'player' as Owner of both Block objects.
            // - Write a log line to _writer:  "{player} player {block1} {block2}"
            //   (Block.ToString() already returns "(row,col)".)
            throw new NotImplementedException();
        }

        public void EndGame()
        {
            _writer.Close();
        }

        private void InitializeBlocks()
        {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++)
                {
                    _blocks[rowIndex, columnIndex] = new Block(rowIndex, columnIndex);
                }
            }
        }

        private void ClearBlocks()
        {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++)
                {
                    _blocks[rowIndex, columnIndex].Owner = Player.None;
                }
            }
        }

        private string GetMoveLogFilePath()
        {
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(myDocumentsPath, "domination.txt");
        }

        private void ClearMoveLogFile()
        {
            _writer.Close();
            string moveLogFilePath = GetMoveLogFilePath();
            _writer = File.CreateText(moveLogFilePath);
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }
    }
}
