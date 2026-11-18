
using System;
using System.Globalization;

public class Validaciones
{
    public static string ValidarNombre()
    {
        string nombre;

        do
        {
            Console.Write("Nombre del cliente: ");
            nombre = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Error: El nombre no puede estar vacío.");
            }

        } while (string.IsNullOrWhiteSpace(nombre));

        return nombre.Trim();
    }

    public static string ValidarCelular()
    {
        string celular;

        do
        {
            Console.Write("Celular: ");
            celular = Console.ReadLine() ?? "";

            bool valido = celular.Length == 9;

            foreach (char digito in celular)
            {
                if (digito < '0' || digito > '9')
                {
                    valido = false;
                    break;
                }
            }

            if (!valido)
            {
                Console.WriteLine("Error: El celular debe tener exactamente 9 dígitos.");
            }

        } while (celular.Length != 9 || !EsNumero(celular));

        return celular;
    }

    public static string ValidarMetodo()
    {
        string opcion;

        do
        {
            Console.Write("Método de envío (1.Agencia/2.Motorizado): ");
            opcion = Console.ReadLine() ?? "";

            if (opcion != "1" && opcion != "2")
            {
                Console.WriteLine("Error: Seleccione 1 o 2.");
            }

        } while (opcion != "1" && opcion != "2");

        return opcion == "1" ? "Agencia" : "Motorizado";
    }

    public static string ValidarDireccion()
    {
        string direccion;

        do
        {
            Console.Write("Dirección: ");
            direccion = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(direccion))
            {
                Console.WriteLine("Error: La dirección no puede estar vacía.");
            }

        } while (string.IsNullOrWhiteSpace(direccion));

        return direccion.Trim();
    }

    public static string ValidarFecha()
    {
        string fecha;
        DateTime fechaValida;

        do
        {
            Console.Write("Fecha de envío DD-MM-AAAA: ");
            fecha = Console.ReadLine() ?? "";

            if (!DateTime.TryParseExact(
                fecha,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechaValida))
            {
                Console.WriteLine("Error: Ingrese una fecha válida (DD-MM-AAAA).");
            }

        } while (!DateTime.TryParseExact(
            fecha,
            "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out fechaValida));

        return fecha;
    }

    private static bool EsNumero(string texto)
    {
        foreach (char digito in texto)
        {
            if (digito < '0' || digito > '9')
                return false;
        }

        return true;
    }
}
