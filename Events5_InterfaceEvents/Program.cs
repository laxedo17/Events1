using System;

namespace Events5_InterfaceEvents
{
    interface IMinhaInterface
    {
        //Un event de interface
        event EventHandler MeuIntCambiado;
    }

    class Emisor : IMinhaInterface
    {
        //Declaramos o event aqui e activamlo no lugar desexado
        public event EventHandler MeuIntCambiado;
        private int meuInt;
        public int MeuInt
        {
            get
            {
                return meuInt;
            }
            set
            {
                //Establecendo un novo valor antes de activar o evento
                meuInt = value;
                OnMeuIntCambiado();
            }
        }

        protected virtual void OnMeuIntCambiado()
        {
            if (MeuIntCambiado != null)
            {
                MeuIntCambiado(this, EventArgs.Empty);
            }
        }
    }

    class Receptor
    {
        public void GetNotificacionDoEmisor(object sender,System.EventArgs e)
        {
            Console.WriteLine("Receptor recibe unha notificacion: o emisor cambiou o valor meuInt recentemente");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Explorando un event con interface.***");
            Emisor emisor = new Emisor();
            Receptor receptor = new Receptor();
            //O receptor rexistrase/suscribese para obter unha notificacion do emisor
            emisor.MeuIntCambiado += receptor.GetNotificacionDoEmisor;

            emisor.MeuInt = 1;
            emisor.MeuInt = 2;
            //Des-rexistrandose
            emisor.MeuIntCambiado -= receptor.GetNotificacionDoEmisor;

            //Agora o receptor xa non recibe notificacion do emisor
            emisor.MeuInt = 3;
            Console.ReadKey();
        }
    }
}
