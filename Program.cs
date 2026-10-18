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

    #region Metodo Principal
    //cambios de Christopher Agip - creación de la función para buscar por código
    public static void Main()
    {
        bool ejecutando = true;
        while (ejecutando)
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine("   SISTEMA DE GESTIÓN DE ENVÍOS   ");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Registrar nuevo envío");
            Console.WriteLine("2. Listar pedidos por estado");
            Console.WriteLine("3. Listar pedidos por método");
            Console.WriteLine("4. Buscar pedido por código");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción (1-5): ");

            string? opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    registrarpedido();
                    break;
                case "2":
                    ListarPorEstado();
                    break;
                case "3":
                    ListarPorMetodo();
                    break;
                case "4":
                    BuscarPorCodigo();
                    break;
                case "5":
                    Console.WriteLine("Saliendo del sistema...");
                    ejecutando = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }
    }
    #endregion

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
            metodos[cantidadpedidos] = "Agencia";
        }
        else
        {
            metodos[cantidadpedidos] = "Motorizado";
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

    static void ListarPorEstado()
    {
        Console.WriteLine("--- LISTADO POR ESTADO ---");
        string[] estadosPosibles = { "Registrado", "Enviado", "Entregado", "Recogido" };

        for (int e = 0; e < estadosPosibles.Length; e++)
        {
            bool hayRegistros = false;
            for (int i = 0; i < cantidadpedidos; i++)
            {
                if (estados[i] == estadosPosibles[e])
                {
                    if (!hayRegistros)
                    {
                        Console.WriteLine($"\n>> ESTADO: {estadosPosibles[e].ToUpper()} <<");
                        hayRegistros = true;
                    }
                    Console.WriteLine($"   {fechas[i]} - {codigos[i]} - {nombres[i]} ({metodos[i]})");
                }
            }
        }
    }

    //cambios de Christopher Agip - creación de la función listado por metodo de envio

    static void ListarPorMetodo()
    {
        Console.WriteLine("--- LISTADO POR MÉTODO ---");
        string[] metodosPosibles = { "Agencia", "Motorizado" };

        for (int m = 0; m < metodosPosibles.Length; m++)
        {
            bool hayRegistros = false;
            for (int i = 0; i < cantidadpedidos; i++)
            {
                if (metodos[i] == metodosPosibles[m])
                {
                    if (!hayRegistros)
                    {
                        Console.WriteLine($"\n>> MÉTODO: {metodosPosibles[m].ToUpper()} <<");
                        hayRegistros = true;
                    }
                    Console.WriteLine($"   {fechas[i]} - {codigos[i]} - {nombres[i]} - Estado: {estados[i]}");
                }
            }
        }
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