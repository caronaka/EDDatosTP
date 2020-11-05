using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace TP_EDD
{
    public class Product
    {   
        //Negocio de cafes en granos (ejemplo)
        //Clase productos
        //Atributos id, nombre del producto, precio y cantidad

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }



        public Product(int id, string name, decimal price, int quantity) //Constructor
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;

        }
    }
}
