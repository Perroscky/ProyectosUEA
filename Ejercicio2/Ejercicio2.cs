using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionEstudioAsignaturas
{
    // Clase que representa una asignatura
    public class Asignatura
    {
        public string Nombre { get; set; }
        
        public Asignatura(string nombre)
        {
            Nombre = nombre;
        }
        
        // Método para generar el mensaje de estudio
        public string GenerarMensajeEstudio()
        {
            return $"Yo estudio {Nombre}";
        }
        
        public override string ToString()
        {
            return Nombre;
        }
    }
    
    // Clase que gestiona el estudio de asignaturas
    public class EstudioAsignaturas
    {
        private List<Asignatura> asignaturas;
        public string NombreEstudiante { get; set; }
        
        public EstudioAsignaturas(string nombreEstudiante = "")
        {
            NombreEstudiante = nombreEstudiante;
            asignaturas = new List<Asignatura>();
        }
        
        // Método para agregar una asignatura
        public void AgregarAsignatura(string nombreAsignatura)
        {
            asignaturas.Add(new Asignatura(nombreAsignatura));
        }
        
        // Método para agregar múltiples asignaturas
        public void AgregarAsignaturas(params string[] nombresAsignaturas)
        {
            foreach (string nombre in nombresAsignaturas)
            {
                AgregarAsignatura(nombre);
            }
        }
        
        // Método principal: mostrar mensajes de estudio (equivalente al bucle for de Python)
        public void MostrarMensajesEstudio()
        {
            Console.WriteLine("=== MENSAJES DE ESTUDIO ===");
            
            if (asignaturas.Count == 0)
            {
                Console.WriteLine("No hay asignaturas para estudiar.");
                return;
            }
            
            foreach (Asignatura asignatura in asignaturas)
            {
                Console.WriteLine(asignatura.GenerarMensajeEstudio());
            }
        }
        
        // Versión personalizada con nombre del estudiante
        public void MostrarMensajesEstudioPersonalizado()
        {
            if (string.IsNullOrEmpty(NombreEstudiante))
            {
                MostrarMensajesEstudio();
                return;
            }
            
            Console.WriteLine($"=== MENSAJES DE ESTUDIO PARA {NombreEstudiante.ToUpper()} ===");
            
            foreach (Asignatura asignatura in asignaturas)
            {
                Console.WriteLine($"{NombreEstudiante} estudia {asignatura.Nombre}");
            }
        }
        
        // Método usando LINQ (programación funcional)
        public void MostrarMensajesEstudioLINQ()
        {
            Console.WriteLine("=== USANDO LINQ ===");
            
            asignaturas
                .Select(a => a.GenerarMensajeEstudio())
                .ToList()
                .ForEach(Console.WriteLine);
        }
        
        // Método para mostrar estadísticas
        public void MostrarEstadisticas()
        {
            Console.WriteLine($"\n=== ESTADÍSTICAS ===");
            Console.WriteLine($"Total de asignaturas: {asignaturas.Count}");
            Console.WriteLine($"Asignatura más larga: {asignaturas.OrderByDescending(a => a.Nombre.Length).First().Nombre}");
            Console.WriteLine($"Asignatura más corta: {asignaturas.OrderBy(a => a.Nombre.Length).First().Nombre}");
        }
        
        // Método para listar todas las asignaturas
        public void ListarAsignaturas()
        {
            Console.WriteLine("\n=== LISTA DE ASIGNATURAS ===");
            for (int i = 0; i < asignaturas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {asignaturas[i].Nombre}");
            }
        }
    }
    
    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE ESTUDIO DE ASIGNATURAS ===\n");
            
            // Crear instancia del gestor de estudio
            EstudioAsignaturas miEstudio = new EstudioAsignaturas();
            
            // Agregar las asignaturas (equivalente a la lista de Python)
            miEstudio.AgregarAsignaturas("Metodologia de la Investigacion", "Fundamentos de Sistemas Digitales", "Cableado Estructurado", "Estructura de Datos", "Administracion de S.O");
            
            // Mostrar mensajes de estudio (equivalente directo al código Python)
            miEstudio.MostrarMensajesEstudio();
            
            Console.WriteLine("\n" + new string('=', 50));
            
            // Mostrar la lista de asignaturas
            miEstudio.ListarAsignaturas();
            
            // Mostrar usando LINQ
            Console.WriteLine();
            miEstudio.MostrarMensajesEstudioLINQ();
            
            // Mostrar estadísticas
            miEstudio.MostrarEstadisticas();
            
            Console.WriteLine("\n" + new string('=', 50));
            
            // Ejemplo con estudiante personalizado
            Console.WriteLine("\n=== EJEMPLO PERSONALIZADO ===");
            EstudioAsignaturas estudioPersonalizado = new EstudioAsignaturas("Luis Samaniego");
            estudioPersonalizado.AgregarAsignaturas("Fundamentos de Sistemas Digitales", "Cableado Estructurado", "Estructura de Datos");
            estudioPersonalizado.MostrarMensajesEstudioPersonalizado();
            
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
    
    // Versión simple y directa (equivalente exacto al código Python)
    public class VersionSimple
    {
        public static void EjecutarVersionSimple()
        {
            Console.WriteLine("=== VERSIÓN SIMPLE (EQUIVALENTE DIRECTO) ===");
            
            List<string> subjects = new List<string> 
            { "Fundamentos de Sistemas Digitales", "Cableado Estructurado", "Estructura de Datos" };
            
            foreach (string subject in subjects)
            {
                Console.WriteLine("Yo estudio " + subject);
            }
        }
    }
}