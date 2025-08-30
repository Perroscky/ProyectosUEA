using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraductorBasico
{
    public class Traductor
    {
        // Diccionarios para traducción bidireccional
        private Dictionary<string, string> inglesEspanol;
        private Dictionary<string, string> espanolIngles;

        public Traductor()
        {
            InicializarDiccionarios();
        }

        private void InicializarDiccionarios()
        {
            inglesEspanol = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "time", "tiempo" },
                { "person", "persona" },
                { "year", "año" },
                { "way", "camino" },
                { "day", "día" },
                { "thing", "cosa" },
                { "man", "hombre" },
                { "world", "mundo" },
                { "life", "vida" },
                { "hand", "mano" },
                { "part", "parte" },
                { "child", "niño" },
                { "eye", "ojo" },
                { "woman", "mujer" },
                { "place", "lugar" },
                { "work", "trabajo" },
                { "week", "semana" },
                { "case", "caso" },
                { "point", "punto" },
                { "government", "gobierno" },
                { "company", "empresa" }
            };

            // Crear diccionario inverso para español-inglés
            espanolIngles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var par in inglesEspanol)
            {
                espanolIngles[par.Value] = par.Key;
            }
        }

        public string TraducirFrase(string frase)
        {
            if (string.IsNullOrWhiteSpace(frase))
                return string.Empty;

            // Separar la frase en palabras manteniendo la puntuación
            var palabras = SepararPalabras(frase);
            var fraseTraducida = new StringBuilder();

            foreach (var elemento in palabras)
            {
                if (EsPalabra(elemento))
                {
                    string palabraTraducida = TraducirPalabra(elemento);
                    fraseTraducida.Append(palabraTraducida);
                }
                else
                {
                    // Es puntuación o espacio, mantenerlo tal como está
                    fraseTraducida.Append(elemento);
                }
            }

            return fraseTraducida.ToString();
        }

        private List<string> SepararPalabras(string frase)
        {
            var elementos = new List<string>();
            var palabraActual = new StringBuilder();

            foreach (char c in frase)
            {
                if (char.IsLetter(c))
                {
                    palabraActual.Append(c);
                }
                else
                {
                    if (palabraActual.Length > 0)
                    {
                        elementos.Add(palabraActual.ToString());
                        palabraActual.Clear();
                    }
                    elementos.Add(c.ToString());
                }
            }

            if (palabraActual.Length > 0)
            {
                elementos.Add(palabraActual.ToString());
            }

            return elementos;
        }

        private bool EsPalabra(string elemento)
        {
            return !string.IsNullOrEmpty(elemento) && char.IsLetter(elemento[0]);
        }

        private string TraducirPalabra(string palabra)
        {
            // Intentar traducir de inglés a español
            if (inglesEspanol.ContainsKey(palabra))
            {
                return inglesEspanol[palabra];
            }
            
            // Intentar traducir de español a inglés
            if (espanolIngles.ContainsKey(palabra))
            {
                return espanolIngles[palabra];
            }

            // Si no se encuentra traducción, devolver la palabra original
            return palabra;
        }

        public bool AgregarPalabra(string ingles, string espanol)
        {
            if (string.IsNullOrWhiteSpace(ingles) || string.IsNullOrWhiteSpace(espanol))
                return false;

            ingles = ingles.Trim().ToLower();
            espanol = espanol.Trim().ToLower();

            // Agregar a ambos diccionarios
            inglesEspanol[ingles] = espanol;
            espanolIngles[espanol] = ingles;

            return true;
        }

        public void MostrarDiccionario()
        {
            Console.WriteLine("\n=== DICCIONARIO ACTUAL ===");
            Console.WriteLine("Inglés → Español");
            Console.WriteLine("─────────────────────────");
            
            var palabrasOrdenadas = inglesEspanol.OrderBy(p => p.Key);
            
            foreach (var par in palabrasOrdenadas)
            {
                Console.WriteLine($"{par.Key.PadRight(15)} → {par.Value}");
            }
            Console.WriteLine($"\nTotal de palabras: {inglesEspanol.Count}");
        }

        public int CantidadPalabras()
        {
            return inglesEspanol.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var traductor = new Traductor();
            bool continuar = true;

            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║     TRADUCTOR BÁSICO INGLÉS-ESPAÑOL   ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine($"Diccionario inicializado con {traductor.CantidadPalabras()} palabras.\n");

            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        TraducirFrase(traductor);
                        break;
                    case "2":
                        AgregarPalabras(traductor);
                        break;
                    case "3":
                        traductor.MostrarDiccionario();
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("\n¡Gracias por usar el traductor! ¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("\n❌ Opción inválida. Por favor, seleccione una opción válida.\n");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("3. Ver diccionario completo");
            Console.WriteLine("0. Salir");
            Console.WriteLine("===============================================");
            Console.Write("Seleccione una opción: ");
        }

        static void TraducirFrase(Traductor traductor)
        {
            Console.WriteLine("\n--- TRADUCIR FRASE ---");
            Console.WriteLine("Ingrese la frase a traducir (en inglés o español):");
            Console.Write("> ");
            
            string frase = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(frase))
            {
                Console.WriteLine("❌ No se ingresó ninguna frase.");
                return;
            }

            string traduccion = traductor.TraducirFrase(frase);
            
            Console.WriteLine("\n📝 Frase original:");
            Console.WriteLine($"   {frase}");
            Console.WriteLine("\n🔄 Traducción:");
            Console.WriteLine($"   {traduccion}");
            
            // Mostrar estadísticas de traducción
            var palabrasOriginales = frase.Split(new char[] { ' ', '.', ',', ';', ':', '!', '?' }, 
                StringSplitOptions.RemoveEmptyEntries);
            var palabrasTraducidas = traduccion.Split(new char[] { ' ', '.', ',', ';', ':', '!', '?' }, 
                StringSplitOptions.RemoveEmptyEntries);
            
            int palabrasCoincidentes = 0;
            for (int i = 0; i < Math.Min(palabrasOriginales.Length, palabrasTraducidas.Length); i++)
            {
                if (!palabrasOriginales[i].Equals(palabrasTraducidas[i], StringComparison.OrdinalIgnoreCase))
                {
                    palabrasCoincidentes++;
                }
            }
            
            Console.WriteLine($"\n📊 Se tradujeron {palabrasCoincidentes} de {palabrasOriginales.Length} palabras.");
        }

        static void AgregarPalabras(Traductor traductor)
        {
            Console.WriteLine("\n--- AGREGAR PALABRAS ---");
            
            while (true)
            {
                Console.WriteLine("\nIngrese la palabra en inglés (o 'volver' para regresar al menú):");
                Console.Write("> ");
                string ingles = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(ingles) || 
                    ingles.Trim().ToLower() == "volver")
                {
                    break;
                }

                Console.WriteLine("Ingrese la traducción en español:");
                Console.Write("> ");
                string espanol = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(espanol))
                {
                    Console.WriteLine("❌ La traducción no puede estar vacía.");
                    continue;
                }

                bool agregada = traductor.AgregarPalabra(ingles, espanol);
                
                if (agregada)
                {
                    Console.WriteLine($"✅ Palabra agregada: {ingles} → {espanol}");
                    Console.WriteLine($"📈 Total de palabras en el diccionario: {traductor.CantidadPalabras()}");
                }
                else
                {
                    Console.WriteLine("❌ Error al agregar la palabra.");
                }
                
                Console.WriteLine("\n¿Desea agregar otra palabra? (s/n):");
                Console.Write("> ");
                string respuesta = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(respuesta) || 
                    !respuesta.Trim().ToLower().StartsWith("s"))
                {
                    break;
                }
            }
        }
    }
}