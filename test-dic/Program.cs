using System;
using ReceptionSdk.Http;
using ReceptionSdk.Models;
using ReceptionSdk;
using System.Collections.Generic;
using ReceptionSdk.Clients;
using ReceptionSdk.Exceptions;
using ReceptionSdk.Enums;
using ReceptionSdk.Utils;
using System.Threading;
using System.Collections;

namespace test_dic
{
    class Program
    {
        
        /////////////////////////////////////////////////
        ///                                           ///
        ///  A R M A D O  D E L  H E A R T B E A T    ///
        ///                                           ///
        /////////////////////////////////////////////////
        
        /*public static void hb(ApiClient apiClient)
        {
            Timer timer = new Timer(
                AsyncCallback =>
                {

                    PaginationOptions options = PaginationOptions.Create();
                    List<Restaurant> restaurants = new List<Restaurant>();
                    IList<Restaurant> newRestaurants = apiClient.Restaurant.GetAll(options);
                    restaurants.AddRange(newRestaurants);
                    while (newRestaurants.Count != 0)
                    {
                        newRestaurants = apiClient.Restaurant.GetAll(options.Next());
                        restaurants.AddRange(newRestaurants);
                    }
                    foreach (var aux in restaurants)
                    {
                        apiClient.Event.HeartBeat(aux.Id);
                    }
                }
                );
            timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(2));
        }*/
        static void Main(string[] args)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.WriteLine("Bienvenido a la Prueba de la Integracion .Net");
            
            /////////////////////////////////////////////////
            ///                                           ///
            ///    C O N E X I O N  A  L A  A P I         ///
            ///                                           ///
            /////////////////////////////////////////////////
            
            var credenciales = new Credentials
            {
                //Nombre: integration_norkys
                //Secret: W%EMaAO88s

                ClientId = "integration_365_4_0",
                ClientSecret = "jC5QWe8cL!",
                //Username = "iglu_helad1696",
                //Password = "39d86708c6",

                //Username = "main_stree9429",
                //Password = "bc7a35cdd5",
                Environment = Environments.PRODUCTION

                

            };
            ApiClient api = new ApiClient(credenciales);
            // Nombre: integration_pixel_point  /  Secret: 7c7e9cfa72 || Nombre: integration_nucleo_it Pass:dj235dslmn4e4
            // integration_zuper_nicaragua %t%HSae6AH || Nombre: integration_ketal  Secret: 1FJ%Cvnr2X
            // Nombre: integration_friday Secret: RXd79DOe%k || Nombre: integration_sofia Contraseña: a36d67955b
            // Nombre: integration_pragmaz Secret: m5!HkiWmyk || Nombre: integration_uruware Secret: f920976e65 || Nombre: integration_sofia Contraseña: a36d67955b
            // "integration_gym_jm_tb" "HSw12%N!Av"; || Nombre: integration_gyn Secret: 8457dcdff4 || Nombre: integration_wolff Secret: UHrOxrP58%
            // Nombre: integration_salemma - Secret: Ty3Xu%E6gu || Nombre: integration_farsiman - Secret: nNScOi%ex_
            //Nombre: integration_kfc_rd Secret: b!EukOgvpn  ||  integration_durum 4d3cf6a7bd  || Nombre: integration_starbucks_ar Secret: 541dfef366
            // Nombre: integration_pronto_copec Secret: u0SYsjZ%LV || Nombre: integration_puppis / Secret: CKeZf%Q4WU

            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///       G E T   D E   P E R F I L E S       ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            
            /*
            PaginationOptions options = PaginationOptions.Create();
            List<Restaurant> restaurants = new List<Restaurant>();
            IList<Restaurant> newRestaurants = api.Restaurant.GetAll(options);
            restaurants.AddRange(newRestaurants);
            while (newRestaurants.Count != 0)
            {
                newRestaurants = api.Restaurant.GetAll(options.Next());
                restaurants.AddRange(newRestaurants);
            }
            Console.WriteLine("Los Partner con la Integracion Son: ");

            foreach (var aux in restaurants)
            {
                Console.WriteLine(aux.Id + " - " + aux.Name);
            }
            */
            
            
            
