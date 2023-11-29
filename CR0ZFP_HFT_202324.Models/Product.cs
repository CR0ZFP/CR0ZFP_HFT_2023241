using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CR0ZFP_HFT_202324.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [ForeignKey(nameof(Product))]
        public int OrderID { get; set; }    

        [StringLength(240)]
        public string ProductName { get; set; }

        public double Weight { get; set; }

        public double Price { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

        public Product()
        {
            
        }

        public Product(string line)
        {
            string [] data = line.Split('#');

            this.ProductID = int.Parse(data[0]);
            this.OrderID = int.Parse(data[1]);
            this.ProductName = data[2];
            this.Weight = double.Parse(data[3]);
            this.Price = double.Parse(data[4]);

        }

        public override bool Equals(object obj)
        {
            Product P = obj as Product;
            if (P == null)
            {
                return false;
            }
            else return this.ProductID == P.ProductID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ProductID);
        }
    }
}
