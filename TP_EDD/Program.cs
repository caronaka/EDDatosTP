using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TP_EDD
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanciamos la clase product, creando cinco productos que van a estar de entrada en nuestra base de datos

            Product one = new Product(1, "Brasil", 320, 389); 
            Product two = new Product(2, "Colombia", 270, 400);
            Product three = new Product(3, "Nicaragua", 183, 350);
            Product four = new Product(4, "Italiano", 100, 450);
            Product five = new Product(5, "Etiopia", 85, 415);

            List<Product> cafes = new List<Product>(); //Declaramos una nueva lista

            cafes.Add(one); //Agregamos todos los objetos a la lista
            cafes.Add(two);
            cafes.Add(three);
            cafes.Add(four);
            cafes.Add(five);

            //Instanciamos los dos usuarios que van a usar el sistema. Por un lado el administrador del stock y por otro lado el empleado.
            //El administrados podra consultar, agregar, eliminar y editar el stock.
            //El empleado solo podra consultar.

            Users admin = new Users("Admin1", "123456");
            Users empleado = new Users("Empleado1", "123456");

            Console.WriteLine("Control de stock de COFFE SHOP\n");

            Console.WriteLine("LOG IN\n");

            string usuario_input;
            string password_input;
            
            //Pido usuario y contrasena hasta que se ingresen correctamente
            
            Console.WriteLine("Ingrese usuario: ");
            usuario_input = Console.ReadLine();
            
            while (usuario_input != admin.Usuario && usuario_input != empleado.Usuario)
            {
                Console.WriteLine("Usuario incorrecto. Ingrese usuario: ");
                usuario_input = Console.ReadLine();
            }

            //Si es el administrador, llama a la funcion menuadmin que tiene todas las funcionalidades

            if (usuario_input == admin.Usuario)
            {
                Console.WriteLine("Ingrese contrasena: ");
                password_input = Console.ReadLine();

                while (password_input != admin.Password)
                {
                    Console.WriteLine("Contrasena incorrecta. Ingrese contrasena: ");
                    password_input = Console.ReadLine();
                }

                Console.WriteLine("\nBienvenido! Presione cualquier tecla.");
                Console.ReadKey();

                MenuAdmin(cafes); //Menu que tiene todas las funciones
            }

            //Si es el empleado, llama a la funcion menuempleado que solo permite consultar el stock.

            if (usuario_input == empleado.Usuario)
            {              
                Console.WriteLine("Ingrese contrasena: ");
                password_input = Console.ReadLine();
                
                while (password_input != empleado.Password)
                {
                    Console.WriteLine("Contrasena incorrecta. Ingrese contrasena: ");
                    password_input = Console.ReadLine();
                }

                Console.WriteLine("\nBienvenido! Presione cualquier tecla.");
                Console.ReadKey();

                MenuEmpleado(cafes); //Ejecuta el menu del empleado
            }

            //Mensaje al salir del programa

           
            Console.ReadKey();

        }

        //Menu del administrador con todas las funciones
        public static void MenuAdmin(List<Product> lista)
        {
            string opc = "0";
            while (opc != "5") //Creamos un ciclo para que se repita el menu hasta que el usuario quiera salir
            {
                Console.Clear();
                Console.WriteLine("\nMENU PRINCIPAL\n");
                Console.WriteLine("1) Agregar nuevo producto");
                Console.WriteLine("2) Modificar precio o cantidad de un producto");
                Console.WriteLine("3) Eliminar producto");
                Console.WriteLine("4) Consultar stock");
                Console.WriteLine("5) Salir");
                Console.Write("\r\nElija una opcion: ");
                opc = Console.ReadLine();

                switch (opc) //Menu
                {
                    case "1":
                        lista = Create(lista); //Le pasamos la lista con los 5 cafes
                        break;
                    case "2":
                        lista = Update(lista);
                        break;
                    case "3":
                        lista = Delete(lista);
                        break;
                    case "4":
                        Read(lista);
                        break;
                    case "5":
                        Console.WriteLine("\nNos vemos!");
                        return;
                }
                Console.WriteLine("\nPresione cualquier tecla para continuar.");
                Console.ReadKey();
            }
        }

        //Menu del empleado
        public static void MenuEmpleado(List<Product> lista)
        {
            string opc = "0";
            while (opc != "5") //Creamos un ciclo para que se repita el menu hasta que el usuario quiera salir
            {
                Console.Clear();
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("1) Consultar stock");
                Console.WriteLine("2) Salir");
                Console.Write("\r\nElija una opcion: ");
                opc = Console.ReadLine();

                switch (opc) //Menu
                {
                    case "1":                    
                        Read(lista);
                        break;
                    case "2":
                        Console.WriteLine("\nNos vemos!");
                        return; //Sale de la funcion
                }
                Console.WriteLine("\nPresione cualquier tecla para continuar.");
                Console.ReadKey();
            }
        }

        //Funcion para agregar productos a la lista.
        public static List<Product> Create(List<Product> lista) 
        {
            Console.WriteLine("Ingrese el Id: ");
            
            try
            {
                int id;
                id = Convert.ToInt32(Console.ReadLine());
                foreach (Product producto in lista)
                {
                    while (producto.Id == id) //Valida que el id nuevo no exista en la lista
                    {
                        Console.WriteLine("Ese id ya existe, escriba uno nuevo.");
                        id = Convert.ToInt32(Console.ReadLine());
                    }
                }


                Console.WriteLine("Ingrese el nombre: ");
                string nombre = Console.ReadLine();

                Console.WriteLine("Ingrese precio: ");
                decimal precio = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("Ingrese cantidad: ");
                int cantidad = Convert.ToInt32(Console.ReadLine());


                Product nuevo = new Product(id, nombre, precio, cantidad); //Instanciamos el nuevo producto 
                lista.Add(nuevo); //Lo agregamos a la lista

                Console.WriteLine("\nSe guardo correctamente.");
            }
           
                
            catch (FormatException)
            {
                Console.WriteLine("Debe ingresar un numero.");
                Create(lista);
            }



            return lista; //Devuelve la lista con el producto agregado
        }

        //Funcion que muestra el contenido de toda la lista y las actualizaciones que se van haciendo
        public static void Read(List<Product> lista) 
        {
            foreach (Product producto in lista) 
            {
                Console.WriteLine("\nId : {0}", producto.Id);
                Console.WriteLine("Nombre : {0}", producto.Name);
                Console.WriteLine("Precio : {0}", producto.Price) ;
                Console.WriteLine("Cantidad : {0}", producto.Quantity);
            }
        }

        //Funcion para modificar cantidad o precio del producto.
        public static List<Product> Update(List<Product> lista) 
        {
            try
            {
                Console.WriteLine("Que desea modificar?  \n 1. Precio \n 2. Cantidad \n Ingrese 1 o 2: "); //Ingrese si quiere cambiar precio o cantidad
                int categoria = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ingrese el id del producto a modificar: "); //Ingrese id del producto a modificar
                int id_modificar = Convert.ToInt32(Console.ReadLine());


                while ((lista.Exists(producto => producto.Id == id_modificar)) == false)
                {
                    Console.WriteLine("\nERROR. El producto con el id {0} no existe.", id_modificar);
                    Console.WriteLine("Ingrese el id del producto a eliminar: ");
                    id_modificar = Convert.ToInt32(Console.ReadLine());
                }


                if (categoria == 1) //Precio
                {
                    Console.WriteLine("Ingrese el nuevo precio: ");

                    decimal nuevo_precio = Convert.ToDecimal(Console.ReadLine()); //Ingresa nuevo precio

                    foreach (Product producto in lista) //Recorre la lista y cuando coincide el id del producto a modificar, reemplaza el precio
                    {
                        if (producto.Id == id_modificar)
                        {
                            producto.Price = nuevo_precio;
                        }
                    }
                }

                if (categoria == 2) //Cantidad
                {
                    Console.WriteLine("Ingrese la nueva cantidad: ");
                    int nueva_cantidad = Convert.ToInt32(Console.ReadLine()); //Ingresa nueva cantidad

                    foreach (Product producto in lista) //Recorre la lista y cuando coincide el id del producto a modificar, reemplaza el cantidad
                    {
                        if (producto.Id == id_modificar)
                        {
                            producto.Quantity = nueva_cantidad;
                        }
                    }
                }
                Console.WriteLine("\nSe guardaron los cambios.");
                
            }

            catch (FormatException)
            {
                Console.WriteLine("Debe ingresar un numero.");
                Update(lista);
            }
            return lista;

        }

        //Funcion para eliminar productos de la lista
        public static List<Product> Delete(List<Product> lista) 
        {

            Console.WriteLine("Ingrese el id del producto a eliminar: "); //Ingresa id del producto a eliminar
            
;           int id_eliminar = 0;
            id_eliminar = Convert.ToInt32(Console.ReadLine());
            
            while ((lista.Exists(producto => producto.Id == id_eliminar)) == false)
            {
                Console.WriteLine("\nERROR. El producto con el id {0} no existe.", id_eliminar);
                Console.WriteLine("Ingrese el id del producto a eliminar: ");
                id_eliminar = Convert.ToInt32(Console.ReadLine());
            }           
        

            foreach (Product producto in lista.ToList())//Recorre la lista y cuando coincide el id del producto a eliminar, te tira los datos
            {
                if (producto.Id == id_eliminar)
                {
                    Console.WriteLine("\nUsted esta por eliminar {0}. Desea continuar? si/no", producto.Name);
                }
            }

            string continuar = Console.ReadLine().ToLower();

            if (continuar == "si")
            {
                foreach (Product producto in lista.ToList())//Recorre la lista y cuando coincide el id del producto a eliminar, se elimina
                {
                    if (producto.Id == id_eliminar)
                    {
                        lista.Remove(producto);
                    }
                }

            }

            if(continuar == "no")
            {
                return lista;
            }

            Console.WriteLine("\nSe elimino correctamente.");
            return lista;
        }

     






    }
}
