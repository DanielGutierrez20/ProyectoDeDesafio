using System;             
public class Program
{
    static string[] codigos = new string[100];
    static string[] nombres = new string[100];
    static string[] celulares = new string[100];
    static string[] metodos = new string[100];
    static string[] direcciones = new string[100];
    static string[] fechas = new string[100];
    static string[] estados = new string[100];
    static int cantidadpedidos = 0;
    public static void Main()
    {
    }
    static void registrarpedido()
    {
        Console.WriteLine("--- REGISTRO DE NUEVO ENVÍO ---");
        if (cantidadpedidos >= 100)
        {
            Console.WriteLine("Error: Se alcanzó el límite máximo de 100 pedidos.");
            return;
        }
        Console.Write("Nombre del cliente: ");
        nombres[cantidadpedidos] = Console.ReadLine();
        Console.Write("Celular: ");
        celulares[cantidadpedidos] = Console.ReadLine();
        Console.Write("Metodo de envío (1.Agencia/2.Motorizado): ");
        string opcMetodo = Console.ReadLine();
        if (opcMetodo == "1")
        {
            metodos[cantidadpedidos] = "Agencia";
        }
        else
        {
            metodos[cantidadpedidos] = "Motorizado";
        }
        Console.Write("Dirección: ");
        direcciones[cantidadpedidos] = Console.ReadLine();
        Console.Write("Fecha de envío (DD-MM-AAAA): ");
        fechas[cantidadpedidos] = Console.ReadLine();
    }
}