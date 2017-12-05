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
    public partial class MainWindow : Window
    {
        TriviaBoard TheBoard;
        public MainWindow()
        {
            InitializeComponent();
            TheBoard = new TriviaBoard();
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
                        if (TheBoard.MoveUp()) //Move successful
                        {
                            /* TODO:
                             * 
                             *      Animate Open gate before player moves
                             *      Animate Open gate behind player after move
                             */
                            //GameGrid.Children
                            //GameGrid.FindName
                            //disable door im walking into
                            DisableWall("n", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("s", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.RowProperty, TheBoard.YPos);

                        }
                        else //Move failed
                        {

                        }
                        break;

                    case Key.Down:
                        if (TheBoard.MoveDown())
                        {
                            //disable door im walking into
                            DisableWall("s", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("n", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.RowProperty, TheBoard.YPos);

                        }
                        else
                        {

                        }

                        break;

                    case Key.Left:
                        if (TheBoard.MoveLeft())
                        {
                            //disable door im walking into
                            DisableWall("w", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("e", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.ColumnProperty, (int)(Player.GetValue(Grid.ColumnProperty)) - 1);

                        }
                        else
                        {

                        }
                        break;

                    case Key.Right:
                        if(TheBoard.MoveRight())
                        {
                            //disable door im walking into
                            DisableWall("e", CurCol, CurRow);

                            //disable door im coming out of (opposite)
                            DisableWall("w", TheBoard.XPos, TheBoard.YPos);

                            //move player
                            Player.SetValue(Grid.ColumnProperty, (int)(Player.GetValue(Grid.ColumnProperty)) + 1);

                        }
                        else
                        {

                        }
                        break;
                }
            }catch(ArgumentException ex)
            {
                Log("Cannot move out of bounds");
                
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
            temp.Fill = Brushes.Black;
        }

        private void Log(String msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
