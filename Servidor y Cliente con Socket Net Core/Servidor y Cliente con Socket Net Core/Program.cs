using System.Net; 
using System.Net.Sockets;
using System.Text;

namespace Servidor_y_Cliente_con_Sockets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            server();
        }

        //Creacion de Metodo
        public static void server()
        {
            //Configuraciones del SERVIDOR

            //declaracion de variables     //localhost => 127.0.0.1
            IPHostEntry host = Dns.GetHostEntry("localhost");

            IPAddress ipAddres = host.AddressList[0];//le indicamos con que direccion vamos a utilizar (Recibe la Conexion)

            //le asignamos una ip y un puerto(Recibe los datos que se estan enviando)
            IPEndPoint localEndPoint = new IPEndPoint(ipAddres, 11200);


            try
            {
                //Creacion del socket que esta escuhando
                Socket listener = new Socket(ipAddres.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                //Unir el EndPoint al Socket
                listener.Bind(localEndPoint);//se le indica el punto final del sector al que llegaran los datos
                                             //
                                             //Cantidad de Conexiones Permitidas
                listener.Listen(10);//Cantidad de conexion que puede recibir antes de indicar que el servidor esta ocupado

                Console.WriteLine("Esperando Conexion");//mensaje en panalla

                //Recibe una conexion y se la entrega al Socket para que la maneje
                Socket handler = listener.Accept();//se encargara de manejar los datos que envie el cliente

                //variables para recibir lo que esta enviando el cliente
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    //Recibe datos desde el cliente
                    int byteRec = handler.Receive(bytes);

                    //Convierte los datos desde bytes a string
                    data += Encoding.ASCII.GetString(bytes, 0, byteRec);
                    /*
                        hola a todos indexof(a) = 3
                        hola a todos indexof(todos) = 7
                        hola a todos indexof(w) = -1 
                    */
                    if (data.IndexOf("<EOF>") > -1) //Verifica cuando el cliente dejo de enviar datos
                        break;

                }
                //Mostrar el mensaje del cliente en pantalla
                Console.WriteLine("Texto del Cliente: " + data);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
