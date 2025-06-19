using System;

class Program
{
    struct CancionMP3
    {
        public String Artista;
        public String Titulo;
        public int DuracionSegundos;
        public double TamanoKB;
        
        public void MostrarInformacion()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("         INFORMACIÓN DE LA CANCIÓN");
            Console.WriteLine("========================================");
            Console.WriteLine($"Artista: {Artista}");
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Duración: {ConvertirSegundosAMinutos(DuracionSegundos)}");
            Console.WriteLine($"Tamaño del archivo: {TamanoKB:F2} KB ({ConvertirKBaMB(TamanoKB):F2} MB)");
            Console.WriteLine("========================================");
        }
        
        private String ConvertirSegundosAMinutos(int segundos)
        {
            int minutos = segundos / 60;
            int segRestantes = segundos % 60;
            return $"{minutos}:{segRestantes:D2} ({segundos} segundos)";
        }
        
        private double ConvertirKBaMB(double kb)
        {
            return kb / 1024.0;
        }
        
        public void IngresarDatos()
        {
            Console.WriteLine("=== INGRESO DE DATOS DE LA CANCIÓN ===");
            
            Console.Write("Ingrese el nombre del artista: ");
            Artista = Console.ReadLine();
            
            Console.Write("Ingrese el título de la canción: ");
            Titulo = Console.ReadLine();
            
            Console.Write("Ingrese la duración en segundos: ");
            DuracionSegundos = int.Parse(Console.ReadLine());
            
            Console.Write("Ingrese el tamaño del archivo en KB: ");
            TamanoKB = double.Parse(Console.ReadLine());
            
            Console.WriteLine("\n¡Datos ingresados correctamente!");
        }
    }
    
    static void Main(string[] args)
    {
        Console.Title = "Gestor de Canciones MP3";
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        CancionMP3 miCancion = new CancionMP3();
        int opcion;
        
        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          GESTOR DE MP3               ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  1. Ingresar datos de canción        ║");
            Console.WriteLine("║  2. Mostrar información de canción    ║");
            Console.WriteLine("║  3. Salir                            ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción (1-3): ");
            
            opcion = int.Parse(Console.ReadLine());
            
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    miCancion.IngresarDatos();
                    break;
                    
                case 2:
                    Console.Clear();
                    if (string.IsNullOrEmpty(miCancion.Titulo))
                    {
                        Console.WriteLine("No hay datos de canción ingresados.");
                        Console.WriteLine("Por favor, ingrese los datos primero (Opción 1).");
                    }
                    else
                    {
                        miCancion.MostrarInformacion();
                    }
                    break;
                    
                case 3:
                    Console.WriteLine("\n¡Gracias por usar el Gestor de MP3!");
                    Environment.Exit(0);
                    break;
                    
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione 1, 2 o 3.");
                    break;
            }
            
            if (opcion != 3)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            
        } while (opcion != 3);
    }
}