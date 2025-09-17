using System;

namespace ArbolBinario
{
    // Clase que representa un nodo del árbol binario
    public class NodoArbol
    {
        public int Valor { get; set; }
        public NodoArbol Izquierdo { get; set; }
        public NodoArbol Derecho { get; set; }

        // Constructor del nodo
        public NodoArbol(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    // Clase que implementa el Árbol Binario de Búsqueda
    public class ArbolBinarioBusqueda
    {
        private NodoArbol raiz;

        // Constructor del árbol
        public ArbolBinarioBusqueda()
        {
            raiz = null;
        }

        // Método público para insertar un valor
        public void Insertar(int valor)
        {
            raiz = InsertarRecursivo(raiz, valor);
        }

        // Método privado recursivo para insertar un nodo
        private NodoArbol InsertarRecursivo(NodoArbol nodo, int valor)
        {
            // Si el nodo es null, crear un nuevo nodo
            if (nodo == null)
            {
                nodo = new NodoArbol(valor);
                return nodo;
            }

            // Si el valor es menor, insertar en el subárbol izquierdo
            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            }
            // Si el valor es mayor, insertar en el subárbol derecho
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
            }
            // Si el valor ya existe, no hacer nada (evitar duplicados)

            return nodo;
        }

        // Método público para buscar un valor
        public bool Buscar(int valor)
        {
            return BuscarRecursivo(raiz, valor);
        }

        // Método privado recursivo para buscar un valor
        private bool BuscarRecursivo(NodoArbol nodo, int valor)
        {
            // Si el nodo es null, el valor no existe
            if (nodo == null)
                return false;

            // Si encontramos el valor
            if (valor == nodo.Valor)
                return true;

            // Buscar en el subárbol izquierdo o derecho según corresponda
            if (valor < nodo.Valor)
                return BuscarRecursivo(nodo.Izquierdo, valor);
            else
                return BuscarRecursivo(nodo.Derecho, valor);
        }

        // Método público para eliminar un nodo
        public void Eliminar(int valor)
        {
            raiz = EliminarRecursivo(raiz, valor);
        }

        // Método privado recursivo para eliminar un nodo
        private NodoArbol EliminarRecursivo(NodoArbol nodo, int valor)
        {
            // Si el nodo es null, no hay nada que eliminar
            if (nodo == null)
                return nodo;

            // Buscar el nodo a eliminar
            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
            }
            else
            {
                // Nodo encontrado, proceder con la eliminación
                
                // Caso 1: Nodo hoja (sin hijos)
                if (nodo.Izquierdo == null && nodo.Derecho == null)
                {
                    return null;
                }
                
                // Caso 2: Nodo con un solo hijo
                if (nodo.Izquierdo == null)
                {
                    return nodo.Derecho;
                }
                if (nodo.Derecho == null)
                {
                    return nodo.Izquierdo;
                }
                
                // Caso 3: Nodo con dos hijos
                // Encontrar el sucesor inorden (menor valor en subárbol derecho)
                int sucesorValor = EncontrarMinimo(nodo.Derecho);
                
                // Reemplazar el valor del nodo actual con el sucesor
                nodo.Valor = sucesorValor;
                
                // Eliminar el sucesor
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesorValor);
            }

