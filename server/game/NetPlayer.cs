using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace krestic.server.game
{
    class NetPlayer
    {
        TcpClient playerClient;
        public NetworkStream playerStream;
        Byte[] bytes = new Byte[256];


        public NetPlayer(TcpClient client)
        {
            playerClient = client;
            playerStream = playerClient.GetStream();
        }
        
        internal bool CheckDataAvalible()
        {
            return playerStream.DataAvailable;
        }


        internal void WriteLine(string msg)
        {
            
            byte[] messageToSend = System.Text.Encoding.UTF8.GetBytes(msg);
            playerStream.Write(messageToSend, 0, messageToSend.Length);
            
            //Console.WriteLine("Sent: {0}", msg);
        }

        internal string ReadLine()
        {
            int i;
            String data = null;
            while((i = playerStream.Read(bytes, 0, bytes.Length))!=0)
            {
            data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
            //Console.WriteLine("Received: {0}", data);
            return data;
            }
            return "error";
        }

        internal int ParseY()
        {
            WriteLine("Input Y:");
            return Int32.Parse(ReadLine());
        }

        internal int ParseX()
        {
            WriteLine("Input X:");
            return Int32.Parse(ReadLine());
        }
        
        internal void Close()
        {
            playerClient.Close();
        }
    } 
}