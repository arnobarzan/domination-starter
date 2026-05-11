using System;
using System.IO;
using System.Windows;

namespace DominationGame
{
    public partial class MovesWindow : Window
    {
        public MovesWindow()
        {
            InitializeComponent();
            // TODO: Load the move log into movesBlock.Text.
            // - Build the file path: combine MyDocuments with "domination.txt".
            //   Use Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).
            // - Open the file with File.OpenText(...) and read all content with ReadToEnd().
            //   Assign the result to movesBlock.Text.
            // - Catch IOException: show the error message in a MessageBox, then re-throw.
            // - Always close the StreamReader in a finally block (check for null first), or
            //   use 'using'.
            throw new NotImplementedException();
        }
    }
}
