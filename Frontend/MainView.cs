using FinalExam_PrabhkiratKaurChugh_8890008.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Frontend
{
    public interface MainView
    {
        void showError();
        void startGame(Game game);
    }
}
