using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCliente253
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Configuracion para conectarse con el servidor
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0]; 
            IPEndPoint  remoteEP = new IPEndPoint(ipAddress, 11200);

            try
            {
                //Se crea un socket para enviar datos 
                Socket sender = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

                //Socket le indicamos conectarse con el servidor
                sender.Connect(remoteEP);

                //Mensaje de confirmacion
                Console.WriteLine("Conectado con el servidor");

                //Solicitamos datos al usuario(En este caso texto) para enviar al servidor
                Console.WriteLine("Ingrese un texto para enviar");
                string texto = Console.ReadLine();

                //Convierte el texto en un arreglo de bytes 
                byte[] msg = Encoding.ASCII.GetBytes(texto + "<EOF>");
                
                //Enviar al servidor el mensaje
                int byteSent = sender.Send(msg);  

                //Cierra la conexion con el sevidor
                sender.Shutdown(SocketShutdown.Both);
                sender.Close(); 
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
