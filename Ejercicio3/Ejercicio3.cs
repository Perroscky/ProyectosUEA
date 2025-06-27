using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace GestionNotas
{
    // Clase que representa una asignatura con su nota
    public class AsignaturaConNota
    {
        public string Nombre { get; set; }
        public double Nota { get; set; }
        public string NotaTexto { get; set; } // Para mantener la entrada original como texto
        
        public AsignaturaConNota(string nombre)
        {
            Nombre = nombre;
            Nota = 0;
            NotaTexto = "";
        }
        
        // Método para establecer la nota (con validación)
        public bool EstablecerNota(string notaTexto)
        {
            NotaTexto = notaTexto;
            
            // Intentar convertir a número para validaciones adicionales
            if (double.TryParse(notaTexto.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double notaNumerica))
            {
                Nota = notaNumerica;
                return true;
            }
            
            // Si no es numérico, mantener como texto
            Nota = 0;
            return false;
        }
        
        // Método para generar el mensaje de resultado
        public string GenerarMensajeResultado()
        {
            return $"En {Nombre} has sacado {NotaTexto}";
        }
        
        // Método para obtener calificación cualitativa (si es numérica)
        public string ObtenerCalificacionCualitativa()
        {
            if (Nota >= 9) return "Excelente";
            if (Nota >= 7) return "Notable";
            if (Nota >= 5) return "Aprobado";
            if (Nota >= 0) return "Suspendido";
            return "No evaluado";
        }
        
        public override string ToString()
        {
            return $"{Nombre}: {NotaTexto}";
        }
    }
    
    // Clase que gestiona las notas de las asignaturas
    public class GestorNotas
    {
        private List<AsignaturaConNota> asignaturas;
        public string NombreEstudiante { get; set; }
        
        public GestorNotas(string nombreEstudiante = "")
        {
            NombreEstudiante = nombreEstudiante;
            asignaturas = new List<AsignaturaConNota>();
        }
        
        // Agregar asignaturas
        public void AgregarAsignaturas(params string[] nombresAsignaturas)
        {
            foreach (string nombre in nombresAsignaturas)
            {
                asignaturas.Add(new AsignaturaConNota(nombre));
            }
        }
        
        // Método principal: solicitar notas (equivalente al primer bucle Python)
        public void SolicitarNotas()
        {
            Console.WriteLine("=== INTRODUCCIÓN DE NOTAS ===");
            Console.WriteLine("Introduce la nota para cada asignatura:\n");
            
            foreach (var asignatura in asignaturas)
            {
                bool notaValida = false;
                
                while (!notaValida)
                {
                    Console.Write($"¿Qué nota has sacado en {asignatura.Nombre}? ");
                    string entrada = Console.ReadLine();
                    
                    if (!string.IsNullOrEmpty(entrada))
                    {
                        asignatura.EstablecerNota(entrada);
                        notaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Por favor, introduce una nota válida.");
                    }
                }
            }
        }
        
        // Método para mostrar resultados (equivalente al segundo bucle Python)
        public void MostrarResultados()
        {
            Console.WriteLine("\n=== RESULTADOS ===");
            
            foreach (var asignatura in asignaturas)
            {
                Console.WriteLine(asignatura.GenerarMensajeResultado());
            }
        }
        
        // Versión mejorada con análisis de notas
        public void MostrarResultadosDetallados()
        {
            Console.WriteLine("\n=== RESULTADOS DETALLADOS ===");
            
            foreach (var asignatura in asignaturas)
            {
                Console.WriteLine($"En {asignatura.Nombre} has sacado {asignatura.NotaTexto} - {asignatura.ObtenerCalificacionCualitativa()}");
            }
        }
        
        // Método para mostrar estadísticas (solo para notas numéricas)
        public void MostrarEstadisticas()
        {
            var notasNumericas = asignaturas.Where(a => a.Nota > 0).ToList();
            
            if (notasNumericas.Count == 0)
            {
                Console.WriteLine("\n=== ESTADÍSTICAS ===");
                Console.WriteLine("No hay notas numéricas para calcular estadísticas.");
                return;
            }
            
            Console.WriteLine("\n=== ESTADÍSTICAS ===");
            Console.WriteLine($"Nota media: {notasNumericas.Average(a => a.Nota):F2}");
            Console.WriteLine($"Nota más alta: {notasNumericas.Max(a => a.Nota)} ({notasNumericas.First(a => a.Nota == notasNumericas.Max(n => n.Nota)).Nombre})");
            Console.WriteLine($"Nota más baja: {notasNumericas.Min(a => a.Nota)} ({notasNumericas.First(a => a.Nota == notasNumericas.Min(n => n.Nota)).Nombre})");
            Console.WriteLine($"Asignaturas aprobadas: {notasNumericas.Count(a => a.Nota >= 7)}");
            Console.WriteLine($"Asignaturas suspendidas: {notasNumericas.Count(a => a.Nota < 7)}");
        }
        
        // Método para listar todas las asignaturas con sus notas
        public void ListarAsignaturasConNotas()
        {
            Console.WriteLine("\n=== RESUMEN DE NOTAS ===");
            
            for (int i = 0; i < asignaturas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {asignaturas[i]}");
            }
        }
        
        // Método para obtener asignaturas (solo lectura)
        public IReadOnlyList<AsignaturaConNota> ObtenerAsignaturas()
        {
            return asignaturas.AsReadOnly();
        }
    }
    
    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE NOTAS ===\n");
            
            // Crear instancia del gestor de notas
            GestorNotas miGestor = new GestorNotas("Estudiante");
            
            // Agregar las asignaturas (equivalente a la lista de Python)
            miGestor.AgregarAsignaturas("Administracion de S.O", "Estructura de Datos", "Cableado Estructutado", "Fundamentos de Sistemas Digitales", "Metodologia de la Investigacion");
            
            // Solicitar notas al usuario (equivalente al primer bucle Python)
            miGestor.SolicitarNotas();
            
            // Mostrar resultados (equivalente al segundo bucle Python)
            miGestor.MostrarResultados();
            
            // Funcionalidad adicional
            miGestor.MostrarResultadosDetallados();
            miGestor.MostrarEstadisticas();
            miGestor.ListarAsignaturasConNotas();
            
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("¡Gracias por usar el sistema de gestión de notas!");
            
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
            { "Administracion de S.O", "Estructura de Datos", "Cableado Estructutado", "Fundamentos de Sistemas Digitales", "Metodologia de la Investigacion" };
            
            List<string> scores = new List<string>();
            
            // Primer bucle: solicitar notas
            foreach (string subject in subjects)
            {
                Console.Write("¿Qué nota has sacado en " + subject + "? ");
                string score = Console.ReadLine();
                scores.Add(score);
            }
            
            // Segundo bucle: mostrar resultados
            for (int i = 0; i < subjects.Count; i++)
            {
                Console.WriteLine("En " + subjects[i] + " has sacado " + scores[i]);
            }
        }
    }
}