using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using FinalExam_PrabhkiratKaurChugh_8890008.Models;

namespace FinalExam_PrabhkiratKaurChugh_8890008.Backend
{
    interface Backend
    {
        public void create(Game game);
        public Game? read(string filename);
        public void update(string gameId, Move move);
        public void clear(string gameId);
    }

    public class BackendImpl : Backend
    {

        public void create(Game game)
        {
            string fileName = "../../../SavedFiles/" + game.Id + ".txt";
            //File.Create(fileName);
            using (var fileStream = new FileStream(fileName, FileMode.Create));
        }

        public Game? read(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    List<Move> moves = new List<Move>();

                    string fileRelativeName = Regex.Match(fileName, @"Game\d+-[a-zA-z]*").Value;
                    string gameId = fileRelativeName;
                    string gameMode = fileRelativeName.Split("-")[1];
                    GameMode mode;
                    Enum.TryParse(gameMode, out mode);
                    while (!reader.EndOfStream)
                    {
                        string[] values = reader.ReadLine().Split(",");
                        int moveId = values[1] != null ? int.Parse(values[1]) : -1;
                        string time = values[2];
                        int playerId = values[3] != null ? int.Parse(values[3]) : -1;
                        string playerType = values[4];
                        string playerSign = values[5];

                        int x = values[6] != null ? int.Parse(Regex.Match(values[6], @"\d+").Value) : -1;
                        int y = values[7] != null ? int.Parse(Regex.Match(values[7], @"\d+").Value) : -1;

                        Sign sign;
                        PlayerType player;
                        Enum.TryParse(playerSign, out sign);
                        Enum.TryParse(playerType, out player);

                        if (moveId != -1 && playerId != -1 && x != -1 && y != -1)
                        {
                            Player current = new Player(playerId, sign);
                            current.PlayerType = player;

                            Move move = new Move(moveId, x, y, current);
                            move.Time = time;
                            moves.Add(move);
                        }
                    }
                    Game game = new Game(gameId, mode, moves);
                    return game;
                }
            }
            catch
            {
                return null;
            }
        }


        public void update(string gameId, Move move)
        {
            StreamWriter writer = null;
            string fileName = "../../../SavedFiles/" + gameId + ".txt";
            string[] lines = { $"{gameId}, {move.MoveId}, {move.Time}, {move.Player.Id}, {move.Player.PlayerType}, {move.Player.Sign}, [{move.X}],[{move.Y}]" };

            try
            {
                writer = new StreamWriter(fileName, true);
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
            catch (IOException)
            {
                return;
            }
            catch (NullReferenceException)
            {
                return;
            }
            finally
            {
                writer.Close();
            }
            
        }


        public void clear(string gameId)
        {
            string filenName = gameId + ".txt";
            File.WriteAllText(filenName, string.Empty);
        }
    }
}
