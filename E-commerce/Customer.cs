namespace E_commerce
{
    class Customer
    {
        public string Name { get; set; }

        public double Balance { get; set; }
        public Customer(string Name, int Balance)
        {
            this.Name = Name;
            this.Balance = Balance;
        }
    }
}
    
