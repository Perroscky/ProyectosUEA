using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

// Clase que representa a una persona en la cola
public class Persona
{
    public string Nombre { get; set; }
    public string Identificacion { get; set; }
    public DateTime HoraLlegada { get; set; }
    public int NumeroAsiento { get; set; }

    public Persona(string nombre, string identificacion)
    {
        Nombre = nombre;
        Identificacion = identificacion;
        HoraLlegada = DateTime.Now;
        NumeroAsiento = -1; // Sin asiento asignado inicialmente
    }

    public override string ToString()
    {
        return $"Persona: {Nombre}, ID: {Identificacion}, Llegada: {HoraLlegada:HH:mm:ss}, Asiento: {(NumeroAsiento > 0 ? NumeroAsiento.ToString() : "No asignado")}";
    }
}

// Clase que representa un asiento en el auditorio
public class Asiento
{
    public int Numero { get; set; }
    public bool Ocupado { get; set; }
    public Persona PersonaAsignada { get; set; }

    public Asiento(int numero)
    {
        Numero = numero;
        Ocupado = false;
        PersonaAsignada = null;
    }

    public void AsignarPersona(Persona persona)
    {
        if (!Ocupado)
        {
            PersonaAsignada = persona;
            Ocupado = true;
            persona.NumeroAsiento = Numero;
        }
    }

    public override string ToString()
    {
        return $"Asiento {Numero}: {(Ocupado ? $"Ocupado por {PersonaAsignada.Nombre}" : "Disponible")}";
    }
}

// Interfaz para operaciones de cola
public interface IColaOperaciones<T>
{
    void Enqueue(T item);
    T Dequeue();
    T Peek();
    int Count { get; }
    bool IsEmpty { get; }
    void Clear();
}

// Implementación personalizada de cola usando lista enlazada
public class ColaPersonalizada<T> : IColaOperaciones<T>
{
    private Queue<T> cola;
    private string nombre;

    public ColaPersonalizada(string nombre)
    {
        this.nombre = nombre;
        cola = new Queue<T>();
    }

    public string Nombre => nombre;
    public int Count => cola.Count;
    public bool IsEmpty => cola.Count == 0;

    public void Enqueue(T item)
    {
        cola.Enqueue(item);
        Console.WriteLine($"[{nombre}] Persona agregada a la cola. Total en cola: {Count}");
    }

    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException($"La cola {nombre} está vacía");
        
        T item = cola.Dequeue();
        Console.WriteLine($"[{nombre}] Persona atendida. Restantes en cola: {Count}");
        return item;
    }

    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException($"La cola {nombre} está vacía");
        
        return cola.Peek();
    }

    public void Clear()
    {
        cola.Clear();
        Console.WriteLine($"[{nombre}] Cola limpiada");
    }

    public List<T> ObtenerElementos()
    {
        return cola.ToList();
    }
}

// Clase principal que gestiona el auditorio
public class SistemaAuditorio
{
    private readonly int CAPACIDAD_MAXIMA = 100;
    private ColaPersonalizada<Persona> cola1;
    private ColaPersonalizada<Persona> cola2;
    private List<Asiento> asientos;
    private int siguienteAsiento;
    private List<Persona> personasAtendidas;

    public SistemaAuditorio()
    {
        cola1 = new ColaPersonalizada<Persona>("Cola 1");
        cola2 = new ColaPersonalizada<Persona>("Cola 2");
        asientos = new List<Asiento>();
        personasAtendidas = new List<Persona>();
        siguienteAsiento = 1;
        
        // Inicializar asientos
        for (int i = 1; i <= CAPACIDAD_MAXIMA; i++)
        {
            asientos.Add(new Asiento(i));
        }
    }

    // Agregar persona a la cola especificada
    public void AgregarPersona(string nombre, string identificacion, int numeroCola)
    {
        if (numeroCola != 1 && numeroCola != 2)
        {
            Console.WriteLine("Error: Solo existen las colas 1 y 2");
            return;
        }

        if (siguienteAsiento > CAPACIDAD_MAXIMA)
        {
            Console.WriteLine("Error: Auditorio lleno. No se pueden agregar más personas.");
            return;
        }

        Persona persona = new Persona(nombre, identificacion);
        
        if (numeroCola == 1)
            cola1.Enqueue(persona);
        else
            cola2.Enqueue(persona);
    }

