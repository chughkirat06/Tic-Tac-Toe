using System.Windows;
using FinalExam_PrabhkiratKaurChugh_8890008.Controller;
using FinalExam_PrabhkiratKaurChugh_8890008.Frontend;
using FinalExam_PrabhkiratKaurChugh_8890008.Models;
using Microsoft.VisualBasic;

namespace FinalExam_PrabhkiratKaurChugh_8890008
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, MainView
    {
        private MainWindowController controller;
        public MainWindow()
        {
            this.controller = new MainWindowControllerImpl(this);
            InitializeComponent();
        }

        private void UserVsUserButton(object sender, RoutedEventArgs e)
        {
            // Launch User vs User mode
            controller.newGame(GameMode.USER_VS_USER);
        }

        private void UserVsComputerButton(object sender, RoutedEventArgs e)
        {
            // Launch User vs Computer mode
            controller.newGame(GameMode.USER_VS_COMPUTER);
        }

        private void ComputerVsComputerButton(object sender, RoutedEventArgs e)
        {
            // Launch Computer vs Computer mode
           controller.newGame(GameMode.COMPUTER_VS_COMPUTER);
        }

        private void ResumeButton(object sender, RoutedEventArgs e)
        {
            string? filename = loadGame();
            if (filename == null) return;
            controller.load(filename);
        }

        private string? loadGame()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }

        public void showError()
        {
            MessageBox.Show("Incorrect file format.");
        }

        public void startGame(Game game)
        {
            Board board = new Board(game);
            board.Show();
            this.Close();
        }
    }
}
