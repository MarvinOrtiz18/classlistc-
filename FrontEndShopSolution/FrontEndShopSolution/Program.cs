using System;
using System.Collections.Generic;

namespace FrontEndShopSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Tienda de Consignacion";
            Console.WriteLine("Bienvenido a la Tienda de Consignacion!");

            var items = new List<(int id, string title, string description, decimal price, bool sold)>
            {
                (1, "Historia Universal de la infamia", "Narrativa Filosófica de Borges", 4.50M, false),
                (2, "La segunda Guerra Mundial", "Sir. Wuiston Church11", 14.50M, false),
                (3, "Moby Dick", "Libro sobre una Bellena Blanca", 3.50M, false),
                (4, "Azul", "Ruben Darío", 24.50M, false),
                (5, "Cien Años de Soledad", "Gabriel García Márquez", 7.50M, false)
            };

            while (true)
            {
                Console.WriteLine("\nArtículos disponibles en la tienda:");
                bool anyAvailable = false;
                foreach (var item in items)
                {
                    if (!item.sold)
                    {
                        anyAvailable = true;
                        Console.WriteLine($" {item.id} - {item.title}: {item.description} - Precio: ${item.price:F2}");
                    }
                }

                if (!anyAvailable)
                {
                    Console.WriteLine("No hay artículos disponibles para la venta.");
                    break;
                }

                Console.WriteLine("\nIngrese el ID del artículo que desea comprar o escriba \"salir\" para terminar:");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (string.Equals(input, "salir", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (!int.TryParse(input, out int itemId))
                {
                    Console.WriteLine("Entrada inválida. Por favor ingrese un ID numérico válido.");
                    continue;
                }

                int index = items.FindIndex(i => i.id == itemId);
                if (index == -1)
                {
                    Console.WriteLine($"No se encontró ningún artículo con ID {itemId}.");
                    continue;
                }

                var selected = items[index];
                if (selected.sold)
                {
                    Console.WriteLine($"El artículo '{selected.title}' (ID {selected.id}) ya fue vendido y no puede comprarse nuevamente.");
                    continue;
                }

                // Marcar como vendido reasignando la tupla en la lista
                items[index] = (selected.id, selected.title, selected.description, selected.price, true);
                Console.WriteLine($"Compra realizada: '{selected.title}' por ${selected.price:F2}. ¡Gracias por su compra!");
            }

            Console.WriteLine();
            Console.WriteLine("Saliendo del programa, presione una tecla...");
            Console.ReadKey();
        }
    }
}
