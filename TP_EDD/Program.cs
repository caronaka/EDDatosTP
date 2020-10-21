using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_EDD
{
    class Program
    {
        static void Main(string[] args)
        {
            Product one = new Product(1, "Brasil", 320, 389); // instancio la clase product, creando cinco productos
            Product two = new Product(2, "Colombia", 270, 400);
            Product three = new Product(3, "Nicaragua", 183, 350);
            Product four = new Product(4, "Italiano", 100, 450);
            Product five = new Product(5, "Etiopia", 85, 415);

            List<Product> cafes = new List<Product>(); // declaro una nueva lista

            cafes.Add(one); //agrego todos los objetos a la lista
            cafes.Add(two);
            cafes.Add(three);
            cafes.Add(four);
            cafes.Add(five);

            string opc = "0";
            while (opc != "5") //creo un ciclo para que se repita el menu hasta que el usuario quiera salir
            {
                Console.Clear();
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("1) Agregar nuevo producto");
                Console.WriteLine("2) Modificar precio o cantidad de un producto");
                Console.WriteLine("3) Eliminar producto");
                Console.WriteLine("4) Consultar stock");
                Console.WriteLine("5) Salir");
                Console.Write("\r\nElija una opcion: ");
                opc = Console.ReadLine();

                switch (opc) //creo menu
                {
                    case "1":
                        cafes = Create(cafes); //le paso la lista con los 5 cafes
                        break;
                    case "2":
                        cafes = Update(cafes);
                        break;
                    case "3":
                        cafes = Delete(cafes);
                        break;
                    case "4":
                        Read(cafes);
                        break;
                    case "5":
                        break;
                }
                Console.ReadKey();
            }
                Console.ReadKey();
        }

        public static List<Product> Create(List<Product> lista) //PARA AGREGAR PRODUCTOS A LA LISTA
        {
            Console.WriteLine("Ingrese el Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el nombre: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese precio: ");
            decimal precio = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Ingrese cantidad: ");
            int cantidad = Convert.ToInt32(Console.ReadLine());


            Product nuevo = new Product(id, nombre, precio, cantidad); //instancio el nuevo producto 
            lista.Add(nuevo); //lo agrego a la lista

            return lista; //devuelve la lista con el producto agregado
        }

        public static void Read(List<Product> lista) //TE MUESTRA EL CONTENIDO DE TODA LA LISTA
        {
            foreach (Product producto in lista) 
            {
                Console.WriteLine("Id : {0}", producto.Id);
                Console.WriteLine("Nombre : {0}", producto.Name);
                Console.WriteLine("Precio : {0}", producto.Price) ;
                Console.WriteLine("Cantidad : {0}", producto.Quantity);
            }
        }

        public static List<Product> Update(List<Product> lista) //PARA MODIFICAR CANTINAD O PRECIO DEL PRODUCTO
        {
            Console.WriteLine("Que desea modificar?  \n 1. Precio \n 2. Cantidad \n Ingrese 1 o 2: "); //ingrese si quiere cambiar precio o cantidad
            int categoria = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el id del producto a modificar: "); //ingrese id del producto a modificar
            int id_modificar = Convert.ToInt32(Console.ReadLine());

            if (categoria == 1) //precio
            {
                Console.WriteLine("Ingrese el nuevo precio: ");
                decimal nuevo_precio = Convert.ToDecimal(Console.ReadLine()); //ingresa nuevo precio

                foreach (Product producto in lista) //recorre la lista y cuando coincide el id del producto a modificar, reemplaza el precio
                {
                    if (producto.Id == id_modificar) 
                    {
                        producto.Price = nuevo_precio ;
                    }
                }
            }

            if (categoria == 2) //cantidad
            {
                Console.WriteLine("Ingrese la nueva cantidad: ");
                int nueva_cantidad = Convert.ToInt32(Console.ReadLine()); //ingresa nueva cantidad

                foreach (Product producto in lista) //recorre la lista y cuando coincide el id del producto a modificar, reemplaza el cantidad
                {
                    if (producto.Id == id_modificar)
                    {
                        producto.Quantity = nueva_cantidad;
                    }
                }
            }
            return lista;
        }

        public static List<Product> Delete(List<Product> lista) //ELIMINA PRODUCTOS DE LA LISTA
        {

            Console.WriteLine("Ingrese el id del producto a eliminar: "); //ingresa id del producto a eliminar
            int id_eliminar = Convert.ToInt32(Console.ReadLine());

            foreach (Product producto in lista.ToList())//recorre la lista y cuando coincide el id del producto a eliminar, se elimina
            {
                if (producto.Id == id_eliminar) 
                {
                    lista.Remove(producto);
                }
            }
            return lista;
        }








    }
}
