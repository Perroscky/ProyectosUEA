using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaRegistroTorneoFutbol
{
    public class Jugador : IComparable<Jugador>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Posicion { get; set; }

        public Jugador()
        {
            Id = 0;
            Nombre = string.Empty;
            Edad = 0;
            Posicion = string.Empty;
        }

        public Jugador(int id, string nombre, int edad, string posicion)
        {
            Id = id;
            Nombre = nombre;
            Edad = edad;
            Posicion = posicion;
        }

        public int CompareTo(Jugador? other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }

        public void Mostrar()
        {
            Console.WriteLine($"ID: {Id} | Nombre: {Nombre} | Edad: {Edad} | Posicion: {Posicion}");
        }
    }

    public class Equipo
    {
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Entrenador { get; set; }
        public SortedSet<Jugador> Jugadores { get; set; }

        public Equipo()
        {
            Nombre = string.Empty;
            Ciudad = string.Empty;
            Entrenador = string.Empty;
            Jugadores = new SortedSet<Jugador>();
        }

        public Equipo(string nombre, string ciudad, string entrenador)
        {
            Nombre = nombre;
            Ciudad = ciudad;
            Entrenador = entrenador;
            Jugadores = new SortedSet<Jugador>();
        }

        public bool AgregarJugador(Jugador jugador)
        {
            if (Jugadores.Count >= 23)
            {
                Console.WriteLine("Error: El equipo ya tiene el maximo de 23 jugadores.");
                return false;
            }

            if (Jugadores.Contains(jugador))
            {
                Console.WriteLine("Error: El jugador ya esta en el equipo.");
                return false;
            }

            Jugadores.Add(jugador);
            return true;
        }

        public bool RemoverJugador(int idJugador)
        {
            var jugador = Jugadores.FirstOrDefault(j => j.Id == idJugador);
            if (jugador != null)
            {
                Jugadores.Remove(jugador);
                return true;
            }
            return false;
        }

        public void Mostrar()
        {
            Console.WriteLine($"\n=== EQUIPO: {Nombre} ===");
            Console.WriteLine($"Ciudad: {Ciudad}");
            Console.WriteLine($"Entrenador: {Entrenador}");
            Console.WriteLine($"Numero de jugadores: {Jugadores.Count}");
            Console.WriteLine("Jugadores:");

            if (Jugadores.Count == 0)
            {
                Console.WriteLine("  No hay jugadores registrados.");
            }
            else
            {
                foreach (var jugador in Jugadores)
                {
                    Console.Write("  ");
                    jugador.Mostrar();
                }
            }
            Console.WriteLine("========================================");
        }
    }

    public class SistemaTorneo
    {
        private Dictionary<string, Equipo> equipos;
        private Dictionary<int, string> jugadorEquipo;
        private HashSet<int> idsJugadores;
        private HashSet<string> posicionesValidas;

        public SistemaTorneo()
        {
            equipos = new Dictionary<string, Equipo>();
            jugadorEquipo = new Dictionary<int, string>();
            idsJugadores = new HashSet<int>();
            posicionesValidas = new HashSet<string> { "Portero", "Defensa", "Mediocampista", "Delantero" };
        }

        public bool RegistrarEquipo(string nombre, string ciudad, string entrenador)
        {
            if (equipos.ContainsKey(nombre))
            {
                Console.WriteLine("Error: Ya existe un equipo con ese nombre.");
                return false;
            }

            equipos[nombre] = new Equipo(nombre, ciudad, entrenador);
            Console.WriteLine($"Equipo '{nombre}' registrado exitosamente.");
            return true;
        }

        public bool RegistrarJugador(int id, string nombre, int edad, string posicion, string nombreEquipo)
        {
            if (idsJugadores.Contains(id))
            {
                Console.WriteLine($"Error: Ya existe un jugador con ID {id}.");
                return false;
            }

            if (!posicionesValidas.Contains(posicion))
            {
                Console.WriteLine("Error: Posicion no valida. Use: Portero, Defensa, Mediocampista o Delantero");
                return false;
            }

            if (!equipos.ContainsKey(nombreEquipo))
            {
                Console.WriteLine($"Error: El equipo '{nombreEquipo}' no existe.");
                return false;
            }

            var nuevoJugador = new Jugador(id, nombre, edad, posicion);
            if (equipos[nombreEquipo].AgregarJugador(nuevoJugador))
            {
                idsJugadores.Add(id);
                jugadorEquipo[id] = nombreEquipo;
                Console.WriteLine($"Jugador '{nombre}' registrado en '{nombreEquipo}'.");
                return true;
            }

            return false;
        }

        public bool TransferirJugador(int idJugador, string equipoDestino)
        {
            if (!jugadorEquipo.ContainsKey(idJugador))
            {
                Console.WriteLine($"Error: Jugador con ID {idJugador} no existe.");
                return false;
            }

            if (!equipos.ContainsKey(equipoDestino))
            {
                Console.WriteLine($"Error: Equipo destino '{equipoDestino}' no existe.");
                return false;
            }

            string equipoOrigen = jugadorEquipo[idJugador];
            if (equipoOrigen == equipoDestino)
            {
                Console.WriteLine("Error: El jugador ya esta en ese equipo.");
                return false;
            }

            var jugadorATransferir = equipos[equipoOrigen].Jugadores.FirstOrDefault(j => j.Id == idJugador);

            if (jugadorATransferir != null)
            {
                equipos[equipoOrigen].RemoverJugador(idJugador);

                if (equipos[equipoDestino].AgregarJugador(jugadorATransferir))
                {
                    jugadorEquipo[idJugador] = equipoDestino;
                    Console.WriteLine($"Jugador transferido de '{equipoOrigen}' a '{equipoDestino}'.");
                    return true;
                }
                else
                {
                    equipos[equipoOrigen].AgregarJugador(jugadorATransferir);
                    return false;
                }
            }

            return false;
        }

        public void MostrarTodosLosEquipos()
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            Console.WriteLine("\n=== TODOS LOS EQUIPOS DEL TORNEO ===");
            foreach (var equipo in equipos.Values)
            {
                equipo.Mostrar();
            }
        }

        public void BuscarJugador(int id)
        {
            if (!jugadorEquipo.ContainsKey(id))
            {
                Console.WriteLine($"Jugador con ID {id} no encontrado.");
                return;
            }

            string nombreEquipo = jugadorEquipo[id];
            var jugador = equipos[nombreEquipo].Jugadores.FirstOrDefault(j => j.Id == id);

            if (jugador != null)
            {
                Console.WriteLine("\n=== INFORMACION DEL JUGADOR ===");
                jugador.Mostrar();
                Console.WriteLine($"Equipo: {nombreEquipo}");
            }
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n=== ESTADISTICAS DEL TORNEO ===");
            Console.WriteLine($"Numero total de equipos: {equipos.Count}");
            Console.WriteLine($"Numero total de jugadores: {idsJugadores.Count}");

            var jugadoresPorPosicion = new Dictionary<string, int>();

            foreach (var equipo in equipos.Values)
            {
                foreach (var jugador in equipo.Jugadores)
                {
                    if (jugadoresPorPosicion.ContainsKey(jugador.Posicion))
                        jugadoresPorPosicion[jugador.Posicion]++;
                    else
                        jugadoresPorPosicion[jugador.Posicion] = 1;
                }
            }

            Console.WriteLine("\nJugadores por posicion:");
            foreach (var posicion in jugadoresPorPosicion)
            {
                Console.WriteLine($"  {posicion.Key}: {posicion.Value}");
            }

            if (equipos.Count > 0)
            {
                var equipoMasJugadores = equipos.Values.OrderByDescending(e => e.Jugadores.Count).First();
                Console.WriteLine($"\nEquipo con mas jugadores: {equipoMasJugadores.Nombre} ({equipoMasJugadores.Jugadores.Count} jugadores)");
            }
        }

        public bool EliminarJugador(int id)
        {
            if (!jugadorEquipo.ContainsKey(id))
            {
                Console.WriteLine($"Error: Jugador con ID {id} no existe.");
                return false;
            }

            string nombreEquipo = jugadorEquipo[id];

            if (equipos[nombreEquipo].RemoverJugador(id))
            {
                idsJugadores.Remove(id);
                jugadorEquipo.Remove(id);
                Console.WriteLine($"Jugador con ID {id} eliminado exitosamente.");
                return true;
            }

            return false;
        }
    }

    public class Program
    {
        public static void MostrarMenu()
        {
            Console.WriteLine("\n=== SISTEMA DE GESTION DE TORNEO DE FUTBOL ===");
            Console.WriteLine("1. Registrar equipo");
            Console.WriteLine("2. Registrar jugador");
            Console.WriteLine("3. Transferir jugador");
            Console.WriteLine("4. Mostrar todos los equipos");
            Console.WriteLine("5. Buscar jugador por ID");
            Console.WriteLine("6. Mostrar estadisticas");
            Console.WriteLine("7. Eliminar jugador");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opcion: ");
        }

        public static void Main(string[] args)
        {
            var torneo = new SistemaTorneo();
            int opcion;

            Console.WriteLine("Inicializando con datos de prueba...");
            torneo.RegistrarEquipo("Real Madrid", "Madrid", "Carlo Ancelotti");
            torneo.RegistrarEquipo("Barcelona", "Barcelona", "Xavi Hernandez");
            torneo.RegistrarEquipo("Atletico Madrid", "Madrid", "Diego Simeone");

            torneo.RegistrarJugador(1, "Lionel Messi", 36, "Delantero", "Barcelona");
            torneo.RegistrarJugador(2, "Sergio Ramos", 37, "Defensa", "Real Madrid");
            torneo.RegistrarJugador(3, "Jan Oblak", 30, "Portero", "Atletico Madrid");
            torneo.RegistrarJugador(4, "Pedri Gonzalez", 21, "Mediocampista", "Barcelona");

            do
            {
                MostrarMenu();
                string? input = Console.ReadLine();
                
                if (int.TryParse(input, out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            {
                                Console.Write("Nombre del equipo: ");
                                string? nombre = Console.ReadLine();
                                Console.Write("Ciudad: ");
                                string? ciudad = Console.ReadLine();
                                Console.Write("Entrenador: ");
                                string? entrenador = Console.ReadLine();

                                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(ciudad) && !string.IsNullOrEmpty(entrenador))
                                {
                                    torneo.RegistrarEquipo(nombre, ciudad, entrenador);
                                }
                                else
                                {
                                    Console.WriteLine("Error: Todos los campos son obligatorios.");
                                }
                                break;
                            }

                        case 2:
                            {
                                Console.Write("ID del jugador: ");
                                string? idInput = Console.ReadLine();
                                
                                if (int.TryParse(idInput, out int id))
                                {
                                    Console.Write("Nombre: ");
                                    string? nombre = Console.ReadLine();
                                    Console.Write("Edad: ");
                                    string? edadInput = Console.ReadLine();
                                    
                                    if (int.TryParse(edadInput, out int edad) && !string.IsNullOrEmpty(nombre))
                                    {
                                        Console.Write("Posicion: ");
                                        string? posicion = Console.ReadLine();
                                        Console.Write("Equipo: ");
                                        string? equipo = Console.ReadLine();

                                        if (!string.IsNullOrEmpty(posicion) && !string.IsNullOrEmpty(equipo))
                                        {
                                            torneo.RegistrarJugador(id, nombre, edad, posicion, equipo);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error: Posicion y equipo son obligatorios.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Nombre y edad valida son obligatorios.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error: ID debe ser un numero.");
                                }
                                break;
                            }

                        case 3:
                            {
                                Console.Write("ID del jugador a transferir: ");
                                string? idInput = Console.ReadLine();
                                
                                if (int.TryParse(idInput, out int id))
                                {
                                    Console.Write("Equipo destino: ");
                                    string? equipoDestino = Console.ReadLine();
                                    
                                    if (!string.IsNullOrEmpty(equipoDestino))
                                    {
                                        torneo.TransferirJugador(id, equipoDestino);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Equipo destino es obligatorio.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error: ID debe ser un numero.");
                                }
                                break;
                            }

                        case 4:
                            torneo.MostrarTodosLosEquipos();
                            break;

                        case 5:
                            {
                                Console.Write("ID del jugador: ");
                                string? idInput = Console.ReadLine();
                                
                                if (int.TryParse(idInput, out int id))
                                {
                                    torneo.BuscarJugador(id);
                                }
                                else
                                {
                                    Console.WriteLine("Error: ID debe ser un numero.");
                                }
                                break;
                            }

                        case 6:
                            torneo.MostrarEstadisticas();
                            break;

                        case 7:
                            {
                                Console.Write("ID del jugador a eliminar: ");
                                string? idInput = Console.ReadLine();
                                
                                if (int.TryParse(idInput, out int id))
                                {
                                    torneo.EliminarJugador(id);
                                }
                                else
                                {
                                    Console.WriteLine("Error: ID debe ser un numero.");
                                }
                                break;
                            }

                        case 0:
                            Console.WriteLine("Gracias por usar el sistema!");
                            break;

                        default:
                            Console.WriteLine("Opcion no valida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Ingrese un numero valido.");
                    opcion = -1;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                }

            } while (opcion != 0);
        }
    }
}