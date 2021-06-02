using System;

namespace EventsEx1
{
    class Remite
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
                //Cando establecemos un novo valor, o evento activase
                OnMeuIntCambiou();
            }
        }

        //EventHandler e un delegate predefinido que se usa
        //para manexar eventos simples.
        //Ten a seguinte firma:         
        //delegate void System.EventHandler(object sender,System.EventArgs e)
        //onde o sender (remite) di quen esta enviando o evento e        
        //EventArgs usase para gardar informacion sobre o evento.
        public event EventHandler MeuIntCambiou;

        public void OnMeuIntCambiou()
        {
            if (MeuIntCambiou != null)
            {
                MeuIntCambiou(this, EventArgs.Empty);
            }
        }

        public void GetNotificacionASiMesmo(object sender,System.EventArgs e)
        {
            Console.WriteLine("Remite mandou unha notificacion a si mesmo: cambiei o valor do meuInt a {0}", meuInt);
        }
    }

    class Receptor
    {
        public void GetNotificacionDeRemite(object sender, System.EventArgs e)
        {
            Console.WriteLine("Receptor recibe unha notificacion: O remite cambiou recentemente o valor meuInt");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Explorando events.***");
            Remite remite = new Remite();
            Receptor receptor = new Receptor();
            //Receptor esta rexistrando unha notificacion do sender
            remite.MeuIntCambiou += receptor.GetNotificacionDeRemite;

            remite.MeuInt = 1;
            remite.MeuInt = 2;
            //Des-rexistrando, des-suscribidndo agora
            remite.MeuIntCambiou -= receptor.GetNotificacionDeRemite;
            //Agora non se envia a notificacion ao receptor
            remite.MeuInt = 3;
            //O remite recibira as suas propias notificacions de aqui en diante
            remite.MeuIntCambiou += remite.GetNotificacionASiMesmo;
            remite.MeuInt = 4;

            Console.ReadKey();
        }
    }
}
