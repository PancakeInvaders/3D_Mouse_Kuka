using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace darkproject
{
    class ScenarioHandler
    {
        private StreamWriter fs;
        System.Collections.Generic.List<NLX.Robot.Kuka.Controller.CartesianPosition> listPos;

        NLX.Robot.Kuka.Controller.CartesianPosition point0;
        NLX.Robot.Kuka.Controller.CartesianPosition point1;
        NLX.Robot.Kuka.Controller.CartesianPosition point2;
        NLX.Robot.Kuka.Controller.CartesianPosition point3;


        public void readAndInterpretFile(string path, NLX.Robot.Kuka.Controller.RobotController robot)
        {

            Console.WriteLine("in readAndInterpretFile");
            Console.WriteLine("path: " + path);

            listPos = new List<NLX.Robot.Kuka.Controller.CartesianPosition>();


            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {

                        double d;
                            new List<NLX.Robot.Kuka.Controller.CartesianPosition>();

                        while (sr.Peek() >= 0)
                        {
                            string str = sr.ReadLine();

                            if (Double.TryParse(str, out d))
                            {
                                // 

                                NLX.Robot.Kuka.Controller.CartesianPosition cpos = new NLX.Robot.Kuka.Controller.CartesianPosition();

                                Console.WriteLine("x: " + d);
                                cpos.X = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                Console.WriteLine("y: " + d);
                                cpos.Y = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                Console.WriteLine("z: " + d);
                                cpos.Z = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                Console.WriteLine("a: " + d);
                                cpos.A = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                Console.WriteLine("b: " + d);
                                cpos.B = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                Console.WriteLine("c: " + d);
                                cpos.C = d;

                                str = sr.ReadLine();
                                Console.WriteLine(str);

                                listPos.Add( robot.GetCurrentPosition() );

                                // add cpos to the listPos
                                listPos.Add(cpos);

                                fctPlay(robot);

                            }
                            else
                            {
                                // play the list of positions if it's not empty
                                Console.WriteLine("rob: " + robot);
                                Console.WriteLine("listpos: " + listPos);

                                fctPlay(robot);

                                if (str == "open")
                                {
                                    // do the open
                                    Console.WriteLine("doing open gripper");
                                    robot.OpenGripper();

                                }
                                else if (str == "close")
                                {
                                    Thread.Sleep(1000);

                                    // do the close
                                    Console.WriteLine("doing close gripper");
                                    robot.CloseGripper();


                                }
                            }

                        }

                        fctPlay(robot);
                    }

                }
                else
                {
                    Console.WriteLine("it doesn't exist");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

        }

        public void parseFile(string path, NLX.Robot.Kuka.Controller.RobotController robot)
        {

            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {

                        double d;
                        new List<NLX.Robot.Kuka.Controller.CartesianPosition>();

                        while (sr.Peek() >= 0)
                        {
                            string str = sr.ReadLine();

                            if (Double.TryParse(str, out d))
                            {
                                // 

                                NLX.Robot.Kuka.Controller.CartesianPosition cpos = new NLX.Robot.Kuka.Controller.CartesianPosition();

                                cpos.X = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                cpos.Y = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                cpos.Z = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                cpos.A = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                cpos.B = d;

                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                cpos.C = d;

                                str = sr.ReadLine();

                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("it doesn't exist");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

        }

        public void addInstruction(string instructions)
        {
            //using (StreamWriter outfile = new StreamWriter())
            //{
            //outfile.Write(instructions);
            //fs.Write(instructions);
            fs.WriteLine(instructions);
            //}
        }

        public void openFile(string path)
        {
            //fs = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None);
            fs = new StreamWriter(path);
        }

        public void closeFile(string path)
        {
            fs.Close();
        }

        public void fctPlay(NLX.Robot.Kuka.Controller.RobotController rob)
        {

            if (listPos.Count() != 0)
            {
                // play the list, then empty it
                rob.PlayTrajectory(listPos);

                listPos.Clear();

            }
        }
    }
}
