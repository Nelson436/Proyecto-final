using Agenda_de_contactos_prueba;
using System.Diagnostics.Contracts;

internal class Program
{
    static List<Contacto> contactos = new List<Contacto>();
    static string archivoCSV = "contactos.csv";

    static void Main(string[] args)
    {
        CargarContactosDesdeCSV();

        bool salir = false;

        while (!salir)
        {
            MostrarMenu();

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarContacto();
                    break;
                case "2":
                    VerContactos();
                    break;
                case "3":
                    GuardarContactosEnCSV();
                    break;
                case "4":
                    salir = true;
                    break;
                default:
                    MostrarMensaje("Opción no válida. Presione cualquier tecla para continuar.", ConsoleColor.Red);
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════════╗");
        Console.WriteLine("║     Agenda de Contactos       ║");
        Console.WriteLine("╠═══════════════════════════════╣");
        Console.WriteLine("║ 1. Agregar contacto           ║");
        Console.WriteLine("║ 2. Ver contactos              ║");
        Console.WriteLine("║ 3. Guardar contactos en CSV   ║");
        Console.WriteLine("║ 4. Salir                      ║");
        Console.WriteLine("╚═══════════════════════════════╝");
        Console.ResetColor();
        Console.Write("Seleccione una opción: ");
    }

    static void AgregarContacto()
    {
        Console.Clear();
        MostrarTitulo("Agregar Contacto");

        Contacto nuevoContacto = new Contacto();

        nuevoContacto.Nombre = PedirDato("Nombre");
        nuevoContacto.Telefono = PedirDato("Teléfono");
        nuevoContacto.Correo = PedirDato("Correo");

        nuevoContacto.FechaHora = PedirFechaHora();

        nuevoContacto.Lugar = PedirDato("Lugar");
        nuevoContacto.Asunto = PedirDato("Asunto");

        contactos.Add(nuevoContacto);

        MostrarMensaje("\nContacto agregado exitosamente. Presione cualquier tecla para continuar.", ConsoleColor.Green);
        Console.ReadKey();
    }

    static void VerContactos()
    {
        Console.Clear();
        MostrarTitulo("Lista de Contactos");

        if (contactos.Count == 0)
        {
            MostrarMensaje("No hay contactos en la agenda.", ConsoleColor.Yellow);
        }
        else
        {
            for (int i = 0; i < contactos.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"--- Contacto {i + 1} ---");
                Console.ResetColor();
                Console.WriteLine(contactos[i]);
            }
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar.");
        Console.ReadKey();
    }

    static void GuardarContactosEnCSV()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(archivoCSV))
            {
                sw.WriteLine("Nombre,Teléfono,Correo,Fecha y Hora,Lugar,Asunto");
                foreach (var contacto in contactos)
                {
                    sw.WriteLine(contacto.ToCSV());
                }
            }
            MostrarMensaje($"\nContactos guardados exitosamente en {archivoCSV}", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            MostrarMensaje($"\nError al guardar los contactos: {ex.Message}", ConsoleColor.Red);
        }

        Console.WriteLine("Presione cualquier tecla para continuar.");
        Console.ReadKey();
    }

    static void CargarContactosDesdeCSV()
    {
        if (File.Exists(archivoCSV))
        {
            try
            {
                using (StreamReader sr = new StreamReader(archivoCSV))
                {
                    sr.ReadLine(); // Saltar la línea de encabezado
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');
                        if (datos.Length == 6)
                        {
                            Contacto contacto = new Contacto
                            {
                                Nombre = datos[0],
                                Telefono = datos[1],
                                Correo = datos[2],
                                FechaHora = DateTime.ParseExact(datos[3], "dd/MM/yyyy HH:mm", null),
                                Lugar = datos[4],
                                Asunto = datos[5]
                            };
                            contactos.Add(contacto);
                        }
                    }
                }
                MostrarMensaje($"Contactos cargados desde {archivoCSV}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar los contactos: {ex.Message}", ConsoleColor.Red);
            }
        }
    }

    static string PedirDato(string campo)
    {
        Console.Write($"{campo}: ");
        return Console.ReadLine();
    }

    static DateTime PedirFechaHora()
    {
        while (true)
        {
            Console.Write("Fecha y Hora (DD/MM/YYYY HH:MM): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fechaHora))
            {
                return fechaHora;
            }
            else
            {
                MostrarMensaje("Formato de fecha y hora no válido. Intente nuevamente.", ConsoleColor.Red);
            }
        }
    }

    static void MostrarTitulo(string titulo)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"=== {titulo} ===");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void MostrarMensaje(string mensaje, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(mensaje);
        Console.ResetColor();
    }
}
