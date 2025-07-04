using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace E_commerce
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test Customer Balance:");
            CustomerBalance();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test Empty Cart");
            EmptyCart();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test Insufficient Funds");
            InsufficientFunds();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test adding expired product");
            AddExpiredProduct();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test checkout expired product");
            CheckoutExpiredProduct();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test More than Stock");
            MoreThanStock();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test Shippable Products");
            ShippableProducts();
        }
        static void ShippingService(List<Tuple<Ishippable, int>> products)
        {
            Console.WriteLine("** Shipment notice **");
            double totalWeight = 0;
            foreach (var product in products)
            {
                 ShippableProduct item = (ShippableProduct)product.Item1;
                 int count = product.Item2;
                 Console.WriteLine($"{count}x {item.Name}\t\t{item.getWeight() * count}g");
                 totalWeight += item.getWeight() * count;
            }
            Console.WriteLine($"Total package weight {totalWeight / 1000.0}kg");
        }
        static void checkout(Customer customer, Cart cart)
        {
            if (cart.isEmpty())
            {
                throw new Exception("Cart is Empty!!!");
            }
            var invoice = cart.getTotal();
            double shipping = invoice.Item1;
            double subtotal = invoice.Item2;
            if(shipping + subtotal>customer.Balance)
            {
                throw new Exception($"Insufficient funds!!!\nTotal = {shipping+subtotal}$ CustomerBalance = {customer.Balance}$");
            }
            foreach (var pair in cart.products)
            {
                Product item = pair.Item1 as Product;
                int count = pair.Item2;
                if (count>item.Quanity)
                {
                    throw new Exception($"Not enough of [{item.Name}] in stock!!!");
                }
                if(item.Expire&&item.Expiry<DateTime.Now)
                {
                    throw new Exception($"Product [{item.Name}] is expired!!!");
                }
            }

            List <Tuple<Ishippable,int>> products = cart.GetShippableProducts();
            ShippingService(products);

            Console.WriteLine("** Checkout receipt **");
            foreach (var pair in cart.products)
            {
                Product item = pair.Item1 as Product;
                int count = pair.Item2;
                item.Quanity -= count;
                Console.WriteLine($"{count}x {item.Name}      {item.Price * count}$");
            }
            Console.WriteLine("---------------");
            Console.WriteLine($"Subtotal     {subtotal:f2}$");
            Console.WriteLine($"Shipping     {shipping:f2}$");
            Console.WriteLine($"Amount       {subtotal + shipping:f2}$");
            customer.Balance -= subtotal + shipping;
            cart.products.Clear();
        }

        static void CustomerBalance()
        {
            Customer Abdo = new Customer("Abdo", 3000);
            Cart cart = new Cart();
            Product cheese = new Product("Cheese", 100, 10, true, DateTime.Now.AddYears(1));
            Product TV = new Product("TV", 2000, 4, false, null);
            cart.add(cheese, 4);
            cart.add(TV, 1);
            Console.WriteLine($"Current balance for {Abdo.Name} = {Abdo.Balance}");
            try
            {
                checkout(Abdo, cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Expected new balance for {Abdo.Name} = {3000-100*4-2000}, Actual Balance = {Abdo.Balance}");
        }
        static void EmptyCart()
        {
            Cart cart = new Cart();
            Customer Abdo = new Customer("Abdo", 1000);
            try { 
                checkout(Abdo, cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        static void InsufficientFunds()
        {

            Customer Abdo = new Customer("Abdo", 3000);
            Cart cart = new Cart();
            Product cheese = new Product("Cheese", 100, 10, true, DateTime.Now.AddYears(1));
            Product TV = new Product("TV", 2000, 4, false, null);
            cart.add(cheese, 4);
            cart.add(TV, 2);
    
            try
            {
                checkout(Abdo, cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        static void AddExpiredProduct()
        {
            Customer Abdo = new Customer("Abdo", 3000);
            Cart cart = new Cart();
            Product cheese = new Product("Cheese", 100, 10, true, DateTime.Parse("2000-01-01"));
            Product TV = new Product("TV", 2000, 4, false, null);
            try
            { 
                cart.add(cheese, 4);
                cart.add(TV, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static void CheckoutExpiredProduct()
        {
            Customer Abdo = new Customer("Abdo", 3000);
            Cart cart = new Cart();
            Product cheese = new Product("Cheese", 100, 10, true, DateTime.Now.AddSeconds(2));
            Product TV = new Product("TV", 2000, 4, false, null);

            try
            {
                cart.add(cheese, 4);
                cart.add(TV, 1);
                Thread.Sleep(2000); //Wait for the 2 seconds to pass
                checkout(Abdo, cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static void MoreThanStock()
        {
            Customer Abdo = new Customer("Abdo", 3000);
            Cart cart = new Cart();
            Product cheese = new Product("Cheese", 100, 10, true, DateTime.Now.AddSeconds(2));
            Product TV = new Product("TV", 2000, 4, false, null);

            try
            {
                cart.add(cheese, 11);
                checkout(Abdo, cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ShippableProducts()
        {
            Customer Abdo = new Customer("Abdo", 3000);
            Cart cart = new Cart();
            Product cheese = new Product("Cheese", 100, 10, true, DateTime.Now.AddSeconds(2));
            ShippableProduct TV = new ShippableProduct("TV", 2000, 4, false,null,500);

            try
            {
                cart.add(cheese, 4);
                cart.add(TV, 1);
                checkout(Abdo, cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}       
