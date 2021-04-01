using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using krestic.server.game;

namespace krestic.server
{
    class NetServer
    {
        TcpListener server=null;
        IPAddress localAddr = IPAddress.Parse("0.0.0.0");
        Int32 port = 13000;
        NetGame netGame = new NetGame();

        public NetServer(string[] array)
        {
            localAddr = IPAddress.Parse(array[0]);
            port = Int32.Parse(array[1]);
        }
        public void Start()
        {
            try
            {
            server = new TcpListener(localAddr, port);
            server.Start();

            // Enter the listening loop.
            while(true)
            {
                Console.Write("Waiting for a connection player1... ");
                NetPlayer player1 = new NetPlayer(server.AcceptTcpClient());
                Console.WriteLine(" connected!");

                Console.Write("Waiting for a connection player2... ");
                NetPlayer player2 = new NetPlayer(server.AcceptTcpClient());
                Console.WriteLine(" connected!");
                
                // StreamReader reader = new StreamReader(stream1);
                // StreamWriter writer = new StreamWriter(stream1);
                // writer.AutoFlush = true;
                netGame.NewGame();

                player1.WriteLine(netGame.GetField());
                player2.WriteLine(netGame.GetField());
                int Y;
                int X;
                string status;
                NetPlayer currentPlayer;
                while (true)
                {
                    
                    string turn = netGame.CheckMove(); 
                    
                    if (turn == "X")
                        currentPlayer = player1;
                    else
                        currentPlayer = player2;
                    
                    if (currentPlayer.CheckDataAvalible())
                        currentPlayer.ReadLine();

                    currentPlayer.WriteLine("YOUR TURN!\n");
                    Y = currentPlayer.ParseY();
                    X = currentPlayer.ParseX();

                    
                    if ((Y > 3 || Y < 1) || (X > 3 || X < 1) || netGame.MakeMove(turn, Y, X))
                    {
                        currentPlayer.WriteLine("Wrong coordinate!\n");
                        continue;
                    }
                    else
                    {
                        player1.WriteLine(netGame.GetField());
                        player2.WriteLine(netGame.GetField());

                        status = netGame.CheckStatus();
                        if (status == Game.WIN)
                        {
                            player1.WriteLine($"{turn} wins!");
                            player2.WriteLine($"{turn} wins!");
                            break;
                        }
                        else if (status == Game.DRAW)
                        {
                            player1.WriteLine("Draw!");
                            player2.WriteLine("Draw!");
                            break;
                        }
                    }
                }
                // Shutdown and end connection
                player1.Close();
                player2.Close();
            }
            }
            catch(SocketException e)
            {
            Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
            // Stop listening for new clients.
            server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}