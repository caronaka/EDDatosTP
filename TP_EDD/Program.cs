using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                Console.WriteLine("Usuario inexistente. Ingrese usuario: ");
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
            while (opc != "6") //Creamos un ciclo para que se repita el menu hasta que el usuario quiera salir
            {
                Console.Clear();
                Console.WriteLine("\nMENU PRINCIPAL\n");
                Console.WriteLine("1) Agregar nuevo producto");
                Console.WriteLine("2) Modificar precio o cantidad de un producto");
                Console.WriteLine("3) Eliminar producto");
                Console.WriteLine("4) Consultar stock");
                Console.WriteLine("5) Imprimir stock");
                Console.WriteLine("6) Salir");
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
                        Imprimir(lista);
                        break;
                    case "6":
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
            while (opc != "3") //Creamos un ciclo para que se repita el menu hasta que el usuario quiera salir
            {
                Console.Clear();
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("1) Consultar stock");
                Console.WriteLine("2) Imprimir stock");
                Console.WriteLine("3) Salir");
                Console.Write("\r\nElija una opcion: ");
                opc = Console.ReadLine();

                switch (opc) //Menu
                {
                    case "1":                    
                        Read(lista);
                        break;
                    case "2":
                        Imprimir(lista);
                        break;
                    case "3":
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
                        
            var string_id = Console.ReadLine();
            int id;
            while (!int.TryParse(string_id, out id))
            {
                Console.WriteLine("Se aceptan solo numeros.");
                Console.WriteLine("Ingrese un id correcto.");
                string_id = Console.ReadLine();
            }
            foreach (Product producto in lista)
            {
                while (producto.Id == id) //Valida que el id nuevo no exista en la lista
                {
                    Console.WriteLine("Ese id ya existe, escriba uno nuevo.");
                    string_id = Console.ReadLine();
                    
                    while (!int.TryParse(string_id, out id))
                    {
                        Console.WriteLine("Se aceptan solo numeros.");
                        Console.WriteLine("Ingrese un id correcto.");
                        string_id = Console.ReadLine();
                    }
                }
            }



            Console.WriteLine("Ingrese el nombre: ");

            string nombre = (Console.ReadLine());
            foreach (Product product in lista)
            {
                while (product.Name == nombre)//Valida que el nombre no se repita
                {
                    Console.WriteLine("Ese producto ya existe, ingrese otro nombre: ");
                    nombre = Convert.ToString(Console.ReadLine());
                        
                while(string.IsNullOrEmpty(nombre))//Valida que no este vacio
                {
                        Console.WriteLine("No puede estar vacio.");
                        Console.WriteLine("Ingrese un nombre correcto por favor: ");
                        nombre = Convert.ToString(Console.ReadLine());
                    }
                }
            }

            Console.WriteLine("Ingrese precio: ");
            var StringAPrecio = Console.ReadLine();
            decimal precio;
            while (!decimal.TryParse(StringAPrecio, out precio)) //Comprueba que sea solo numeros
            {
                Console.WriteLine("Se aceptan solo numeros.");
                Console.WriteLine("Ingrese el precio correcto: ");
                StringAPrecio = Console.ReadLine();
            }
               

            Console.WriteLine("Ingrese cantidad: ");
            var StringACantidad = (Console.ReadLine());
            int cantidad;
            while (!int.TryParse(StringACantidad, out cantidad)) //Comprueba que sea solo numeros
            {
                Console.WriteLine("Se aceptan solo numeros.");
                Console.WriteLine("Ingrese la cantidad nuevamente: ");
                StringACantidad = Console.ReadLine();
            }

            Product nuevo = new Product(id, nombre, precio, cantidad); //Instanciamos el nuevo producto 
            lista.Add(nuevo); //Lo agregamos a la lista

            Console.WriteLine("\nSe guardo correctamente.");


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
                    Console.WriteLine("\nSe guardaron los cambios.");
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
                    Console.WriteLine("\nSe guardaron los cambios.");
                }
                




                
                
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

            var string_id = Console.ReadLine();
            int id;
            while (!int.TryParse(string_id, out id))
            {
                Console.WriteLine("Se aceptan solo numeros.");
                Console.WriteLine("Ingrese un id correcto.");
                string_id = Console.ReadLine();
            }

            while ((lista.Exists(producto => producto.Id == id)) == false)  //Valida que el producto a eliminar exista
            {
                Console.WriteLine("\nERROR. El producto con el id {0} no existe.", id);
                Console.WriteLine("Ingrese el id del producto a eliminar: ");
                string_id = Console.ReadLine();
                
                while (!int.TryParse(string_id, out id))
                {
                    Console.WriteLine("Se aceptan solo numeros.");
                    Console.WriteLine("Ingrese un id correcto.");
                    string_id = Console.ReadLine();
                }
            }           
        

            foreach (Product producto in lista.ToList())//Recorre la lista y cuando coincide el id del producto a eliminar, te da el nombre y te pide que confirmes
            {
                if (producto.Id == id)
                {
                    Console.WriteLine("\nUsted esta por eliminar el producto {0}. Desea continuar? si/no", producto.Name.ToUpper());
                }
            }
           
            
            string continuar = Console.ReadLine().ToLower(); //valida que tome el si/no en minusculas o mayusculas

            while (continuar != "si" && continuar != "no")
            {
                Console.WriteLine("Debe ingresar si o no: ");
                continuar = Console.ReadLine().ToLower(); //valida que tome el si/no en minusculas o mayusculas
            }
            
            if (continuar == "si") //si ingresa si se elimina
            {
                foreach (Product producto in lista.ToList())//Recorre la lista y cuando coincide el id del producto a eliminar, se elimina
                {
                    if (producto.Id == id)
                    {
                        lista.Remove(producto);
                    }
                }
                Console.WriteLine("\nSe elimino correctamente.");

            }

            if(continuar == "no") 
            {
                return lista;
            }


            
            return lista;
        }

        public static void Imprimir(List<Product> lista) //Funcion que permite guardar el stock en un archivo para luego imprimirlo
        {
            try
            {
                Console.WriteLine("Ingrese el nombre del archivo: "); //Te pide el nombre del archivo
                
                string fileName = Console.ReadLine()+".txt";  //Agrega extension

                StreamWriter writer = File.CreateText(fileName); 

                DateTime today = DateTime.Now;  //Usamos datetime para agregar la fecha al archivo
                DateTime dateonly = today.Date;

                writer.WriteLine("Stock de COFFESHOP al dia {0}.", today.ToString("MM/dd/yyyy HH:mm")); //Titulo del archivo



                foreach (Product producto in lista.ToList())  //Print de todos los productos
                {

                    writer.WriteLine("\nId : {0}, Nombre : {1}, Precio : {2}, Cantidad : {3}", producto.Id, producto.Name, producto.Price, producto.Quantity);

                }

                writer.Close(); //Cierro archivo

                FileInfo fi = new FileInfo(fileName); //Uso file info para acceder a informacion del archivo y luego poder consultar el directorio
                DirectoryInfo di = fi.Directory; //Guardo el directorio en una variable para indicarselo al usuario

                Console.WriteLine("\nEl archivo {0} esta listo para imprimir en la ruta {1}.", fileName.ToUpper(),di);
            }

            catch (IOException) //Atrapa el error al manipular el archivo si es que hay
            {
                Console.WriteLine("\nError con el archivo."); 
            }

        }

     






    }
}
