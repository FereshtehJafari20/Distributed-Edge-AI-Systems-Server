using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Server
{
    class Program
    {
        
            private const int PORT = 12345;
            private static Dictionary<(int, int), int> occupancyData = new Dictionary<(int, int), int>();
            private static readonly object lockObj = new object();

            static void Main(string[] args)
            {
                TcpListener serverSocket = new TcpListener(IPAddress.Any, PORT);
                serverSocket.Start();
                Console.WriteLine("Server started. Waiting for connections...");

                while (true)
                {
                    TcpClient clientSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

                    Thread clientThread = new Thread(() => HandleClient(clientSocket));
                    clientThread.Start();
                }
            }

            static void HandleClient(TcpClient clientSocket)
            {
                NetworkStream networkStream = clientSocket.GetStream();
                BinaryFormatter formatter = new BinaryFormatter();

                while (clientSocket.Connected)
                {
                    try
                    {
                        object receivedData = formatter.Deserialize(networkStream);
                        if (receivedData is Dictionary<(int, int), int> occupancyRequest)
                        {
                            SendAggregatedOccupancyData(networkStream);
                        }
                        else if (receivedData is KeyValuePair<(int, int), int> occupancyDataPoint)
                        {
                            UpdateOccupancyData(occupancyDataPoint.Key, occupancyDataPoint.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        break;
                    }
                }

                clientSocket.Close();
                Console.WriteLine("Client disconnected.");
            }

            static void UpdateOccupancyData((int, int) coordinates, int value)
            {
                lock (lockObj)
                {
                    occupancyData[coordinates] = value;
                }
            }

            static void SendAggregatedOccupancyData(NetworkStream networkStream)
            {
                lock (lockObj)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(networkStream, occupancyData);
                }
            }
        
    }
}
