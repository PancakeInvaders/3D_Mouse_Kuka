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

        private NLX.Robot.Kuka.Controller.CartesianPosition point0;
        private NLX.Robot.Kuka.Controller.CartesianPosition point1;
        private NLX.Robot.Kuka.Controller.CartesianPosition point2;
        private NLX.Robot.Kuka.Controller.CartesianPosition point3;


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

        public void parseFile(string path)
        {
            point0 = new NLX.Robot.Kuka.Controller.CartesianPosition();
            point1 = new NLX.Robot.Kuka.Controller.CartesianPosition();
            point2 = new NLX.Robot.Kuka.Controller.CartesianPosition();
            point3 = new NLX.Robot.Kuka.Controller.CartesianPosition();

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

                            if (str == "p0")
                            {
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point0.X = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point0.Y = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point0.Z = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point0.A = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point0.B = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point0.C = d;
                                str = sr.ReadLine();

                            }
                            else if (str == "p1")
                            {
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point1.X = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point1.Y = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point1.Z = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point1.A = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point1.B = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point1.C = d;
                                str = sr.ReadLine();

                            }
                            else if (str == "p2")
                            {
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point2.X = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point2.Y = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point2.Z = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point2.A = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point2.B = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point2.C = d;
                                str = sr.ReadLine();

                            }
                            else if (str == "p3")
                            {
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point3.X = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point3.Y = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point3.Z = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point3.A = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point3.B = d;
                                str = sr.ReadLine();
                                Double.TryParse(str, out d);
                                point3.C = d;
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

        public void readAndInterpretFile2(string path, NLX.Robot.Kuka.Controller.RobotController robot)
        {
            Console.WriteLine("in readAndInterpretFile2");
            Console.WriteLine("path: " + path);

            int nbLignesPalette = 4;
            int nbColonnesPalette = 4;

            parseFile(path);

            double xdecx = point1.X - point2.X;
            double ydecx = point1.Y - point2.Y;

            double xdecy = point1.X - point3.X;
            double ydecy = point1.Y - point3.Y;

            Console.WriteLine("xdecx: " + xdecx);
            Console.WriteLine("ydecx: " + ydecx);
            Console.WriteLine("xdecy: " + xdecy);
            Console.WriteLine("ydecy: " + ydecy);


            try
            {

                for (int ligne = 0; ligne <= nbLignesPalette; ligne++)
                {
                    for (int colonne = 0; colonne <= nbColonnesPalette; colonne++)
                    {
                        Console.WriteLine("ligne: " + ligne + ",colonne: " + colonne);

                        if (File.Exists(path))
                        {
                            using (StreamReader sr = new StreamReader(path))
                            {

                                double d;
                                //new List<NLX.Robot.Kuka.Controller.CartesianPosition>();
                                listPos = new List<NLX.Robot.Kuka.Controller.CartesianPosition>();

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

                                        Console.WriteLine("robot: " + robot);
                                        Console.WriteLine("listPos: " + listPos);

                                        listPos.Add(robot.GetCurrentPosition());

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
                                        else if (str == "p0")
                                        {
                                            NLX.Robot.Kuka.Controller.CartesianPosition cpos = new NLX.Robot.Kuka.Controller.CartesianPosition();

                                            str = sr.ReadLine();
                                            Double.TryParse(str, out d);
                                            //cpos.X = d + ligne*xdecy + colonne*xdecy;
                                            cpos.X = d + (-1)*ligne*xdecx + (-1)*colonne*xdecy;


                                            str = sr.ReadLine();
                                            Double.TryParse(str, out d);
                                            //cpos.Y = d + ligne*ydecx + colonne*ydecy;
                                            cpos.Y = d + (-1)*ligne*ydecx + (-1)*colonne*ydecy;


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

                                            listPos.Add(robot.GetCurrentPosition());

                                            // add cpos to the listPos
                                            listPos.Add(cpos);

                                            fctPlay(robot);


                                        }
                                        else if (str == "p1")
                                        {
                                            NLX.Robot.Kuka.Controller.CartesianPosition cpos = new NLX.Robot.Kuka.Controller.CartesianPosition();

                                            str = sr.ReadLine();
                                            Double.TryParse(str, out d);
                                            //cpos.X = d + ligne * xdecx + colonne * xdecy;
                                            cpos.X = d + (-1) * ligne * xdecx + (-1) * colonne * xdecy;

                                            str = sr.ReadLine();
                                            Double.TryParse(str, out d);
                                            //cpos.Y = d + ligne * ydecx + colonne * ydecy;
                                            cpos.Y = d + (-1) * ligne * ydecx + (-1) * colonne * ydecy;

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

                                            listPos.Add(robot.GetCurrentPosition());

                                            // add cpos to the listPos
                                            listPos.Add(cpos);

                                            fctPlay(robot);


                                        }
                                        else if (str == "p2")
                                        {
                                            NLX.Robot.Kuka.Controller.CartesianPosition cpos = new NLX.Robot.Kuka.Controller.CartesianPosition();

                                            str = sr.ReadLine();
                                            Double.TryParse(str, out d);
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
                                        else if (str == "p3")
                                        {
                                            NLX.Robot.Kuka.Controller.CartesianPosition cpos = new NLX.Robot.Kuka.Controller.CartesianPosition();

                                            str = sr.ReadLine();
                                            Double.TryParse(str, out d);
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

                                fctPlay(robot);
                            }

                        }
                        else
                        {
                            Console.WriteLine("it doesn't exist");
                        }
                    }
                }

                Console.WriteLine("in readAndInterpretFile");
                Console.WriteLine("path: " + path);

                listPos = new List<NLX.Robot.Kuka.Controller.CartesianPosition>();
                
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
