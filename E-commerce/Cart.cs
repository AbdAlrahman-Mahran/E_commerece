namespace E_commerce
{
    class Cart
    {
        public List<Tuple<Product,int>> products;
        public Cart()
        {
            products = new List<Tuple<Product, int>>();
        }
        public void add(Product product,int quantity)
        {
            if (product.Quanity < quantity)
            {
                throw new Exception($"Ordered [{product.Name}] More than stock!!!");
            }
            if (product.Expire && product.Expiry < DateTime.Now)
            {
                throw new Exception($"Expired product [{product.Name}]!!!");
            }
            products.Add(new Tuple<Product, int> (product,quantity));
        }
        public bool isEmpty()
        {
            return (products.Count() == 0);
        }

        public Tuple<double,double> getTotal()
        {
            double SHIPPING = 30;
            double total = 0;
            double shipping = 0;
            foreach (var pair in products)
            {
                Product item = pair.Item1 as Product;
                int count = pair.Item2;
                if (item is ShippableProduct)
                {
                    ShippableProduct temp = (ShippableProduct)item;
                    shipping += temp.getWeight() * count;
                }
            }
            foreach (var pair in products)
            {
                Product item = pair.Item1 as Product;
                int count = pair.Item2;
                total += (item.Price * count);
            }
            double shipping_cost = (shipping / 1000) * SHIPPING;
            return new Tuple<double,double>(shipping_cost, total);
        }
        public List<Tuple<Ishippable, int>> GetShippableProducts()
        {

            List<Tuple<Ishippable,int>> ret = new List<Tuple<Ishippable, int>>();
            foreach (var pair in products)
            {
                Product item = pair.Item1 as Product;
                if (item is Ishippable)
                {
                    Ishippable temp = (Ishippable)item;
                    int count = pair.Item2;
                    ret.Add(new Tuple<Ishippable, int> (temp,count));
                }
            }

            return ret;
        }

    }
}
    
