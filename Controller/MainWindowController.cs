using System.Collections.Generic;
using System.IO;
using FinalExam_PrabhkiratKaurChugh_8890008.Backend;
using FinalExam_PrabhkiratKaurChugh_8890008.Frontend;
using FinalExam_PrabhkiratKaurChugh_8890008.Models;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Controller
{
    interface MainWindowController
    {
        void newGame(GameMode mode);
        void load(string fileName);
    }
    public class MainWindowControllerImpl : MainWindowController
    {
        BackendImpl backend = new BackendImpl();

        MainView? view;

        public MainWindowControllerImpl(MainView view)
        {
            this.view = view;
        }

        public void load(string fileName)
        {
            Game? game = backend.read(fileName);
            if(game != null)
            {
                view?.startGame(game);
            } else
            {
                view?.showError();
            }
        }

        public void newGame(GameMode mode)
        {
            string gameId = $"Game1-{mode}";
            int gameIdCounter = 1;
            while (File.Exists($"{gameId}.txt"))
            {
                gameIdCounter++;
                gameId = $"Game{gameIdCounter}-{mode}";
            }

            Game game = new Game(gameId, mode, new List<Move>());
            backend.create(game);
            view?.startGame(game);
            /*return game;*/
        }
    }
}