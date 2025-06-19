using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    struct Persona
    {
        public String Nombre;
        public String DUI;
        public String Direccion;
        public String Telefono;
        public String Email;
        public String ProfesionOficio;
        
        public void IngresarDatos()
        {
            Console.WriteLine("═══════════════════════════════════");
            Console.WriteLine("       INGRESO DE DATOS");
            Console.WriteLine("═══════════════════════════════════");
            
            Console.Write("Nombre completo: ");
            Nombre = Console.ReadLine();
            
            Console.Write("DUI (formato: 12345678-9): ");
            DUI = Console.ReadLine();
            
            Console.Write("Dirección: ");
            Direccion = Console.ReadLine();
            
            Console.Write("Teléfono: ");
            Telefono = Console.ReadLine();
            
            Console.Write("Email: ");
            Email = Console.ReadLine();
            
            Console.Write("Profesión u Oficio: ");
            ProfesionOficio = Console.ReadLine();
            
            Console.WriteLine("\n¡Datos guardados exitosamente!");
        }
        
        public void MostrarDatos()
        {
            Console.WriteLine("┌─────────────────────────────────────────────────────┐");
            Console.WriteLine("│                  DATOS PERSONALES                   │");
            Console.WriteLine("├─────────────────────────────────────────────────────┤");
            Console.WriteLine($"│ Nombre:     {Nombre,-39} │");
            Console.WriteLine($"│ DUI:        {DUI,-39} │");
            Console.WriteLine($"│ Dirección:  {Direccion,-39} │");
            Console.WriteLine($"│ Teléfono:   {Telefono,-39} │");
            Console.WriteLine($"│ Email:      {Email,-39} │");
            Console.WriteLine($"│ Profesión:  {ProfesionOficio,-39} │");
            Console.WriteLine("└─────────────────────────────────────────────────────┘");
        }
        
        public bool TieneDatos()
        {
            return !string.IsNullOrEmpty(Nombre) || !string.IsNullOrEmpty(DUI);
        }
    }
    
    static List<Persona> agenda = new List<Persona>();
    
    static void IngresarDatos()
    {
        Console.Clear();
        Persona nuevaPersona = new Persona();
        
        // Verificar si el DUI ya existe
        bool duiExiste = false;
        string duiTemp;
        
        do
        {
            Console.Write("DUI (formato: 12345678-9): ");
            duiTemp = Console.ReadLine();
            
            duiExiste = agenda.Any(p => p.DUI.Equals(duiTemp, StringComparison.OrdinalIgnoreCase));
            
            if (duiExiste)
            {
                Console.WriteLine("¡ERROR! Ya existe una persona con ese DUI en la agenda.");
                Console.WriteLine("Por favor, ingrese un DUI diferente.");
                Console.WriteLine();
            }
        } while (duiExiste);
        
        nuevaPersona.DUI = duiTemp;
        
        Console.Write("Nombre completo: ");
        nuevaPersona.Nombre = Console.ReadLine();
        
        Console.Write("Dirección: ");
        nuevaPersona.Direccion = Console.ReadLine();
        
        Console.Write("Teléfono: ");
        nuevaPersona.Telefono = Console.ReadLine();
        
        Console.Write("Email: ");
        nuevaPersona.Email = Console.ReadLine();
        
        Console.Write("Profesión u Oficio: ");
        nuevaPersona.ProfesionOficio = Console.ReadLine();
        
        agenda.Add(nuevaPersona);
        Console.WriteLine("\n¡Contacto agregado exitosamente a la agenda!");
    }
    
    static void MostrarDatos()
    {
        Console.Clear();
        Console.WriteLine("══════════════════════════════════════════════");
        Console.WriteLine("              AGENDA COMPLETA");
        Console.WriteLine("══════════════════════════════════════════════");
        
        if (agenda.Count == 0)
        {
            Console.WriteLine("La agenda está vacía.");
            Console.WriteLine("Agregue contactos usando la opción 1 del menú.");
        }
        else
        {
            Console.WriteLine($"Total de contactos: {agenda.Count}");
            Console.WriteLine();
            
            for (int i = 0; i < agenda.Count; i++)
            {
                Console.WriteLine($"CONTACTO #{i + 1}:");
                agenda[i].MostrarDatos();
                Console.WriteLine();
            }
        }
    }
    
    static void BuscarPersona()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════");
        Console.WriteLine("          BUSCAR PERSONA");
        Console.WriteLine("═══════════════════════════════════");
        
        if (agenda.Count == 0)
        {
            Console.WriteLine("No hay contactos en la agenda para buscar.");
            return;
        }
        
        Console.Write("Ingrese el DUI a buscar: ");
        string duiBuscar = Console.ReadLine();
        
        Persona? personaEncontrada = null;
        int indiceEncontrado = -1;
        
        for (int i = 0; i < agenda.Count; i++)
        {
            if (agenda[i].DUI.Equals(duiBuscar, StringComparison.OrdinalIgnoreCase))
            {
                personaEncontrada = agenda[i];
                indiceEncontrado = i;
                break;
            }
        }
        
        if (personaEncontrada.HasValue)
        {
            Console.WriteLine("\n¡PERSONA ENCONTRADA!");
            Console.WriteLine($"Posición en la agenda: {indiceEncontrado + 1}");
            personaEncontrada.Value.MostrarDatos();
            
            Console.WriteLine("\n¿Qué desea hacer?");
            Console.WriteLine("1. Volver al menú principal");
            Console.WriteLine("2. Eliminar este contacto");
            Console.Write("Seleccione una opción: ");
            
            int opcionBusqueda = int.Parse(Console.ReadLine());
            if (opcionBusqueda == 2)
            {
                agenda.RemoveAt(indiceEncontrado);
                Console.WriteLine("¡Contacto eliminado exitosamente!");
            }
        }
        else
        {
            Console.WriteLine($"\nNo se encontró ninguna persona con el DUI: {duiBuscar}");
            Console.WriteLine("Verifique que el DUI esté correcto e intente nuevamente.");
        }
    }
    
    static void MostrarEstadisticas()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════");
        Console.WriteLine("          ESTADÍSTICAS");
        Console.WriteLine("═══════════════════════════════════");
        
        Console.WriteLine($"Total de contactos: {agenda.Count}");
        
        if (agenda.Count > 0)
        {
            // Estadísticas adicionales
            var profesiones = agenda.GroupBy(p => p.ProfesionOficio.ToLower())
                                   .Select(g => new { Profesion = g.Key, Cantidad = g.Count() })
                                   .OrderByDescending(x => x.Cantidad)
                                   .Take(3);
            
            Console.WriteLine("\nTop 3 profesiones más comunes:");
            int contador = 1;
            foreach (var profesion in profesiones)
            {
                Console.WriteLine($"{contador}. {profesion.Profesion} ({profesion.Cantidad} contacto(s))");
                contador++;
            }
            
            // Mostrar algunos contactos recientes
            Console.WriteLine($"\nÚltimos contactos agregados:");
            int mostrar = Math.Min(3, agenda.Count);
            for (int i = agenda.Count - mostrar; i < agenda.Count; i++)
            {
                Console.WriteLine($"- {agenda[i].Nombre} ({agenda[i].DUI})");
            }
        }
    }
    
    static void Main(string[] args)
    {
        Console.Title = "Agenda Personal Digital";
        Console.ForegroundColor = ConsoleColor.Magenta;
        
        int opcion;
        
        Console.WriteLine("╔══════════════════════════════════════════════════╗");
        Console.WriteLine("║            AGENDA PERSONAL DIGITAL              ║");
        Console.WriteLine("║                                                  ║");
        Console.WriteLine("║        Bienvenido al sistema de agenda          ║");
        Console.WriteLine("║       Mantenga sus contactos organizados        ║");
        Console.WriteLine("╚══════════════════════════════════════════════════╝");
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
        
        do
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║                AGENDA PERSONAL                    ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════╣");
            Console.WriteLine("║  1. Ingresar Datos                                ║");
            Console.WriteLine("║  2. Mostrar Datos                                 ║");
            Console.WriteLine("║  3. Buscar Persona                                ║");
            Console.WriteLine("║  4. Estadísticas                                  ║");
            Console.WriteLine("║  5. Salir                                         ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.WriteLine($"📊 Contactos en agenda: {agenda.Count}");
            Console.Write("\n🔹 Seleccione una opción (1-5): ");
            
            try
            {
                opcion = int.Parse(Console.ReadLine());
                
                switch (opcion)
                {
                    case 1:
                        IngresarDatos();
                        break;
                        
                    case 2:
                        MostrarDatos();
                        break;
                        
                    case 3:
                        BuscarPersona();
                        break;
                        
                    case 4:
                        MostrarEstadisticas();
                        break;
                        
                    case 5:
                        Console.Clear();
                        Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                        Console.WriteLine("║                   HASTA LUEGO                     ║");
                        Console.WriteLine("║                                                   ║");
                        Console.WriteLine("║     Gracias por usar la Agenda Personal          ║");
                        Console.WriteLine("║          ¡Que tenga un excelente día!            ║");
                        Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                        Environment.Exit(0);
                        break;
                        
                    default:
                        Console.WriteLine("❌ Opción inválida. Por favor, seleccione una opción del 1 al 5.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("❌ Error: Por favor, ingrese un número válido.");
                opcion = 0; // Para que no salga del bucle
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error inesperado: {ex.Message}");
                opcion = 0;
            }
            
            if (opcion != 5)
            {
                Console.WriteLine("\n📌 Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            
        } while (opcion != 5);
    }
}