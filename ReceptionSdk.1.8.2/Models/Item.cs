namespace ReceptionSdk.Models

{

    public class Item
    {
        public string id { get; set; }
        public string type { get; set; }
        public BeforePrice before_price { get; set; }
        public bool enable { get; set; }
    }
}