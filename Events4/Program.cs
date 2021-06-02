using System;

namespace Events4
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

    //Crea un Emisor -ou Editor- para o evento
    class Emisor
    {
        //Crea o evento basandose no teu delegate
        #region codigo equivalente
        //public evento MeuIntCambiadoEventHandler MeuIntCambiado
        private MeuIntCambiadoEventHandler meuIntCambiado;
        public event MeuIntCambiadoEventHandler MeuIntCambiado
        {
            add
            {
                Console.WriteLine("***Dentro do accessor de add. Punto de entrada***");
                meuIntCambiado += value;
            }
            remove
            {
                meuIntCambiado -= value;
                Console.WriteLine("***Dentro de accessor remove. Punto de saida***");
            }
        }
        #endregion

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
                //Cando estblecemos un novo valor, o vento activase
                OnMeuIntCambiado();
            }
        }

        protected virtual void OnMeuIntCambiado()
        {
            //if (MeuIntCambiado != null)
            if (meuIntCambiado != null)
            {
                //Combina os teus datos co argumento do evento
                NumTarefaEventArgs numTarefaEventArgs = new NumTarefaEventArgs();
                numTarefaEventArgs.NumTarefa = meuInt;
                //MeuIntCambiado(this,numTarefaEventArgs);
                meuIntCambiado(this, numTarefaEventArgs);
            }
        }
    }

    //Crea un Receptor -ou subscriptor- para o evento.
    class Receptor
    {
        public void GetNotificacionDoEmisor(object sender,NumTarefaEventArgs e)
        {
            Console.WriteLine("Receptor recibe unha notificacion: Emisor cambiou recientemente o valor meuInt a {0}", e.NumTarefa);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Usando accessors de eventos.***");
            Emisor emisor = new Emisor();
            Receptor receptor = new Receptor();
            //O receptor estase suscribindo ás notificacións do emisor
            emisor.MeuIntCambiado += receptor.GetNotificacionDoEmisor;

            emisor.MeuInt = 1;
            emisor.MeuInt = 2;
            //Des-rexistrandose agora
            emisor.MeuIntCambiado -= receptor.GetNotificacionDoEmisor;
            //O receptor agora non recibe notificacions do emisor
            emisor.MeuInt = 3;
            Console.ReadKey();
        }
    }
}
