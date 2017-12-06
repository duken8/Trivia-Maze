using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /*
     * Free Sprites
     * https://cdn.tutsplus.com/gamedev/uploads/2013/05/spriteSheet.png
     */
    [Serializable]
    public partial class MainWindow : Window
    {
        TriviaBoard TheBoard;
        private BitmapImage pforward = new BitmapImage(new Uri(@"Resources/playerforward.png", UriKind.Relative));
        private BitmapImage pbackward = new BitmapImage(new Uri(@"../../Resources/playerbackward.png", UriKind.Relative));
        private BitmapImage pleft = new BitmapImage(new Uri(@"../../Resources/playerleft.png", UriKind.Relative));
        private BitmapImage pright = new BitmapImage(new Uri(@"../../Resources/playerright.png", UriKind.Relative));
        public MainWindow()
        {
            InitializeComponent();
            TheBoard = new TriviaBoard();
            //Player.Source = pforward;
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Log(e.Key.ToString());
            int CurRow, CurCol;
            CurRow = (int)Player.GetValue(Grid.RowProperty);
            CurCol = (int)Player.GetValue(Grid.ColumnProperty);
            try
            {
                switch (e.Key)
                {
                    case Key.Up:
                        Player.Source = pbackward;
                        if (TheBoard.MoveUp()) //Move successful
                        {
                            //disable door im walking into
                            DisableWall("n", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("s", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.RowProperty, TheBoard.YPos);
                        }
                        else //Move failed
                        {
                            ReinforceWall("n", CurCol, CurRow);
                        }
                        break;

                    case Key.Down:
                        Player.Source = pforward;
                        if (TheBoard.MoveDown())
                        {
                            //disable door im walking into
                            DisableWall("s", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("n", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.RowProperty, TheBoard.YPos);
                            CheckForWinner();
                        }
                        else
                        {
                            ReinforceWall("s", CurCol, CurRow);
                        }

                        break;

                    case Key.Left:
                        Player.Source = pleft;
                        if (TheBoard.MoveLeft())
                        {
                            //disable door im walking into
                            DisableWall("w", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("e", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.ColumnProperty, TheBoard.XPos);

                        }
                        else
                        {
                            ReinforceWall("w", CurCol, CurRow);
                        }
                        break;

                    case Key.Right:
                        Player.Source = pright;
                        if (TheBoard.MoveRight())
                        {
                            //disable door im walking into
                            DisableWall("e", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("w", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.ColumnProperty, TheBoard.XPos);


                            CheckForWinner();

                        }
                        else
                        {
                            Log("Failed to open door");
                            //String WallName = $"e{CurCol}{CurRow}";
                            //var temp = (Rectangle)GameGrid.FindName(WallName);
                            //temp.Stroke = Brushes.Black;
                            ReinforceWall("e", CurCol, CurRow);
                        }
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Log("Cannot move out of bounds");

            }
        }

        private void CheckForWinner()
        {
            if ((int)Player.GetValue(Grid.ColumnProperty) == 4 && (int)Player.GetValue(Grid.RowProperty) == 4)
            {
                MessageBox.Show("YOU WON!!!!!!", "YOU ROCK!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
        }

        private void DisableWall(string v, int curCol, int curRow)
        {
            String WallName = $"{v}{curCol}{curRow}";
            var temp = (Rectangle)GameGrid.FindName(WallName);
            temp.Visibility = Visibility.Hidden;
        }
        
        private void ReinforceWall(String v, int curCol, int curRow)
        {
            String WallName = $"{v}{curCol}{curRow}";
            var temp = (Rectangle)GameGrid.FindName(WallName);
            temp.Stroke = Brushes.Black;
        }

        private void Log(String msg)
        {
            Debug.WriteLine(msg);
        }

        private void SaveGameMenu_Click(object sender, RoutedEventArgs e)
        {
            //This should save the game once everything is serializable
            MessageBox.Show("We can't let you save the game, Hal.");
        }

        private void LoadGameMenu_Click(object sender, RoutedEventArgs e)
        {
            //This should ask for a new file when it is finally implemented
            MessageBox.Show("I can't load you do that.");
        }

        private void QuitMenu_Click(object sender, RoutedEventArgs e)
        {
            //This quits the game
            //We can later ask if they want to save if the game hasn't been completed yet.
            this.Close();
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Final project for CSCD371\n" +
                "Produced by Adam Holzer, Nick Duke, and Tyger Hugh.\n" +
                "This is a maze filled with trivia about EWU and Cheney.\n" +
                "Soon to be about Riverpoint.");
        }

        private void GameplayInfoMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a maze filled with EWU trivia. Go Eags!\n" +
                "Move using the arrow keys.\n" +
                "Move into a red door to get asked a question.\n" +
                "If you answer it correctly, the door will open.\n" +
                "Otherwise, the door will turn black and be locked for the rest of the game.\n\n" +
                "Reach the bottom right corner to win!");
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //this is a tight little tester to find my errors :(
            MessageBox.Show("This is broken.");
        }
    }
}

