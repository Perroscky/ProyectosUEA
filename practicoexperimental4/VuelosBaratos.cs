using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SistemaVuelos
{
    // Clase que representa un vuelo (arista del grafo)
    public class Vuelo
    {
        public string Origen { get; set; }
        public string Destino { get; set; }
        public double Precio { get; set; }
        public string Aerolinea { get; set; }
        public string NumeroVuelo { get; set; }

        public Vuelo(string origen, string destino, double precio, string aerolinea, string numeroVuelo)
        {
            Origen = origen;
            Destino = destino;
            Precio = precio;
            Aerolinea = aerolinea;
            NumeroVuelo = numeroVuelo;
        }

        public override string ToString()
        {
            return $"{NumeroVuelo} ({Aerolinea}): {Origen} → {Destino} - ${Precio:F2}";
        }
    }

    // Clase que representa el grafo de vuelos
    public class GrafoVuelos
    {
        // Diccionario de adyacencia: cada ciudad tiene una lista de vuelos salientes
        private Dictionary<string, List<Vuelo>> grafo;
        private HashSet<string> ciudades;

        public GrafoVuelos()
        {
            grafo = new Dictionary<string, List<Vuelo>>();
            ciudades = new HashSet<string>();
        }

        // Agregar un vuelo al grafo
        public void AgregarVuelo(string origen, string destino, double precio, string aerolinea, string numeroVuelo)
        {
            // Agregar ciudades al conjunto
            ciudades.Add(origen);
            ciudades.Add(destino);

            // Crear el vuelo
            Vuelo vuelo = new Vuelo(origen, destino, precio, aerolinea, numeroVuelo);

            // Agregar al grafo
            if (!grafo.ContainsKey(origen))
            {
                grafo[origen] = new List<Vuelo>();
            }
            grafo[origen].Add(vuelo);
        }

        // Obtener todas las ciudades
        public List<string> ObtenerCiudades()
        {
            return ciudades.OrderBy(c => c).ToList();
        }

        // Obtener vuelos desde una ciudad
        public List<Vuelo> ObtenerVuelosDesde(string ciudad)
        {
            if (grafo.ContainsKey(ciudad))
            {
                return grafo[ciudad];
            }
            return new List<Vuelo>();
        }

        // Mostrar toda la red de vuelos
        public void MostrarRedCompleta()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("           RED COMPLETA DE VUELOS DISPONIBLES");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            foreach (var ciudad in ciudades.OrderBy(c => c))
            {
                var vuelos = ObtenerVuelosDesde(ciudad);
                if (vuelos.Count > 0)
                {
                    Console.WriteLine($"📍 Desde {ciudad}:");
                    foreach (var vuelo in vuelos.OrderBy(v => v.Precio))
                    {
                        Console.WriteLine($"   ✈️  {vuelo}");
                    }
                    Console.WriteLine();
                }
            }
        }

        // Algoritmo de Dijkstra para encontrar la ruta más barata
        public (List<string> ruta, double costoTotal, List<Vuelo> vuelosRuta) EncontrarRutaMasBarata(string origen, string destino)
        {
            // Validar que existan las ciudades
            if (!ciudades.Contains(origen) || !ciudades.Contains(destino))
            {
                return (new List<string>(), double.MaxValue, new List<Vuelo>());
            }

            // Inicialización
            var distancias = new Dictionary<string, double>();
            var previos = new Dictionary<string, string>();
            var vueloPrevio = new Dictionary<string, Vuelo>();
            var noVisitados = new HashSet<string>(ciudades);

            foreach (var ciudad in ciudades)
            {
                distancias[ciudad] = double.MaxValue;
            }
            distancias[origen] = 0;

            while (noVisitados.Count > 0)
            {
                // Encontrar la ciudad no visitada con menor distancia
                string ciudadActual = null;
                double menorDistancia = double.MaxValue;

                foreach (var ciudad in noVisitados)
                {
                    if (distancias[ciudad] < menorDistancia)
                    {
                        menorDistancia = distancias[ciudad];
                        ciudadActual = ciudad;
                    }
                }

                if (ciudadActual == null || menorDistancia == double.MaxValue)
                    break;

                noVisitados.Remove(ciudadActual);

                // Si llegamos al destino, podemos terminar
                if (ciudadActual == destino)
                    break;

                // Explorar vecinos
                var vuelosDesde = ObtenerVuelosDesde(ciudadActual);
                foreach (var vuelo in vuelosDesde)
                {
                    double nuevaDistancia = distancias[ciudadActual] + vuelo.Precio;
                    if (nuevaDistancia < distancias[vuelo.Destino])
                    {
                        distancias[vuelo.Destino] = nuevaDistancia;
                        previos[vuelo.Destino] = ciudadActual;
                        vueloPrevio[vuelo.Destino] = vuelo;
                    }
                }
            }

            // Reconstruir la ruta
            var ruta = new List<string>();
            var vuelosRuta = new List<Vuelo>();
            string actual = destino;

            if (!previos.ContainsKey(destino))
            {
                return (new List<string>(), double.MaxValue, new List<Vuelo>());
            }

            while (actual != null)
            {
                ruta.Insert(0, actual);
                if (previos.ContainsKey(actual))
                {
                    vuelosRuta.Insert(0, vueloPrevio[actual]);
                    actual = previos[actual];
                }
                else
                {
                    break;
                }
            }

            return (ruta, distancias[destino], vuelosRuta);
        }

        // Encontrar todas las rutas directas entre dos ciudades
        public List<Vuelo> EncontrarVuelosDirectos(string origen, string destino)
        {
            var vuelosDirectos = new List<Vuelo>();
            var vuelosDesde = ObtenerVuelosDesde(origen);

            foreach (var vuelo in vuelosDesde)
            {
                if (vuelo.Destino == destino)
                {
                    vuelosDirectos.Add(vuelo);
                }
            }

            return vuelosDirectos.OrderBy(v => v.Precio).ToList();
        }

        // Obtener estadísticas del grafo
        public void MostrarEstadisticas()
        {
            int totalVuelos = grafo.Values.Sum(lista => lista.Count);
            double precioPromedio = grafo.Values.SelectMany(lista => lista).Average(v => v.Precio);
            double precioMin = grafo.Values.SelectMany(lista => lista).Min(v => v.Precio);
            double precioMax = grafo.Values.SelectMany(lista => lista).Max(v => v.Precio);

            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("              ESTADÍSTICAS DEL SISTEMA");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine($"📊 Total de ciudades: {ciudades.Count}");
            Console.WriteLine($"✈️  Total de vuelos: {totalVuelos}");
            Console.WriteLine($"💰 Precio promedio: ${precioPromedio:F2}");
            Console.WriteLine($"💵 Precio mínimo: ${precioMin:F2}");
            Console.WriteLine($"💸 Precio máximo: ${precioMax:F2}");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Crear el grafo de vuelos
            GrafoVuelos sistemaVuelos = new GrafoVuelos();

            // Base de datos ficticia de vuelos
            Console.WriteLine("Cargando base de datos de vuelos...\n");
            CargarBaseDatosVuelos(sistemaVuelos);

            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        sistemaVuelos.MostrarRedCompleta();
                        break;

                    case "2":
                        BuscarRutaMasBarata(sistemaVuelos);
                        break;

                    case "3":
                        BuscarVuelosDirectos(sistemaVuelos);
                        break;

                    case "4":
                        MostrarCiudadesDisponibles(sistemaVuelos);
                        break;

                    case "5":
                        ConsultarVuelosDesdeciudad(sistemaVuelos);
                        break;

                    case "6":
                        sistemaVuelos.MostrarEstadisticas();
                        break;

                    case "7":
                        AnalizarRendimiento(sistemaVuelos);
                        break;

                    case "8":
                        MostrarAnalisisEstructura();
                        break;

                    case "9":
                        continuar = false;
                        Console.WriteLine("\n✈️  ¡Gracias por usar el Sistema de Búsqueda de Vuelos!");
                        break;

                    default:
                        Console.WriteLine("\n❌ Opción no válida. Intente nuevamente.\n");
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

        static void CargarBaseDatosVuelos(GrafoVuelos sistema)
        {
            // Vuelos desde Quito
            sistema.AgregarVuelo("Quito", "Guayaquil", 80.00, "TAME", "TM101");
            sistema.AgregarVuelo("Quito", "Guayaquil", 95.00, "Avianca", "AV201");
            sistema.AgregarVuelo("Quito", "Cuenca", 70.00, "TAME", "TM102");
            sistema.AgregarVuelo("Quito", "Bogotá", 180.00, "Avianca", "AV301");
            sistema.AgregarVuelo("Quito", "Lima", 200.00, "LATAM", "LA401");
            sistema.AgregarVuelo("Quito", "Miami", 450.00, "Copa", "CM501");

            // Vuelos desde Guayaquil
            sistema.AgregarVuelo("Guayaquil", "Quito", 85.00, "TAME", "TM103");
            sistema.AgregarVuelo("Guayaquil", "Cuenca", 65.00, "TAME", "TM104");
            sistema.AgregarVuelo("Guayaquil", "Bogotá", 170.00, "Avianca", "AV302");
            sistema.AgregarVuelo("Guayaquil", "Lima", 190.00, "LATAM", "LA402");
            sistema.AgregarVuelo("Guayaquil", "Panamá", 280.00, "Copa", "CM601");
            sistema.AgregarVuelo("Guayaquil", "Miami", 420.00, "Copa", "CM502");

            // Vuelos desde Cuenca
            sistema.AgregarVuelo("Cuenca", "Quito", 75.00, "TAME", "TM105");
            sistema.AgregarVuelo("Cuenca", "Guayaquil", 68.00, "TAME", "TM106");
            sistema.AgregarVuelo("Cuenca", "Lima", 210.00, "LATAM", "LA403");

            // Vuelos desde Bogotá
            sistema.AgregarVuelo("Bogotá", "Quito", 175.00, "Avianca", "AV303");
            sistema.AgregarVuelo("Bogotá", "Guayaquil", 165.00, "Avianca", "AV304");
            sistema.AgregarVuelo("Bogotá", "Lima", 220.00, "LATAM", "LA404");
            sistema.AgregarVuelo("Bogotá", "Miami", 380.00, "Avianca", "AV601");
            sistema.AgregarVuelo("Bogotá", "Panamá", 190.00, "Copa", "CM602");

            // Vuelos desde Lima
            sistema.AgregarVuelo("Lima", "Quito", 195.00, "LATAM", "LA405");
            sistema.AgregarVuelo("Lima", "Guayaquil", 185.00, "LATAM", "LA406");
            sistema.AgregarVuelo("Lima", "Bogotá", 215.00, "LATAM", "LA407");
            sistema.AgregarVuelo("Lima", "Miami", 500.00, "LATAM", "LA701");
            sistema.AgregarVuelo("Lima", "Panamá", 320.00, "Copa", "CM603");

            // Vuelos desde Panamá
            sistema.AgregarVuelo("Panamá", "Guayaquil", 275.00, "Copa", "CM604");
            sistema.AgregarVuelo("Panamá", "Bogotá", 185.00, "Copa", "CM605");
            sistema.AgregarVuelo("Panamá", "Lima", 315.00, "Copa", "CM606");
            sistema.AgregarVuelo("Panamá", "Miami", 250.00, "Copa", "CM503");

            // Vuelos desde Miami
            sistema.AgregarVuelo("Miami", "Quito", 440.00, "Copa", "CM504");
            sistema.AgregarVuelo("Miami", "Guayaquil", 410.00, "Copa", "CM505");
            sistema.AgregarVuelo("Miami", "Bogotá", 375.00, "Avianca", "AV602");
            sistema.AgregarVuelo("Miami", "Lima", 495.00, "LATAM", "LA702");
            sistema.AgregarVuelo("Miami", "Panamá", 245.00, "Copa", "CM506");

            Console.WriteLine("✅ Base de datos cargada exitosamente!\n");
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║     SISTEMA DE BÚSQUEDA DE VUELOS BARATOS            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.WriteLine("\n  1️⃣  Ver red completa de vuelos");
            Console.WriteLine("  2️⃣  Buscar ruta más barata");
            Console.WriteLine("  3️⃣  Buscar vuelos directos");
            Console.WriteLine("  4️⃣  Ver ciudades disponibles");
            Console.WriteLine("  5️⃣  Consultar vuelos desde una ciudad");
            Console.WriteLine("  6️⃣  Ver estadísticas del sistema");
            Console.WriteLine("  7️⃣  Análisis de rendimiento");
            Console.WriteLine("  8️⃣  Análisis de estructura de datos");
            Console.WriteLine("  9️⃣  Salir");
            Console.Write("\n➤ Seleccione una opción: ");
        }

        static void BuscarRutaMasBarata(GrafoVuelos sistema)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║           BÚSQUEDA DE RUTA MÁS BARATA               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            Console.Write("Ciudad de origen: ");
            string origen = Console.ReadLine();

            Console.Write("Ciudad de destino: ");
            string destino = Console.ReadLine();

            Stopwatch sw = Stopwatch.StartNew();
            var resultado = sistema.EncontrarRutaMasBarata(origen, destino);
            sw.Stop();

            if (resultado.ruta.Count == 0)
            {
                Console.WriteLine($"\n❌ No se encontró ruta entre {origen} y {destino}");
            }
            else
            {
                Console.WriteLine("\n✅ ¡Ruta encontrada!");
                Console.WriteLine("\n═══════════════════════════════════════════════════════");
                Console.WriteLine($"💰 Costo total: ${resultado.costoTotal:F2}");
                Console.WriteLine($"🔄 Escalas: {resultado.vuelosRuta.Count - 1}");
                Console.WriteLine($"⏱️  Tiempo de búsqueda: {sw.ElapsedMilliseconds} ms");
                Console.WriteLine("═══════════════════════════════════════════════════════\n");

                Console.WriteLine("📍 Itinerario:");
                for (int i = 0; i < resultado.vuelosRuta.Count; i++)
                {
                    var vuelo = resultado.vuelosRuta[i];
                    Console.WriteLine($"\n   Vuelo {i + 1}:");
                    Console.WriteLine($"   {vuelo}");
                }

                Console.WriteLine("\n📊 Ruta completa: " + string.Join(" → ", resultado.ruta));
            }
        }

        static void BuscarVuelosDirectos(GrafoVuelos sistema)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            BÚSQUEDA DE VUELOS DIRECTOS               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            Console.Write("Ciudad de origen: ");
            string origen = Console.ReadLine();

            Console.Write("Ciudad de destino: ");
            string destino = Console.ReadLine();

            Stopwatch sw = Stopwatch.StartNew();
            var vuelosDirectos = sistema.EncontrarVuelosDirectos(origen, destino);
            sw.Stop();

            if (vuelosDirectos.Count == 0)
            {
                Console.WriteLine($"\n❌ No hay vuelos directos entre {origen} y {destino}");
            }
            else
            {
                Console.WriteLine($"\n✅ Se encontraron {vuelosDirectos.Count} vuelo(s) directo(s):");
                Console.WriteLine($"⏱️  Tiempo de búsqueda: {sw.ElapsedMilliseconds} ms\n");

                foreach (var vuelo in vuelosDirectos)
                {
                    Console.WriteLine($"   ✈️  {vuelo}");
                }
            }
        }

        static void MostrarCiudadesDisponibles(GrafoVuelos sistema)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            CIUDADES DISPONIBLES                      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            var ciudades = sistema.ObtenerCiudades();
            int count = 1;
            foreach (var ciudad in ciudades)
            {
                Console.WriteLine($"   {count}. 📍 {ciudad}");
                count++;
            }
        }

        static void ConsultarVuelosDesdeciudad(GrafoVuelos sistema)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║        CONSULTAR VUELOS DESDE UNA CIUDAD            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            Console.Write("Ingrese la ciudad: ");
            string ciudad = Console.ReadLine();

            var vuelos = sistema.ObtenerVuelosDesde(ciudad);

            if (vuelos.Count == 0)
            {
                Console.WriteLine($"\n❌ No hay vuelos desde {ciudad}");
            }
            else
            {
                Console.WriteLine($"\n✈️  Vuelos disponibles desde {ciudad}:\n");
                foreach (var vuelo in vuelos.OrderBy(v => v.Precio))
                {
                    Console.WriteLine($"   {vuelo}");
                }
            }
        }

        static void AnalizarRendimiento(GrafoVuelos sistema)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ANÁLISIS DE RENDIMIENTO                    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            var ciudades = sistema.ObtenerCiudades();
            
            // Análisis de búsqueda de ruta más barata
            Console.WriteLine("🔍 Analizando algoritmo de Dijkstra...\n");

            var tiempos = new List<long>();
            int iteraciones = 100;

            for (int i = 0; i < iteraciones; i++)
            {
                var origen = ciudades[new Random().Next(ciudades.Count)];
                var destino = ciudades[new Random().Next(ciudades.Count)];

                Stopwatch sw = Stopwatch.StartNew();
                sistema.EncontrarRutaMasBarata(origen, destino);
                sw.Stop();

                tiempos.Add(sw.ElapsedTicks);
            }

            double promedioTicks = tiempos.Average();
            double promedioMs = tiempos.Average() * 1000.0 / Stopwatch.Frequency;

            Console.WriteLine($"📊 Resultados de {iteraciones} búsquedas aleatorias:");
            Console.WriteLine($"   ⏱️  Tiempo promedio: {promedioMs:F4} ms");
            Console.WriteLine($"   ⚡ Tiempo mínimo: {tiempos.Min() * 1000.0 / Stopwatch.Frequency:F4} ms");
            Console.WriteLine($"   🐢 Tiempo máximo: {tiempos.Max() * 1000.0 / Stopwatch.Frequency:F4} ms");

            // Complejidad teórica
            int V = ciudades.Count; // Número de vértices
            Console.WriteLine($"\n📈 Complejidad teórica:");
            Console.WriteLine($"   • Dijkstra implementado: O((V + E) log V)");
            Console.WriteLine($"   • En este grafo: V = {V} ciudades");
            Console.WriteLine($"   • Búsqueda de vuelos directos: O(E) donde E es el grado del nodo");
        }

        static void MostrarAnalisisEstructura()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║        ANÁLISIS DE ESTRUCTURA DE DATOS              ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            Console.WriteLine("📊 ESTRUCTURA UTILIZADA: GRAFO DIRIGIDO PONDERADO\n");

            Console.WriteLine("✅ VENTAJAS:");
            Console.WriteLine("   • Representación natural de redes de vuelos");
            Console.WriteLine("   • Permite modelar conexiones directas e indirectas");
            Console.WriteLine("   • Fácil agregar o eliminar rutas dinámicamente");
            Console.WriteLine("   • Soporta múltiples vuelos entre dos ciudades");
            Console.WriteLine("   • Algoritmo de Dijkstra eficiente para camino más corto");
            Console.WriteLine("   • Escalable para redes grandes de vuelos\n");

            Console.WriteLine("❌ DESVENTAJAS:");
            Console.WriteLine("   • Consumo de memoria puede ser alto con muchas rutas");
            Console.WriteLine("   • Requiere actualización constante de precios");
            Console.WriteLine("   • No considera restricciones de tiempo/horarios");
            Console.WriteLine("   • Dijkstra puede ser lento en grafos muy densos");
            Console.WriteLine("   • No optimiza por múltiples criterios simultáneos\n");

            Console.WriteLine("🔧 IMPLEMENTACIÓN:");
            Console.WriteLine("   • Dictionary<string, List<Vuelo>> para lista de adyacencia");
            Console.WriteLine("   • Complejidad espacial: O(V + E)");
            Console.WriteLine("   • V = número de ciudades, E = número de vuelos\n");

            Console.WriteLine("⏱️  COMPLEJIDAD TEMPORAL:");
            Console.WriteLine("   • Búsqueda ruta más barata (Dijkstra): O((V + E) log V)");
            Console.WriteLine("   • Búsqueda vuelos directos: O(E_v) donde E_v = vuelos desde v");
            Console.WriteLine("   • Agregar vuelo: O(1)");
            Console.WriteLine("   • Listar vuelos desde ciudad: O(E_v)\n");

            Console.WriteLine("💡 CASOS DE USO IDEALES:");
            Console.WriteLine("   • Sistemas de reserva de vuelos");
            Console.WriteLine("   • Planificación de rutas de transporte");
            Console.WriteLine("   • Análisis de conectividad entre ciudades");
            Console.WriteLine("   • Optimización de costos de viaje");
        }
    }
}
