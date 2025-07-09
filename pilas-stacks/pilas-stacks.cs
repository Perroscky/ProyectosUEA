using System;
using System.Collections.Generic;

namespace EjercicioPilas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIOS CON PILAS (STACKS) ===\n");

            // Ejercicio 1: Verificación de paréntesis balanceados
            Console.WriteLine("1. VERIFICACIÓN DE PARÉNTESIS BALANCEADOS");
            Console.WriteLine("----------------------------------------");
            
            // Casos de prueba para verificación de paréntesis
            string[] expresiones = {
                "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}",
                "(2 + 3) * [4 - 1]",
                "{[(2 + 3) * 4] - 1}",
                "((2 + 3) * 4",
                "{[2 + 3) * 4]}"
            };

            foreach (string expresion in expresiones)
            {
                bool resultado = VerificarParentesisBalanceados(expresion);
                Console.WriteLine($"Expresión: {expresion}");
                Console.WriteLine($"Resultado: {(resultado ? "Fórmula balanceada" : "Fórmula NO balanceada")}\n");
            }

            // Ejercicio 2: Torres de Hanoi
            Console.WriteLine("2. TORRES DE HANOI");
            Console.WriteLine("------------------");
            
            int numDiscos = 3;
            Console.WriteLine($"Resolviendo Torres de Hanoi con {numDiscos} discos:\n");
            
            ResolverTorresHanoi(numDiscos);

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        /// <summary>
        /// Verifica si los paréntesis, llaves y corchetes están balanceados en una expresión
        /// Utiliza el principio LIFO de las pilas para validar el emparejamiento correcto
        /// </summary>
        /// <param name="expresion">La expresión matemática a verificar</param>
        /// <returns>True si está balanceada, False en caso contrario</returns>
        static bool VerificarParentesisBalanceados(string expresion)
        {
            // Crear una pila para almacenar los símbolos de apertura
            Stack<char> pila = new Stack<char>();

            // Recorrer cada carácter de la expresión
            foreach (char caracter in expresion)
            {
                // Si es un símbolo de apertura, agregarlo a la pila (Push)
                if (caracter == '(' || caracter == '{' || caracter == '[')
                {
                    pila.Push(caracter);
                }
                // Si es un símbolo de cierre, verificar el emparejamiento
                else if (caracter == ')' || caracter == '}' || caracter == ']')
                {
                    // Si la pila está vacía, no hay símbolo de apertura correspondiente
                    if (pila.Count == 0)
                        return false;

                    // Sacar el último símbolo de apertura de la pila (Pop)
                    char simboloApertura = pila.Pop();

                    // Verificar si el par de símbolos coincide
                    if (!EsParejaCorrecto(simboloApertura, caracter))
                        return false;
                }
            }

            // La expresión está balanceada si la pila está vacía al final
            return pila.Count == 0;
        }

        /// <summary>
        /// Verifica si dos símbolos forman una pareja correcta
        /// </summary>
        /// <param name="apertura">Símbolo de apertura</param>
        /// <param name="cierre">Símbolo de cierre</param>
        /// <returns>True si forman una pareja correcta</returns>
        static bool EsParejaCorrecto(char apertura, char cierre)
        {
            return (apertura == '(' && cierre == ')') ||
                   (apertura == '{' && cierre == '}') ||
                   (apertura == '[' && cierre == ']');
        }

        /// <summary>
        /// Resuelve el problema de las Torres de Hanoi utilizando pilas
        /// Implementa el algoritmo recursivo clásico con visualización paso a paso
        /// </summary>
        /// <param name="numDiscos">Número de discos a mover</param>
        static void ResolverTorresHanoi(int numDiscos)
        {
            // Crear las tres torres como pilas
            Stack<int> torreA = new Stack<int>(); // Torre origen
            Stack<int> torreB = new Stack<int>(); // Torre auxiliar
            Stack<int> torreC = new Stack<int>(); // Torre destino

            // Inicializar la torre A con los discos (del más grande al más pequeño)
            for (int i = numDiscos; i >= 1; i--)
            {
                torreA.Push(i);
            }

            Console.WriteLine("Estado inicial:");
            MostrarTorres(torreA, torreB, torreC);
            Console.WriteLine();

            // Contador de movimientos
            int contadorMovimientos = 0;

            // Resolver usando recursividad
            MoverDiscos(numDiscos, torreA, torreC, torreB, 'A', 'C', 'B', ref contadorMovimientos);

            Console.WriteLine($"Problema resuelto en {contadorMovimientos} movimientos!");
            Console.WriteLine("\nEstado final:");
            MostrarTorres(torreA, torreB, torreC);
        }

        /// <summary>
        /// Función recursiva para mover discos entre torres
        /// Implementa el algoritmo clásico de Torres de Hanoi
        /// </summary>
        /// <param name="n">Número de discos a mover</param>
        /// <param name="origen">Torre origen</param>
        /// <param name="destino">Torre destino</param>
        /// <param name="auxiliar">Torre auxiliar</param>
        /// <param name="nombreOrigen">Nombre de la torre origen</param>
        /// <param name="nombreDestino">Nombre de la torre destino</param>
        /// <param name="nombreAuxiliar">Nombre de la torre auxiliar</param>
        /// <param name="contador">Contador de movimientos</param>
        static void MoverDiscos(int n, Stack<int> origen, Stack<int> destino, Stack<int> auxiliar,
                               char nombreOrigen, char nombreDestino, char nombreAuxiliar, ref int contador)
        {
            // Caso base: si solo hay un disco, moverlo directamente
            if (n == 1)
            {
                // Sacar el disco de la torre origen (Pop)
                int disco = origen.Pop();
                
                // Colocarlo en la torre destino (Push)
                destino.Push(disco);
                
                contador++;
                Console.WriteLine($"Movimiento {contador}: Mover disco {disco} de torre {nombreOrigen} a torre {nombreDestino}");
                
                // Mostrar el estado actual de las torres
                MostrarTorres(
                    nombreOrigen == 'A' ? origen : (nombreOrigen == 'B' ? auxiliar : destino),
                    nombreOrigen == 'B' ? origen : (nombreAuxiliar == 'B' ? auxiliar : destino),
                    nombreOrigen == 'C' ? origen : (nombreDestino == 'C' ? destino : auxiliar)
                );
                Console.WriteLine();
            }
            else
            {
                // Paso 1: Mover n-1 discos de origen a auxiliar
                MoverDiscos(n - 1, origen, auxiliar, destino, nombreOrigen, nombreAuxiliar, nombreDestino, ref contador);
                
                // Paso 2: Mover el disco más grande de origen a destino
                MoverDiscos(1, origen, destino, auxiliar, nombreOrigen, nombreDestino, nombreAuxiliar, ref contador);
                
                // Paso 3: Mover n-1 discos de auxiliar a destino
                MoverDiscos(n - 1, auxiliar, destino, origen, nombreAuxiliar, nombreDestino, nombreOrigen, ref contador);
            }
        }

        /// <summary>
        /// Muestra el estado actual de las tres torres
        /// Utiliza la operación Peek para ver los elementos sin eliminarlos
        /// </summary>
        /// <param name="torreA">Torre A</param>
        /// <param name="torreB">Torre B</param>
        /// <param name="torreC">Torre C</param>
        static void MostrarTorres(Stack<int> torreA, Stack<int> torreB, Stack<int> torreC)
        {
            Console.WriteLine($"Torre A: {MostrarPila(torreA)}");
            Console.WriteLine($"Torre B: {MostrarPila(torreB)}");
            Console.WriteLine($"Torre C: {MostrarPila(torreC)}");
        }

        /// <summary>
        /// Convierte una pila en una representación de string para mostrar
        /// Utiliza foreach para recorrer los elementos sin modificar la pila
        /// </summary>
        /// <param name="pila">La pila a mostrar</param>
        /// <returns>Representación en string de la pila</returns>
        static string MostrarPila(Stack<int> pila)
        {
            if (pila.Count == 0)
                return "[]";

            string resultado = "[";
            bool primero = true;

            // Recorrer la pila sin modificarla (de arriba hacia abajo)
            foreach (int disco in pila)
            {
                if (!primero)
                    resultado += ", ";
                resultado += disco;
                primero = false;
            }

            resultado += "]";
            return resultado;
        }
    }
}