    // Procesar ambas colas alternadamente
    public void ProcesarColas()
    {
        Console.WriteLine("\n=== INICIANDO PROCESAMIENTO DE COLAS ===");
        
        while ((!cola1.IsEmpty || !cola2.IsEmpty) && siguienteAsiento <= CAPACIDAD_MAXIMA)
        {
            // Procesar cola 1 si tiene elementos
            if (!cola1.IsEmpty)
            {
                ProcesarPersona(cola1);
            }
            
            // Procesar cola 2 si tiene elementos
            if (!cola2.IsEmpty && siguienteAsiento <= CAPACIDAD_MAXIMA)
            {
                ProcesarPersona(cola2);
            }
        }
        
        Console.WriteLine("\n=== PROCESAMIENTO COMPLETADO ===");
    }

    private void ProcesarPersona(ColaPersonalizada<Persona> cola)
    {
        if (siguienteAsiento > CAPACIDAD_MAXIMA)
        {
            Console.WriteLine("Auditorio lleno. No se pueden asignar más asientos.");
            return;
        }

        Persona persona = cola.Dequeue();
        Asiento asiento = asientos[siguienteAsiento - 1];
        
        asiento.AsignarPersona(persona);
        personasAtendidas.Add(persona);
        
        Console.WriteLine($"✓ Asiento {siguienteAsiento} asignado a {persona.Nombre} desde {cola.Nombre}");
        siguienteAsiento++;
    }

    // Reportería - Visualizar estado actual
    public void MostrarEstadoActual()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("REPORTE DEL ESTADO ACTUAL DEL SISTEMA");
        Console.WriteLine(new string('=', 50));
        
        Console.WriteLine($"Capacidad total: {CAPACIDAD_MAXIMA} asientos");
        Console.WriteLine($"Asientos ocupados: {personasAtendidas.Count}");
        Console.WriteLine($"Asientos disponibles: {CAPACIDAD_MAXIMA - personasAtendidas.Count}");
        Console.WriteLine($"Personas en Cola 1: {cola1.Count}");
        Console.WriteLine($"Personas en Cola 2: {cola2.Count}");
    }

    // Reportería - Mostrar elementos en las colas
    public void MostrarElementosEnColas()
    {
        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine("ELEMENTOS EN LAS COLAS");
        Console.WriteLine(new string('-', 40));
        
        Console.WriteLine($"\n{cola1.Nombre} ({cola1.Count} personas):");
        if (!cola1.IsEmpty)
        {
            var elementos1 = cola1.ObtenerElementos();
            for (int i = 0; i < elementos1.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {elementos1[i]}");
            }
        }
        else
        {
            Console.WriteLine("  (Vacía)");
        }
        
        Console.WriteLine($"\n{cola2.Nombre} ({cola2.Count} personas):");
        if (!cola2.IsEmpty)
        {
            var elementos2 = cola2.ObtenerElementos();
            for (int i = 0; i < elementos2.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {elementos2[i]}");
            }
        }
        else
        {
            Console.WriteLine("  (Vacía)");
        }
    }

    // Reportería - Mostrar asientos ocupados
    public void MostrarAsientosOcupados()
    {
        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine("ASIENTOS OCUPADOS");
        Console.WriteLine(new string('-', 40));
        
        var asientosOcupados = asientos.Where(a => a.Ocupado).OrderBy(a => a.Numero).ToList();
        
        if (asientosOcupados.Count > 0)
        {
            foreach (var asiento in asientosOcupados)
            {
                Console.WriteLine($"  {asiento}");
            }
        }
        else
        {
            Console.WriteLine("  No hay asientos ocupados");
        }
    }

    // Reportería - Mostrar orden de llegada vs orden de asignación
    public void MostrarOrdenAsignacion()
    {
        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine("ORDEN DE ASIGNACIÓN DE ASIENTOS");
        Console.WriteLine(new string('-', 40));
        
        var personasOrdenadas = personasAtendidas.OrderBy(p => p.NumeroAsiento).ToList();
        
        foreach (var persona in personasOrdenadas)
        {
            Console.WriteLine($"  Asiento {persona.NumeroAsiento}: {persona.Nombre} (Llegada: {persona.HoraLlegada:HH:mm:ss})");
        }
    }

    // Análisis de rendimiento
    public void AnalizarRendimiento()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("ANÁLISIS DE RENDIMIENTO");
        Console.WriteLine(new string('=', 50));
        
        // Simular operaciones y medir tiempo
        Stopwatch sw = new Stopwatch();
        
        // Tiempo de enqueue
        sw.Start();
        for (int i = 0; i < 1000; i++)
        {
            // Simulamos operación enqueue
            var tempCola = new Queue<int>();
            tempCola.Enqueue(i);
        }
        sw.Stop();
        long tiempoEnqueue = sw.ElapsedTicks;
        
        // Tiempo de dequeue
        sw.Restart();
        var colaTest = new Queue<int>();
        for (int i = 0; i < 1000; i++)
        {
            colaTest.Enqueue(i);
        }
        for (int i = 0; i < 1000; i++)
        {
            colaTest.Dequeue();
        }
        sw.Stop();
        long tiempoDequeue = sw.ElapsedTicks;
        
        Console.WriteLine($"Operaciones Enqueue (1000 elementos): {tiempoEnqueue} ticks");
        Console.WriteLine($"Operaciones Dequeue (1000 elementos): {tiempoDequeue} ticks");
        Console.WriteLine($"Complejidad temporal Enqueue: O(1)");
        Console.WriteLine($"Complejidad temporal Dequeue: O(1)");
        Console.WriteLine($"Complejidad espacial: O(n) donde n = número de elementos");
    }

    // Obtener estadísticas del sistema
    public void MostrarEstadisticas()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("ESTADÍSTICAS DEL SISTEMA");
        Console.WriteLine(new string('=', 50));
        
        Console.WriteLine($"Total de personas procesadas: {personasAtendidas.Count}");
        Console.WriteLine($"Porcentaje de ocupación: {(personasAtendidas.Count * 100.0 / CAPACIDAD_MAXIMA):F2}%");
        Console.WriteLine($"Personas restantes en colas: {cola1.Count + cola2.Count}");
        
        if (personasAtendidas.Count > 0)
        {
            var primerPersona = personasAtendidas.OrderBy(p => p.NumeroAsiento).First();
            var ultimaPersona = personasAtendidas.OrderBy(p => p.NumeroAsiento).Last();
            
            Console.WriteLine($"Primera persona atendida: {primerPersona.Nombre} (Asiento {primerPersona.NumeroAsiento})");
            Console.WriteLine($"Última persona atendida: {ultimaPersona.Nombre} (Asiento {ultimaPersona.NumeroAsiento})");
        }
    }
}

