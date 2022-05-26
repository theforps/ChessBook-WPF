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

namespace RotTsar.Parties
{
    /// <summary>
    /// Логика взаимодействия для ItalianWindow.xaml
    /// </summary>
    public partial class ItalianWindow : Page
    {
        public ItalianWindow()
        {
            InitializeComponent();
            CreateBoard();
            pawn.Opacity = 100;
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
                    if (j == 1 && i != 4) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_pawn.png", UriKind.Relative)); board[i, j].Name = "pawnBlack" + i + j; }
                    else if (j == 2 && i == 4) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_pawn.png", UriKind.Relative)); board[i, j].Name = "pawnBlack" + i; }
                    else if (j == 0 && (i == 0 || i == 7)) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_rook.png", UriKind.Relative)); board[i, j].Name = "rookBlack" + i; }
                    else if (j == 0 && i == 6) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_knight.png", UriKind.Relative)); board[i, j].Name = "knightBlack" + i; }
                    else if (j == 2 && i == 2) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_knight.png", UriKind.Relative)); board[i, j].Name = "knightBlack" + i; }
                    else if (j == 0 && i == 2) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_bishop.png", UriKind.Relative)); board[i, j].Name = "bishopBlack" + i; }
                    else if (j == 3 && i == 2) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_bishop.png", UriKind.Relative)); board[i, j].Name = "bishopBlack" + i; }
                    else if (j == 0 && i == 3) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_queen.png", UriKind.Relative)); board[i, j].Name = "queenBlack" + i; }
                    else if (j == 0 && i == 4) { board[i, j].IsHitTestVisible = false; cell.Source = new BitmapImage(new Uri("Pieces/black_king.png", UriKind.Relative)); board[i, j].Name = "king" + i; }
                    // белые пешки
                    else if (i == 5 && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_bishop.png", UriKind.Relative)); board[i, j].Name = "Whitebishop" + i; board[i, j].IsHitTestVisible = false; }
                    else if (i == 2 && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_bishop.png", UriKind.Relative)); board[i, j].Name = "Whitebishop" + i; board[i, j].IsHitTestVisible = false; }
                    else if ((i == 0 || i == 7) && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_rook.png", UriKind.Relative)); board[i, j].Name = "Whiterook" + i; board[i, j].IsHitTestVisible = false; }
                    else if (i == 1 && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_knight.png", UriKind.Relative)); board[i, j].Name = "Whiteknight" + i; board[i, j].IsHitTestVisible = false; }
                    else if (i == 6 && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_knight.png", UriKind.Relative)); board[i, j].Name = "Whiteknight" + i; board[i, j].IsHitTestVisible = false; }
                    else if (i == 3 && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_queen.png", UriKind.Relative)); board[i, j].Name = "Whitequeen" + i; board[i, j].IsHitTestVisible = false; }
                    else if (i == 4 && j == 7) { cell.Source = new BitmapImage(new Uri("Pieces/white_king.png", UriKind.Relative)); board[i, j].Name = "Whiteking" + i; board[i, j].IsHitTestVisible = false; }
                    else if (j == 6 && i != 4) { cell.Source = new BitmapImage(new Uri("Pieces/white_pawn.png", UriKind.Relative)); board[i, j].Name = "pawnWhite" + i+j; board[i, j].IsHitTestVisible = false; }
                    else if (j == 6 && i == 4) { cell.Source = new BitmapImage(new Uri("Pieces/white_pawn.png", UriKind.Relative)); board[i, j].Name = "pawnWhite" + i;}
                    else { board[i, j].Name = "empty" + i + j; board[i, j].IsHitTestVisible = false; }

                    pieces[i, j] = cell;
                    cell.IsHitTestVisible = false; //делает фигуры прозрачными для мышки 
                    Board.Children.Add(cell);
                    Grid.SetColumn(cell, i);
                    Grid.SetRow(cell, j);
                }
            }
        }
        public int x, y;
        public int n = 0;
        
        private void Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (pieces[i, j].Source == null) { board[i, j].Name = board[i, j].Name + $"{i}"; board[i, j].IsHitTestVisible = false; }

            if (n == 0)
        {
                    //закрашивает все возможные ходы
                    #region
                    if (button.Name == "pawnWhite4" && button.IsDefault)
                    {
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                            {
                                board[i, j].Background = board[i, j].BorderBrush;
                                board[i, j].Name = board[i, j].Name.Replace("host", "");
                            }

                        board[4, 4].Name += "host";
                        board[4, 4].Background = Brushes.Green;
                        board[4, 4].Name = "empty" + 454;
                        board[4, 4].IsDefault = false;
                        board[4, 4].IsHitTestVisible = true;
                        pawn.Opacity = 0;
                        pawnStep.Opacity = 100;
                    }
                    #endregion
                    //при выборе желаемой ячейки, фигура перемещается
                    #region
                    else if (button.Background == Brushes.Green && button.IsDefault == false)
                    {

                        if (board[4, 4].Name == button.Name)
                        {
                            pieces[4, 4].Source = pieces[4, 4 + 2].Source;
                            pieces[4, 4 + 2].Source = null;
                            board[4, 4 + 2].IsDefault = true;
                            board[4, 4].Name = board[4, 4 + 1].Name;
                            board[4, 4 + 2].Name = "empty" + 5454;
                            button.Background = button.BorderBrush;
                            button.IsDefault = true;
                            board[4,4].IsHitTestVisible = false;
                            board[4,4+2].IsHitTestVisible = false;
                            board[6, 7].IsHitTestVisible = true;
                            pawnStep.Opacity = 0;
                        n++;
                        }

                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                            {
                                if (board[i, j].Name != "Whiteknight6")
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
            if (n == 1)
            {
                knight.Opacity = 100;
                if (button.Name.Contains("Whiteknight6") && button.IsDefault)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            board[i, j].Background = board[i, j].BorderBrush;
                            board[i, j].Name = board[i, j].Name.Replace("host", "");
                            board[i, j].IsDefault = true;
                            board[i, j].IsHitTestVisible = true;

                        }
                    if (board[6, 7].Name == button.Name)
                    {
                        board[6, 7].Name += "host";
                        board[5, 5].Background = Brushes.Green;
                        board[5, 5].IsDefault = false;
                        board[5, 5].IsHitTestVisible = true;
                        knight.Opacity = 0;
                        knightStep.Opacity = 100;
                    }
                }
                else if (button.Background == Brushes.Green && button.IsDefault == false)
                {
                    if (board[5, 5].Name == button.Name)
                    {
                        pieces[5, 5].Source = pieces[6, 7].Source;
                        pieces[6, 7].Source = null;
                        board[6, 7].IsDefault = true;
                        board[5, 5].Name = board[6, 7].Name;
                        board[6, 7].Name = "empty" + 6 + 7;
                        button.Background = button.BorderBrush;
                        button.IsDefault = true;
                        board[5, 5].IsHitTestVisible = false;
                        board[6, 7].IsHitTestVisible = false;
                        board[2, 4].IsHitTestVisible = true;
                        knightStep.Opacity = 0;
                        knight.Opacity = 0;
                        n++;
                    }
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            board[i, j].Background = board[i, j].BorderBrush;
                            board[i, j].IsDefault = true;
                        }
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Name != "Whitebishop5")
                            {
                                board[i, j].IsHitTestVisible = false;
                            }
                            board[i, j].Background = board[i, j].BorderBrush;
                        }
                }
                //возвращает изначальный цвет ячейки
                else
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Background == Brushes.Green)
                                board[i, j].Background = board[i, j].BorderBrush;
                            board[i, j].Name = board[i, j].Name.Replace("host", "");
                            board[i, j].IsDefault = true;
                            if (board[i, j].Name != "Whiteknight6")
                            {
                                board[i, j].IsHitTestVisible = false;
                            }
                        }
                }
                
            }
            if (n == 2)
            {
                bishop.Opacity = 100;
                //закрашивает все возможные ходы
                #region 
                if (button.Name == "Whitebishop5" && button.IsDefault)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            board[i, j].Background = board[i, j].BorderBrush;
                            board[i, j].Name = board[i, j].Name.Replace("host", "");
                            board[i, j].IsDefault = true;
                            board[i, j].IsHitTestVisible = true;
                        }
                    //все варианты хода наверх влево
                    for (int i = 7; i > -1; i--)
                        for (int j = 0; j < 8; j++)
                        {
                            if (board[i, j].Name == button.Name)
                            {
                                board[i, j].Name += "host";
                                x = i; y = j;
                                board[2, 4].Background = Brushes.Green;
                                board[2, 4].IsDefault = false;
                                board[2, 4].IsHitTestVisible = true;
                                bishop.Opacity = 0;
                                bishopStep.Opacity = 100;
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
                                board[i, j].IsHitTestVisible = false;
                                board[x, y].IsHitTestVisible = false;
                                bishop.Opacity = 0;
                                bishopStep.Opacity = 0;
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
                            
                                board[i, j].IsHitTestVisible = false;
                            
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
