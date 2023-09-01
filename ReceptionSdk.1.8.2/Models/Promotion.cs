using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    public class Promotion 
    {
        [DeserializeAs(Name = "id")]
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        
        [DeserializeAs(Name = "name")]
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        
        [DeserializeAs(Name = "items")]
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Items { get; set; }
        
        [DeserializeAs(Name = "vendors")]
        [JsonProperty(PropertyName = "vendors", NullValueHandling = NullValueHandling.Ignore)]
        public List<Vendor> Vendors { get; set; }
        
        [DeserializeAs(Name = "enable")]
        [JsonProperty(PropertyName = "enable", NullValueHandling = NullValueHandling.Ignore)]
        public bool Enable { get; set; }

        [DeserializeAs(Name = "exposition_dates")]
        [JsonProperty(PropertyName = "exposition_dates", NullValueHandling = NullValueHandling.Ignore)]
        public ExpositionDates ExpositionDates { get; set; }
        
   
        public Promotion()
        {
            Items = new List<Item>(); 
            Vendors = new List<Vendor>();
          
        }
        
        public void AddItem(string itemId, string type, double amount, bool enableItem)
        {
            var beforePrice = new BeforePrice()
            {
                amount = amount
            };
             var item = new Item()
             {
                 id = itemId,
                 type = type,
                 before_price = beforePrice,
                 enable = enableItem
             };
            Items.Add(item);
        }

        public void AddVendor(long vendorsId,bool enableVendor)
        {
            var vendor = new Vendor()
            {
                id = vendorsId,
                enable = enableVendor
            };
            Vendors.Add(vendor);
        }
        
        
        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
        
    }
}