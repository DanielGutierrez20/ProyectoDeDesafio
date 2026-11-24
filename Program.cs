using System; 
using System.IO;            
public class Program
{
    //Commit 1: Rutas dinamicas
    static string carpetaDatos = ObtenerRutaCarpetaDatos();
    static string rutaArchivo = Path.Combine(carpetaDatos, "pedidos.txt");
    static string rutaUsuarios = Path.Combine(carpetaDatos, "usuarios.txt");
    static string carpetaRespaldos = Path.Combine(carpetaDatos, "respaldos");
    static string[] codigos = new string[100];
    static string[] nombres = new string[100];
    static string[] celulares = new string[100];
    static string[] metodos = new string[100];
    static string[] direcciones = new string[100];
    static string[] fechas = new string[100];
    static string[] estados = new string[100];
    static string[] dnis = new string[100];
    static string[] detalles = new string[100];
    static int cantidadpedidos = 0;

    // Commit 2: ubicar la raíz del proyecto
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
    // Commit 3: crear carpeta y archivos si fueron borrados
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

    #region Metodo Principal
    //cambios de Christopher Agip - creación de la función para buscar por código
    public static void Main()
    {
        AsegurarArchivos();
        CargarDatos(); //Carga los pedidos guardados al arrancar
        bool ejecutando = true;
        while (ejecutando)
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine("   SISTEMA DE GESTIÓN DE ENVÍOS   ");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Registrar nuevo envío");
            Console.WriteLine("2. Cambiar estado de un pedido");
            Console.WriteLine("3. Cancelar un pedido");
            Console.WriteLine("4. Listar pedidos por estado");
            Console.WriteLine("5. Listar pedidos por método");
            Console.WriteLine("6. Buscar pedido por código");
            Console.WriteLine("7. Crear respaldo");
            Console.WriteLine("8. Listar respaldo");
            Console.WriteLine("9. Restaurar respaldo");
            Console.WriteLine("10. Salir");
            Console.Write("Seleccione una opción (1-10): ");

            string? opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    registrarpedido();
                    break;
                case "2":
                    CambiarEstadoPedido();
                    break;
                case "3":
                    CancelarPedido();
                    break;
                case "4":
                    ListarPorEstado();
                    break;
                case "5":
                    ListarPorMetodo();
                    break;
                case "6":
                    BuscarPorCodigo();
                    break;
                case "7":
                    CrearRespaldo();
                    break;
                case "8":
                    ListarRespaldos();
                    break;
                case "9":
                    RestaurarRespaldo();
                    break;    
                case "10":
                    GuardarDatos();//Guarda los pedidos registrados
                    Console.WriteLine("Datos guardados con éxito. Saliendo del programa...");
                    ejecutando = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }
    }


    static void registrarpedido()
    {
        Console.WriteLine("--- REGISTRO DE NUEVO ENVIO ---");   
        if (cantidadpedidos >= 100)
        {
            Console.WriteLine("error: Se alcanzo el limite maximo de 100 pedidos.");
            return;
        }

        // Modificación de Daniel Gutierrez - validar dni y crear usuario con dni nuevo
        string dni = "";
        while (true)
        {
            Console.Write("DNI del cliente (8 dígitos): ");
            dni = Console.ReadLine()?.Trim() ?? "";

            if (EsDniValido(dni))
            {
                break;
            }
            Console.WriteLine("Error: El DNI debe tener exactamente 8 dígitos numéricos. Intente nuevamente.");
        }

        string? nombreCliente = BuscarUsuarioPorDni(dni);

        if (nombreCliente != null)
        {
            Console.WriteLine($"Cliente encontrado: {nombreCliente}");
        }
        else
        {
            Console.WriteLine("El DNI no se encuentra registrado.");
            Console.Write("¿Desea registrar usuario? (SI / NO): ");
            string resp = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (resp == "SI" || resp == "S" || resp == "1")
            {
                Console.Write("Nombre del cliente: ");
                nombreCliente = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(nombreCliente))
                {
                    Console.WriteLine("Error: El nombre no puede estar vacío. Pedido cancelado.");
                    return;
                }
                RegistrarUsuario(dni, nombreCliente);
                Console.WriteLine("Usuario registrado con éxito.");
            }
            else
            {
                Console.WriteLine("Pedido cancelado.");
                return;
            }
        }

        dnis[cantidadpedidos] = dni;
        nombres[cantidadpedidos] = nombreCliente; //DG

        string celular = "";
        while (true)
        {
            Console.Write("Celular (9 dígitos, inicia con 9): ");
            celular = Console.ReadLine()?.Trim() ?? "";
            if (EsCelularValido(celular))
            {
                break;
            }
            Console.WriteLine("Error: El celular debe tener exactamente 9 dígitos numéricos y comenzar con 9. Intente nuevamente.");
        }
        celulares[cantidadpedidos] = celular;

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
        Console.Write("Detalle del pedido: ");
        detalles[cantidadpedidos] = Console.ReadLine();
        Console.Write("Fecha de envío DD-MM-AAAA: ");
        fechas[cantidadpedidos] = Console.ReadLine();
        codigos[cantidadpedidos] = "ENV" + (cantidadpedidos + 1).ToString("D3");
        estados[cantidadpedidos] = "Registrado";
        Console.WriteLine($"Pedido registrado CÓDIGO UNICO: {codigos[cantidadpedidos]}");
        cantidadpedidos++;

        // DG- Guarda inmediatamente el pedido para no perder datos si se cierra la consola
        GuardarDatos();
    }

    //Metodo para validar que el Celular tenga 9 digitos y empiece con 9
    static bool EsCelularValido(string celular)
    {
        if (celular.Length != 9) return false;
        if (celular[0] != '9') return false;
        foreach (char c in celular)
        {
            if (!char.IsDigit(c)) return false;
        }
        return true;
    }

    //Metodo para validar que el Dni tenga 8 caracteres: Josemaria Roque
    static bool EsDniValido(string dni)
    {
        if (dni.Length != 8) return false;
        foreach (char c in dni)
        {
            if (!char.IsDigit(c)) return false;
        }
        return true;
    }
    //Metodo que permite buscar validar si ya existe el dni ingresado en el archivo txt 
    static string? BuscarUsuarioPorDni(string dni)
    {
        if (!File.Exists(rutaUsuarios)) return null;

        string[] lineas = File.ReadAllLines(rutaUsuarios);
        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            string[] datos = linea.Split('|');
            if (datos.Length >= 2 && datos[0].Trim().Equals(dni.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return datos[1].Trim();
            }
        }
        return null;
    }

    //Metodo que verifica si existe la carpeta donde se guardaran los txt en caso no exista la crea
    //Valida si existe el Usuarios.txt sino existe la crea
    //Si existe coloca el cursor de escritura al final para no sobrescribir ni eliminar usuarios guardados
    static void RegistrarUsuario(string dni, string nombre)
    {
        if (!Directory.Exists(carpetaDatos))
        {
            Directory.CreateDirectory(carpetaDatos);
        }
        using (StreamWriter sw = File.AppendText(rutaUsuarios))
        {
            sw.WriteLine($"{dni.Trim()}|{nombre.Trim()}");
        }
    }
    static void ListarPorEstado()
    {
        Console.WriteLine("--- LISTADO POR ESTADO ---");
        string[] estadosPosibles = { "Registrado", "Enviado", "Entregado", "Recogido", "Cancelado" };

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
                    Console.WriteLine($"    Detalle del pedido: {detalles[i]}"); 
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
                    Console.WriteLine($"    Detalle del pedido: {detalles[i]}"); 
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
                Console.WriteLine($"Detalle del pedido: {detalles[i]}");
                Console.WriteLine($"Fecha: {fechas[i]} | ESTADO: {estados[i]}");
                encontrado = true;
                break;
            }
        }
        if (!encontrado) Console.WriteLine("Código no encontrado.");
    }

    // Cambios de Daniel Gutierrez - Agregando funcion de busqueda
    static void BuscarPorDni()
    {
        Console.WriteLine("--- BUSCAR PEDIDOS POR DNI ---");
        Console.Write("Ingrese el DNI a buscar (8 dígitos): ");
        string dniBuscado = Console.ReadLine()?.Trim() ?? "";

        if (!EsDniValido(dniBuscado))
        {
            Console.WriteLine("Error: El DNI debe tener exactamente 8 dígitos numéricos.");
            return;
        }

        bool encontrado = false;

        for (int i = 0; i < cantidadpedidos; i++)
        {
            if (string.Equals(dnis[i], dniBuscado, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n--- RESUMEN ---");
                Console.WriteLine($"Código: {codigos[i]}");
                Console.WriteLine($"Cliente: {nombres[i]} | DNI: {dnis[i]} | Celular: {celulares[i]}");
                Console.WriteLine($"Método: {metodos[i]} | Dirección: {direcciones[i]}");
                Console.WriteLine($"Detalle del pedido: {detalles[i]}"); 
                Console.WriteLine($"Fecha: {fechas[i]} | ESTADO: {estados[i]}");
                encontrado = true;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron pedidos registrados con el DNI ingresado.");
        }
    }

    static void CambiarEstadoPedido()
    {
        Console.WriteLine("--- CAMBIAR ESTADO DE UN PEDIDO ---");
        Console.Write("Ingrese el código del pedido: ");
        string codigoBuscado = Console.ReadLine()?.Trim() ?? "";

        int indice = -1;
        for (int i = 0; i < cantidadpedidos; i++)
        {
            if (string.Equals(codigos[i], codigoBuscado, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(codigos[i], "ENV-" + codigoBuscado.PadLeft(3, '0'), StringComparison.OrdinalIgnoreCase))
            {
                indice = i;
                break;
            }
        }

        if (indice == -1)
        {
            Console.WriteLine("Error: Pedido no encontrado.");
            return;
        }

        Console.WriteLine($"\nPedido encontrado: {codigos[indice]} - Cliente: {nombres[indice]}");
        Console.WriteLine($"Estado actual: {estados[indice]}");
        Console.WriteLine("\nSeleccione el nuevo estado:");
        Console.WriteLine("1. Registrado");
        Console.WriteLine("2. Enviado");
        Console.WriteLine("3. Entregado");
        Console.WriteLine("4. Recogido");
        Console.WriteLine("5. Cancelado");
        Console.Write("Opción (1-5): ");
        string opc = Console.ReadLine()?.Trim() ?? "";

        string nuevoEstado = opc switch
        {
            "1" => "Registrado",
            "2" => "Enviado",
            "3" => "Entregado",
            "4" => "Recogido",
            "5" => "Cancelado",
            _ => ""
        };

        if (string.IsNullOrEmpty(nuevoEstado))
        {
            Console.WriteLine("Opción no válida. No se realizaron cambios.");
            return;
        }

        estados[indice] = nuevoEstado;
        GuardarDatos();
        Console.WriteLine($"Estado actualizado con éxito a: {nuevoEstado}");
    }

    static void CancelarPedido()
    {
        Console.WriteLine("--- CANCELAR UN PEDIDO ---");
        Console.Write("Ingrese el código del pedido a cancelar: ");
        string codigoBuscado = Console.ReadLine()?.Trim() ?? "";

        int indice = -1;
        for (int i = 0; i < cantidadpedidos; i++)
        {
            if (string.Equals(codigos[i], codigoBuscado, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(codigos[i], "ENV-" + codigoBuscado.PadLeft(3, '0'), StringComparison.OrdinalIgnoreCase))
            {
                indice = i;
                break;
            }
        }

        if (indice == -1)
        {
            Console.WriteLine("Error: Pedido no encontrado.");
            return;
        }

        if (estados[indice] == "Cancelado")
        {
            Console.WriteLine("El pedido ya se encuentra en estado Cancelado.");
            return;
        }

        Console.WriteLine($"\nPedido: {codigos[indice]} | Cliente: {nombres[indice]} | Estado actual: {estados[indice]}");
        Console.Write("¿Está seguro de que desea cancelar este pedido? (SI / NO): ");
        string confirmacion = Console.ReadLine()?.Trim().ToUpper() ?? "";

        if (confirmacion == "SI" || confirmacion == "S")
        {
            estados[indice] = "Cancelado";
            GuardarDatos();
            Console.WriteLine("Pedido cancelado correctamente.");
        }
        else
        {
            Console.WriteLine("Operación anulada. El pedido no fue modificado.");
        }
    }

    //Metodos de percistencia: Guardar y Cargar  : Josemaria Roque
    static void GuardarDatos()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i < cantidadpedidos; i++)
                {
                    sw.WriteLine($"{codigos[i]}|{dnis[i]}|{nombres[i]}|{celulares[i]}|{metodos[i]}|{direcciones[i]}|{detalles[i]}|{fechas[i]}|{estados[i]}");
                }
            }
        }
    
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar: " + ex.Message);
        }
    }
    static void CrearRespaldo()
    {
        try
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("No existe el archivo de pedidos.");
                return;
            }

            if (!Directory.Exists(carpetaRespaldos))
            {
                Directory.CreateDirectory(carpetaRespaldos);
            }
            string nombreRespaldo = "respaldo_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            string rutaRespaldo = Path.Combine(carpetaRespaldos, nombreRespaldo);
            File.Copy(rutaArchivo, rutaRespaldo);
            Console.WriteLine("Respaldo creado correctamente.");
            Console.WriteLine("Archivo: " + nombreRespaldo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al crear respaldo: " + ex.Message);
        }
    }
    static void ListarRespaldos()
    {
         if (!Directory.Exists(carpetaRespaldos))
         {
            Console.WriteLine("No existe la carpeta de respaldos.");
            return;
         }
         string[] respaldos = Directory.GetFiles(carpetaRespaldos, "respaldo_*.txt");
         if (respaldos.Length == 0)
        {
            Console.WriteLine("No hay respaldos disponibles");
            return;
        }
        Console.WriteLine("Respaldos Disponibles");
        for (int i=0; i<respaldos.Length; i++)
        {
            Console.WriteLine($"{i + 1}.{Path.GetFileName(respaldos[i])}");
        }

    }
    static void RestaurarRespaldo()
{
    if (!Directory.Exists(carpetaRespaldos))
    {
        Console.WriteLine("No existe la carpeta de respaldos.");
        return;
    }
    string[] respaldos = Directory.GetFiles(carpetaRespaldos, "respaldo_*.txt");

    if (respaldos.Length == 0)
    {
        Console.WriteLine("No hay respaldos disponibles.");
        return;
    }
    Console.WriteLine("\n--- SELECCIONAR RESPALDO ---");
    for (int i = 0; i < respaldos.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {Path.GetFileName(respaldos[i])}");
    }
    Console.Write("Seleccione el número del respaldo: ");
    string opcion = Console.ReadLine() ?? "";
    if (!int.TryParse(opcion, out int seleccion) ||
        seleccion < 1 || seleccion > respaldos.Length)
    {
        Console.WriteLine("Selección no válida.");
        return;
    }
    string rutaSeleccionada = respaldos[seleccion - 1];
    try
    {
        string[] lineas = File.ReadAllLines(rutaSeleccionada);

        foreach (string linea in lineas)
        {
            string[] datos = linea.Split('|');

            if (datos.Length != 9 )
            {
                Console.WriteLine("El respaldo tiene un formato incorrecto.");
                return;
            }
        }
        GuardarDatos();

        CrearRespaldo();

        File.Copy(rutaSeleccionada, rutaArchivo, true);
        cantidadpedidos = 0;

        CargarDatos();

        Console.WriteLine("Respaldo restaurado correctamente.");
    }

    catch (Exception ex)
    {
        Console.WriteLine("Error al restaurar: " + ex.Message);
    }
}

    static void CargarDatos()
    {
        if (File.Exists(rutaArchivo))
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);
            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                string[] datos = linea.Split('|');
                if (datos.Length == 9 && cantidadpedidos < 100)
                {
                    codigos[cantidadpedidos] = datos[0];
                    dnis[cantidadpedidos] = datos[1]; //DG
                    nombres[cantidadpedidos] = datos[2];
                    celulares[cantidadpedidos] = datos[3];
                    metodos[cantidadpedidos] = datos[4];
                    direcciones[cantidadpedidos] = datos[5];
                    detalles[cantidadpedidos] = datos [6];
                    fechas[cantidadpedidos] = datos[7];
                    estados[cantidadpedidos] = datos[8];
                    cantidadpedidos++;
                }
            }
        }
    }

    #endregion
}
