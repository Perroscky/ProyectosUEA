using System;

// Clase para representar un estudiante
public class Estudiante
{
    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Correo { get; set; }
    public double NotaDefinitiva { get; set; }
    
    public Estudiante(string cedula, string nombre, string apellido, string correo, double notaDefinitiva)
    {
        Cedula = cedula;
        Nombre = nombre;
        Apellido = apellido;
        Correo = correo;
        NotaDefinitiva = notaDefinitiva;
    }
    
    // Propiedad para determinar si el estudiante está aprobado
    public bool EstaAprobado => NotaDefinitiva >= 7.0;
    
    // Método para mostrar la información del estudiante
    public void MostrarInformacion()
    {
        string estado = EstaAprobado ? "APROBADO" : "REPROBADO";
        Console.WriteLine($"Cédula: {Cedula}");
        Console.WriteLine($"Nombre: {Nombre} {Apellido}");
        Console.WriteLine($"Correo: {Correo}");
        Console.WriteLine($"Nota Definitiva: {NotaDefinitiva:F2}");
        Console.WriteLine($"Estado: {estado}");
        Console.WriteLine(new string('-', 40));
    }
}

// Clase para representar un nodo de la lista enlazada

public class NodoEstudiante
{
    public Estudiante Estudiante { get; set; }
    public NodoEstudiante Siguiente { get; set; }
    
    public NodoEstudiante(Estudiante estudiante)
    {
        Estudiante = estudiante;
        Siguiente = null;
    }
}

// Clase para manejar la lista enlazada de estudiantes
public class ListaEstudiantes
{
    private NodoEstudiante cabeza;
    private int totalEstudiantes;
    
    public ListaEstudiantes()
    {
        cabeza = null;
        totalEstudiantes = 0;
    }
    
    // Propiedad para obtener el total de estudiantes
    public int TotalEstudiantes => totalEstudiantes;
    
    // Método para agregar estudiante al inicio (aprobados)
    public void AgregarAlInicio(Estudiante estudiante)
    {
        NodoEstudiante nuevoNodo = new NodoEstudiante(estudiante);
        nuevoNodo.Siguiente = cabeza;
        cabeza = nuevoNodo;
        totalEstudiantes++;
    }
    
    // Método para agregar estudiante al final (reprobados)
    public void AgregarAlFinal(Estudiante estudiante)
    {
        NodoEstudiante nuevoNodo = new NodoEstudiante(estudiante);
        
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            NodoEstudiante actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
        totalEstudiantes++;
    }
    
    // Método para buscar estudiante por cédula
    public Estudiante BuscarPorCedula(string cedula)
    {
        NodoEstudiante actual = cabeza;
        while (actual != null)
        {
            if (actual.Estudiante.Cedula.Equals(cedula, StringComparison.OrdinalIgnoreCase))
            {
                return actual.Estudiante;
            }
            actual = actual.Siguiente;
        }
        return null;
    }
    
    // Método para eliminar estudiante por cédula
    public bool EliminarPorCedula(string cedula)
    {
        if (cabeza == null)
            return false;
        
        // Si el estudiante a eliminar está en la cabeza
        if (cabeza.Estudiante.Cedula.Equals(cedula, StringComparison.OrdinalIgnoreCase))
        {
            cabeza = cabeza.Siguiente;
            totalEstudiantes--;
            return true;
        }
        
        // Buscar el estudiante en el resto de la lista
        NodoEstudiante actual = cabeza;
        while (actual.Siguiente != null)
        {
            if (actual.Siguiente.Estudiante.Cedula.Equals(cedula, StringComparison.OrdinalIgnoreCase))
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                totalEstudiantes--;
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }
    
    // Método para contar estudiantes aprobados
    public int ContarAprobados()
    {
        int contador = 0;
        NodoEstudiante actual = cabeza;
        while (actual != null)
        {
            if (actual.Estudiante.EstaAprobado)
                contador++;
            actual = actual.Siguiente;
        }
        return contador;
    }
    
    // Método para contar estudiantes reprobados
    public int ContarReprobados()
    {
        int contador = 0;
        NodoEstudiante actual = cabeza;
        while (actual != null)
        {
            if (!actual.Estudiante.EstaAprobado)
                contador++;
            actual = actual.Siguiente;
        }
        return contador;
    }
    
    // Método para mostrar todos los estudiantes
    public void MostrarTodosLosEstudiantes()
    {
        if (cabeza == null)
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }
        
        Console.WriteLine("\n=== LISTA DE ESTUDIANTES ===");
        NodoEstudiante actual = cabeza;
        int contador = 1;
        while (actual != null)
        {
            Console.WriteLine($"\n--- Estudiante {contador} ---");
            actual.Estudiante.MostrarInformacion();
            actual = actual.Siguiente;
            contador++;
        }
    }
    
