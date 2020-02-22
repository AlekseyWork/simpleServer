using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
		static int port = 8005; 
        static void Main(string[] args)
        {

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
             
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
 
 
                listenSocket.Listen(10);
 
                Console.WriteLine("Server started listen connections...");
 
                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256]; 
 
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
						
                    }
                    while (handler.Available>0);
					
                    Console.WriteLine(DateTime.Now.ToLongTimeString() + ": " + builder.ToString());
 
                    string message = "\n" + builder.ToString();
                    data = Encoding.Unicode.GetBytes(message);
					handler.Send(data);
					
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
			
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
