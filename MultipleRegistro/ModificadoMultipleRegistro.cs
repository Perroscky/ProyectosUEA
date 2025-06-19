using System;

class Program
{
    struct programa
    {
        public String Universidad;
        public String Carrera;
    }
    
    struct Estudiante
    {
        public String nombre;
        public int edad;
        public programa Estudios;
        
        public void MostrarDatos()
        {
            Console.WriteLine("Estudiante: " + nombre);
            Console.WriteLine("Edad: " + edad);
            Console.WriteLine("Carrera: " + Estudios.Carrera);
            Console.WriteLine("Universidad: " + Estudios.Universidad);
            Console.WriteLine("----------------------------------------");
        }
    }
    
    static void Main(string[] args)
    {
        Console.Title = "Ejemplo 4 Modificado - Múltiples Registros de Estudiantes";
        
        int cantidadEstudiantes;
        Console.WriteLine("¿Cuántos estudiantes desea registrar?");
        cantidadEstudiantes = int.Parse(Console.ReadLine());
        
        Estudiante[] estudiantes = new Estudiante[cantidadEstudiantes];
        int opcion;
        
        do
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("           MENÚ PRINCIPAL");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Ingresar datos de estudiantes");
            Console.WriteLine("2. Mostrar todos los estudiantes");
            Console.WriteLine("3. Mostrar estudiante específico");
            Console.WriteLine("4. Salir");
            Console.WriteLine("======================================");
            Console.Write("Seleccione una opción (1-4): ");
            opcion = int.Parse(Console.ReadLine());
            
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("=== INGRESO DE DATOS ===");
                    for (int i = 0; i < cantidadEstudiantes; i++)
                    {
                        Console.WriteLine($"\nEstudiante #{i + 1}:");
                        Console.Write("Ingrese Nombre: ");
                        estudiantes[i].nombre = Console.ReadLine();
                        Console.Write("Edad: ");
                        estudiantes[i].edad = int.Parse(Console.ReadLine());
                        Console.Write("Carrera: ");
                        estudiantes[i].Estudios.Carrera = Console.ReadLine();
                        Console.Write("Universidad: ");
                        estudiantes[i].Estudios.Universidad = Console.ReadLine();
                        Console.WriteLine("--- Datos guardados ---");
                    }
                    break;
                    
                case 2:
                    Console.Clear();
                    Console.WriteLine("=== LISTADO DE TODOS LOS ESTUDIANTES ===");
                    for (int i = 0; i < cantidadEstudiantes; i++)
                    {
                        Console.WriteLine($"\nEstudiante #{i + 1}:");
                        estudiantes[i].MostrarDatos();
                    }
                    break;
                    
                case 3:
                    Console.Clear();
                    Console.WriteLine("=== MOSTRAR ESTUDIANTE ESPECÍFICO ===");
                    Console.Write($"Ingrese el número de estudiante (1-{cantidadEstudiantes}): ");
                    int indice = int.Parse(Console.ReadLine()) - 1;
                    if (indice >= 0 && indice < cantidadEstudiantes)
                    {
                        Console.WriteLine($"\nDatos del Estudiante #{indice + 1}:");
                        estudiantes[indice].MostrarDatos();
                    }
                    else
                    {
                        Console.WriteLine("Número de estudiante inválido.");
                    }
                    break;
                    
                case 4:
                    Console.WriteLine("¡Hasta luego!");
                    Environment.Exit(0);
                    break;
                    
                default:
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                    break;
            }
            
            if (opcion != 4)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            
        } while (opcion != 4);
    }
}