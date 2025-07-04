namespace E_commerce
{
    class Product
    {
        public string Name { get; }
        public double Price { get; set; }
        public int Quanity { get; set; }
        public bool Expire { get; set; }

        public DateTime? Expiry { get; set; }
        public Product(string Name, double Price, int Quantity, bool Expire, DateTime? Expiry)
        {
            this.Name = Name;
            this.Price = Price;
            this.Quanity = Quantity;
            this.Expire = Expire;
            this.Expiry = Expiry;
        }
    }
    interface Ishippable
    {
        public string Name { get; }
        public double Weight { get; }
        public string getName();
        public double getWeight();
    }
    class ShippableProduct : Product, Ishippable
    {

        public double Weight { get; }

        public ShippableProduct(string Name, double Price, int Quantity, bool Expire, DateTime? Expiry, double Weight) : base(Name, Price, Quantity, Expire, Expiry)
        {
            this.Weight = Weight;
        }
        public string getName()
        {
            return Name;
        }

        public double getWeight()
        {
            return Weight;
        }
    }
}

