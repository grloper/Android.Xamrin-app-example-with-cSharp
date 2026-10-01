namespace Android_Ofek
{
    public class BasicPrice
    {
        protected int price;

        public BasicPrice(int price)
        {
            this.price = price;
        }
        public int GetPrice()
        {
            return this.price;
        }
        public void SetPrice(int p)
        {
            this.price = p;
        }
    }
}