using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkelandStore.Core.Entities
{
    public class BasketItem//not inherit From BaseEntity Cause it not Represent in database only Represent InMemory
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
    }
}
