using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private int MinNeighbour;
        private int MaxNeighbour;
        private int ReqNeighbour;

        private int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            Reset();
        }

        public void Reset(Object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            if(StartBtn.IsChecked == true)
            {
                StartBtn.IsChecked = false;
            }
            SetBoardSize(Convert.ToInt32(BoardWidthTxtBox.Text), Convert.ToInt32(BoardHeightTxtBox.Text));
            MinNeighbour = Convert.ToInt32(MinNeighbourTxtBox.Text);
            MaxNeighbour = Convert.ToInt32(MaxNeighbourTxtBox.Text);
            ReqNeighbour = Convert.ToInt32(ReqNeighbourTxtBox.Text);
        }

        public void SetBoardSize(int width, int height)
        {
            BoardGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            BoardGrid.VerticalAlignment = VerticalAlignment.Stretch;

            //Clear Child
            BoardGrid.Children.Clear();
            //Clear Column Def
            BoardGrid.ColumnDefinitions.Clear();
            //Clear Row Def
            BoardGrid.RowDefinitions.Clear();

            //Build Col
            for (int i = 0; i < width; i++)
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
            for (int row = 0; row < BoardGrid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < BoardGrid.ColumnDefinitions.Count; column++)
                {
                    Cell cell = new Cell(false, row, column);
                    cell.Opacity = 0;
                    cell.Width = BoardGrid.Width / BoardGrid.ColumnDefinitions.Count;
                    cell.Height = BoardGrid.Height / BoardGrid.RowDefinitions.Count;
                    cell.SetValue(Grid.ColumnProperty, column);
                    cell.SetValue(Grid.RowProperty, row);
                    BoardGrid.Children.Add(cell);
                }
            }
        }

        public void UpdateSize(Object sender, RoutedEventArgs e)
        {
            SetBoardSize(Convert.ToInt32(BoardWidthTxtBox.Text), Convert.ToInt32(BoardHeightTxtBox.Text));
        }

        public async void Start(Object sender, RoutedEventArgs e)
        {
            ToggleButton auto = (ToggleButton)sender;
            await Run(auto);
        }

        private async Task Run(ToggleButton toggle)
        {
            int delay = Convert.ToInt32(DelayTxtBox.Text);


            while (toggle.IsChecked == true)
            {
                if (counter == 0)
                {
                    Next();
                }
                else
                {
                    UpdateBoard();
                }
                counter = (counter + 1) % 2;
                await Task.Delay(delay);
            }
        }

        public int CountNeighbour(Cell cell)
        {
            int width = BoardGrid.ColumnDefinitions.Count;
            int height = BoardGrid.RowDefinitions.Count;

            int count = 0;

            //for (int j = -1; j < 2; j++)
            //{
            //    for (int i = -1; i < 2; i++)
            //    {
            //        if (!(i == 0 && j == 0)
            //            && (cell.Col + i >= width && cell.Col + i <= width - 1)
            //            && (cell.Row + j >= height && cell.Row + j <= height - 1))
            //        {
            //            Cell neighbour = BoardGrid.Children[cell.Row * BoardGrid.ColumnDefinitions.Count + cell.Col] as Cell;
            //            if (neighbour.IsAlive)
            //                count++;
            //        }
            //    }
            //}
            for (int y = cell.Row - 1; y <= cell.Row + 1; y++)
            {
                if (y >= 0 && y <= BoardGrid.RowDefinitions.Count - 1)
                {
                    for (int x = cell.Col - 1; x <= cell.Col + 1; x++)
                    {
                        if (x >= 0 && x <= BoardGrid.ColumnDefinitions.Count - 1)
                        {
                            Cell neighbour = BoardGrid.Children[y * BoardGrid.ColumnDefinitions.Count + x] as Cell;
                            if (neighbour.IsAlive == true && (cell.Col != x || cell.Row != y))
                                count++;
                        }
                    }
                }
            }

            return count;
        }

        public void Next(Object sender, RoutedEventArgs e)
        {
            
            if(counter == 0)
            {
                Next();
            }
            else
            {
                UpdateBoard();
            }
            counter = (counter + 1) % 2;
            D:\Michael\Nonlinear\GameOfLifeUI\MainWindow.xaml
        }

        public void Next()
        {
            foreach (Cell cell in BoardGrid.Children) //Iteratre through all cell in the grid
            {
                int neighbourCount = CountNeighbour(cell); //Count the number of neighbour alive

                cell.NextRound(neighbourCount, MinNeighbour, MaxNeighbour, ReqNeighbour); //Determine if it should alive or die
            }
        }

        public void UpdateBoard()
        {
            foreach (Cell cell in BoardGrid.Children)
            {
                cell.IsAlive = cell.IsAliveNextRound;
                if (cell.IsAlive)
                {
                    cell.Background = Brushes.Black;
                    cell.Opacity = 1;
                }
                else
                {
                    cell.Opacity = 0;
                }
            }
        }
    }
}
