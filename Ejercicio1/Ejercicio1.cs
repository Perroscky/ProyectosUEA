using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionAsignaturas
{
    // Clase que representa una asignatura
    public class Asignatura
    {
        public string Nombre { get; set; }
        
        public Asignatura(string nombre)
        {
            Nombre = nombre;
        }
        
        public override string ToString()
        {
            return Nombre;
        }
    }
    
    // Clase que gestiona el curso y sus asignaturas
    public class Curso
    {
        private List<Asignatura> asignaturas;
        public string NombreCurso { get; set; }
        
        public Curso(string nombreCurso)
        {
            NombreCurso = nombreCurso;
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
        
        // Método para mostrar todas las asignaturas
        public void MostrarAsignaturas()
        {
            Console.WriteLine($"Asignaturas del curso: {NombreCurso}");
            Console.WriteLine(new string('-', 40));
            
            if (asignaturas.Count == 0)
            {
                Console.WriteLine("No hay asignaturas registradas.");
                return;
            }
            
            for (int i = 0; i < asignaturas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {asignaturas[i]}");
            }
            
            Console.WriteLine($"\nTotal de asignaturas: {asignaturas.Count}");
        }
        
        // Método para obtener la lista de asignaturas (solo lectura)
        public IReadOnlyList<Asignatura> ObtenerAsignaturas()
        {
            return asignaturas.AsReadOnly();
        }
        
        // Método para mostrar asignaturas como lista simple (equivalente al print de Python)
        public void MostrarListaSimple()
        {
            var nombresAsignaturas = asignaturas.Select(a => a.Nombre).ToList();
            Console.WriteLine($"[{string.Join(", ", nombresAsignaturas)}]");
        }
    }
    
    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE ASIGNATURAS ===\n");
            
            // Crear un curso
            Curso miCurso = new Curso("Bachillerato");
            
            // Agregar las asignaturas (equivalente a la lista de Python)
            miCurso.AgregarAsignaturas("Matemáticas", "Física", "Química", "Historia", "Lengua");
            
            // Mostrar las asignaturas de forma estructurada
            miCurso.MostrarAsignaturas();
            
            Console.WriteLine("\n" + new string('=', 50));
            
            // Mostrar como lista simple (equivalente directo al código Python)
            Console.WriteLine("Equivalente directo al código Python:");
            miCurso.MostrarListaSimple();
            
            // Ejemplo de funcionalidad adicional
            Console.WriteLine("\n=== EJEMPLO DE FUNCIONALIDAD ADICIONAL ===");
            
            // Crear otro curso y agregar asignaturas individualmente
            Curso cursoTecnico = new Curso("Técnico en Informática");
            cursoTecnico.AgregarAsignatura("Programación");
            cursoTecnico.AgregarAsignatura("Base de Datos");
            cursoTecnico.AgregarAsignatura("Redes");
            
            cursoTecnico.MostrarAsignaturas();
            
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}