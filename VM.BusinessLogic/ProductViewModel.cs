using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.BusinessLogic
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableItems { get; set; }
        public int MaxItems { get; set; }
        public int SoldItems { get; set; }
        public decimal Price { get; set; }

    }
}
