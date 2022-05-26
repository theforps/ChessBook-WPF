using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RotTsar
{
    public partial class BishopWindow : Page
    {
        public BishopWindow()
        {
            InitializeComponent();
            CreateBoard();
        }

        Button[,] board = new Button[8, 8]; //создание поля
        Image[,] pieces = new Image[8, 8];//добавление фигур на поле

        public void CreateBoard()
        {
            //как устроено поле
            //00  01  02  03  04  05  06  07 
            //10  11  12  13  14  15  16  17 
            //20  21  22  23  24  25  26  27 
            //30  31  32  33  34  35  36  37 
            //40  41  42  43  44  45  46  47 
            //50  51  52  53  54  55  56  57 
            //60  61  62  63  64  65  66  67 
            //70  71  72  73  74  75  76  77 

            //построение поля
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Button cell = new Button();
                    
                    if ((i + j) % 2 == 0)
                    {
                        cell.Background = Brushes.PeachPuff;
                        cell.BorderBrush = Brushes.PeachPuff; 
                    }
                    else
                    {
                        cell.Background = Brushes.Sienna;
                        cell.BorderBrush = Brushes.Sienna;
                    }

                    cell.Click += new RoutedEventHandler(Click);
                    cell.IsDefault = true;
                    board[i, j] = cell;
                    Board.Children.Add(cell);
                    Grid.SetColumn(cell, i);
                    Grid.SetRow(cell, j);
                }
            
            //создание фигур
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Image cell = new Image();

                    //черные фигур
                    if (j == 1) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_pawn.png", UriKind.Relative)); board[i, j].Name = "pawnBlack" + i; }
                    else if (j == 0 && (i == 0 || i == 7)) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_rook.png", UriKind.Relative)); board[i, j].Name = "rookBlack" + i; }
                    else if (j == 0 && (i == 1 || i == 6)) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_knight.png", UriKind.Relative)); board[i, j].Name = "knightBlack" + i; }
                    else if (j == 0 && (i == 2 || i == 5)) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_bishop.png", UriKind.Relative)); board[i, j].Name = "bishopBlack" + i; }
                    else if (j == 0 && i == 3) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_queen.png", UriKind.Relative)); board[i, j].Name = "queenBlack" + i; }
                    else if (j == 0 && i == 4) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_king.png", UriKind.Relative)); board[i, j].Name = "king" + i; }
                    // белые пешки
                    else if ((i == 2 || i == 5) && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_bishop.png", UriKind.Relative)); board[i, j].Name = "Whitebishop" + i; }
                    else { board[i, j].Name = "empty"+i+j; board[i, j].IsHitTestVisible = false; }

                    pieces[i, j] = cell;
                    cell.IsHitTestVisible = false; //делает фигуры прозрачными для мышки 
                    Board.Children.Add(cell);
                    Grid.SetColumn(cell, i);
                    Grid.SetRow(cell, j);
                }
            }
        }
        public int x, y;
        private void Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (pieces[i, j].Source == null) { board[i, j].Name = board[i, j].Name + $"{i}"; board[i, j].IsHitTestVisible = false; }
            {
                //закрашивает все возможные ходы
                #region 
                if (button.Name.Contains("White") && button.IsDefault)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            board[i, j].Background = board[i, j].BorderBrush;
                            board[i, j].Name = board[i, j].Name.Replace("host", "");
                            board[i, j].IsDefault = true;
                            board[i, j].IsHitTestVisible = true;
                        }

                    //все варианты хода наверх вправо
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Name == button.Name)
                            {
                                board[i, j].Name += "host";
                                x = i; y = j;
                                for (int p = i; p < 8; p++)
                                {
                                    for (int k = j; k > -1; k--)
                                    {
                                        if (k - 1 > -1 && p + 1 < 8 && Math.Abs(i - (k - 1)) == Math.Abs(j - (p + 1)) && (pieces[p + 1, k - 1].Source == null || board[p + 1, k - 1].Name.Contains("Black")))
                                        {
                                            board[p + 1, k - 1].Background = Brushes.Green;
                                            board[p + 1, k - 1].IsDefault = false;
                                            board[p + 1, k - 1].IsHitTestVisible = true;
                                            if (board[p + 1, k - 1].Name.Contains("Black"))
                                            {
                                                board[p + 1, k - 1].Background = Brushes.Green;
                                                board[p + 1, k - 1].IsDefault = false;
                                                board[p + 1, k - 1].IsHitTestVisible = true;
                                                break;
                                            }
                                            i++; j--;
                                        }
                                        else break;
                                    }
                                }
                            }
                        }
                    
                    //все варианты хода наверх влево
                    for (int i = 7; i > -1; i--)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Name == button.Name)
                            {
                                board[i, j].Name += "host";
                                x = i; y = j;
                                for (int p = i; p > -1; p--)
                                {
                                    for (int k = j; k > -1; k--)
                                    {
                                        {
                                            if (k - 1 > -1 && p - 1 > -1 && Math.Abs(i - (p - 1)) == Math.Abs(j - (k - 1)) && (pieces[p - 1, k - 1].Source == null || board[p - 1, k - 1].Name.Contains("Black")))
                                            {
                                                board[p - 1, k - 1].Background = Brushes.Green;
                                                board[p - 1, k - 1].IsDefault = false;
                                                board[p - 1, k - 1].IsHitTestVisible = true;
                                                if (board[p - 1, k - 1].Name.Contains("Black"))
                                                {
                                                    board[p - 1, k - 1].Background = Brushes.Green;
                                                    board[p - 1, k - 1].IsDefault = false;
                                                    board[p - 1, k - 1].IsHitTestVisible = true;
                                                    break;
                                                }
                                                i--; j--;
                                            }
                                            else break;
                                        }
                                    }
                                }
                            }
                        }

                    //все варианты хода вниз влево
                    for (int i = 7; i > -1; i--)
                        for (int j = 7; j > -1; j--)
                        {
                            if (board[i, j].Name == button.Name)
                            {
                                board[i, j].Name += "host";
                                x = i; y = j;
                                for (int k = j; k < 8; k++)
                                {
                                    for (int p = i; p > -1; p--)
                                    {
                                        {
                                            if (k + 1 < 8 && p - 1 > -1 && Math.Abs(i - (p - 1)) == Math.Abs(j - (k + 1)) && (pieces[p - 1, k + 1].Source == null || board[p - 1, k + 1].Name.Contains("Black")))
                                            {
                                                board[p - 1, k + 1].Background = Brushes.Green;
                                                board[p - 1, k + 1].IsDefault = false;
                                                board[p - 1, k + 1].IsHitTestVisible = true;
                                                if (board[p - 1, k + 1].Name.Contains("Black"))
                                                {
                                                    board[p - 1, k + 1].Background = Brushes.Green;
                                                    board[p - 1, k + 1].IsDefault = false;
                                                    board[p - 1, k + 1].IsHitTestVisible = true;
                                                    break;
                                                }
                                                i--; j++;
                                            }
                                            else break;
                                        }
                                    }
                                }
                            }
                        }

                    //все варианты хода вниз вправо
                    for (int i = 0; i < 8; i++)
                        for (int j = 7; j > -1; j--)
                        {
                            if (board[i, j].Name == button.Name)
                            {
                                board[i, j].Name += "host";
                                x = i;  y = j;
                                for (int k = j; k < 8; k++)
                                {
                                    for (int p = i; p < 8; p++)
                                    {
                                        if (k + 1 < 8 && p + 1 < 8 && Math.Abs(i - (p + 1)) == Math.Abs(j - (k + 1)) && (pieces[p + 1, k + 1].Source == null || board[p + 1, k + 1].Name.Contains("Black")))
                                        {
                                            board[p + 1, k + 1].Background = Brushes.Green;
                                            board[p + 1, k + 1].IsDefault = false;
                                            board[p + 1, k + 1].IsHitTestVisible = true;
                                            if (board[p + 1, k + 1].Name.Contains("Black"))
                                            {
                                                board[p + 1, k + 1].Background = Brushes.Green;
                                                board[p + 1, k + 1].IsDefault = false;
                                                board[p + 1, k + 1].IsHitTestVisible = true;
                                                break;
                                            }
                                            i++; j++;
                                        }
                                        else break;
                                    }
                                }
                            }
                        }
                }
                #endregion

                //при выборе желаемой ячейки, фигура перемещается
                #region
                else if (button.Background == Brushes.Green && button.IsDefault == false)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Name == button.Name && board[i, j].Background == Brushes.Green && board[x, y].Name.Contains("host"))
                            {
                                pieces[i, j].Source = pieces[x, y].Source;
                                pieces[x, y].Source = null;
                                board[x, y].IsDefault = true;
                                board[i, j].Name = board[x, y].Name;
                                board[x, y].Name = "empty" + i + j;
                                button.Background = button.BorderBrush;
                                board[i, j].IsHitTestVisible = true;
                                board[x, y].IsHitTestVisible = false;
                            }
                        }
                        for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            board[i, j].Background = board[i, j].BorderBrush;
                            button.IsDefault = true;
                        }
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (!board[i, j].Name.Contains("White"))
                            {

                                board[i, j].IsHitTestVisible = false;
                            }
                            board[i, j].Background = board[i, j].BorderBrush;
                        }
                }
                #endregion

                //возвращает изначальный цвет ячейки
                else
                {
                    button.IsDefault = true;
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Background == Brushes.Green)
                                board[i, j].Background = board[i, j].BorderBrush;
                            board[i, j].Name = board[i, j].Name.Replace("host", "");
                            board[i, j].IsDefault = true;

                        }
                }
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            CreateBoard();
        }
    }
}