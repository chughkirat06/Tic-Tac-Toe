using Microsoft.VisualBasic;
using System;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Models
{
    public class Move
    {
        public Move(int id, int x, int y, Player player)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.player = player;

        }

        private int id;
        private int x;
        private int y;
        private Player player;

        private string time = DateTime.Now.TimeOfDay.ToString();

        public int MoveId { get => id; }
        public int X { get => x; }
        public int Y { get => y; }
        public Player Player { get => player; }
        public string Time { get => time; set => time = value; }
    }
}