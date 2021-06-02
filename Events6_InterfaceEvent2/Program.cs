using System;

namespace Events6_InterfaceEvent2
{
    interface IAntesDaInterface
    {
        public event EventHandler MeuIntCambiado;
    }

    interface IDespoisDaInterface
    {
        public event EventHandler MeuIntCambiado;
    }

    class Emisor : IAntesDaInterface, IDespoisDaInterface
    {
        //Creando 2 events separados por dous interface events
        public event EventHandler AntesMeuIntCambiado;
        public event EventHandler DespoisMeuIntCambiado;
        //Microsoft recomenda isto, ex., para usar un lock dentro dos accessors
        object objectLock = new Object();

        private int meuInt;
        public int MeuInt
        {
            get
            {
                return meuInt;
            }
            set
            {
                //Lanza un evento antes de que cambiemos meuInt.
                OnMeuIntCambiadoAntes();
                Console.WriteLine("Facendo un cambio a meuInt de {0} a {1},", meuInt, value);
                meuInt = value;
                //Lanza un evento despois de que cambiemos meuInt
                OnMeuIntCambiadoDespois();
            }
        }

        //Requirese implementacion explicita da interface
        //Asocia o evento IAntesDaInterface con
        //AntesMeuIntCambiado
        event EventHandler IAntesDaInterface.MeuIntCambiado
        {

            add
            {
                lock (objectLock)
                {
                    AntesMeuIntCambiado += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    AntesMeuIntCambiado -= value;
                }
            }
        }

        //Requirese implementacion explicita da interface
        //Asocia o evento IAntesDaInterface con
        //AntesMeuIntCambiado
        event EventHandler IDespoisDaInterface.MeuIntCambiado
        {
            add
            {
                lock (objectLock)
                {
                    DespoisMeuIntCambiado += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    DespoisMeuIntCambiado -= value;
                }
            }
        }

        //Este metodo utiliza o event AntesMeuIntCambiado
        protected virtual void OnMeuIntCambiadoAntes()
        {
            if (AntesMeuIntCambiado != null)
            {
                AntesMeuIntCambiado(this, EventArgs.Empty);
            }
        }

        //Este metodo utiliza o event DespoisMeuIntCambiado
        protected virtual void OnMeuIntCambiadoDespois()
        {
            if (DespoisMeuIntCambiado != null)
            {
                DespoisMeuIntCambiado(this, EventArgs.Empty);
            }
        }
    }

    //Primeiro receptor: class ReceptorAntes
    class ReceptorAntes
    {
        public void GetNotificacionDoEmisor(object sender, System.EventArgs e)
        {
            Console.WriteLine("ReceptorAntes recibe mensaxe: Emisor esta a punto de cambiar o valor meuInt");
        }
    }

    //Segundo receptor: class ReceptorDespois
    class ReceptorDespois
    {
        public void GetNotificacionDoEmisor(object sender, System.EventArgs e)
        {
            Console.WriteLine("ReceptorDespois recibe mensaxe: O emisor cambiou o valor meuInt recentemente");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Manexando interface events explicitos.***");
            Emisor emisor = new Emisor();
            ReceptorAntes receptorAntes = new ReceptorAntes();
            ReceptorDespois receptorDespois = new ReceptorDespois();
            //Os receptores estanse suscribindo para obter
            //notificacions do emisor
            emisor.AntesMeuIntCambiado += receptorAntes.GetNotificacionDoEmisor;
            emisor.DespoisMeuIntCambiado += receptorDespois.GetNotificacionDoEmisor;
            emisor.MeuInt = 1;
            Console.WriteLine("");
            emisor.MeuInt = 2;
            //De-suscribindose agora
            emisor.AntesMeuIntCambiado -= receptorAntes.GetNotificacionDoEmisor;
            Console.WriteLine("");
            //Non se envian notificacions do emisor ao receptor agora
            emisor.MeuInt = 3;

            Console.ReadKey();

        }
    }
}
