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
    static void AsegurarArchivos()
    {
        try
        {
            if (!Directory.Exists(carpetaDatos)) Directory.CreateDirectory(carpetaDatos);
            if (!File.Exists(rutaUsuarios)) File.Create(rutaUsuarios).Close();
            if (!File.Exists(rutaArchivo)) File.Create(rutaArchivo).Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Advertencia al verificar archivos: " + ex.Message);
        }
    }
    public static void Main()
    {
        AsegurarArchivos();
        Console.WriteLine("Sistema listo para integrar los módulos del equipo");
    }
}