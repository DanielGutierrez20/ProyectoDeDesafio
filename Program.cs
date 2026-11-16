using System;
using System.IO;
public class Program
{
    static string carpetaDatos = ObtenerRutaCarpetaDatos();
    static string rutaArchivo = Path.Combine(carpetaDatos, "pedidos.txt");
    static string rutaUsuarios = Path.Combine(carpetaDatos, "usuarios.txt");
    static string ObtenerRutaCarpetaDatos()
    {
        string dir = AppDomain.CurrentDomain.BaseDirectory;
        DirectoryInfo? info = new DirectoryInfo(dir);
        while (info != null)
        {
            if (File.Exists(Path.Combine(info.FullName, "mesaDePartes.csproj")) ||
                Directory.GetFiles(info.FullName, "*.sln").Length > 0)
            {
                return Path.Combine(info.FullName, "datos");
            }
            info = info.Parent;
        }
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datos");
    }
    public static void Main()
    {
        Console.WriteLine("Ruta de carpeta datos configurada correctamente");
    }
}