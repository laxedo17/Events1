using System;

namespace Events7_AbstractEvent
{
    abstract class Emisor
    {
        private int meuInt;
        public int MeuInt
        {
            get
            {
                return meuInt;
            }
            set
            {
                meuInt = value;
                //Cada vez que establecemos un valor, o evento activase
                OnMeuIntCambiado();
            }
        }

        //Event abstracto. A clase contenedora volvese abstracta a causa disto (por iso o abstract na definicion da clase)
        public abstract event EventHandler MeuIntCambiado;
        protected virtual void OnMeuIntCambiado()
        {
            Console.WriteLine("Emisor.OnMeuIntCambiado");
        }
    }

    class EmisorConcreto : Emisor
    {
        public override event EventHandler MeuIntCambiado;
        protected override void OnMeuIntCambiado()
        {
            if (MeuIntCambiado != null)
            {
                MeuIntCambiado(this, EventArgs.Empty);
            }
        }
    }

    class Receptor
    {
        public void GetNotificacionDoEmisor(object sender, System.EventArgs e)
        {
            Console.WriteLine("O receptor recibe unha notificacion: O emisor cambiou o valor meuInt recentemente");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Explorando un event abstract.***");
            Emisor emisor = new EmisorConcreto();
            Receptor receptor = new Receptor();
            //O receptor rexistraxe para recibir notificacions do emisor
            emisor.MeuIntCambiado += receptor.GetNotificacionDoEmisor;
            emisor.MeuInt = 1;
            emisor.MeuInt = 2;
            //Des-rexistrandose agora
            emisor.MeuIntCambiado -= receptor.GetNotificacionDoEmisor;
            //Agora non hai notificacions enviadas por parte do emisor
            emisor.MeuInt = 3;

            Console.ReadKey();
        }
    }
}
