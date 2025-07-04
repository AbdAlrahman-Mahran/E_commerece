# E-commerce Console Application

A simple E-commerce simulation written in C# (.NET 8.0), demonstrating basic shopping cart, product, and customer management logic. This project showcases object-oriented programming concepts, error handling, and basic business logic for an online store.

## Features
- **Product Management**: Supports regular and shippable products, with expiry and stock tracking.
- **Cart System**: Add products to a cart, check for stock and expiry, and calculate totals including shipping.
- **Customer Accounts**: Customers have balances and can check out if they have sufficient funds.
- **Shipping Calculation**: Shipping costs are calculated based on the weight of shippable products.
- **Error Handling**: Handles cases like expired products, insufficient stock, empty carts, and insufficient funds.
- **Test Scenarios**: The main program runs a series of test cases demonstrating all features and error conditions.

## Project Structure
- `Product.cs`: Defines `Product`, `ShippableProduct`, and the `Ishippable` interface.
- `Cart.cs`: Implements the shopping cart logic, including adding products, calculating totals, and shipping.
- `Customer.cs`: Represents a customer with a name and balance.
- `Program.cs`: Entry point; runs a series of test scenarios covering all major features and error cases.

## Requirements
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Build & Run
1. **Clone the repository**:
   ```sh
   git clone <repo-url>
   cd E_commerece/E-commerce
   ```
2. **Build the project:**
   ```sh
   dotnet build
   ```
3. **Run the application:**
   ```sh
   dotnet run
   ```

