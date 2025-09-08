using System;
using System.Collections.Generic;

namespace CatalogoRevistas
{
    class Program
    {
        // Lista que contendrá el catálogo de revistas
        static List<string> catalogoRevistas = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("=== CATÁLOGO DE REVISTAS ===");
            Console.WriteLine();

            // Inicializar el catálogo con títulos de revistas
            InicializarCatalogo();

            int opcion;
            do
            {
                MostrarMenu();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        MostrarCatalogo();
                        break;
                    case 2:
                        BuscarTituloRecursivo();
                        break;
                    case 3:
                        BuscarTituloIterativo();
                        break;
                    case 4:
                        AgregarRevista();
                        break;
                    case 5:
                        Console.WriteLine("\n¡Gracias por usar el catálogo de revistas!");
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Intente nuevamente.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 5);
        }

        /// <summary>
        /// Inicializa el catálogo con al menos 10 títulos de revistas
        /// </summary>
        static void InicializarCatalogo()
        {
            catalogoRevistas.Add("National Geographic");
            catalogoRevistas.Add("Time Magazine");
            catalogoRevistas.Add("Scientific American");
            catalogoRevistas.Add("Reader's Digest");
            catalogoRevistas.Add("Vogue");
            catalogoRevistas.Add("Popular Science");
            catalogoRevistas.Add("Forbes");
            catalogoRevistas.Add("Cosmopolitan");
            catalogoRevistas.Add("Nature");
            catalogoRevistas.Add("The Economist");
            catalogoRevistas.Add("Wired");
            catalogoRevistas.Add("Sports Illustrated");

            Console.WriteLine($"Catálogo inicializado con {catalogoRevistas.Count} revistas.\n");
        }

        /// <summary>
        /// Muestra el menú principal de opciones
        /// </summary>
        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("=== CATÁLOGO DE REVISTAS ===");
            Console.WriteLine($"Total de revistas: {catalogoRevistas.Count}");
            Console.WriteLine();
            Console.WriteLine("MENÚ DE OPCIONES:");
            Console.WriteLine("1. Mostrar catálogo completo");
            Console.WriteLine("2. Buscar título (Búsqueda Recursiva)");
            Console.WriteLine("3. Buscar título (Búsqueda Iterativa)");
            Console.WriteLine("4. Agregar nueva revista");
            Console.WriteLine("5. Salir");
            Console.WriteLine();
            Console.Write("Seleccione una opción (1-5): ");
        }

        /// <summary>
        /// Lee y valida la opción seleccionada por el usuario
        /// </summary>
        static int LeerOpcion()
        {
            if (int.TryParse(Console.ReadLine(), out int opcion))
            {
                return opcion;
            }
            return -1; // Opción inválida
        }

        /// <summary>
        /// Muestra todas las revistas del catálogo
        /// </summary>
        static void MostrarCatalogo()
        {
            Console.WriteLine("\n=== CATÁLOGO COMPLETO ===");
            Console.WriteLine();
            for (int i = 0; i < catalogoRevistas.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}. {catalogoRevistas[i]}");
            }
        }

        /// <summary>
        /// Realiza búsqueda usando algoritmo recursivo
        /// </summary>
        static void BuscarTituloRecursivo()
        {
            Console.WriteLine("\n=== BÚSQUEDA RECURSIVA ===");
            Console.Write("Ingrese el título a buscar: ");
            string titulo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                Console.WriteLine("Debe ingresar un título válido.");
                return;
            }

            bool encontrado = BusquedaRecursiva(catalogoRevistas, titulo.Trim(), 0);

            Console.WriteLine();
            Console.WriteLine($"Resultado: {(encontrado ? "Encontrado" : "No encontrado")}");

            if (encontrado)
            {
                int posicion = BuscarPosicionRecursiva(catalogoRevistas, titulo.Trim(), 0);
                Console.WriteLine($"Posición en el catálogo: {posicion + 1}");
            }
        }

        /// <summary>
        /// Implementación recursiva de búsqueda
        /// </summary>
        static bool BusquedaRecursiva(List<string> lista, string titulo, int indice)
        {
            // Caso base: si hemos llegado al final de la lista
            if (indice >= lista.Count)
            {
                return false;
            }

            // Si encontramos el título (comparación sin distinción de mayúsculas/minúsculas)
            if (string.Equals(lista[indice], titulo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Llamada recursiva con el siguiente índice
            return BusquedaRecursiva(lista, titulo, indice + 1);
        }

        /// <summary>
        /// Encuentra la posición del elemento usando recursión
        /// </summary>
        static int BuscarPosicionRecursiva(List<string> lista, string titulo, int indice)
        {
            if (indice >= lista.Count)
            {
                return -1;
            }

            if (string.Equals(lista[indice], titulo, StringComparison.OrdinalIgnoreCase))
            {
                return indice;
            }

            return BuscarPosicionRecursiva(lista, titulo, indice + 1);
        }

        /// <summary>
        /// Realiza búsqueda usando algoritmo iterativo
        /// </summary>
        static void BuscarTituloIterativo()
        {
            Console.WriteLine("\n=== BÚSQUEDA ITERATIVA ===");
            Console.Write("Ingrese el título a buscar: ");
            string titulo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                Console.WriteLine("Debe ingresar un título válido.");
                return;
            }

            var resultado = BusquedaIterativa(catalogoRevistas, titulo.Trim());

            Console.WriteLine();
            Console.WriteLine($"Resultado: {(resultado.encontrado ? "Encontrado" : "No encontrado")}");

            if (resultado.encontrado)
            {
                Console.WriteLine($"Posición en el catálogo: {resultado.posicion + 1}");
            }
        }

        /// <summary>
        /// Implementación iterativa de búsqueda
        /// </summary>
        static (bool encontrado, int posicion) BusquedaIterativa(List<string> lista, string titulo)
        {
            // Recorremos la lista de forma iterativa
            for (int i = 0; i < lista.Count; i++)
            {
                // Comparación sin distinción de mayúsculas/minúsculas
                if (string.Equals(lista[i], titulo, StringComparison.OrdinalIgnoreCase))
                {
                    return (true, i);
                }
            }

            return (false, -1);
        }

        /// <summary>
        /// Permite agregar una nueva revista al catálogo
        /// </summary>
        static void AgregarRevista()
        {
            Console.WriteLine("\n=== AGREGAR NUEVA REVISTA ===");
            Console.Write("Ingrese el nombre de la nueva revista: ");
            string nuevaRevista = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nuevaRevista))
            {
                Console.WriteLine("Debe ingresar un nombre válido.");
                return;
            }

            // Verificar si ya existe
            var resultado = BusquedaIterativa(catalogoRevistas, nuevaRevista.Trim());
            if (resultado.encontrado)
            {
                Console.WriteLine("Esta revista ya existe en el catálogo.");
                return;
            }

            // Agregar la nueva revista
            catalogoRevistas.Add(nuevaRevista.Trim());
            Console.WriteLine($"Revista '{nuevaRevista.Trim()}' agregada exitosamente.");
            Console.WriteLine($"Total de revistas en el catálogo: {catalogoRevistas.Count}");
        }
    }
}