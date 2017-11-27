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
                        if (TheBoard.MoveUp())
                        {
                            Player.SetValue(Grid.RowProperty, (int)(Player.GetValue(Grid.RowProperty)) - 1);

                        }
                        break;

                    case Key.Down:
                        if (TheBoard.MoveDown())
                        {
                            Player.SetValue(Grid.RowProperty, (int)(Player.GetValue(Grid.RowProperty)) + 1);

                        }
                        break;

                    case Key.Left:
                        if (TheBoard.MoveLeft())
                        {
                            Player.SetValue(Grid.ColumnProperty, (int)(Player.GetValue(Grid.ColumnProperty)) - 1);

                        }
                        break;

                    case Key.Right:
                        if(TheBoard.MoveRight())
                        {
                            Player.SetValue(Grid.ColumnProperty, (int)(Player.GetValue(Grid.ColumnProperty)) + 1);

                        }
                        break;
                }
            }catch(ArgumentException ex)
            {
                Log("Cannot move out of bounds");
                
            }
        }
        private void Log(String msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
