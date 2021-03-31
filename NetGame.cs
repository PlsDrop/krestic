using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace krestic
{
    class NetGame
    {
        private Game game = new Game();

        // public NetGame()
        // {
        //     StartGame();
        // }

        public string GetField()
        {
            String[] field = game.GetField();
            return $"{field[0]}|{field[1]}|{field[2]}\n" + $"—————\n{field[3]}|{field[4]}|{field[5]}\n—————\n{field[6]}|{field[7]}|{field[8]}\n";
        }

        public string CheckMove()
        {
            if (game.GetMoves() % 2 == 0)
            {
                return "X";
            }
            else
            {
                return "O";
            }
        }

        public bool MakeMove(String turn, int Y, int X)
        {
            game.InputXY(turn, Y, X);
            return game.GetError();
        }

        public string CheckStatus()
        {
            game.CheckStatus();
            return game.GetStatus();
        }
        
        
    }
}