            ////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////
            ///                                                      ///
            ///   E V E N T O   D E  I N I C I A L I Z A C I O N     ///
            ///                                                      ///
            ////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////

            /*Console.WriteLine("");
            dynamic version = new System.Dynamic.ExpandoObject();
            version.os = "Mac OS";
            version.app = "Mandarina_Alfa";
            foreach (var aux in restaurants)
            {
                Console.WriteLine(aux.Id + aux.Name);
                api.Event.Initialization(version, aux.Id);
            }*/

            ////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////
            ///                                                      ///
            ///        E V E N T O    D E   H E A R T B E A T        ///
            ///                                                      ///
            ////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////


            // Thread thread = new Thread(() => hb(api));
            // thread.Start();


            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///       D E L I V E R Y    T I M E S        ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////

            /*IList<DeliveryTime> tiempos = api.Order.DeliveryTime.GetAll();
            Console.WriteLine("");
            Console.WriteLine("Los tiempos de Entrega Son: ");
            Console.WriteLine("");
            long tiem1 = 1;
            foreach (var aux in tiempos)
            {
                if (aux.Id < 6)
                {
                    Console.WriteLine(aux.Id + ") " + aux.Description);
                    if (aux.Id == 1)
                    {
                        tiem1 = aux.Id;
                    }
                }
            }
            Console.WriteLine("");*/

            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///     M O T I V O S  D E  R E C H A Z O     ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////

            /*
            IList<RejectMessage> rechazos = api.Order.RejectMessage.GetAll();
            Console.WriteLine("Los Motivos de Rechazo MKP son:");
            Console.WriteLine("");
            int cont = 1;
            foreach (var aux in rechazos)
            {
                if ((aux.ForLogistics && aux.ForPickup) || aux.ForPickup)
                {
                    Console.WriteLine(aux.Id + ") " + aux.DescriptionES);
                    cont++;
                }
            }
            Console.WriteLine("Los Motivos de Rechazo PickUP son:");
            Console.WriteLine("");
            int cont2 = 1;
            foreach (var aux in rechazos)
            {
                if (aux.ForPickup)
                {
                    Console.WriteLine(aux.Id + ") " + aux.DescriptionES);
                    cont2++;
                }
            }
            Console.WriteLine("Los Motivos de Rechazo Logistico son:");
            Console.WriteLine("");
            int cont3 = 1;
            foreach (var aux in rechazos)
            {
                if (aux.ForLogistics)
                {
                    Console.WriteLine(aux.Id + ") " + aux.DescriptionES);
                    cont3++;
                }
            }*/

            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///      O B T E N E R    O R D E N E S       ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////

            /*
            //long orden1 = 0;
            long rest = 0;
            //bool log = false;
            //bool pre = false;

            Console.WriteLine("Esperando por Nuevas Ordenes");
            Console.WriteLine(" < -   >  - >");
            api.Order.GetAll(onSuccess: (Order order) => {
                Console.WriteLine("Tienes un nuevo Pedido: " + order.Id);
                Console.WriteLine("");
                string state = order.State;
                Console.WriteLine("Orden " + order.Id + " en esstado: " + state);
                //rest = order.Restaurant.Id;
                //log = order.Logistics;
                //pre = order.PreOrder;
                //agregar try catch para cuando la orden no existe
                //api.Event.Reception(order.Id, rest);
                //orden1 = order.Id;
                //Mostrar la comanda
                api.Event.Acknowledgement(order.Id, rest);
                //api.Order.Confirm(orden1);
               
                return false;
            },
            onError: (ApiException ex) => {
                //poner excepciones
                Console.WriteLine(ex);
                throw ex;

            }
            );
            Console.WriteLine(" < -  ||  - >");
            */
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///     C O N F I R M A R   O R D E N E S     ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////

