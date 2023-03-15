using System;
using Gtk;

namespace Barberos_21310438
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();

        // Definimos la estructura de un nodo de la lista enlazada de clientes
        public class ClienteNode
        {
            public int id;
            public ClienteNode next;

            public ClienteNode(int id)
            {
                this.id = id;
                this.next = null;
            }
        }

        // Definimos la estructura de la barbería
        public class Barberia
        {
            private int capacidad;           // Capacidad máxima de la barbería
            private int clientesEnEspera;    // Número de clientes en espera
            private ClienteNode primerCliente;  // Puntero al primer cliente en espera
            private ClienteNode ultimoCliente;  // Puntero al último cliente en espera

            public Barberia(int capacidad)
            {
                this.capacidad = capacidad;
                this.clientesEnEspera = 0;
                this.primerCliente = null;
                this.ultimoCliente = null;
            }

            // Función para ingresar un cliente a la barbería
            public void ingresarCliente(int id)
            {
                Console.WriteLine("Cliente " + id + " ha ingresado a la barbería.");

                // Creamos un nuevo nodo para el cliente
                ClienteNode nuevoCliente = new ClienteNode(id);

                // Si la barbería está llena, el cliente se va
                if (clientesEnEspera == capacidad)
                {
                    Console.WriteLine("La barbería está llena, el cliente " + id + " se va.");
                    return;
                }

                // Agregamos al cliente a la lista de espera
                if (primerCliente == null)
                {
                    primerCliente = nuevoCliente;
                    ultimoCliente = nuevoCliente;
                }
                else
                {
                    ultimoCliente.next = nuevoCliente;
                    ultimoCliente = nuevoCliente;
                }

                clientesEnEspera++;

                // Intentamos atender al cliente recién llegado
                atenderCliente();
            }

            // Función para atender a un cliente
            private void atenderCliente()
            {
                // Si no hay clientes en espera, no hay nada que hacer
                if (clientesEnEspera == 0)
                    return;

                // Intentamos asignar un barbero al cliente
                for (int i = 1; i <= 3; i++)
                {
                    if (Barbero.estaDisponible(i))
                    {
                        // Si el barbero está disponible, lo asignamos al cliente
                        Barbero.asignarCliente(i, primerCliente.id);

                        // Sacamos al cliente de la lista de espera
                        primerCliente = primerCliente.next;
                        clientesEnEspera--;

                        Console.WriteLine("El barbero " + i + " está atendiendo al cliente " + Barbero.clienteEnEspera(i) + ".");

                        // Intentamos atender al siguiente cliente en espera
                        atenderCliente();

                        return;
                    }
                }

                // Si no hay barberos disponibles, no hay nada que hacer
                return;
            }
        }

        // Definimos la estructura de un barbero
        public class Barbero
        {
            private static int[] clientesEnEspera = new int[4];  // Arreglo que indica el cliente en espera de cada barbero (el índice 0 no se usa)
            private static bool[] disponibles = new bool[4] { false, true, true, true }; // Arreglo que indica si cada barbero está disponible (el índice 0 no se usa)

            // Función que indica si un barbero está disponible
            public static bool estaDisponible(int id)
            {
                return disponibles[id];
            }

            // Función que asigna un cliente a un barbero
            public static void asignarCliente(int id, int cliente)
            {
                clientesEnEspera[id] = cliente;
                disponibles[id] = false;
            }

            // Función que libera a un barbero y devuelve el cliente que estaba atendiendo
            public static int liberarBarbero(int id)
            {
                disponibles[id] = true;
                return clientesEnEspera[id];
            }

            // Función que devuelve el cliente en espera de un barbero
            public static int clienteEnEspera(int id)
            {
                return clientesEnEspera[id];
            }
        }

        // Programa principal
        class Program
        {
            static void Main(string[] args)
            {
                // Creamos una barbería con capacidad para 5 clientes en espera
                Barberia barberia = new Barberia(5);

            }
    }
}
