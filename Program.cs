using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using krestic.server;

namespace krestic
{
    class Program
    {
        static void Main(string[] array)
        {
            
            NetServer gameServer = new NetServer(array);
            gameServer.Start();
            






            // Game game1 = new Game();
            // int Y;
            // int X;

            // string[] field = game1.GetField();
            // Console.WriteLine($"{field[0]}|{field[1]}|{field[2]}\n" + $"—————\n{field[3]}|{field[4]}|{field[5]}\n—————\n{field[6]}|{field[7]}|{field[8]}");

            // while (true)
            // {
            //     if (game1.GetMoves() % 2 == 0)
            //     {
            //         Console.WriteLine("-- PLAYER X --\nInput Y:");
            //         Y = Convert.ToInt16(Console.ReadLine());

            //         Console.WriteLine("Input X:");
            //         X = Convert.ToInt16(Console.ReadLine());

            //         game1.InputXY("X", Y, X);
            //     }
            //     else
            //     {
            //         Console.WriteLine("-- PLAYER O --\nInput Y:");
            //         Y = Convert.ToInt16(Console.ReadLine());

            //         Console.WriteLine("Input X:");
            //         X = Convert.ToInt16(Console.ReadLine());

            //         game1.InputXY("O", Y, X);
            //     }

            //     if (game1.GetError() == true)
            //     {
            //         Console.WriteLine("Клетка занята!");
            //         continue;
            //     }
            //     else
            //     {
            //         field = game1.GetField();
            //         Console.WriteLine($"{field[0]}|{field[1]}|{field[2]}\n—————\n{field[3]}|{field[4]}|{field[5]}\n—————\n{field[6]}|{field[7]}|{field[8]}");

            //         game1.CheckStatus();

            //         if (game1.GetStatus() == Game.WIN)
            //         {
            //             if (game1.GetMoves() % 2 == 1)
            //                 Console.WriteLine("X win!!");
            //             else
            //                 Console.WriteLine("O win!!");
            //             break;
            //         }
            //         else if (game1.GetStatus() == Game.DRAW)
            //         {
            //             Console.WriteLine("Draw!!");
            //             break;
            //         }
            //     }
            // }
        }

    }
}
