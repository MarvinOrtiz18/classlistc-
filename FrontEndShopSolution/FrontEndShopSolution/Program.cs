//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="UNAN Managua - CUR Carazo">
//     Author: MAOG18
//     Copyright (c) 2026. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace FrontEndShopSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
            /// Agregar mensaje de titulo de consola
            Console.Title = "Tienda de Consignacion";

            /// Mensaje de Bienvenida
            Console.WriteLine("Bienvenido a la Tienda de Consignacion!");

            /// Lista de articulos disponibles en la tienda
            /// Usaremos una lista de tuplas para representar los articulos X, esto es con fines didacticos y academicos
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
            };

            /// Bucle principal de la tienda
            /// Permite al usuario ver los articulos disponibles y comprar uno hasta que el usuario decida salir
            while (true)
            {
                Console.WriteLine("\nArticulos disponibles en la tienda:");
                /// anyAvailable: Indica si hay articulos disponibles para la venta
                /// Esta variable va a tomar el valor true si al menos un articulo no ha sido vendido
                bool anyAvailable = false;

                /// foreach: Recorre la lista de articulos y muestra solo los que no han sido vendidos
                foreach (var item in items)
                {
                    /// Si el articulo no ha sido vendido, mostrarlo en la consola
                    /// !item.sold: Verifica si el articulo no ha sido vendido
                    if (!item.sold)
                    {
                        /// anyAvailable: Marcar que hay al menos un articulo disponible
                        anyAvailable = true;
                        Console.WriteLine($" {item.id} - {item.title}: {item.description} - Precio: ${item.price:F2}");
                    }
                }

                /// Si no hay articulos disponibles, informar al usuario y salir del bucle
                if (!anyAvailable)
                {
                    Console.WriteLine("No hay articulos disponibles para la venta.");
                    break;
                }

                /// Solicitar al usuario que ingrese el ID del articulo que desea comprar o salir
                Console.WriteLine("\nIngrese el ID del articulo que desea comprar o escriba \"salir\" para terminar:");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                /// Verificar si el usuario desea salir
                if (string.Equals(input, "salir", StringComparison.OrdinalIgnoreCase))
                {
                    /// Salir del bucle principal
                    break;
                }

                /// Intentar convertir la entrada del usuario a un numero entero (ID del articulo)
                if (!int.TryParse(input, out int itemId))
                {
                    Console.WriteLine("Entrada invalida. Por favor ingrese un ID numérico valido.");
                    continue;
                }

                /// Buscar el articulo por su ID
                int index = items.FindIndex(i => i.id == itemId);
                /// Verificar si el articulo existe
                if (index == -1)
                {
                    Console.WriteLine($"No se encontro ningun articulo con ID {itemId}.");
                    continue;
                }

                /// Verificar si el articulo ya ha sido vendido
                var selected = items[index];
                /// Si el articulo ya fue vendido, informar al usuario
                if (selected.sold)
                {
                    Console.WriteLine($"El articulo '{selected.title}' (ID {selected.id}) ya fue vendido y no puede comprarse nuevamente.");
                    continue;
                }

                /// Procesar la compra del articulo
                /// sold: Marcar el articulo como vendido (true)
                items[index] = (selected.id, selected.title, selected.description, selected.price, true);
                /// Informar al usuario sobre la compra exitosa
                Console.WriteLine($"Compra realizada: '{selected.title}' por ${selected.price:F2}. ¡Gracias por su compra!");
            }

            Console.WriteLine();
            Console.WriteLine("Saliendo del programa, presione una tecla...");
            Console.ReadKey();
        }
    }
}
