using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FinalExam_PrabhkiratKaurChugh_8890008.Controller;
using FinalExam_PrabhkiratKaurChugh_8890008.Frontend;
using FinalExam_PrabhkiratKaurChugh_8890008.Models;

namespace FinalExam_PrabhkiratKaurChugh_8890008
{
    public partial class Board : Window, BoardView
    {
        private Player player1 = new Player(1, Sign.X);
        private Player player2 = new Player(2, Sign.O);
        private Player player;
        private string[,] gameBoard = new string[3, 3];
        private bool gameOver = false;
        private Middleware middlewareController;
        private bool checkWin = false;

        public Board(Game game)
        {
            this.middlewareController = new MiddlewareController(game, this);

            InitializeComponent();

            middlewareController.play();
            newGame();
        }
        

        private void newGame()
        { 
            autoPlay();
        }


        private async void autoPlay()
        {
            while (player.shouldAutoPlay())
            {
                if (!CheckForWinnerOrTie())
                {
                    await Task.Delay(500);

                    // make computer move
                    int[] rowCols = MakeComputerMove();
                    middlewareController.playMove(rowCols[0], rowCols[1], player);

                    if (CheckForWinnerOrTie() && !checkWin)
                    {
                        checkWin = true;
                        break;
                    }
                    switchPlayer();
                } 
                else
                {
                    break;
                }
            }
        }


        private async void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver || middlewareController.isComputerVsComputer())
            {
                return;
            }
            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);

            gameBoard[row, column] = player.Sign.ToString();
            button.Content = player.Sign.ToString();

            middlewareController.playMove(row, column, player);

            switchPlayer();

            if (CheckForWinnerOrTie())
            {
                return;
            }

            while (player.shouldAutoPlay())
            {
                await Task.Delay(500);
                if (!CheckForWinnerOrTie())
                {
                    // make computer move
                    int[] rowCols = MakeComputerMove();
                    middlewareController.playMove(rowCols[0], rowCols[1], player);

                    if(CheckForWinnerOrTie() && !checkWin)
                    {
                        checkWin = true;
                        break;
                    }
                    switchPlayer();
                }
                break;
            }
        }


        private void switchPlayer()
        {
            player = player == player1 ? player2 : player1;
        }


        private int[] MakeComputerMove()
        {
            // create a list of available cells
            List<Button> availableCells = gB.Children
              .OfType<Button>()
              .Where(b => b.Content == null)
              .ToList();

            // if no available cells, return
            if (availableCells.Count == 0)
            {
                throw new InvalidProgramException("Game should be over");
            }

            // choose a random cell from the available cells and mark it with "O"
            string computerSymbol = player.Sign.ToString();
            int randomIndex = new Random().Next(availableCells.Count);
            availableCells[randomIndex].Content = computerSymbol;

            // update the game board
            int row = Grid.GetRow(availableCells[randomIndex]);
            int column = Grid.GetColumn(availableCells[randomIndex]);
            gameBoard[row, column] = computerSymbol;
            return new int[2] { row, column };
        }


        private bool CheckForWinnerOrTie()
        {
            int totalMoves = 0;
            for (int i = 0; i < 3; i++)
            {
                // check rows
                if (gameBoard[i, 0] != null && gameBoard[i, 0] == gameBoard[i, 1] && gameBoard[i, 0] == gameBoard[i, 2])
                {
                    gameOver = true;
                    MessageBox.Show($"Player {gameBoard[i, 0]} wins!");
                    //ResetGameBoard();
                    return true;
                }

                // check columns
                if (gameBoard[0, i] != null && gameBoard[0, i] == gameBoard[1, i] && gameBoard[0, i] == gameBoard[2, i])
                {
                    gameOver = true;
                    MessageBox.Show($"Player {gameBoard[0, i]} wins!");
                    //ResetGameBoard();
                    return true;
                }
            }

            // check diagonals
            if (gameBoard[0, 0] != null && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[0, 0] == gameBoard[2, 2])
            {
                gameOver = true;
                MessageBox.Show($"Player {gameBoard[0, 0]} wins!");
                //ResetGameBoard();
                return true;
            }
            if (gameBoard[0, 2] != null && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[0, 2] == gameBoard[2, 0])
            {
                gameOver = true;
                MessageBox.Show($"Player {gameBoard[0, 2]} wins!");
                //ResetGameBoard();
                return true;
            }

            // check for a tie
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i, j] != null)
                    {
                        totalMoves++;
                    }
                }
            }
            if (totalMoves == 9)
            {
                gameOver = true;
                MessageBox.Show("Tie game!");
                //ResetGameBoard();
                return true;
            }
            return false;
        }


        private void ResetGameBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    gameBoard[row, column] = null;
                    Button button = (Button)gB.Children
                        .Cast<UIElement>()
                        .First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                    button.Content = null;
                }
            }
            //File.WriteAllText($"{game.GameId}.txt", string.Empty);
            gameOver = false;
            player = player1;
            middlewareController.reset();
            autoPlay();
        }


        private async void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetGameBoard();
        }


        public string? LoadGame() 
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }



        private void UpdateUI()
        {
            // Update the UI with the current state of the board
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string buttonName = "cell" + i + j;
                    Button button = (Button)FindName(buttonName);
                    button.Content = gameBoard[i, j];
                }
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Close();
        }


        public void setBoard(int x, int y, string sign)
        {
            // check index out of range -- incorrect coordinate sign won't get displayed
            if (x < 0 || y < 0 || x > 2 || y > 2)
            {
                return;
            }
            gameBoard[x, y] = sign;
            UpdateUI();
            CheckForWinnerOrTie();
        }


        public void setPlayerTypes(PlayerType one, PlayerType two, Player? current)
        {
            player1.PlayerType = one;
            player2.PlayerType = two;

            if (current == null)
            {
                this.player = player1;
            } else 
            {
                this.player = current.Sign == player1.Sign ? player2 : player1; 
            }
        }
    }
}
