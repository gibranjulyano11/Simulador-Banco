using System;
using System.Diagnostics;
using System.Threading;

namespace Simulacion_Banco
{
    class Program
    {
        public static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            watch.Stop();
            var milisegundosTranscurridos = watch.ElapsedMilliseconds;
            Console.WriteLine($"El tiempo trancurrido en milisegundos es : {milisegundosTranscurridos}");

            var watch1 = Stopwatch.StartNew();
            watch1.Stop();
            var ticksTranscurridos = watch.ElapsedTicks;
            Console.WriteLine($"El tiempo trancurrido en ticks es : {ticksTranscurridos}");

            int i,
            NumeroHilos = 20;
            Thread[] clientes = new Thread[NumeroHilos];
            for (i = 0; i < NumeroHilos; i++)
            {
                clientes[i] = new Thread(() => Atencion());
                clientes[i].Start();
                clientes[i].Join();
            }
            Console.ReadKey();
        }

        public static void Atencion()
        {
            int tiempOcio = 0,
            i = 0,
            MaximoClientes = 10;
            int[] cliente = new int[MaximoClientes];
            int[] tiempoEntrada = new int[MaximoClientes];
            int[] tiempoEspera = new int[MaximoClientes];
            int[] tiempoAtencion = new int[MaximoClientes];
            int[] tiempoSalida = new int[MaximoClientes];

            cliente[0] = 1;
            tiempoEntrada[0] = 0;
            tiempoEspera[0] = 0;
            tiempoAtencion[0] = new Random().Next(1, 10);
            tiempoSalida[0] = tiempoEntrada[0] + tiempoEspera[0] + tiempoAtencion[0];

            Console.WriteLine("-----------------------");
            Console.WriteLine("Cliente = " + cliente[0]);
            Console.WriteLine("Tiempo de Entrada: " + tiempoEntrada[0]);
            Console.WriteLine("Tiempo de Espera: " + tiempoEspera[0]);
            Console.WriteLine("Tiempo de Atención: " + tiempoAtencion[0]);
            Console.WriteLine("Tiempo de Salida: " + tiempoSalida[0]);

            for (i = 1; i < MaximoClientes; i++)
            {
                cliente[i] = i + 1;
                tiempoEntrada[i] = tiempoEntrada[i - 1] + new Random().Next(1, 5);
                if (tiempoSalida[i - 1] - tiempoEntrada[i] > 0)
                {
                    tiempoEspera[i] = tiempoSalida[i - 1] - tiempoEntrada[i];
                }
                else
                {
                    tiempOcio = tiempOcio + Math.Abs(tiempoSalida[i - 1] - tiempoEntrada[i]);
                    tiempoEspera[i] = 0;
                }
                tiempoAtencion[i] = new Random().Next(1, 10);
                tiempoSalida[i] = tiempoEntrada[i] + tiempoEspera[i] + tiempoAtencion[i];
                Console.WriteLine("-----------------------");
                Console.WriteLine("Cliente = " + cliente[i]);
                Console.WriteLine("Tiempo de Entrada: " + tiempoEntrada[i]);
                Console.WriteLine("Tiempo de Espera: " + tiempoEspera[i]);
                Console.WriteLine("Tiempo de Atención: " + tiempoAtencion[i]);
                Console.WriteLine("Tiempo de Salida: " + tiempoSalida[i]);
            }
            Console.WriteLine("\n Tiempo total: " + tiempOcio);
            Console.ReadKey();
        }
    }
}

