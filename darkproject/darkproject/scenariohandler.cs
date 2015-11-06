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
        public void readAndInterpretFile(string path, NLX.Robot.Kuka.Controller.RobotController robot)
        {

            Console.WriteLine("in readAndInterpretFile");
            Console.WriteLine("path: " + path);

            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {

                        double d;
                        System.Collections.Generic.List < NLX.Robot.Kuka.Controller.CartesianPosition > listPos =
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

                                // add cpos to the listPos
                                listPos.Add(cpos);

                            }
                            else
                            {
                                // play the list of positions if it's not empty

                                if( listPos.Count() != 0)
                                {
                                    // play the list, then empty it
                                    robot.PlayTrajectory(listPos);

                                    listPos.Clear();

                                }

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

                        if (listPos.Count() != 0)
                        {
                            // play the list, then empty it
                            robot.PlayTrajectory(listPos);

                            listPos.Clear();

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


    }
}
