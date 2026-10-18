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
        Console.WriteLine("--- REGISTRO DE NUEVO ENVIO ---");   
        if (cantidadpedidos >= 100)
        {
            Console.WriteLine("error: Se alcanzo el limite maximo de 100 pedidos.");
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
            metodos[cantidadpedidos] = "agencia";
        }
        else
        {
            metodos[cantidadpedidos] = "motorizado";
        }
        Console.Write("Dirección: ");
        direcciones[cantidadpedidos] = Console.ReadLine();
        Console.Write("Fecha de envío DD-MM-AAAA: ");
        fechas[cantidadpedidos] = Console.ReadLine();
        codigos[cantidadpedidos] = "ENV" + (cantidadpedidos + 1).ToString("D3");
        estados[cantidadpedidos] = "Registrado";
        Console.WriteLine($"Pedido registrado CÓDIGO UNICO: {codigos[cantidadpedidos]}");
        cantidadpedidos++;
    }

    //cambios de Daniel Gutiérrez - creación de la función para buscar por código
    static void BuscarPorCodigo()
    {
        Console.WriteLine("--- BUSCAR POR CÓDIGO ---");
        Console.Write("Ingrese el código: ");
        string codigoBuscado = Console.ReadLine()?.Trim() ?? "";
        bool encontrado = false;

        for (int i = 0; i < cantidadpedidos; i++)
        {
            if (string.Equals(codigos[i], codigoBuscado, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(codigos[i], "ENV-" + codigoBuscado.PadLeft(3, '0'), StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n--- RESUMEN ---");
                Console.WriteLine($"Código: {codigos[i]}");
                Console.WriteLine($"Cliente: {nombres[i]} | Celular: {celulares[i]}");
                Console.WriteLine($"Método: {metodos[i]} | Dirección: {direcciones[i]}");
                Console.WriteLine($"Fecha: {fechas[i]} | ESTADO: {estados[i]}");
                encontrado = true;
                break;
            }
        }
        if (!encontrado) Console.WriteLine("Código no encontrado.");
    }
}