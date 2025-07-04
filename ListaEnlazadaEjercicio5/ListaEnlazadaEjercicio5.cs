using System;

// Clase para representar un nodo de la lista enlazada
public class Nodo
{
    public int Dato { get; set; }
    public Nodo Siguiente { get; set; }
    
    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

// Clase para manejar la lista enlazada
public class ListaEnlazada
{
    private Nodo cabeza;
    private int contador;
    
    public ListaEnlazada()
    {
        cabeza = null;
        contador = 0;
    }
    
    // Propiedad para obtener el número de elementos
    public int Contador => contador;
    
    // Método para agregar al final de la lista
    public void AgregarAlFinal(int dato)
    {
        Nodo nuevoNodo = new Nodo(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
        contador++;
    }
    
    // Método para agregar al inicio de la lista
    public void AgregarAlInicio(int dato)
    {
        Nodo nuevoNodo = new Nodo(dato);
        nuevoNodo.Siguiente = cabeza;
        cabeza = nuevoNodo;
        contador++;
    }
    
    // Método para mostrar todos los elementos
    public void MostrarElementos(string tipoLista)
    {
        Console.WriteLine($"\n--- {tipoLista} ---");
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }
        
        Nodo actual = cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}

// Clase principal del programa
public class ProgramaListasEnlazadas
{
    private ListaEnlazada listaPrimos;
    private ListaEnlazada listaArmstrong;
    
    public ProgramaListasEnlazadas()
    {
        listaPrimos = new ListaEnlazada();
        listaArmstrong = new ListaEnlazada();
    }
    
    // Método para verificar si un número es primo
    private bool EsPrimo(int numero)
    {
        if (numero <= 1) return false;
        if (numero <= 3) return true;
        if (numero % 2 == 0 || numero % 3 == 0) return false;
        
        for (int i = 5; i * i <= numero; i += 6)
        {
            if (numero % i == 0 || numero % (i + 2) == 0)
                return false;
        }
        return true;
    }
    
    // Método para verificar si un número es Armstrong
    private bool EsArmstrong(int numero)
    {
        int numeroOriginal = numero;
        int suma = 0;
        int digitos = ContarDigitos(numero);
        
        while (numero > 0)
        {
            int digito = numero % 10;
            suma += (int)Math.Pow(digito, digitos);
            numero /= 10;
        }
        
        return suma == numeroOriginal;
    }
    
    // Método auxiliar para contar dígitos
    private int ContarDigitos(int numero)
    {
        if (numero == 0) return 1;
        int contador = 0;
        while (numero > 0)
        {
            contador++;
            numero /= 10;
        }
        return contador;
    }
    
    // Método para procesar un número y agregarlo a la lista correspondiente
    public void ProcesarNumero(int numero)
    {
        if (EsPrimo(numero))
        {
            listaPrimos.AgregarAlFinal(numero);
            Console.WriteLine($"{numero} es primo - agregado al final de la lista de primos.");
        }
        else if (EsArmstrong(numero))
        {
            listaArmstrong.AgregarAlInicio(numero);
            Console.WriteLine($"{numero} es Armstrong - agregado al inicio de la lista de Armstrong.");
        }
        else
        {
            Console.WriteLine($"{numero} no es ni primo ni Armstrong - no se agrega a ninguna lista.");
        }
    }
    
    // Método para mostrar los resultados finales
    public void MostrarResultados()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("RESULTADOS FINALES");
        Console.WriteLine(new string('=', 50));
        
        // a. Número de datos insertados en cada lista
        Console.WriteLine($"a) Número de elementos en lista de primos: {listaPrimos.Contador}");
        Console.WriteLine($"   Número de elementos en lista de Armstrong: {listaArmstrong.Contador}");
        
        // b. Lista que contiene más elementos
        Console.WriteLine("\nb) Lista con más elementos:");
        if (listaPrimos.Contador > listaArmstrong.Contador)
        {
            Console.WriteLine($"   La lista de números primos tiene más elementos ({listaPrimos.Contador} elementos).");
        }
        else if (listaArmstrong.Contador > listaPrimos.Contador)
        {
            Console.WriteLine($"   La lista de números Armstrong tiene más elementos ({listaArmstrong.Contador} elementos).");
        }
        else
        {
            Console.WriteLine($"   Ambas listas tienen la misma cantidad de elementos ({listaPrimos.Contador} elementos).");
        }
        
        // c. Mostrar todos los datos insertados
        Console.WriteLine("\nc) Datos insertados en las listas:");
        listaPrimos.MostrarElementos("NÚMEROS PRIMOS (agregados al final)");
        listaArmstrong.MostrarElementos("NÚMEROS ARMSTRONG (agregados al inicio)");
    }
    
    // Método principal para ejecutar el programa
    public void Ejecutar()
    {
        Console.WriteLine("=== PROGRAMA DE LISTAS ENLAZADAS ===");
        Console.WriteLine("Números Primos y Números Armstrong\n");
        
        Console.WriteLine("Ingrese números enteros (ingrese 0 para terminar):");
        
        int numero;
        do
        {
            Console.Write("Ingrese un número: ");
            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.Write("Por favor, ingrese un número entero válido: ");
            }
            
            if (numero != 0)
            {
                ProcesarNumero(numero);
            }
            
        } while (numero != 0);
        
        MostrarResultados();
    }
}

// Clase principal con el método Main
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ProgramaListasEnlazadas programa = new ProgramaListasEnlazadas();
            programa.Ejecutar();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}