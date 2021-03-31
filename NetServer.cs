using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace krestic
{
    class NetServer
    {
        byte[] messageToSend;
        Byte[] bytes = new Byte[256];
        TcpListener server=null;
        IPAddress localAddr = IPAddress.Parse("0.0.0.0");
        Int32 port = 13000;


        public void Start(NetGame netGame)
        {
            
            try
            {
            // Set the TcpListener on port 13000.
            

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            // Enter the listening loop.
            while(true)
            {
                Console.Write("Waiting for a connection player1... ");
                TcpClient client1 = server.AcceptTcpClient();
                Console.WriteLine(" connected!");

                Console.Write("Waiting for a connection player2... ");
                TcpClient client2 = server.AcceptTcpClient();
                Console.WriteLine(" connected!");
                



                // Get a stream object for reading and writing
                NetworkStream stream1 = client1.GetStream();
                NetworkStream stream2 = client2.GetStream();
                // StreamReader reader = new StreamReader(stream1);
                // StreamWriter writer = new StreamWriter(stream1);
                // writer.AutoFlush = true;

                WriteLine(netGame.GetField(), stream1);
                WriteLine(netGame.GetField(), stream2);
                int Y;
                int X;
                string status;
                NetworkStream currentStream;
                while (true)
                {
                    string turn = netGame.CheckMove(); 
                    if (turn == "X")
                        currentStream = stream1;
                    else
                        currentStream = stream2;

                    Y = ReadY(turn, currentStream);
                    X = ReadX(turn, currentStream);
                    
                    if (netGame.MakeMove(turn, Y, X))
                    {
                        WriteLine("Wrong cell!", currentStream);
                        continue;
                    }
                    else
                    {
                        WriteLine(netGame.GetField(), stream1);
                        WriteLine(netGame.GetField(), stream2);

                        status = netGame.CheckStatus();
                        if (status == Game.WIN)
                        {
                            WriteLine($"{turn} wins!", stream1);
                            WriteLine($"{turn} wins!", stream2);
                            break;
                        }
                        else if (status == Game.DRAW)
                        {
                            WriteLine("Draw!", stream1);
                            WriteLine("Draw!", stream2);
                            break;
                        }

                    }
                }
                

                // Shutdown and end connection
                client1.Close();
                client2.Close();
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

        private void WriteLine(string msg, NetworkStream stream)
        {
            messageToSend = System.Text.Encoding.ASCII.GetBytes(msg);
            stream.Write(messageToSend, 0, messageToSend.Length);
            
            //Console.WriteLine("Sent: {0}", msg);
        }

        private string ReadLine(NetworkStream stream)
        {
            int i;
            String data = null;
            while((i = stream.Read(bytes, 0, bytes.Length))!=0)
            {
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            //Console.WriteLine("Received: {0}", data);
            return data;
            }
            return "error";
        }





        private int ReadY(string turn, NetworkStream stream)
        {
            WriteLine($"-- PLAYER {turn} --\nInput Y:", stream);
            return Int32.Parse(ReadLine(stream));
        }

        private int ReadX(string turn, NetworkStream stream)
        {
            WriteLine($"-- PLAYER {turn} --\nInput X:", stream);
            return Int32.Parse(ReadLine(stream));
        }
    }
}