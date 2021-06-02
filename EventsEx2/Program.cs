using System;

namespace EventsEx2
{
    //Paso 1-Crea un delegate.     
    //Podes elexir un nome (este nome sera o teu nome de evento)     
    //o cal ten o sufixo EventHandler. Por exemplo, no seguinte caso   
    //'MeuIntCambiado' e o nome do evento que ten o sufixo 'EventHandler'
    delegate void MeuIntCambiadoEventHandler(object sender, EventArgs eventArgs);

    //Crea un remite -ou tb chamado editor- para o evento
    class Remite
    {
        //Paso 2-Crea un evento basado no teu delegate
        public event MeuIntCambiadoEventHandler MeuIntCambiado;

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
                //Lanza o evento
                //Cada vez que establecemos un novo valor, o evento activarase
                OnMeuIntCambiado();
            }
        }

        /*Paso-3.          
         * Na practica estandar, o nome do metodo e o nome do evento co prefixo 'On'. Por exemplo, MeuIntCambiado(nome de evento) esta prefixado con 'On' neste caso.
         * Ademais, como practicas normales, en vez de facer o metodo 'public', faise 'protected virtual'.         */
        protected virtual void OnMeuIntCambiado()
        {
            if (MeuIntCambiado != null)
            {
                MeuIntCambiado(this, EventArgs.Empty);
            }
        }
    }

    //Paso-4. Creamos un Receptor ou Subscriptor para o evento
    class Receptor
    {
        public void GetNotificacionDoRemite(object sender, System.EventArgs e)
        {
            Console.WriteLine("O receptor recibe unha notificacion: O remite cambiou o valor de int recentemente");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Explorando un event personalizado.***");
            Remite remite = new Remite();
            Receptor receptor = new Receptor();
            //Receptor agora suscribese e rexistrase en calquer evento do Remite
            remite.MeuIntCambiado += receptor.GetNotificacionDoRemite;

            remite.MeuInt = 1;
            remite.MeuInt = 2;

            //Receptor agora de-suscribese e xa non estara rexistrado nos cambios efectuados por remite
            remite.MeuIntCambiado -= receptor.GetNotificacionDoRemite;
            //Agora non se envia notificacion do cambio ao receptor
            remite.MeuInt = 3;
            Console.ReadKey();
        }
    }
}
