using System;

namespace EventsEx3
{
    //Crea unha subclase de System.EventArgs
    class NumTarefaEventArgs : EventArgs
    {
        int numTarefa = 0;
        public int NumTarefa
        {
            get
            {
                return numTarefa;
            }
            set
            {
                numTarefa = value;
            }
        }
    }

    //Crea un delegate
    delegate void MeuIntCambiadoEventHandler(object sender, NumTarefaEventArgs eventArgs);

    //Crea un remite -ou editor- para o evento
    class Emisor
    {
        //Crea un evento basado no teu delegate
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
                //Activa o evento
                //Cando estableces un novo valor, o evento dispárase
                OnMeuIntCambiado();
            }
        }

        /*Na practica estandar, o nome do metodo e o nome do evento co prefixo 'On'. Por exemplo, MeuIntCambiado(nome de evento) esta prefixado con 'On' neste caso.
         * Ademais, como practicas normales, en vez de facer o metodo 'public', faise 'protected virtual'.         */
        private void OnMeuIntCambiado()
        {
            if (MeuIntCambiado != null)
            {
                //Combina os teus datos cos argumentos dos eventos
                NumTarefaEventArgs numTarefaEventArgs = new NumTarefaEventArgs();
                numTarefaEventArgs.NumTarefa = meuInt;
                MeuIntCambiado(this, numTarefaEventArgs);
            }
        }
    }

    class Receptor
    {
        public void GetNotificacionDeRemite(object sender, NumTarefaEventArgs e)
        {
            Console.WriteLine("O receptor recibe a notificacion: O remite cambiou o valor de meuInt recentemente a {0}", e.NumTarefa);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Pasando datos nos argumentos do evento.***");
            Emisor remite = new Emisor();
            Receptor receptor = new Receptor();
            //Receptor estase suscribidno a unha notificacion do remite (emisor)
            remite.MeuIntCambiado += receptor.GetNotificacionDeRemite;
            remite.MeuInt = 1;
            remite.MeuInt = 2;
            //Agora de-suscribindose
            remite.MeuIntCambiado -= receptor.GetNotificacionDeRemite;
            //Agora non se notifica nada pola parte do receptor
            remite.MeuInt = 3;
            Console.ReadKey();
        }
    }
}
