using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace darkproject
{
    class Program
    {

        static TDx.TDxInput.Device device;

        static void Main(string[] args)
        {
            


            Console.WriteLine("Start of programm....");

            device = new TDx.TDxInput.Device();
            Console.WriteLine(device.Sensor);
            Console.WriteLine(device.Sensor.Translation);
            System.Threading.Thread.Sleep(2500);

            Console.WriteLine(device.IsConnected);
            device.Connect();
            Console.WriteLine(device.IsConnected);
            System.Threading.Thread.Sleep(2500);
            // la souris est connecte


            while (true)
            {


                var translation = device.Sensor.Translation;
                var rotation = device.Sensor.Rotation;

                Console.WriteLine("Translation : " + translation.X + ";" + translation.Y + ";" + translation.Z);
                Console.WriteLine("Rotation : " + rotation.X + ";" + rotation.Y + ";" + rotation.Z);

                System.Threading.Thread.Sleep(50);
            }

            Console.WriteLine("End programm, press a key to exit.....");
            Console.ReadKey();
        }
    }
}
