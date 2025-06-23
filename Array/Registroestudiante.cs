using System;
using System.Collections.Generic;
using System.Linq;

namespace RegistroEstudiante
{
    // Representa los datos de un estudiante y sus teléfonos
    public class Estudiante
    {
        // Propiedades básicas
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

        // Array de teléfonos con longitud fija de 3
        public string[] Telefonos { get; set; }

        // Constructor que inicializa propiedades y array de teléfonos
        public Estudiante(int id, string nombres, string apellidos, string direccion)
        {
            Id = id;
            Nombres = nombres ?? throw new ArgumentNullException(nameof(nombres));
            Apellidos = apellidos ?? throw new ArgumentNullException(nameof(apellidos));
            Direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            Telefonos = new string[3]; // capacidad para 3 números
        }

        // Constructor por defecto para facilitar la creación
        public Estudiante() : this(0, "", "", "")
        {
        }

        // Método para asignar un teléfono en una posición (0 a 2)
        public void AsignarTelefono(int indice, string telefono)
        {
            if (indice < 0 || indice >= Telefonos.Length)
                throw new ArgumentOutOfRangeException(nameof(indice), "Índice debe estar entre 0 y 2.");
            Telefonos[indice] = telefono;
        }

        // Método para obtener el nombre completo
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        // Imprimir datos del estudiante
        public void MostrarDatos()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombre completo: {NombreCompleto}");
            Console.WriteLine($"Dirección: {Direccion}");
            Console.WriteLine("Teléfonos:");
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"  [{i + 1}] {Telefonos[i] ?? "(no asignado)"}");
            }
            Console.WriteLine(new string('=', 50));
        }

        // Override ToString para mejor representación
        public override string ToString()
        {
            return $"ID: {Id} - {NombreCompleto}";
        }
    }

    // Clase para gestionar múltiples estudiantes
    public class GestorEstudiantes
    {
        private List<Estudiante> estudiantes;

        public GestorEstudiantes()
        {
            estudiantes = new List<Estudiante>();
        }

        // Agregar un estudiante
        public void AgregarEstudiante(Estudiante estudiante)
        {
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));

            if (ExisteEstudiante(estudiante.Id))
                throw new InvalidOperationException($"Ya existe un estudiante con ID: {estudiante.Id}");

            estudiantes.Add(estudiante);
            Console.WriteLine($"Estudiante {estudiante.NombreCompleto} agregado exitosamente.");
        }

        // Verificar si existe un estudiante por ID
        public bool ExisteEstudiante(int id)
        {
            return estudiantes.Any(e => e.Id == id);
        }

        // Buscar estudiante por ID
        public Estudiante BuscarEstudiante(int id)
        {
            return estudiantes.FirstOrDefault(e => e.Id == id);
        }

        // Mostrar todos los estudiantes
        public void MostrarTodosLosEstudiantes()
        {
            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Console.WriteLine($"\n=== LISTA DE ESTUDIANTES REGISTRADOS ({estudiantes.Count}) ===");
            foreach (var estudiante in estudiantes)
            {
                estudiante.MostrarDatos();
            }
        }

        // Mostrar lista resumida
        public void MostrarListaResumida()
        {
            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Console.WriteLine($"\n=== RESUMEN DE ESTUDIANTES ({estudiantes.Count}) ===");
            for (int i = 0; i < estudiantes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {estudiantes[i]}");
            }
        }

        // Obtener la cantidad de estudiantes
        public int CantidadEstudiantes => estudiantes.Count;

        // Método para crear un estudiante interactivamente
        public Estudiante CrearEstudianteInteractivo()
        {
            try
            {
                Console.WriteLine("\n=== REGISTRO DE NUEVO ESTUDIANTE ===");
                
                Console.Write("Ingrese ID del estudiante: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    throw new FormatException("ID debe ser un número válido.");
                }

                if (ExisteEstudiante(id))
                {
                    throw new InvalidOperationException($"Ya existe un estudiante con ID: {id}");
                }

                Console.Write("Ingrese nombres: ");
                string nombres = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(nombres))
                    throw new ArgumentException("Los nombres no pueden estar vacíos.");

                Console.Write("Ingrese apellidos: ");
                string apellidos = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(apellidos))
                    throw new ArgumentException("Los apellidos no pueden estar vacíos.");

                Console.Write("Ingrese dirección: ");
                string direccion = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(direccion))
                    throw new ArgumentException("La dirección no puede estar vacía.");

                Estudiante nuevoEstudiante = new Estudiante(id, nombres, apellidos, direccion);

                // Solicitar teléfonos
                Console.WriteLine("\nIngrese hasta 3 números de teléfono (presione Enter para omitir):");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"Teléfono {i + 1}: ");
                    string telefono = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrWhiteSpace(telefono))
                    {
                        nuevoEstudiante.AsignarTelefono(i, telefono);
                    }
                }

                return nuevoEstudiante;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear estudiante: {ex.Message}");
                return null;
            }
        }
    }

    class Program
    {
        private static GestorEstudiantes gestor = new GestorEstudiantes();

        static void Main(string[] args)
        {
            // Agregar estudiante inicial (el que ya tenías)
            AgregarEstudianteInicial();

            // Mostrar menú principal
            MostrarMenu();
        }

        static void AgregarEstudianteInicial()
        {
            try
            {
                Estudiante est = new Estudiante(
                    id: 923552533, // Removí el 0 inicial para evitar problemas con octal
                    nombres: "Luis Alberto",
                    apellidos: "Samaniego Segura",
                    direccion: "JipiJapa y Jose Marti, Santo Domingo de los Tsáchilas"
                );

                est.AsignarTelefono(0, "0996219688");
                est.AsignarTelefono(1, "0996818970");
                est.AsignarTelefono(2, "02-2994765");

                gestor.AgregarEstudiante(est);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar estudiante inicial: {ex.Message}");
            }
        }

        static void MostrarMenu()
        {
            bool continuar = true;
            
            while (continuar)
            {
                Console.WriteLine("\n" + new string('*', 60));
                Console.WriteLine("           SISTEMA DE REGISTRO DE ESTUDIANTES");
                Console.WriteLine(new string('*', 60));
                Console.WriteLine("1. Registrar nuevo estudiante");
                Console.WriteLine("2. Mostrar todos los estudiantes");
                Console.WriteLine("3. Mostrar lista resumida");
                Console.WriteLine("4. Buscar estudiante por ID");
                Console.WriteLine("5. Mostrar cantidad de estudiantes");
                Console.WriteLine("6. Salir");
                Console.WriteLine(new string('*', 60));
                Console.Write("Seleccione una opción (1-6): ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarNuevoEstudiante();
                        break;
                    case "2":
                        gestor.MostrarTodosLosEstudiantes();
                        break;
                    case "3":
                        gestor.MostrarListaResumida();
                        break;
                    case "4":
                        BuscarEstudiantePorId();
                        break;
                    case "5":
                        Console.WriteLine($"\nTotal de estudiantes registrados: {gestor.CantidadEstudiantes}");
                        break;
                    case "6":
                        continuar = false;
                        Console.WriteLine("¡Gracias por usar el sistema!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 6.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void RegistrarNuevoEstudiante()
        {
            Estudiante nuevoEstudiante = gestor.CrearEstudianteInteractivo();
            if (nuevoEstudiante != null)
            {
                try
                {
                    gestor.AgregarEstudiante(nuevoEstudiante);
                    Console.WriteLine("\n¡Estudiante registrado exitosamente!");
                    Console.WriteLine("\nDatos del estudiante registrado:");
                    nuevoEstudiante.MostrarDatos();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al registrar estudiante: {ex.Message}");
                }
            }
        }

        static void BuscarEstudiantePorId()
        {
            Console.Write("\nIngrese el ID del estudiante a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Estudiante estudiante = gestor.BuscarEstudiante(id);
                if (estudiante != null)
                {
                    Console.WriteLine("\nEstudiante encontrado:");
                    estudiante.MostrarDatos();
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún estudiante con ID: {id}");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Debe ingresar un número.");
            }
        }
    }
}