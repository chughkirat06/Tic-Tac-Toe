using System.Collections.Generic;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Models
{
    public class Game
    {
        public Game(string id, GameMode mode, List<Move> moves)
        {
            this.id = id;
            this.mode = mode;
            this.moves = moves;
        }

        private string id;
        private GameMode mode;
        private List<Move> moves;

        public void play(Move move)
        {
            moves.Add(move);
        }

        public string Id { get => id; }
        public GameMode Mode { get => mode; }
        public List<Move> Moves { get => moves; }
    }
}