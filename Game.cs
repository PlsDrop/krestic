using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace krestic
{
    class Game
    {
        public const string WIN = "win";
        public const string DRAW = "draw";
        public const string CONTINUE = "continue";

        private string[,] field = 
        { 
            { " ", " ", " " },
            { " ", " ", " " },
            { " ", " ", " " } 
        };
        private bool error = false;
        private string status = "";
        private int playersMoveCounter = 0;

        public string[] GetField()
        {
            return new string[] { field[0, 0], field[0, 1], field[0, 2], field[1, 0], field[1, 1], field[1, 2], field[2, 0], field[2, 1], field[2, 2] };
        }
        
        public int GetMoves()
        {
            return playersMoveCounter;
        }

        public string GetStatus()
        {
            return status;
        }

        public bool GetError()
        {
            return error;
        }


        public void InputXY(string turn, int Y, int X)
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

        public void CheckStatus()
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