            /*
            string entrada = Console.ReadLine();
            long re = long.Parse(entrada);
            if (log == true || pre == true)
            {
                api.Order.Confirm(orden1);
            }
            else
            {
                try
                {
                    if (entrada.Equals("1"))
                    {
                        api.Order.Confirm(orden1, 1);
                        Console.WriteLine("Succes Pedido confirmado!!!");
                    }
                    /*else if (entrada.Equals("2"))
                    {
                        api.Order.Confirm(orden1, tiem2);
                        Console.WriteLine("Succes Pedido confirmado!!!");
                    }
                    else if (entrada.Equals("3"))
                    {
                        api.Order.Confirm(orden1, tiem3);
                        Console.WriteLine("Succes Pedido confirmado!!!");
                    }
                    else if (entrada.Equals("4"))
                    {
                        api.Order.Confirm(orden1, tiem4);
                        Console.WriteLine("Succes Pedido confirmado!!!");
                    }
                    else if (entrada.Equals("5"))
                    {
                        api.Order.Confirm(orden1, tiem5);
                        Console.WriteLine("Succes Pedido confirmado!!!");
                    }*/
            /*
                    else
                    {
                        //mejorar el rechazo seleccionando el motivo que sea.
                        api.Order.Reject(orden1, re, "Rejection Note");
                    }

                }
                catch (ApiException ex)
                {
                    Console.WriteLine("Orden No existe");
                }
            }*/
            //}

            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///  C A T A L O G U E    O P E R A T I O N   ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////


            long restaurantId = 438276;


            ///////////////////////////////////////////////////
            ///                                             ///
            ///       S E C T I O N    C R U D              ///
            ///                                             ///
            ///////////////////////////////////////////////////

            /*IList<object> sections = api.Section.GetAll(null, restaurantId);  // <-- Descomentar para utilizar el GetAll Section
            IDictionary productLine;
            foreach (var item in sections)
            {
                productLine = (Dictionary<String, object>)item;
                String nombre = productLine["name"].ToString();
                String intCode = productLine["integrationCode"].ToString();
                Console.WriteLine(nombre + " - " + intCode);
            }*/




            Schedule sectionSchedule = new Schedule();
            Section section = new Section()
            {
                Name = "q1",
                IntegrationCode = "q1",
            };
            sectionSchedule.Entity = section;
            sectionSchedule.From = "15:10";
            sectionSchedule.To = "15:30";
            sectionSchedule.Day = 4;

            //api.Section.CreateSchedule(sectionSchedule, restaurantId)

            /*
            //List<object> sections = api.Section.GetAll("", 262737);
            //Console.WriteLine(sections);
            */


            Section sectionC = new Section
            {
                IntegrationCode = "test1",  // Mandatory, cannot be empty or null
                Name = "test .Net",         // Mandatory, cannot be empty or null
                IntegrationName = "algo",
                Enabled = true,
                Index = 1
            };
            //api.Section.Create(sectionC, restaurantId);  // <-- Descomentar para utilizar el Create Section

            Section sectionD = new Section
            {
                IntegrationCode = "test1",  // Mandatory, cannot be empty or null
                Name = "test .Net",         // Mandatory, cannot be empty or null
                IntegrationName = "algo",
                Enabled = true,
                Index = 1
            };
            //api.Section.Delete(sectionD, restaurantId);  // <-- Descomentar para utilizar el Delete Section

            Section sectionU = new Section
            {
                IntegrationCode = "test1",  // Mandatory, cannot be empty or null
                Name = "test .Net",         // Mandatory, cannot be empty or null
                IntegrationName = "algo",
                Enabled = true,
                Index = 1
            };
            //api.Section.Update(sectionU, restaurantId);  // <-- Descomentar para utilizar el Update Section


            ///////////////////////////////////////////////////
            ///                                             ///
            ///       P R O D U C T S    C R U D            ///
            ///                                             ///
            ///////////////////////////////////////////////////

