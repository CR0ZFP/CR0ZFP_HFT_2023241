﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace CR0ZFP_HFT_202324.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(240)]
        public string CustomerName { get; set; }

        [StringLength(240)]
        public string Email { get; set; }

        [StringLength(240)]
        public string Address { get; set; }

        [Required]
        public int Age { get; set; }

        
        public virtual ICollection<Order> Orders {get;set;}

        public Customer ()
        {
            Orders = new HashSet<Order> ();
        }

        public Customer(string line)
        {
            string[] data = line.Split ('#');
            this.CustomerID = int.Parse(data[0]);
            this.CustomerName = data[1];
            this.Email = data[2];
            this.Address = data[3];
            this.Age = int.Parse(data[4]);
            Orders = new HashSet<Order>();
        }

        public override bool Equals(object obj)
        {
            Customer C = obj as Customer;

            if (C == null)
            {
                return false;
            }
            else
            {
                return this.CustomerID == C.CustomerID;
            }

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.CustomerID);
        }


    }
}
