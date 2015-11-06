using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Forms;



namespace darkproject
{
    class Program
    {
        static public event KeyEventHandler KeyUp;

        static NLX.Robot.Kuka.Controller.RobotController theRobot;

        static TDx.TDxInput.Device device;

        static bool started;
        static double coeffTrans;
        static double coeffRot;

        static void Main(string[] args)
        {
            started = false;

            Program prog = new Program();

            Console.WriteLine("Start of programm....");
            coeffTrans = 1.0;
            coeffRot = 1.0;

            Thread myThreadKeyboard;
            myThreadKeyboard = new Thread(new ThreadStart(KeyboardThreadLoop));
            myThreadKeyboard.Start();


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

            theRobot = new NLX.Robot.Kuka.Controller.RobotController();
            theRobot.Connect("192.168.1.1");
            Console.WriteLine("Connection avec le robot OK !");



            ScenarioHandler sh = new ScenarioHandler();
            sh.readAndInterpretFile(@"C:\Users\IMERIR14\Desktop\Novalinxproject\trunk\darkproject\scenario.sck", theRobot);

            Console.WriteLine("File interpretation finished");


            Console.WriteLine("On entre dans le while");
            while (true)
            {

                //if (Control.ModifierKeys == Keys.Shift)
                //{
                //    Console.WriteLine("*******KEYPRESSED***********");
                //}

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

                switch (valeurMax)
                {
                    case "x":
                        Console.WriteLine("Avancer sur x");
                        traiterDeplacements(0.0, (-1)*coeffTrans * translation.X / 2900, 0.0, 0.0, 0.0, 0.0);
                        Console.WriteLine("Fin Avancer sur x");
                        break;
                    case "y":
                        Console.WriteLine("Avancer sur y");
                        traiterDeplacements(0.0, 0.0, coeffTrans * translation.Y / 2900, 0.0, 0.0, 0.0);
                        Console.WriteLine("Fin Avancer sur y");
                        break;
                    case "z":
                        Console.WriteLine("Avancer sur z");
                        traiterDeplacements((-1) * coeffTrans * translation.Z / 2900, 0.0, 0.0, 0.0, 0.0, 0.0);
                        Console.WriteLine("Fin Avancer sur z");
                        break;
                    case "a":
                        traiterDeplacements(0.0, 0.0, 0.0, coeffRot*rotation.X*10, 0.0, 0.0);
                        Console.WriteLine("Rotation sur a");
                        break;
                    case "b":
                        traiterDeplacements(0.0, 0.0, 0.0, 0.0, coeffRot * rotation.Y*10, 0.0);
                        Console.WriteLine("Rotation sur b");
                        break;
                    case "c":
                        traiterDeplacements(0.0, 0.0, 0.0, 0.0, 0.0, coeffRot * rotation.Z*10);
                        Console.WriteLine("Rotation sur c");
                        break;
                    default:
                        traiterDeplacements(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
                        break;
                }
                System.Threading.Thread.Sleep(50);
            }

            Console.WriteLine("End programm, press a key to exit.....");
            Console.ReadKey();
        }

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
            pos.A = _A;
            pos.B = _B;
            pos.C = _C;

            if ((pos.X == 0) && (pos.Y == 0) && (pos.Z == 0) && (pos.A == 0) && (pos.B == 0) && (pos.C == 0))
            {
                //Console.WriteLine("0 pos");
                if (started == true)
                {
                    Console.WriteLine("about to stop");

                    theRobot.StopRelativeMovement();
                    started = false;
                }

                //Console.WriteLine("print after stop , nigga");

            }
            else
            {
                if (started == false)
                {
                    Console.WriteLine("about to start");

                    started = true;
                    //Console.WriteLine("test7");
                    Console.WriteLine("before StartRelativeMovement");
                    theRobot.StartRelativeMovement();
                    Console.WriteLine("after StartRelativeMovement");
                }


                //var sensorTest = theRobot.ReadSensor();
                //Console.WriteLine("b: " + sensorTest);
                //Console.WriteLine("test5");
                //theRobot.OpenGripper();
                //Thread.Sleep(1000);
                //theRobot.CloseGripper();
                //Console.WriteLine("test6");


                //var pos = theRobot.GetCurrentPosition();

                //Console.WriteLine("pos.X: " + pos.X);
                //Console.WriteLine("pos.X: " + pos.X);

                Console.WriteLine("before setRelativeMovement");
                theRobot.SetRelativeMovement(pos);
                Console.WriteLine("after setRelativeMovement");

                
                //Console.WriteLine("test8");

            }
        }