// Programa principal
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== SISTEMA DE ASIGNACIÓN DE ASIENTOS DEL AUDITORIO ===\n");
        
        SistemaAuditorio auditorio = new SistemaAuditorio();
        
        // Simular llegada de personas a ambas colas
        Console.WriteLine("Simulando llegada de personas...\n");
        
        // Agregar personas a la cola 1
        auditorio.AgregarPersona("Luis Samaniego", "0923552533", 1);
        auditorio.AgregarPersona("Liham Samaniego", "0916567809", 1);
        auditorio.AgregarPersona("Luis Samaniego jr", "1004446009", 1);
        auditorio.AgregarPersona("Loany Samaniego", "1745768900", 1);
        auditorio.AgregarPersona("Eloisa Bravo", "1300768900", 1);
        
        // Agregar personas a la cola 2
        auditorio.AgregarPersona("Laura Sánchez", "99999999", 2);
        auditorio.AgregarPersona("Diego Fernández", "88888888", 2);
        auditorio.AgregarPersona("Sofia Herrera", "55555555", 2);
        auditorio.AgregarPersona("Roberto Silva", "66666666", 2);
        auditorio.AgregarPersona("Joe Sevillano", "1726448489", 2);
        
        // Mostrar estado inicial
        auditorio.MostrarEstadoActual();
        auditorio.MostrarElementosEnColas();
        
        // Procesar las colas
        auditorio.ProcesarColas();
        
        // Mostrar reportes finales
        auditorio.MostrarEstadoActual();
        auditorio.MostrarAsientosOcupados();
        auditorio.MostrarOrdenAsignacion();
        auditorio.MostrarEstadisticas();
        auditorio.AnalizarRendimiento();
        
        Console.WriteLine("\n¡Presiona cualquier tecla para salir!");
        Console.ReadKey();
    }
}