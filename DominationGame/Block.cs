using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DominationGame
{
    public class Block
    {
        public const double Size = 45.0;

        private int _rowIndex;
        private int _columnIndex;

        private Rectangle _rectangle;

        private Player _owner;
        public Player Owner
        {
            get { return _owner; }
            set
            {
                // TODO: Store 'value' in _owner, then update _rectangle.Fill with a SolidColorBrush:
                //   Player.Red   → Colors.Red
                //   Player.Blue  → Colors.Blue
                //   Player.None  → Colors.White
                throw new NotImplementedException();
            }
        }

        public Block(int rowIndex, int columnIndex)
        {
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _rectangle = new Rectangle
            {
                Height = Size,
                Width = Size,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            double x = columnIndex * Size + Board.Margin;
            double y = rowIndex * Size + Board.Margin;
            _rectangle.Margin = new Thickness(x, y, 0, 0);
        }

        public void DisplayOn(Canvas canvas)
        {
            canvas.Children.Add(_rectangle);
        }

        public bool IsEmpty()
        {
            return Owner == 0;
        }

        public override string ToString()
        {
            return $"({_rowIndex},{_columnIndex})";
        }
    }
}
