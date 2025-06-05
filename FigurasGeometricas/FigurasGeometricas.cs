using System;

// Clase para crear un círculo
public class Circulo
{
    // Variable privada para guardar el radio (tipo de dato primitivo double)
    private double radio;
    
    // Constructor: se ejecuta cuando creamos un nuevo círculo
    public Circulo(double radio)
    {
        this.radio = radio;
    }
    
    // Método para obtener el radio
    public double GetRadio()
    {
        return radio;
    }
    
    // Método para cambiar el radio
    public void SetRadio(double nuevoRadio)
    {
        radio = nuevoRadio;
    }
    
    // Calcular el área es una función que devuelve un valor double, se utiliza para calcular el área de un círculo
    // Fórmula del área: π × radio × radio
    public double CalcularArea()
    {
        return Math.PI * radio * radio;
    }
    
    // Calcular el perimetro es una función que devuelve un valor double, se utiliza para calcular el perímetro de un círculo
    // Fórmula del perímetro: 2 × π × radio
    public double CalcularPerimetro()
    {
        return 2 * Math.PI * radio;
    }
}

// Clase para crear un cuadrado
public class Cuadrado
{
    // Variable privada para guardar el lado (tipo de dato primitivo double)
    private double lado;
    
    // Constructor: se ejecuta cuando creamos un nuevo cuadrado
    public Cuadrado(double lado)
    {
        this.lado = lado;
    }
    
    // Método para obtener el lado
    public double GetLado()
    {
        return lado;
    }
    
    // Método para cambiar el lado
    public void SetLado(double nuevoLado)
    {
        lado = nuevoLado;
    }
    
    // Calcular el área es una función que devuelve un valor double, se utiliza para calcular el área de un cuadrado
    // Fórmula del área: lado × lado
    public double CalcularArea()
    {
        return lado * lado;
    }
    
    // Calcular el perimetro es una función que devuelve un valor double, se utiliza para calcular el perímetro de un cuadrado  
    // Fórmula del perímetro: 4 × lado
    public double CalcularPerimetro()
    {
        return 4 * lado;
    }
}

// Programa principal para probar nuestras clases
public class Program
{
    public static void Main()
    {
        // Crear un círculo con radio 5
        Circulo miCirculo = new Circulo(6.0);
        
        Console.WriteLine("=== MI CÍRCULO ===");
        Console.WriteLine("Radio: " + miCirculo.GetRadio());
        Console.WriteLine("Área: " + miCirculo.CalcularArea());
        Console.WriteLine("Perímetro: " + miCirculo.CalcularPerimetro());
        
        Console.WriteLine(); // Línea en blanco
        
        // Cambiar el radio del círculo
        miCirculo.SetRadio(3.0);
        Console.WriteLine("Después de cambiar el radio a 3:");
        Console.WriteLine("Radio: " + miCirculo.GetRadio());
        Console.WriteLine("Área: " + miCirculo.CalcularArea());
        Console.WriteLine("Perímetro: " + miCirculo.CalcularPerimetro());
        
        Console.WriteLine(); // Línea en blanco
        Console.WriteLine("=====================================");
        Console.WriteLine(); // Línea en blanco
        
        // Crear un cuadrado con lado 4
        Cuadrado miCuadrado = new Cuadrado(5.0);
        
        Console.WriteLine("=== MI CUADRADO ===");
        Console.WriteLine("Lado: " + miCuadrado.GetLado());
        Console.WriteLine("Área: " + miCuadrado.CalcularArea());
        Console.WriteLine("Perímetro: " + miCuadrado.CalcularPerimetro());
        
        Console.WriteLine(); // Línea en blanco
        
        // Cambiar el lado del cuadrado
        miCuadrado.SetLado(6.0);
        Console.WriteLine("Después de cambiar el lado a 6:");
        Console.WriteLine("Lado: " + miCuadrado.GetLado());
        Console.WriteLine("Área: " + miCuadrado.CalcularArea());
        Console.WriteLine("Perímetro: " + miCuadrado.CalcularPerimetro());
        
        Console.WriteLine(); // Línea en blanco
        Console.WriteLine("Presiona Enter para terminar...");
        Console.ReadLine();
    }
}
