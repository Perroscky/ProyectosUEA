using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Crear y llenar la lista con los números del 1 al 10
        List<int> numeros = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            numeros.Add(i);
        }

        // Mostrar en orden inverso separados por comas
        Console.WriteLine("Números en orden inverso:");
        for (int i = numeros.Count - 1; i >= 0; i--)
        {
            Console.Write(numeros[i]);
            if (i != 0)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine(); // Salto de línea final
    }
}
