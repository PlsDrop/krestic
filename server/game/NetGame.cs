using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace krestic.server.game
{
    class NetGame
    {
        private Game game;

        // public NetGame()
        // {
        //     StartGame();
        // }

        internal string GetField()
        {
            String[] field = game.GetField();
            return $"/////////////\n{field[0]}|{field[1]}|{field[2]}\n" + $"—————\n{field[3]}|{field[4]}|{field[5]}\n—————\n{field[6]}|{field[7]}|{field[8]}\n";
        }

        internal string CheckMove()
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

        internal bool MakeMove(String turn, int Y, int X)
        {
            game.InputXY(turn, Y, X);
            return game.GetError();
        }

        internal string CheckStatus()
        {
            game.CheckStatus();
            return game.GetStatus();
        }

        internal void NewGame()
        {
            game = new Game();
        }
        
        
    }
}