using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace krestic.server.game
{
    class Game
    {
        internal const string WIN = "win";
        internal const string DRAW = "draw";
        internal const string CONTINUE = "continue";

        private string[,] field = 
        { 
            { " ", " ", " " },
            { " ", " ", " " },
            { " ", " ", " " } 
        };
        private bool error = false;
        private string status = "";
        private int playersMoveCounter = 0;

        internal string[] GetField()
        {
            return new string[] { field[0, 0], field[0, 1], field[0, 2], field[1, 0], field[1, 1], field[1, 2], field[2, 0], field[2, 1], field[2, 2] };
        }
        
        internal int GetMoves()
        {
            return playersMoveCounter;
        }

        internal string GetStatus()
        {
            return status;
        }

        internal bool GetError()
        {
            return error;
        }


        internal void InputXY(string turn, int Y, int X)
        {
            X = X - 1;
            Y = Y - 1;

            if (field[Y, X] == " ")
            {
                if (turn == "X")
                {
                    field[Y, X] = "X";
                    playersMoveCounter++;
                    error = false;
                    return;
                }
                else if (turn == "0" || turn == "O")
                {
                    field[Y, X] = "O";
                    playersMoveCounter++;
                    error = false;
                    return;
                }
            }
            error = true;
            return;
        }

        internal void CheckStatus()
        {
            //horisontal check
            if (((field[0, 0] == field[0, 1]) && (field[0, 1] == field[0, 2])) && (field[0, 0] != " "))
            {
                status = WIN;
                return;
            }
            else if (((field[1, 0] == field[1, 1]) && (field[1, 1] == field[1, 2])) && (field[1, 0] != " "))
            {
                status = WIN;
                return;
            }
            else if (((field[2, 0] == field[2, 1]) && (field[2, 1] == field[2, 2])) && (field[2, 0] != " "))
            {
                status = WIN;
                return;
            }

            //vertical check
            else if (((field[0, 0] == field[1, 0]) && (field[1, 0] == field[2, 0])) && (field[0, 0] != " "))
            {
                status = WIN;
                return;
            }
            else if (((field[0, 1] == field[1, 1]) && (field[1, 1] == field[2, 1])) && (field[0, 1] != " "))
            {
                status = WIN;
                return;
            }
            else if (((field[0, 2] == field[1, 2]) && (field[1, 2] == field[2, 2])) && (field[0, 2] != " "))
            {
                status = WIN;
                return;
            }

            //diagonal check
            else if (((field[0, 0] == field[1, 1]) && (field[1, 1] == field[2, 2])) && (field[0, 0] != " "))
            {
                status = WIN;
                return;
            }
            else if (((field[2, 0] == field[1, 1]) && (field[1, 1] == field[0, 2])) && (field[2, 0] != " "))
            {
                status = WIN;
                return;
            }

            //continue check
            else
                foreach (string elem in field)
                {
                    if (elem == " ")
                    {
                        status = CONTINUE;
                        return;
                    }
                }

            //draw
            status = DRAW;
            return;
        }
    }
}