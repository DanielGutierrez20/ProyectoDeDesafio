public class Program
{
    static string[] codigos = new string[100];
    static string[] nombres = new string[100];
    static int cantidadPedidos = 0;
    static void RegistrarPedido()
    {
        Console.WriteLine("--- REGISTRO DE NUEVO ENVÍO ---");
        if (cantidadPedidos >= 100)
        {
            Console.WriteLine("Error: Se alcanzó el límite máximo de 100 pedidos.");
            return;
        }
    }
}