    // Método para mostrar solo estudiantes aprobados
    public void MostrarEstudiantesAprobados()
    {
        Console.WriteLine("\n=== ESTUDIANTES APROBADOS ===");
        NodoEstudiante actual = cabeza;
        int contador = 0;
        while (actual != null)
        {
            if (actual.Estudiante.EstaAprobado)
            {
                contador++;
                Console.WriteLine($"\n--- Aprobado {contador} ---");
                actual.Estudiante.MostrarInformacion();
            }
            actual = actual.Siguiente;
        }
        
        if (contador == 0)
            Console.WriteLine("No hay estudiantes aprobados.");
    }
    
    // Método para mostrar solo estudiantes reprobados
    public void MostrarEstudiantesReprobados()
    {
        Console.WriteLine("\n=== ESTUDIANTES REPROBADOS ===");
        NodoEstudiante actual = cabeza;
        int contador = 0;
        while (actual != null)
        {
            if (!actual.Estudiante.EstaAprobado)
            {
                contador++;
                Console.WriteLine($"\n--- Reprobado {contador} ---");
                actual.Estudiante.MostrarInformacion();
            }
            actual = actual.Siguiente;
        }
        
        if (contador == 0)
            Console.WriteLine("No hay estudiantes reprobados.");
    }
}

// Clase principal del sistema de registro
public class SistemaRegistroEstudiantes
{
    private ListaEstudiantes listaEstudiantes;
    
    public SistemaRegistroEstudiantes()
    {
        listaEstudiantes = new ListaEstudiantes();
    }
    
    // Método para validar cédula (formato básico)
    private bool ValidarCedula(string cedula)
    {
        return !string.IsNullOrWhiteSpace(cedula) && cedula.Length >= 6 && cedula.Length <= 15;
    }
    