            Product productC = new Product
            {
                IntegrationCode = "7730900572262",
                Name = "Perifar",
                //Description = "este es un test creado en .Net",
                Price = 999,
                Gtin = "7730900572262",
                Image = "https://images.deliveryhero.io/image/pedidosya/products/214608_b7422965-6e4d-4f64-a005-c744197e491f.jpg?quality=95",
                Section = new Section
                {
                    Name = "Lacteos"
                },
                Enabled = true,
                Index = 1
            };
            //Object result = api.Product.Create(productC, restaurantId);  // <-- Descomentar para utilizar el Create Product
            //Console.WriteLine(result);


            Product productG = new Product   //   <-- Get a Product by integrationCode
            {
                IntegrationCode = "7730900572262",
                //Name = "P.u queso de cerdo al vacio 250 g",
                Section = new Section
                {
                    Name = "Lacteos",
                },
            };
            /*object product = api.Product.GetByIntegrationCode(productG, restaurantId);
            IDictionary productLine;
            productLine = (Dictionary<String, object>)product;
            string name = productLine["name"].ToString();
            string price = productLine["price"].ToString();
            //string gtin = productLine["gtin"].ToString();
            Console.WriteLine("Nombre: " + name+ " - Precio: " + price);*/


            Product productGetAll = new Product    //  <-- Get all products
            {
                Section = new Section
                {
                    Name = "Aguas Y Jugos"
                },
            };
            
            IList<object> products = api.Product.GetAll(productGetAll, restaurantId);
            IDictionary productLine;
            foreach (var item in products)
            {
                productLine = (Dictionary<String, object>)item;
                string name = productLine["name"].ToString();
                string price = productLine["price"].ToString();
                Console.WriteLine(name + price);
            }
            

            /*
            Product productGetByIC = new Product
            {
                Section = new Section
                {
                    Name = "Alacena",
                    //IntegrationCode = "test",
                },
                IntegrationCode = "test",
            };*/
            //api.Product.GetByIntegrationCode(productGetByIC, 42);



            Product productU = new Product
            {
                IntegrationCode = "7730900572262",
                //Name = "Perifar",
                //Description = "Descripcion Test",
                Price = 111,
                Section = new Section
                {
                    Name = "Lacteos"
                },
                Enabled = true,
            };
            //Object result = api.Product.Update(productU, restaurantId); // <-- Descomentar para utilizar el Update Product
            //Console.WriteLine(result);


            Product productD = new Product
            {
                IntegrationCode = "7730900572262",
                //Name = "test .Net product",
                Section = new Section
                {
                    Name = "Lacteos",
                    //IntegrationCode = "test1"
                },
            };
            // Object result = api.Product.Delete(productD, restaurantId);  // <-- Descomentar para utilizar el Delete Product
            // Console.WriteLine(result);


            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///          P R O M O T I O N S              ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////

            /*Promotion promocion = new Promotion();
            promocion.Name = "Precio Tachado";
            promocion.Enable = true;
            promocion.AddItem("7702010381881", "GTIN", 150, true);
            promocion.AddVendor(206419, true);
            promocion.ExpositionDates = new ExpositionDates
            {
                from = "2022-03-03",
                to = "2022-03-30"
            };
            long partnerId = 206419;
            var response = api.Promotion.Create(promocion, partnerId);
            Console.WriteLine(response);*/

            /////////////////////////////////////////////////
            /////////////////////////////////////////////////
            ///                                           ///
            ///           G E T   T A X E S               ///
            ///                                           ///
            /////////////////////////////////////////////////
            /////////////////////////////////////////////////


            /*long restaurantId = 7267;

            string[] products = new string[]
            {
                "07730987420043",
                "07730105970085",
                "07730987420012"
            };
            IList<object> taxes = api.Order.GetTaxes(products, restaurantId);
            foreach (var item in taxes)
            {
                Console.WriteLine(item);
            }
            */

        }
    }
}
