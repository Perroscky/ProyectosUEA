using System;
using System.Collections.Generic;
using System.Linq;

namespace LoteriaPrivitiva
{
    // Clase que representa un número de lotería
    public class NumeroLoteria
    {
        public int Valor { get; set; }
        
        public NumeroLoteria(int valor)
        {
            Valor = valor;
        }
        
        // Validar si el número está en el rango válido de la lotería primitiva (1-100)
        public bool EsNumeroValido()
        {
            return Valor >= 1 && Valor <= 100;
        }
        
        public override string ToString()
        {
            return Valor.ToString();
        }
        
        public override bool Equals(object obj)
        {
            if (obj is NumeroLoteria otro)
                return Valor == otro.Valor;
            return false;
        }
        
        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
    
    // Clase que gestiona los números ganadores de la lotería
    public class GestorLoteriaPrivitiva
    {
        private List<NumeroLoteria> numerosGanadores;
        public int CantidadNumeros { get; private set; }
        
        public GestorLoteriaPrivitiva(int cantidadNumeros = 6)
        {
            CantidadNumeros = cantidadNumeros;
            numerosGanadores = new List<NumeroLoteria>();
        }
        
        // Método principal: solicitar números ganadores (equivalente al bucle for de Python)
        public void SolicitarNumerosGanadores()
        {
            Console.WriteLine("=== INTRODUCCIÓN DE NÚMEROS GANADORES ===");
            Console.WriteLine($"Introduce los {CantidadNumeros} números ganadores de la lotería primitiva (1-100):\n");
            
            for (int i = 0; i < CantidadNumeros; i++)
            {
                bool numeroValido = false;
                
                while (!numeroValido)
                {
                    Console.Write($"Introduce el número ganador {i + 1}: ");
                    string entrada = Console.ReadLine();
                    
                    if (int.TryParse(entrada, out int numero))
                    {
                        NumeroLoteria nuevoNumero = new NumeroLoteria(numero);
                        
                        if (nuevoNumero.EsNumeroValido())
                        {
                            if (!numerosGanadores.Contains(nuevoNumero))
                            {
                                numerosGanadores.Add(nuevoNumero);
                                numeroValido = true;
                            }
                            else
                            {
                                Console.WriteLine("¡Ese número ya fue introducido! Introduce un número diferente.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Por favor, introduce un número entre 1 y 100.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Por favor, introduce un número válido.");
                    }
                }
            }
        }
        
        // Método para ordenar los números (equivalente al .sort() de Python)
        public void OrdenarNumeros()
        {
            numerosGanadores = numerosGanadores.OrderBy(n => n.Valor).ToList();
        }
        
        // Método para mostrar números ordenados (equivalente al print final de Python)
        public void MostrarNumerosGanadores()
        {
            OrdenarNumeros();
            
            Console.WriteLine("\n=== NÚMEROS GANADORES ===");
            var numerosOrdenados = numerosGanadores.Select(n => n.Valor).ToList();
            Console.WriteLine($"Los números ganadores son [{string.Join(", ", numerosOrdenados)}]");
        }
        
        // Versión más elegante de mostrar los números
        public void MostrarNumerosGanadoresDetallado()
        {
            OrdenarNumeros();
            
            Console.WriteLine("\n=== NÚMEROS GANADORES DETALLADOS ===");
            Console.WriteLine("Números ganadores ordenados de menor a mayor:");
            
            for (int i = 0; i < numerosGanadores.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {numerosGanadores[i].Valor}");
            }
            
            Console.WriteLine($"\nCombinación ganadora: {string.Join(" - ", numerosGanadores.Select(n => n.Valor))}");
        }
        
        // Método para mostrar estadísticas
        public void MostrarEstadisticas()
        {
            if (numerosGanadores.Count == 0) return;
            
            OrdenarNumeros();
            
            Console.WriteLine("\n=== ESTADÍSTICAS ===");
            Console.WriteLine($"Cantidad de números: {numerosGanadores.Count}");
            Console.WriteLine($"Número más bajo: {numerosGanadores.Min(n => n.Valor)}");
            Console.WriteLine($"Número más alto: {numerosGanadores.Max(n => n.Valor)}");
            Console.WriteLine($"Suma total: {numerosGanadores.Sum(n => n.Valor)}");
            Console.WriteLine($"Promedio: {numerosGanadores.Average(n => n.Valor):F1}");
            
            // Verificar si hay números consecutivos
            var consecutivos = new List<string>();
            for (int i = 0; i < numerosGanadores.Count - 1; i++)
            {
                if (numerosGanadores[i + 1].Valor - numerosGanadores[i].Valor == 1)
                {
                    consecutivos.Add($"{numerosGanadores[i].Valor}-{numerosGanadores[i + 1].Valor}");
                }
            }
            
            if (consecutivos.Count > 0)
            {
                Console.WriteLine($"Números consecutivos: {string.Join(", ", consecutivos)}");
            }
            else
            {
                Console.WriteLine("No hay números consecutivos.");
            }
        }
        
        // Método para verificar si un número está entre los ganadores
        public bool EsNumeroGanador(int numero)
        {
            return numerosGanadores.Any(n => n.Valor == numero);
        }
        
        // Método para obtener los números ganadores (solo lectura)
        public IReadOnlyList<int> ObtenerNumerosGanadores()
        {
            OrdenarNumeros();
            return numerosGanadores.Select(n => n.Valor).ToList().AsReadOnly();
        }
        
        // Método para simular un boleto de lotería
        public void SimularBoleto()
        {
            Console.WriteLine("\n=== SIMULACIÓN DE BOLETO ===");
            Console.WriteLine("Genera números aleatorios para comparar con los ganadores:");
            
            Random random = new Random();
            List<int> boletoAleatorio = new List<int>();
            
            while (boletoAleatorio.Count < CantidadNumeros)
            {
                int numeroAleatorio = random.Next(1, 101);
                if (!boletoAleatorio.Contains(numeroAleatorio))
                {
                    boletoAleatorio.Add(numeroAleatorio);
                }
            }
            
            boletoAleatorio.Sort();
            Console.WriteLine($"Boleto simulado: [{string.Join(", ", boletoAleatorio)}]");
            
            int aciertos = boletoAleatorio.Count(n => EsNumeroGanador(n));
            Console.WriteLine($"Aciertos: {aciertos} de {CantidadNumeros}");
        }
    }
    
    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE LOTERÍA PRIMITIVA ===\n");
            
            // Crear instancia del gestor de lotería
            GestorLoteriaPrivitiva miLoteria = new GestorLoteriaPrivitiva();
            
            // Solicitar números ganadores (equivalente al bucle for de Python)
            miLoteria.SolicitarNumerosGanadores();
            
            // Mostrar números ordenados (equivalente al sort() y print() de Python)
            miLoteria.MostrarNumerosGanadores();
            
            // Funcionalidad adicional
            miLoteria.MostrarNumerosGanadoresDetallado();
            miLoteria.MostrarEstadisticas();
            miLoteria.SimularBoleto();
            
            Console.WriteLine("\n" + new string('=', 101));
            Console.WriteLine("¡Gracias por usar el sistema de lotería primitiva!");
            
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
            
            List<int> awarded = new List<int>();
            
            // Bucle for para introducir 6 números (equivalente al range(6) de Python)
            for (int i = 0; i < 6; i++)
            {
                Console.Write("Introduce un número ganador: ");
                int numero = int.Parse(Console.ReadLine());
                awarded.Add(numero);
            }
            
            // Ordenar la lista (equivalente al .sort() de Python)
            awarded.Sort();
            
            // Mostrar resultado (equivalente al print() de Python)
            Console.WriteLine($"Los números ganadores son [{string.Join(", ", awarded)}]");
        }
    }
}