    // Método para validar email básico
    private bool ValidarEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
    }
    
    // Método para validar nota
    private bool ValidarNota(double nota)
    {
        return nota >= 1.0 && nota <= 10.0;
    }
    
    // a. Agregar estudiante
    public void AgregarEstudiante()
    {
        Console.WriteLine("\n=== AGREGAR NUEVO ESTUDIANTE ===");
        
        string cedula;
        do
        {
            Console.Write("Ingrese la cédula: ");
            cedula = Console.ReadLine();
            
            if (!ValidarCedula(cedula))
            {
                Console.WriteLine("Cédula inválida. Debe tener entre 6 y 15 caracteres.");
                continue;
            }
            
            if (listaEstudiantes.BuscarPorCedula(cedula) != null)
            {
                Console.WriteLine("Ya existe un estudiante con esta cédula.");
                return;
            }
            break;
            
        } while (true);
        
        Console.Write("Ingrese el nombre: ");
        string nombre = Console.ReadLine();
        
        Console.Write("Ingrese el apellido: ");
        string apellido = Console.ReadLine();
        
        string correo;
        do
        {
            Console.Write("Ingrese el correo electrónico: ");
            correo = Console.ReadLine();
            
            if (!ValidarEmail(correo))
            {
                Console.WriteLine("Correo inválido. Debe contener @ y un punto.");
                continue;
            }
            break;
            
        } while (true);
        
        double nota;
        do
        {
            Console.Write("Ingrese la nota definitiva (1-10): ");
            if (!double.TryParse(Console.ReadLine(), out nota) || !ValidarNota(nota))
            {
                Console.WriteLine("Nota inválida. Debe estar entre 1 y 10.");
                continue;
            }
            break;
            
        } while (true);
        
        Estudiante nuevoEstudiante = new Estudiante(cedula, nombre, apellido, correo, nota);
        
        // Agregar al inicio si está aprobado, al final si está reprobado
        if (nuevoEstudiante.EstaAprobado)
        {
            listaEstudiantes.AgregarAlInicio(nuevoEstudiante);
            Console.WriteLine($"\nEstudiante {nombre} {apellido} agregado como APROBADO al inicio de la lista.");
        }
        else
        {
            listaEstudiantes.AgregarAlFinal(nuevoEstudiante);
            Console.WriteLine($"\nEstudiante {nombre} {apellido} agregado como REPROBADO al final de la lista.");
        }
    }
    
    // b. Buscar estudiante por cédula
    public void BuscarEstudiante()
    {
        Console.WriteLine("\n=== BUSCAR ESTUDIANTE ===");
        Console.Write("Ingrese la cédula del estudiante a buscar: ");
        string cedula = Console.ReadLine();
        
        Estudiante estudiante = listaEstudiantes.BuscarPorCedula(cedula);
        
        if (estudiante != null)
        {
            Console.WriteLine("\n--- ESTUDIANTE ENCONTRADO ---");
            estudiante.MostrarInformacion();
        }
        else
        {
            Console.WriteLine("No se encontró ningún estudiante con esa cédula.");
        }
    }
    
    // c. Eliminar estudiante
    public void EliminarEstudiante()
    {
        Console.WriteLine("\n=== ELIMINAR ESTUDIANTE ===");
        Console.Write("Ingrese la cédula del estudiante a eliminar: ");
        string cedula = Console.ReadLine();
        
        Estudiante estudiante = listaEstudiantes.BuscarPorCedula(cedula);
        
        if (estudiante != null)
        {
            Console.WriteLine("\n--- ESTUDIANTE A ELIMINAR ---");
            estudiante.MostrarInformacion();
            
            Console.Write("¿Está seguro que desea eliminar este estudiante? (S/N): ");
            string confirmacion = Console.ReadLine();
            
            if (confirmacion.ToUpper() == "S")
            {
                if (listaEstudiantes.EliminarPorCedula(cedula))
                {
                    Console.WriteLine("Estudiante eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al eliminar el estudiante.");
                }
            }
            else
            {
                Console.WriteLine("Eliminación cancelada.");
            }
        }
        else
        {
            Console.WriteLine("No se encontró ningún estudiante con esa cédula.");
        }
    }
    
    // d. y e. Total estudiantes aprobados y reprobados
    public void MostrarEstadisticas()
    {
        Console.WriteLine("\n=== ESTADÍSTICAS ===");
        
        int totalEstudiantes = listaEstudiantes.TotalEstudiantes;
        int aprobados = listaEstudiantes.ContarAprobados();
        int reprobados = listaEstudiantes.ContarReprobados();
        
        Console.WriteLine($"Total de estudiantes registrados: {totalEstudiantes}");
        Console.WriteLine($"Total de estudiantes aprobados: {aprobados}");
        Console.WriteLine($"Total de estudiantes reprobados: {reprobados}");
        
        if (totalEstudiantes > 0)
        {
            double porcentajeAprobados = (double)aprobados / totalEstudiantes * 100;
            double porcentajeReprobados = (double)reprobados / totalEstudiantes * 100;
            
            Console.WriteLine($"Porcentaje de aprobados: {porcentajeAprobados:F2}%");
            Console.WriteLine($"Porcentaje de reprobados: {porcentajeReprobados:F2}%");
        }
    }
    
    // Método para mostrar el menú principal
    public void MostrarMenu()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("SISTEMA DE REGISTRO - REDES III");
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("1. Agregar estudiante");
        Console.WriteLine("2. Buscar estudiante por cédula");
        Console.WriteLine("3. Eliminar estudiante");
        Console.WriteLine("4. Mostrar estadísticas");
        Console.WriteLine("5. Mostrar todos los estudiantes");
        Console.WriteLine("6. Mostrar solo estudiantes aprobados");
        Console.WriteLine("7. Mostrar solo estudiantes reprobados");
        Console.WriteLine("8. Salir");
        Console.Write("\nSeleccione una opción: ");
    }
    
    // Método principal para ejecutar el sistema
    public void Ejecutar()
    {
        Console.WriteLine("¡Bienvenido al Sistema de Registro de Estudiantes de Redes III!");
        
        int opcion;
        do
        {
            MostrarMenu();
            
            while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 8)
            {
                Console.Write("Opción inválida. Seleccione una opción válida (1-8): ");
            }
            
            switch (opcion)
            {
                case 1:
                    AgregarEstudiante();
                    break;
                case 2:
                    BuscarEstudiante();
                    break;
                case 3:
                    EliminarEstudiante();
                    break;
                case 4:
                    MostrarEstadisticas();
                    break;
                case 5:
                    listaEstudiantes.MostrarTodosLosEstudiantes();
                    break;
                case 6:
                    listaEstudiantes.MostrarEstudiantesAprobados();
                    break;
                case 7:
                    listaEstudiantes.MostrarEstudiantesReprobados();
                    break;
                case 8:
                    Console.WriteLine("¡Gracias por usar el sistema! Hasta luego.");
                    break;
            }
            
            if (opcion != 8)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            
        } while (opcion != 8);
    }
}

// Clase principal con el método Main
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            SistemaRegistroEstudiantes sistema = new SistemaRegistroEstudiantes();
            sistema.Ejecutar();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error del sistema: {ex.Message}");
        }
    }
}