using System;

namespace RegistroEstudiante
{
    // Representa los datos de un estudiante y sus teléfonos
    public class Estudiante
    {
        // Propiedades básicas
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

        // Array de teléfonos con longitud fija de 3
        public string[] Telefonos { get; set; }

        // Constructor que inicializa propiedades y array de teléfonos
        public Estudiante(int id, string nombres, string apellidos, string direccion)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefonos = new string[3]; // capacidad para 3 números
        }

        // Método para asignar un teléfono en una posición (0 a 2)
        public void AsignarTelefono(int indice, string telefono)
        {
            if (indice < 0 || indice >= Telefonos.Length)
                throw new ArgumentOutOfRangeException(nameof(indice), "Índice debe estar entre 0 y 2.");
            Telefonos[indice] = telefono;
        }

        // Imprimir datos del estudiante
        public void MostrarDatos()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombre completo: {Nombres} {Apellidos}");
            Console.WriteLine($"Dirección: {Direccion}");
            Console.WriteLine("Teléfonos:");
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"  [{i+1}] {Telefonos[i] ?? "(no asignado)"}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crear instancia de estudiante
            Estudiante est = new Estudiante(
                id: 0923552533,
                nombres: "Luis Alberto",
                apellidos: "Samaniego Segura",
                direccion: "JipiJapa y Jose Marti, Santo Domingo de los Tsáchilas"
            );

            // Asignar tres teléfonos
            est.AsignarTelefono(0, "0996219688");
            est.AsignarTelefono(1, "0996818970");
            est.AsignarTelefono(2, "02-2994765");

            // Mostrar información
            est.MostrarDatos();

            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
