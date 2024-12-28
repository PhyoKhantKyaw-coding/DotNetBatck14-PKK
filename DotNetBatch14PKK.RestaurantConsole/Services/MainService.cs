using DotNetBatch14PKK.RestaurantConsole.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetBatch14PKK.RestaurantConsole.Models;

namespace DotNetBatch14PKK.RestaurantConsole.Services
{
    public class MainService
    {
        private readonly MiniRestaurantService _miniRestaurantService;
        public MainService()
        {
            _miniRestaurantService = new MiniRestaurantService();
        } 
        public async Task RunAsync()
        {
            int choice;
            do
            {
                Console.WriteLine("\nMini Restaurant Management System");
                Console.WriteLine("1. Manage Menu");
                Console.WriteLine("2. Take Order");
                Console.WriteLine("3. View Orders");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        await ManageMenuAsync();
                        break;
                    case 2:
                        await TakeOrderAsync();
                        break;
                    case 3:
                        await ViewOrdersAsync();
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (choice != 4);
        }
        private async Task ManageMenuAsync()
        {
            int option;
            do
            {
                Console.WriteLine("\nMenu Management");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. View Menu");
                Console.WriteLine("3. Back");
                Console.Write("Enter your choice: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        await AddMenuItemAsync();
                        break;
                    case 2:
                        await ViewMenuAsync();
                        break;
                    case 3:
                        Console.WriteLine("Returning to main menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (option != 3);
        }
        private async Task AddMenuItemAsync()
        {
            Console.Write("Enter item code: ");
            int code = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();
            Console.Write("Enter item price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            MenuItem newItem = new MenuItem
            {
                Name = name,
                Price = price,
                MenuItemCode = code,
            };

            var response = await _miniRestaurantService.CreateMenuItem(newItem);
            Console.WriteLine(response.IsSuccessful ? "Item added successfully!" : $"Failed to add item: {response.Message}");
        }

        private async Task ViewMenuAsync()
        {
            var menuItems = await _miniRestaurantService.GetMenuItems();

            if (menuItems.Count == 0)
            {
                Console.WriteLine("Menu is empty!");
                return;
            }

            Console.WriteLine("\nMenu:");
            Console.WriteLine("{0,-10} {1,-20} {2,10}", "Code", "Name", "Price");
            foreach (var item in menuItems)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,10:C2}", item.MenuItemCode, item.Name, item.Price);
            }
        }

        private async Task TakeOrderAsync()
        {
            List<Itemlist> items = new List<Itemlist>();
            int menuItemCode, quantity;
            do
            {
                await ViewMenuAsync();
                Console.Write("Enter menu item code (-1 to finish): ");
                menuItemCode = Convert.ToInt32(Console.ReadLine());

                if (menuItemCode == -1)
                {
                    break;
                }

                Console.Write("Enter quantity: ");
                quantity = Convert.ToInt32(Console.ReadLine());

                items.Add(new Itemlist { MenuItemCode = menuItemCode, Quantity = quantity });

            } while (menuItemCode != -1);

            if (items.Count > 0)
            {
                decimal total=0;
                Console.Write("Enter order code : ");
                int code = Convert.ToInt32(Console.ReadLine());
                int orderCode = code;
                Console.WriteLine("\nOrder Summary");
                Console.WriteLine("{0,-10} {1,-20} {2,10} {3,10} {4,10}", "Product Code", "Name", "Price", "Quantity" ,"Total Price");
                foreach (var item in items)
                {
                    MenuItem menuItem = await _miniRestaurantService.GetMenuItem(item.MenuItemCode);
                    Console.WriteLine("{0,-10} {1,-20} {2,10:C2} {3,10}", menuItem.MenuItemCode, menuItem.Name, menuItem.Price, item.Quantity,menuItem.Price*item.Quantity);
                    total =+ menuItem.Price * item.Quantity;
                }
                Console.WriteLine("Total Amount: {0:C2}",total);
                var response = await _miniRestaurantService.CreateOrder(orderCode, items);
                Console.WriteLine(response.IsSuccessful ? "Order placed successfully!" : $"Failed to place order: {response.Message}");
            }
            else
            {
                Console.WriteLine("No items added to order!");
            }
        }
        private async Task ViewOrdersAsync()
        {
            var orders = await _miniRestaurantService.GetOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found!");
                return;
            }
            Console.WriteLine("\nOrders:");
            Console.WriteLine("{0,-10} {1,10}", "Order Code", "Total");
            foreach (var order in orders)
            {
                Console.WriteLine("{0,-10} {1,10:C2}", order.OrderCode, order.TotalPrice);
            }
        }
    }
}
