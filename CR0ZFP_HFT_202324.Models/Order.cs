using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CR0ZFP_HFT_202324.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Order()
        {
            Products = new HashSet<Product>();
        }

        public Order()
        {
            
        }

        public Order(string line)
        {
            string[] data =  line.Split('#');
            this.OrderID = int.Parse(data[0]);
            this.OrderID= int.Parse(data[1]);
            this.OrderDate = DateTime.Parse(data[2]);
            Products = new HashSet<Product>();
        }
    }
}
