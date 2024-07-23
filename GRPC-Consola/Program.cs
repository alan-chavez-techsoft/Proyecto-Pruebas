using Grpc.Net.Client;
using GRPC_Service;

namespace GRPC_Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5242");
            var client = new Greeter.GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest { Name = "GRPC-Consola" });
            Console.WriteLine(reply.Message);
        }
    }
}
