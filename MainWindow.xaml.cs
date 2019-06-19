using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLifeUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        public void StartGame()
        {
            SetBoardSize(30, 30);
        }

        public void SetBoardSize(int widht, int height)
        {
            //Clear Column Def
            BoardGrid.ColumnDefinitions.Clear();
            //Clear Row Def
            BoardGrid.RowDefinitions.Clear();

            //Build Col
            for (int i = 0; i < widht; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                BoardGrid.ColumnDefinitions.Add(col);
            }
            //Build Row
            for (int i = 0; i < height; i++)
            {
                RowDefinition row = new RowDefinition();
                BoardGrid.RowDefinitions.Add(row);
            }

            //Populate the dead cell
            for (int x = 0; x < BoardGrid.ColumnDefinitions.Count; x++)
            {
                for (int y = 0; y < BoardGrid.RowDefinitions.Count; y++)
                {
                    Cell cell = new Cell(true, x, y);
                    cell.Opacity = 0;
                    cell.Width = BoardGrid.Width / BoardGrid.ColumnDefinitions.Count;
                    cell.Height = BoardGrid.Height / BoardGrid.RowDefinitions.Count;
                    cell.SetValue(Grid.ColumnProperty, x);
                    cell.SetValue(Grid.RowProperty, y);
                    BoardGrid.Children.Add(cell);
                }
            }
        }

        public void Update(Object sender, RoutedEventArgs e)
        {
            SetBoardSize(Convert.ToInt32(BoardWidth.Text), Convert.ToInt32(BoardHeight.Text));
        }
    }
}
