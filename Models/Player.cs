using System.Runtime.Serialization;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Models
{
    public class Player
    {
        public Player(int id, Sign sign)
        {
            this.id = id;
            this.sign = sign;
        }
        private int id;
        private PlayerType playerType;
        private Sign sign;

        public bool shouldAutoPlay()
        {
            return playerType == PlayerType.COMPUTER;
        }

        public int Id { get => id; set => id = value; }
        public PlayerType PlayerType { get => playerType; set => playerType = value; }
        public Sign Sign { get => sign; }

    }
}