using System;
using System.Collections.Generic; 

public static class CotizadorEnvios
{

    // Tarifas por distrito de Lima y Callao : Josemaria Roque
    private static readonly Dictionary<string, double> tarifasDistritos = new Dictionary<string, double>
    {
        { "El Agustino", 8.00 },
        { "Santa Anita", 9.00 },
        { "San Luis", 9.00 },
        { "La Victoria", 10.00 },
        { "Cercado de Lima", 10.00 },
        { "Rimac", 10.00 },
        { "Breña", 11.00 },
        { "San Borja", 11.00 },
        { "Lince", 11.00 },
        { "Jesus Maria", 12.00 },
        { "Pueblo Libre", 12.00 },
        { "San Isidro", 12.00 },
        { "Surquillo", 12.00 },
        { "Miraflores", 13.00 },
        { "Magdalena del Mar", 13.00 },
        { "San Miguel", 13.00 },
        { "Santiago de Surco", 13.00 },
        { "La Molina", 13.00 },
        { "Barranco", 14.00 },
        { "Ate", 12.00 },
        { "San Juan de Lurigancho", 12.00 },
        { "Chaclacayo", 18.00 },
        { "Chosica", 22.00 },
        { "Cieneguilla", 22.00 },
        { "Independencia", 14.00 },
        { "San Martin de Porres", 14.00 },
        { "Los Olivos", 15.00 },
        { "Comas", 16.00 },
        { "Carabayllo", 18.00 },
        { "Puente Piedra", 20.00 },
        { "Chorrillos", 15.00 },
        { "San Juan de Miraflores", 15.00 },
        { "Villa Maria del Triunfo", 17.00 },
        { "Villa El Salvador", 18.00 },
        { "Lurin", 22.00 },
        { "Callao", 16.00 },
        { "Bellavista", 15.00 },
        { "La Perla", 15.00 },
        { "Carmen de la Legua", 15.00 },
        { "Ventanilla", 22.00 }
    };


    public static void MostrarCotizador()
    {
        bool enCotizador = true;

        while (enCotizador)
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine("       COTIZADOR DE ENVÍOS        ");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Envío por Motorizado");
            Console.WriteLine("2. Envío por Agencia");
            Console.WriteLine("3. Volver al menú principal");
            Console.Write("Seleccione modalidad (1-3): ");

            string opcion = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    CotizarMotorizado();
                    break;
                case "2":
                    Console.WriteLine("Agencia: pendiente de implementación.");
                    break;
                case "3":
                    enCotizador = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    private static void CotizarMotorizado()
    {
        Console.WriteLine("--- COTIZACIÓN: MOTORIZADO (LIMA) ---");
        Console.Write("Ingrese el distrito de destino: ");
        string distritoIngresado = (Console.ReadLine() ?? "").Trim();

        string? distritoEncontrado = null;
        double tarifaDistrito = 0;

        foreach (var item in tarifasDistritos)
        {
            if (string.Equals(item.Key, distritoIngresado, StringComparison.OrdinalIgnoreCase))
            {
                distritoEncontrado = item.Key;
                tarifaDistrito = item.Value;
                break;
            }
        }

        if (distritoEncontrado == null)
        {
            Console.WriteLine($"\nLo sentimos, no contamos con cobertura en el distrito '{distritoIngresado}'.");
            return;
        }

        Console.WriteLine("\n--- RESUMEN DE COTIZACIÓN ---");
        Console.WriteLine("Modalidad:       Motorizado");
        Console.WriteLine($"Distrito:        {distritoEncontrado}");
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"TOTAL ESTIMADO:  S/ {tarifaDistrito:F2}");
    }
}