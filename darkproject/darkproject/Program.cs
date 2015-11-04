using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace darkproject
{
    class Program
    {

        static TDx.TDxInput.Device device;

        static void Main(string[] args)
        {

            /*

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

            */

            // init robot
            var a = new NLX.Robot.Kuka.Controller.RobotController();
            a.Connect("192.168.1.1");

            float _X = 50;
            float _Y = 0;
            float _Z = 0;
            float _A = 0;
            float _B = 0;
            float _C = 0;

            traiterDeplacements(_X, _Y, _Z, _A, _B, _C);
        }

        static void traiterDeplacements(double _X, double _Y, double _Z, double _A, double _B, double _C)
        { 


            // get pos from souris
            var pos = new NLX.Robot.Kuka.Controller.CartesianPosition();
            pos.X = _X;
            pos.Y = _Y;
            pos.Z = _Z;
            pos.A = _A;
            pos.B = _B;
            pos.C = _C;

            if ((pos.X == 0) && (pos.Y == 0) && (pos.Z == 0) && (pos.A == 0) && (pos.B == 0) && (pos.C == 0))
            {
                Console.WriteLine("0 pos");

                a.StopRelativeMovement();

            }
            else
            {
                
                var sensorTest = a.ReadSensor();
                Console.WriteLine("b: " + sensorTest);
                Console.WriteLine("test5");
                a.OpenGripper();
                Thread.Sleep(1000);
                a.CloseGripper();
                Console.WriteLine("test6");
                

                //var pos = a.GetCurrentPosition();

                Console.WriteLine("pos.X: " + pos.X);
                Console.WriteLine("pos.X: " + pos.X);
                a.SetRelativeMovement(pos);
                Console.WriteLine("test7");
                a.StartRelativeMovement();
                Console.WriteLine("test8");

            }

            Console.WriteLine("End programm, press a key to exit.....");
            Console.ReadKey();
        }
    }
}
