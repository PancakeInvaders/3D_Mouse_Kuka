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

        static NLX.Robot.Kuka.Controller.RobotController a;

        static TDx.TDxInput.Device device;

        static void Main(string[] args)
        {
            Program prog = new Program();

            Console.WriteLine("Start of programm....");

            device = new TDx.TDxInput.Device();
            //keyboard = new TDx.TDxInput.Keyboard();
            var keyboard = device.Keyboard;
            Console.WriteLine(device.Sensor);
            Console.WriteLine(device.Sensor.Translation);
            //System.Threading.Thread.Sleep(2500);

            Console.WriteLine(device.IsConnected);
            device.Connect();
            Console.WriteLine(device.IsConnected);
            //System.Threading.Thread.Sleep(2500);
            // la souris est connecte

            /*Console.WriteLine("nombre de touches : ");
            Console.WriteLine(keyboard.Keys);

            Console.WriteLine("nombre de touches programmable : ");
            Console.WriteLine(keyboard.ProgrammableKeys);*/

            //keyboard.KeyDown += new System.EventHandler(prog.onKeyDown);

            //double xMax = 0, yMax = 0, zMax = 0;

            a = new NLX.Robot.Kuka.Controller.RobotController();
            a.Connect("192.168.1.1");
            Console.WriteLine("Connection avec le robot OK !");

            while (true)
            {
                
                var translation = device.Sensor.Translation;
                var rotation = device.Sensor.Rotation;

                //Console.WriteLine("Translation : " + translation.X + ";" + translation.Y + ";" + translation.Z);
                //Console.WriteLine("Rotation : " + rotation.X + ";" + rotation.Y + ";" + rotation.Z);

                /*if (translation.X > xMax)
                    xMax = translation.X; Console.WriteLine("xMax= " + xMax);
                if (translation.Y > yMax)
                    yMax = translation.Y; Console.WriteLine("yMax= " + yMax);
                if (translation.Z > zMax)
                    zMax = translation.Z; Console.WriteLine("zMax= " + zMax);*/

                String valeurMax = getPriorityMouvement(translation.X/2900, translation.Y/2900, translation.Z/2900, rotation.X, rotation.Y, rotation.Z);
                Console.WriteLine("before switch");
                switch (valeurMax)
                {
                    case "x":
                        traiterDeplacements(10.0, 0.0, 0.0, 0.0, 0.0, 0.0);
                        Console.WriteLine("Avancer sur x");
                        break;
                    case "y":
                        traiterDeplacements(0.0, 10.0, 0.0, 0.0, 0.0, 0.0);
                        Console.WriteLine("Avancer sur y");
                        break;
                    case "z":
                    traiterDeplacements(0.0, 0.0, 10.0, 0.0, 0.0, 0.0);
                        Console.WriteLine("Avancer sur z");
                        break;
                    /*case "a":
                        traiterDeplacements(0.0, 0.0, 0.0, 10.0, 0.0, 0.0);
                        Console.WriteLine("Rotation sur a");
                        break;
                    case "b":
                        traiterDeplacements(0.0, 0.0, 0.0, 0.0, 10.0, 0.0);
                        Console.WriteLine("Rotation sur b");
                        break;
                    case "c":
                        traiterDeplacements(0.0, 0.0, 0.0, 0.0, 0.0, 10.0);
                        Console.WriteLine("Rotation sur c");
                        break;*/
                    default:
                        //Console.WriteLine("Default case");
                        traiterDeplacements(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
                        break;
                }
                System.Threading.Thread.Sleep(50);
            }

            Console.WriteLine("End programm, press a key to exit.....");
            Console.ReadKey();
        }

        /*public void onKeyDown(object sender, EventArgs e)
        {
            Console.WriteLine("a key down");
        }*/

        //Fonction de trie , renvoie l'ordre dominante et applique un filtre sur les valeurs.
        static public string getPriorityMouvement(double _x, double _y, double _z, double _a, double _b, double _c)
        {
            var x = Math.Abs(_x);
            var y = Math.Abs(_y);
            var z = Math.Abs(_z);
            var a = Math.Abs(_a);
            var b = Math.Abs(_b);
            var c = Math.Abs(_c);

            var returnValue = "";
            if (x > y && x > z && x > a && x > b && x > c)
            {
                if (x > 0.3)
                {
                    returnValue = "x";
                }
            }

            else if (y > x && y > z && y > a && y > b && y > c)
            {
                if (y > 0.3)
                {
                    returnValue = "y";
                }
            }
            else if (z > y && z > x && z > a && z > b && z > c)
            {
                if (z > 0.3)
                {
                    returnValue = "z";
                }
            }
            else if (a > y && a > z && a > x && a > b && a > c)
            {
                if (a > 0.3)
                {
                    returnValue = "a";
                }
            }
            else if (b > y && b > z && b > a && b > x && b > c)
            {
                if (b > 0.3)
                {
                    returnValue = "b";
                }
            }
            else if (c > y && c > z && c > a && c > b && c > x)
            {
                if (c > 0.3)
                {
                    returnValue = "c";
                }
            }

            return returnValue;
        }

        static void traiterDeplacements(double _X, double _Y, double _Z, double _A, double _B, double _C)
        {
            // init robot

            // get pos from souris
            var pos = new NLX.Robot.Kuka.Controller.CartesianPosition();
            pos.X = _X;
            pos.Y = _Y;
            pos.Z = _Z;
            pos.A = 0.0;
            pos.B = 0.0;
            pos.C = 0.0;

            if ((pos.X == 0) && (pos.Y == 0) && (pos.Z == 0) && (pos.A == 0) && (pos.B == 0) && (pos.C == 0))
            {
                Console.WriteLine("0 pos");

                a.StopRelativeMovement();

                //Console.WriteLine("print after stop , nigga");

            }
            else
            {

                //var sensorTest = a.ReadSensor();
                //Console.WriteLine("b: " + sensorTest);
                //Console.WriteLine("test5");
                //a.OpenGripper();
                //Thread.Sleep(1000);
                //a.CloseGripper();
                //Console.WriteLine("test6");


                //var pos = a.GetCurrentPosition();

                //Console.WriteLine("pos.X: " + pos.X);
                //Console.WriteLine("pos.X: " + pos.X);

                Console.WriteLine("before setRelativeMovement");
                a.SetRelativeMovement(pos);
                Console.WriteLine("after setRelativeMovement");

                //Console.WriteLine("test7");
                Console.WriteLine("before StartRelativeMovement");
                a.StartRelativeMovement();
                Console.WriteLine("after StartRelativeMovement");
                //Console.WriteLine("test8");

            }


        }
    }   
}
