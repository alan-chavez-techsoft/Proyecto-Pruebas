using Grpc.Net.Client;
using GRPC_Service;
using GRPCServer;

namespace GRPC_Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5161");
            var client1 = new Greeter.GreeterClient(channel);

            var reply1 = client1.SayHello(new HelloRequest { Name = "GRPC-Consola" });
            Console.WriteLine($"Request 1: {reply1.Message}");

            var client2 = new PruebaGRPC.PruebaGRPCClient(channel);

            var reply2 = client2.GetPrueba(new PruebaRequest { Name = "GRPC-Consola" });
            Console.WriteLine($"Request 2: {reply2.Message}");
        }
    }
}
