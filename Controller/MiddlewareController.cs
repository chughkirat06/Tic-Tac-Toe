using System;
using System.Collections.Generic;
using FinalExam_PrabhkiratKaurChugh_8890008.Backend;
using FinalExam_PrabhkiratKaurChugh_8890008.Frontend;
using FinalExam_PrabhkiratKaurChugh_8890008.Models;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Controller
{

    public interface Middleware
    {
        bool isComputerVsComputer();
        public void play();
        public void playMove(int row, int column, Player player);
        public void reset();
    }

    public class MiddlewareController : Middleware
    {
        private Game game;

        private BackendImpl backend = new BackendImpl();
        BoardView? view;
        public MiddlewareController(Game game, BoardView view)
        {
            this.game = game;
            this.view = view;
        }

        public bool isComputerVsComputer()
        {
            return game.Mode == GameMode.COMPUTER_VS_COMPUTER;
        }

        public void play()
        {
            PlayerType one;
            PlayerType two;
            switch (game.Mode)
            {
                case GameMode.USER_VS_USER:
                    one = PlayerType.HUMAN;
                    two = PlayerType.HUMAN;
                    break;
                case GameMode.USER_VS_COMPUTER:
                    one = PlayerType.HUMAN;
                    two = PlayerType.COMPUTER;
                    break;
                case GameMode.COMPUTER_VS_COMPUTER:
                    one = PlayerType.COMPUTER;
                    two = PlayerType.COMPUTER;
                    break;
                default: throw new InvalidProgramException("Invalid play mode");
            }

            view?.setPlayerTypes(one, two, game.Moves.Count > 0 ? game.Moves[game.Moves.Count - 1].Player : null);

            foreach (Move move in game.Moves)
            {
                view?.setBoard(move.X, move.Y, move.Player.Sign.ToString());
            }


        }

        public void playMove(int row, int column, Player player)
        {
            int moveId = game.Moves.Count;
            Move move = new Move(moveId, row, column, player);
            game.play(move);
            backend.update(game.Id, move);
        }

        public void reset()
        {
            game = new Game(game.Id, game.Mode, new List<Move>());
            backend.clear(game.Id);
        }
    }
}