        public static void KeyboardThreadLoop()
        {
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                Console.WriteLine(keyinfo.Key + " was pressed");

                if ((keyinfo.KeyChar == 'a') || (keyinfo.KeyChar == 'A')) { // demander un nouveau coeff translation
                    Console.WriteLine("donne le nouveau coeff de translation stp (entre 0 et 1.0, il vaut actuellement " + coeffTrans + "):");
                    string s = Console.ReadLine();
                    try {
                        double newCoeff = Double.Parse(s);
                        coeffTrans = newCoeff;
                        Console.WriteLine("le nouveau coeff de translation vaut: " + coeffTrans);

                    }
                    catch (FormatException e) {
                        Console.WriteLine("FormatException occured while trying to parse your data");


                    }

                }
                else if ((keyinfo.KeyChar == 'z') || (keyinfo.KeyChar == 'Z')) // demander un nouveau coeff rotation
                { // demander une nouvelle vitesse
                    Console.WriteLine("donne le nouveau coeff de rotation stp  (entre 0 et 1.0, il vaut actuellement " + coeffRot + "):");
                    string s = Console.ReadLine();
                    try
                    {
                        double newCoeff = Double.Parse(s);
                        coeffRot = newCoeff;
                        Console.WriteLine("le nouveau coeff de rotation vaut: " + coeffRot);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("FormatException occured while trying to parse your data");
                    }
                }

                else if ((keyinfo.KeyChar == 'e') || (keyinfo.KeyChar == 'E')) // enregistrer la position current du robot
                { // demander une nouvelle vitesse
                    Console.WriteLine("enregistre la position current du robot");
                    NLX.Robot.Kuka.Controller.CartesianPosition currentPos = theRobot.GetCurrentPosition();
                    Console.WriteLine("current position:");
                    Console.WriteLine("x: " + currentPos.X);
                    Console.WriteLine("y: " + currentPos.Y);
                    Console.WriteLine("z: " + currentPos.Z);
                    Console.WriteLine("a: " + currentPos.A);
                    Console.WriteLine("b: " + currentPos.B);
                    Console.WriteLine("c: " + currentPos.C);


                }
                else if ((keyinfo.KeyChar == 'r') || (keyinfo.KeyChar == 'R')) // incr coeff trans
                { // demander une nouvelle vitesse

                    coeffTrans += 0.1;

                    if(coeffTrans > 1.0)
                    {
                        coeffTrans = 1.0;
                    }

                    Console.WriteLine("coeffTrans: " + coeffTrans);

                }
                else if ((keyinfo.KeyChar == 't') || (keyinfo.KeyChar == 'T')) // desincr coeff trans
                { // demander une nouvelle vitesse

                    coeffTrans -= 0.1;

                    if (coeffTrans < 0.0)
                    {
                        coeffTrans = 0.0;
                    }

                    Console.WriteLine("coeffTrans: " + coeffTrans);

                }
                else if ((keyinfo.KeyChar == 'y') || (keyinfo.KeyChar == 'Y')) // incr coeff rot
                { // demander une nouvelle vitesse


                    coeffRot += 0.1;

                    if (coeffRot > 1.0)
                    {
                        coeffRot = 1.0;
                    }

                    Console.WriteLine("coeffRot: " + coeffRot);

                }
                else if ((keyinfo.KeyChar == 'u') || (keyinfo.KeyChar == 'U')) // desincr coeff rot
                { // demander une nouvelle vitesse

                    coeffRot -= 0.1;

                    if (coeffRot < 0.0)
                    {
                        coeffRot = 0.0;
                    }

                    Console.WriteLine("coeffRot: " + coeffRot);

                }
                else if ((keyinfo.KeyChar == 'i') || (keyinfo.KeyChar == 'I')) // open grip
                { // demander une nouvelle vitesse

                    theRobot.OpenGripper();

                    Console.WriteLine("open grip");

                }
                else if ((keyinfo.KeyChar == 'o') || (keyinfo.KeyChar == 'O')) // close grip
                { // demander une nouvelle vitesse

                    theRobot.CloseGripper();

                    Console.WriteLine("close grip");

                }
            }
            while (Thread.CurrentThread.IsAlive);
        }

    }   
}
