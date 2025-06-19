using System;

class Program
{
    struct Vehiculo
    {
        public String Placa;
        public String Color;
        public int Año;
        public String Marca;
        public String Modelo;
        public String Propietario;
        
        public void IngresarDatos()
        {
            Console.Write("Placa del vehículo: ");
            Placa = Console.ReadLine().ToUpper();
            
            Console.Write("Color: ");
            Color = Console.ReadLine();
            
            Console.Write("Año: ");
            Año = int.Parse(Console.ReadLine());
            
            Console.Write("Marca: ");
            Marca = Console.ReadLine();
            
            Console.Write("Modelo: ");
            Modelo = Console.ReadLine();
            
            Console.Write("Nombre del propietario: ");
            Propietario = Console.ReadLine();
        }
        
        public void MostrarDatos()
        {
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│         DATOS DEL VEHÍCULO          │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine($"│ Placa:       {Placa,-22} │");
            Console.WriteLine($"│ Color:       {Color,-22} │");
            Console.WriteLine($"│ Año:         {Año,-22} │");
            Console.WriteLine($"│ Marca:       {Marca,-22} │");
            Console.WriteLine($"│ Modelo:      {Modelo,-22} │");
            Console.WriteLine($"│ Propietario: {Propietario,-22} │");
            Console.WriteLine("└─────────────────────────────────────┘");
        }
    }
    
    static void MostrarTodosLosVehiculos(Vehiculo[] vehiculos, int cantidad)
    {
        Console.Clear();
        Console.WriteLine("══════════════════════════════════════════");
        Console.WriteLine("         LISTADO DE VEHÍCULOS");
        Console.WriteLine("══════════════════════════════════════════");
        
        if (cantidad == 0)
        {
            Console.WriteLine("No hay vehículos registrados.");
        }
        else
        {
            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"\nVehículo #{i + 1}:");
                vehiculos[i].MostrarDatos();
            }
        }
    }
    
    static void Main(string[] args)
    {
        Console.Title = "Sistema de Registro de Vehículos";
        Console.ForegroundColor = ConsoleColor.Green;
        
        int maxVehiculos;
        Console.WriteLine("════════════════════════════════════");
        Console.WriteLine("  SISTEMA DE REGISTRO DE VEHÍCULOS");
        Console.WriteLine("════════════════════════════════════");
        Console.Write("¿Cuántos vehículos desea registrar? ");
        maxVehiculos = int.Parse(Console.ReadLine());
        
        Vehiculo[] vehiculos = new Vehiculo[maxVehiculos];
        int vehiculosRegistrados = 0;
        int opcion;
        
        do
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       MENÚ PRINCIPAL - VEHÍCULOS       ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║  1. Registrar vehículo                 ║");
            Console.WriteLine("║  2. Mostrar todos los vehículos        ║");
            Console.WriteLine("║  3. Buscar vehículo por placa          ║");
            Console.WriteLine("║  4. Estadísticas                       ║");
            Console.WriteLine("║  5. Salir                              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine($"Vehículos registrados: {vehiculosRegistrados}/{maxVehiculos}");
            Console.Write("\nSeleccione una opción (1-5): ");
            
            opcion = int.Parse(Console.ReadLine());
            
            switch (opcion)
            {
                case 1:
                    if (vehiculosRegistrados < maxVehiculos)
                    {
                        Console.Clear();
                        Console.WriteLine($"=== REGISTRO DE VEHÍCULO #{vehiculosRegistrados + 1} ===");
                        vehiculos[vehiculosRegistrados].IngresarDatos();
                        vehiculosRegistrados++;
                        Console.WriteLine("\n¡Vehículo registrado exitosamente!");
                    }
                    else
                    {
                        Console.WriteLine("Ya se ha alcanzado el máximo de vehículos a registrar.");
                    }
                    break;
                    
                case 2:
                    MostrarTodosLosVehiculos(vehiculos, vehiculosRegistrados);
                    break;
                    
                case 3:
                    Console.Clear();
                    Console.WriteLine("=== BÚSQUEDA POR PLACA ===");
                    if (vehiculosRegistrados == 0)
                    {
                        Console.WriteLine("No hay vehículos registrados.");
                    }
                    else
                    {
                        Console.Write("Ingrese la placa a buscar: ");
                        string placaBuscar = Console.ReadLine().ToUpper();
                        bool encontrado = false;
                        
                        for (int i = 0; i < vehiculosRegistrados; i++)
                        {
                            if (vehiculos[i].Placa.Equals(placaBuscar))
                            {
                                Console.WriteLine("\n¡Vehículo encontrado!");
                                vehiculos[i].MostrarDatos();
                                encontrado = true;
                                break;
                            }
                        }
                        
                        if (!encontrado)
                        {
                            Console.WriteLine($"No se encontró ningún vehículo con la placa: {placaBuscar}");
                        }
                    }
                    break;
                    
                case 4:
                    Console.Clear();
                    Console.WriteLine("=== ESTADÍSTICAS ===");
                    Console.WriteLine($"Total de vehículos registrados: {vehiculosRegistrados}");
                    Console.WriteLine($"Espacios disponibles: {maxVehiculos - vehiculosRegistrados}");
                    
                    if (vehiculosRegistrados > 0)
                    {
                        // Encontrar el año más antiguo y más reciente
                        int añoMasAntiguo = vehiculos[0].Año;
                        int añoMasReciente = vehiculos[0].Año;
                        
                        for (int i = 0; i < vehiculosRegistrados; i++)
                        {
                            if (vehiculos[i].Año < añoMasAntiguo)
                                añoMasAntiguo = vehiculos[i].Año;
                            if (vehiculos[i].Año > añoMasReciente)
                                añoMasReciente = vehiculos[i].Año;
                        }
                        
                        Console.WriteLine($"Vehículo más antiguo: {añoMasAntiguo}");
                        Console.WriteLine($"Vehículo más reciente: {añoMasReciente}");
                    }
                    break;
                    
                case 5:
                    Console.WriteLine("\n¡Gracias por usar el Sistema de Registro de Vehículos!");
                    Environment.Exit(0);
                    break;
                    
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción del 1 al 5.");
                    break;
            }
            
            if (opcion != 5)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            
        } while (opcion != 5);
    }
}