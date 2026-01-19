//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="UNAN Managua - CUR Carazo">
//     Author: MAOG18
//     Copyright (c) 2026. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace FrontEndShopSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Titulo de la consola
            Console.Title = "Tienda de Consignacion";

            Console.WriteLine("Bienvenido a la Tienda de Consignacion!");

            // Lista de articulos disponibles en la tienda
            var items = InitializeItems();

            // Bucle principal de la tienda
            while (true)
            {
                Console.WriteLine("\nArticulos disponibles en la tienda:");
                // Mostrar los articulos disponibles; anyAvailable indica si hay al menos uno
                bool anyAvailable = DisplayAvailableItems(items);

                // Si no hay articulos disponibles, informar al usuario y salir del bucle
                if (!anyAvailable)
                {
                    Console.WriteLine("No hay articulos disponibles para la venta.");
                    break;
                }

                // Manejar la compra en un unico metodo que contiene toda la logica
                bool continueLoop = HandlePurchase(items);

                // Si el metodo indica salir, romper el bucle principal
                if (!continueLoop)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Saliendo del programa, presione una tecla...");
            Console.ReadKey();
        }

        // Inicializa y retorna la lista de articulos
        public static List<(int id, string title, string description, decimal price, bool sold)> InitializeItems()
        {
            // Usaremos una lista de tuplas para representar los articulos X, esto es con fines didacticos y academicos
            var items = new List<(int id, string title, string description, decimal price, bool sold)>
            {
                (1, "Historia Universal de la infamia", "Narrativa Filosófica de Borges", 4.50M, false),
                (2, "La segunda Guerra Mundial", "Sir. Wuiston Church11", 14.50M, false),
                (3, "Moby Dick", "Libro sobre una Bellena Blanca", 3.50M, false),
                (4, "Azul", "Ruben Darío", 24.50M, false),
                (5, "Cien Años de Soledad", "Gabriel García Márquez", 7.50M, false)
                
                /// id: Identificador unico del producto
                /// title: Titulo del producto
                /// descriptiopn: Descripcion breve del producto
                /// decimalprice: Precio del producto
                /// sold: Indica si el producto ha sido vendido o no
                /// Price casting de double a decimal
            };

            return items;
        }

        // Recorre la lista de articulos y muestra solo los que no han sido vendidos
        // Devuelve true si al menos un articulo esta disponible
        public static bool DisplayAvailableItems(List<(int id, string title, string description, decimal price, bool sold)> items)
        {
            bool anyAvailable = false;

            foreach (var item in items)
            {
                // Si el articulo no ha sido vendido, mostrarlo en la consola
                if (!item.sold)
                {
                    // anyAvailable: Marcar que hay al menos un articulo disponible
                    anyAvailable = true;
                    Console.WriteLine($" {item.id} - {item.title}: {item.description} - Precio: ${item.price:F2}");
                }
            }

            return anyAvailable;
        }

        // Maneja toda la logica de seleccion, parseo, busqueda y proceso de compra
        public static bool HandlePurchase(List<(int id, string title, string description, decimal price, bool sold)> items)
        {
            Console.Write("\nIngrese el ID del articulo que desea comprar o escriba \"salir\" para terminar: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            // Verificar si el usuario desea salir
            if (string.Equals(input, "salir", StringComparison.OrdinalIgnoreCase))
            {
                return false; // indicar salir del bucle principal
            }

            // Casting la entrada del usuario a un numero entero (ID del articulo)
            if (!int.TryParse(input, out int itemId))
            {
                Console.WriteLine("Entrada invalida. Por favor ingrese un ID numerico valido.");
                return true; 
            }

            // Buscar el indice del articulo por su ID
            int index = -1;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].id == itemId)
                {
                    index = i;
                    break;
                }
            }

            // Verificar si el articulo existe
            if (index == -1)
            {
                Console.WriteLine($"No se encontro ningun articulo con ID {itemId}.");
                return true;
            }

            // Obtener el articulo seleccionado
            var selected = items[index];

            // Verificar si el articulo ya ha sido vendido
            if (selected.sold)
            {
                Console.WriteLine($"El articulo '{selected.title}' (ID {selected.id}) ya fue vendido y no puede comprarse nuevamente.");
                return true;
            }

            // Procesar la compra: marcar el articulo como vendido, cambiamos el valor de sold a true
            items[index] = (selected.id, selected.title, selected.description, selected.price, true);

            // Informar al usuario sobre la compra exitosa
            Console.WriteLine($"Compra realizada: '{selected.title}' por ${selected.price:F2}. ¡Gracias por su compra!");
 
            return true;
        }
    }
}
