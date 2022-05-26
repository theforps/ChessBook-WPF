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

namespace RotTsar
{
    public partial class PawnWindow : Page
    {
        public PawnWindow()
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
                    if (j ==2) { cell.Source = new BitmapImage(new Uri("Pieces/black_pawn.png", UriKind.Relative)); board[i, j].Name = "pawnBlack" + i;
                        board[i, j].IsHitTestVisible = false; }
                    // белые пешки
                    else if (j == 6) { cell.Source = new BitmapImage(new Uri("Pieces/white_pawn.png", UriKind.Relative)); board[i, j].Name = "pawnWhite" + i; }
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
                        }

                        for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {   
                            //если спереди свободно то закрашивает
                            if (j > 0 && board[i, j].Name == button.Name && pieces[i, j - 1].Source == null)
                            {
                                board[i, j].Name += "host";
                                board[i, j - 1].Background = Brushes.Green;
                                board[i, j - 1].Name = "empty" + i;
                                board[i, j - 1].IsDefault = false;
                                board[i, j - 1].IsHitTestVisible = true;
                                x = i; y = j;
                            }
                            if (j == 6 && board[i, j].Name == button.Name && pieces[i, j - 2].Source == null)
                            {
                                board[i, j].Name += "host";
                                board[i, j - 2].Background = Brushes.Green;
                                board[i, j - 2].Name = "empty" + i;
                                board[i, j - 2].IsDefault = false;
                                board[i, j - 2].IsHitTestVisible = true;
                                x = i; y = j;
                            }
                            //если справа враг то закрашивает
                            if (j > 0 && i < 7  && board[i, j].Name == button.Name && board[i + 1, j - 1].Name.Contains("Black"))
                            {
                                board[i, j].Name += "host";
                                board[i + 1, j - 1].Background = Brushes.Green;
                                board[i + 1, j - 1].IsDefault = false;
                                board[i + 1, j - 1].IsHitTestVisible = true;
                                x = i; y = j;
                            }
                            //если слева враг то закрашивает
                            if (j > 0 && i > 0 && board[i, j].Name == button.Name && board[i -1, j - 1].Name.Contains("Black"))
                            {
                                board[i, j].Name += "host";
                                board[i-1, j - 1].Background = Brushes.Green;
                                board[i-1, j - 1].IsDefault = false;
                                board[i - 1, j - 1].IsHitTestVisible = true;
                                x = i; y = j;
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
                        {   //перемещение вперед
                            if (board[i, j].Name == button.Name && pieces[i, j].Source == null && j<7)
                            {
                                pieces[i, j].Source = pieces[i, j+1].Source;
                                pieces[i, j+1].Source = null;
                                board[i, j + 1].IsDefault = true;
                                board[i, j].Name = board[i, j+1].Name;
                                board[i, j+1].Name = "empty"+i;
                                board[i, j].IsHitTestVisible = true;
                                board[i, j+1].IsHitTestVisible = false;
                                button.Background = button.BorderBrush;
                                button.IsDefault = true; 
                            }
                            //перемещение вперед первый ход
                            if (board[i, j].Name == button.Name && pieces[i, j].Source == null && j == 4)
                            {
                                pieces[i, j].Source = pieces[i, j + 2].Source;
                                pieces[i, j + 2].Source = null;
                                board[i, j + 2].IsDefault = true;
                                board[i, j].Name = board[i, j + 2].Name;
                                board[i, j + 2].Name = "empty" + i;
                                board[i, j].IsHitTestVisible = true;
                                board[i, j + 2].IsHitTestVisible = false;
                                button.Background = button.BorderBrush;
                                button.IsDefault = true;
                            }
                            //перемещение влево
                            if (j > 0 && i < 7 && board[i, j].Name == button.Name && board[i, j].Background == Brushes.Green && board[i+1,j+1].Name.Contains("host"))
                            {
                                pieces[i, j].Source = pieces[i+1, j + 1].Source;
                                pieces[i+1, j + 1].Source = null;
                                board[i+1, j + 1].IsDefault = true;
                                board[i, j].Name = board[i+1, j + 1].Name;
                                board[i+1, j + 1].Name = "empty" + i;
                                board[i, j].IsHitTestVisible = true;
                                board[i+1, j + 1].IsHitTestVisible = false;
                                button.Background = button.BorderBrush;
                                button.IsDefault = true;
                                
                            }
                            //перемещение вправо
                            if (j > 0 && i > 0 && board[i, j].Name == button.Name && board[i, j].Background == Brushes.Green && board[i - 1, j + 1].Name.Contains("host"))
                            {
                                pieces[i, j].Source = pieces[i - 1, j + 1].Source;
                                pieces[i - 1, j + 1].Source = null;
                                board[i - 1, j + 1].IsDefault = true;
                                board[i, j].Name = board[i - 1, j + 1].Name;
                                board[i - 1, j + 1].Name = "empty" + i;
                                board[i, j].IsHitTestVisible = true;
                                board[i-1, j + 1].IsHitTestVisible = false;
                                button.Background = button.BorderBrush;
                                button.IsDefault = true;
                            }
                        }
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        { if (!board[i, j].Name.Contains("White"))
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
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Background == Brushes.Green)
                            board[i, j].Background = board[i, j].BorderBrush;
                            board[i, j].Name = board[i, j].Name.Replace("host", "");
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