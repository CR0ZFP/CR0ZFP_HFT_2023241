using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace CR0ZFP_HFT_202324.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [StringLength(240)]
        public string CustomerName { get; set; }

        [StringLength(240)]
        public string Email { get; set; }

        [StringLength(240)]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders {get;set;}

        public Customer ()
        {
            Orders = new HashSet<Order> ();
        }

        public Customer()
        {
            
        }

        public Customer (string line)
        {
            string[] data = line.Split ('#');
            this.CustomerID = int.Parse(data[0]);
            this.CustomerName = data[1];
            this.Email = data[2];
            this.Address = data[3];
            Orders = new HashSet<Order>();
        }


    }
}