            return nodo;
        }

        // Método auxiliar para encontrar el valor mínimo en un subárbol
        private int EncontrarMinimo(NodoArbol nodo)
        {
            while (nodo.Izquierdo != null)
            {
                nodo = nodo.Izquierdo;
            }
            return nodo.Valor;
        }

        // Recorrido Inorden (Izquierda - Raíz - Derecha)
        public void RecorridoInorden()
        {
            Console.Write("Recorrido Inorden: ");
            InordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void InordenRecursivo(NodoArbol nodo)
        {
            if (nodo != null)
            {
                InordenRecursivo(nodo.Izquierdo);  // Visitar subárbol izquierdo
                Console.Write(nodo.Valor + " ");    // Visitar raíz
                InordenRecursivo(nodo.Derecho);     // Visitar subárbol derecho
            }
        }

        // Recorrido Preorden (Raíz - Izquierda - Derecha)
        public void RecorridoPreorden()
        {
            Console.Write("Recorrido Preorden: ");
            PreordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PreordenRecursivo(NodoArbol nodo)
        {
            if (nodo != null)
            {
                Console.Write(nodo.Valor + " ");    // Visitar raíz
                PreordenRecursivo(nodo.Izquierdo);  // Visitar subárbol izquierdo
                PreordenRecursivo(nodo.Derecho);    // Visitar subárbol derecho
            }
        }

        // Recorrido Postorden (Izquierda - Derecha - Raíz)
        public void RecorridoPostorden()
        {
            Console.Write("Recorrido Postorden: ");
            PostordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PostordenRecursivo(NodoArbol nodo)
        {
            if (nodo != null)
            {
                PostordenRecursivo(nodo.Izquierdo); // Visitar subárbol izquierdo
                PostordenRecursivo(nodo.Derecho);   // Visitar subárbol derecho
                Console.Write(nodo.Valor + " ");    // Visitar raíz
            }
        }

        // Método para verificar si el árbol está vacío
        public bool EstaVacio()
        {
            return raiz == null;
        }

        // Método para mostrar la estructura del árbol de forma visual
        public void MostrarArbol()
        {
            if (raiz == null)
            {
                Console.WriteLine("El árbol está vacío.");
                return;
            }
            
            Console.WriteLine("\nEstructura del árbol:");
            MostrarArbolRecursivo(raiz, "", true);
        }

        private void MostrarArbolRecursivo(NodoArbol nodo, string prefijo, bool esUltimo)
        {
            if (nodo != null)
            {
                Console.WriteLine(prefijo + (esUltimo ? "└── " : "├── ") + nodo.Valor);
                
                string nuevoPrefijo = prefijo + (esUltimo ? "    " : "│   ");
                
                // Mostrar hijo derecho primero (para mejor visualización)
                if (nodo.Derecho != null || nodo.Izquierdo != null)
                {
                    MostrarArbolRecursivo(nodo.Derecho, nuevoPrefijo, nodo.Izquierdo == null);
                    MostrarArbolRecursivo(nodo.Izquierdo, nuevoPrefijo, true);
                }
            }
        }
    }

    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
            int opcion;

            Console.WriteLine("=== ÁRBOL BINARIO DE BÚSQUEDA ===");
            Console.WriteLine("Proyecto Universitario - Estructuras de Datos\n");

            do
            {
                MostrarMenu();
                
                // Validar entrada del usuario
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    continue;
                }

                Console.WriteLine(); // Línea en blanco para mejor legibilidad

                switch (opcion)
                {
                    case 1:
                        InsertarNodo(arbol);
                        break;
                    case 2:
                        BuscarNodo(arbol);
                        break;
                    case 3:
                        EliminarNodo(arbol);
                        break;
                    case 4:
                        MostrarRecorridos(arbol);
                        break;
                    case 5:
                        arbol.MostrarArbol();
                        break;
                    case 0:
                        Console.WriteLine("¡Gracias por usar el programa!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción del menú.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 0);
        }

        // Método para mostrar el menú principal
        static void MostrarMenu()
        {
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║            MENÚ PRINCIPAL             ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Insertar nodo                      ║");
            Console.WriteLine("║ 2. Buscar nodo                        ║");
            Console.WriteLine("║ 3. Eliminar nodo                      ║");
            Console.WriteLine("║ 4. Mostrar recorridos                 ║");
            Console.WriteLine("║ 5. Mostrar estructura del árbol       ║");
            Console.WriteLine("║ 0. Salir                              ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");
        }

        // Método para insertar un nodo
        static void InsertarNodo(ArbolBinarioBusqueda arbol)
        {
            Console.Write("Ingrese el valor a insertar: ");
            
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                arbol.Insertar(valor);
                Console.WriteLine($"Valor {valor} insertado correctamente.");
            }
            else
            {
                Console.WriteLine("Error: Debe ingresar un número entero válido.");
            }
        }

        // Método para buscar un nodo
        static void BuscarNodo(ArbolBinarioBusqueda arbol)
        {
            if (arbol.EstaVacio())
            {
                Console.WriteLine("El árbol está vacío. No hay elementos para buscar.");
                return;
            }

            Console.Write("Ingrese el valor a buscar: ");
            
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                bool encontrado = arbol.Buscar(valor);
                
                if (encontrado)
                {
                    Console.WriteLine($"✓ El valor {valor} SÍ se encuentra en el árbol.");
                }
                else
                {
                    Console.WriteLine($"✗ El valor {valor} NO se encuentra en el árbol.");
                }
            }
            else
            {
                Console.WriteLine("Error: Debe ingresar un número entero válido.");
            }
        }

        // Método para eliminar un nodo
        static void EliminarNodo(ArbolBinarioBusqueda arbol)
        {
            if (arbol.EstaVacio())
            {
                Console.WriteLine("El árbol está vacío. No hay elementos para eliminar.");
                return;
            }

            Console.Write("Ingrese el valor a eliminar: ");
            
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                if (arbol.Buscar(valor))
                {
                    arbol.Eliminar(valor);
                    Console.WriteLine($"Valor {valor} eliminado correctamente.");
                }
                else
                {
                    Console.WriteLine($"El valor {valor} no existe en el árbol.");
                }
            }
            else
            {
                Console.WriteLine("Error: Debe ingresar un número entero válido.");
            }
        }

        // Método para mostrar todos los recorridos
        static void MostrarRecorridos(ArbolBinarioBusqueda arbol)
        {
            if (arbol.EstaVacio())
            {
                Console.WriteLine("El árbol está vacío. No hay elementos para mostrar.");
                return;
            }

            Console.WriteLine("RECORRIDOS DEL ÁRBOL:");
            Console.WriteLine("═════════════════════");
            arbol.RecorridoInorden();
            arbol.RecorridoPreorden();
            arbol.RecorridoPostorden();
        }
    }
}