using FinalExam_PrabhkiratKaurChugh_8890008.Models;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Frontend
{
    public interface BoardView
    {
        void setBoard(int x, int y, string sign);

        void setPlayerTypes(PlayerType one, PlayerType two, Player? current);
    }
}