using System;
using System.IO;

public class Program
{
    static string carpetaDatos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datos");
    static string rutaArchivo = Path.Combine(carpetaDatos, "pedidos.txt");
    static string rutaUsuarios = Path.Combine(carpetaDatos, "usuarios.txt");

    public static void Main()
    {
        Console.WriteLine("Configuracion de rutas inicializada");
    }
}