using FinchAPI;
using System;
using System.Collections.Generic;

namespace CommandArray
{
    // *************************************************************
    // Finch Command Array
    // Brinda Earnest
    // CIT 110 John Velis
    // 3/20/2018
    // *************************************************************

    /// <summary>
    /// control commands for the finch robot
    /// </summary>
    public enum FinchCommand
    {
        DONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        DELAY,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF
    }

    class Program
    {
        static void Main(string[] args)
        {
            Finch myFinch = new Finch();

            DisplayOpeningScreen();
            DisplayInitializeFinch(myFinch);
            DisplayMainMenu(myFinch);
            DisplayClosingScreen(myFinch);
        }

        /// <summary>
        /// display the main menu
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayMainMenu(Finch myFinch)
        {
            string menuChoice;
            bool exiting = false;
            int delayDuration = 0;
            int numberOfCommands = 0;
            int motorSpeed = 0;
            int LEDBrightness = 0;
            FinchCommand[] commands = null;
            //List<FinchCommand> commands = new List<FinchCommand>();

            while (!exiting)
            {
                //
                // display menu
                //
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();
                Console.WriteLine("\t1) Get Command Parameters");
                Console.WriteLine("\t2) Get Finch Robot Commands");
                Console.WriteLine("\t3) Display Finch Robot Commands");
                Console.WriteLine("\t4) Execute Finch Commands");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                //
                // process menu
                //
                switch (menuChoice)
                {
                    case "1":
                        numberOfCommands = DisplayGetNumberOfCommands();
                        delayDuration = DisplayGetDelayDuration();
                        motorSpeed = DisplayGetMotorSpeed();
                        LEDBrightness = DisplayGetLEDBrightness();
                        break;
                    case "2":
                        commands = DisplayGetFinchCommands(numberOfCommands);
                        //DisplayGetFinchCommands(commands, numberOfCommands);
                        break;
                    case "3":
                        DisplayFinchCommands(numberOfCommands, commands);
                        //DisplayFinchCommands(commands);
                        break;
                    case "4":
                        DisplayExecuteFinchCommands(myFinch, commands, motorSpeed, LEDBrightness, delayDuration);
                        //DisplayExecuteFinchCommands(myFinch, commands, motorSpeed, LEDBrightness, delayDuration);
                        break;
                    case "e":
                    case "E":
                        exiting = true;
                        break;
                    default:
                        break;
                }
            }
        }

        //static void DisplayFinchCommands(List<FinchCommand> commands)
        static FinchCommand[] DisplayFinchCommands(int numberOfCommands, FinchCommand[] commands)
        
        {

            DisplayHeader("Finch Commands");

            if (commands !=null)
            {
                Console.WriteLine("The commands: ");
                foreach (FinchCommand command in commands)
                {
                    Console.WriteLine(command);
                }
            }
            else
            {
                Console.WriteLine("Please enter Finch Robot commands first");
            }

            DisplayContinuePrompt();

            return commands;
        }

        //static void DisplayExecuteFinchCommands(Finch myFinch, List<FinchCommand> commands, int motorSpeed, int lEDBrightness, int delayDuration)
        static void DisplayExecuteFinchCommands(Finch myFinch, FinchCommand[] commands, int motorSpeed, int LEDBrightness, int delayDuration)
        {
            DisplayHeader("Execute Finch Commands");
            Console.WriteLine("Press any key when ready to initialize commands.");
            DisplayContinuePrompt();

            for (int index = 0; index < commands.Length; index++)           
            {
                Console.WriteLine($"Command: {commands[index]}");

                switch (commands[index])
                {
                    case FinchCommand.DONE:
                        break;
                    case FinchCommand.MOVEFORWARD:
                        myFinch.setMotors(motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.MOVEBACKWARD:
                        myFinch.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.STOPMOTORS:
                        myFinch.setMotors(0, 0);
                        break;
                    case FinchCommand.DELAY:
                        myFinch.wait(delayDuration);
                        break;
                    case FinchCommand.TURNRIGHT:
                        myFinch.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.TURNLEFT:
                        myFinch.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.LEDON:
                        myFinch.setLED(LEDBrightness, LEDBrightness, LEDBrightness);
                        break;
                    case FinchCommand.LEDOFF:
                        myFinch.setLED(0, 0, 0);
                        break;
                    default:
                        break;
                }
            }

            DisplayContinuePrompt();

        }

        //static void DisplayGetFinchCommands(List<FinchCommand> commands, int numberOfCommands)
        static FinchCommand[] DisplayGetFinchCommands (int numberOfCommands)
        {

            FinchCommand commands = new FinchCommand[numberOfcommands];

            DisplayHeader("Get Finch Commands");

           Console.WriteLine();


               for (int index = 0; index < numberOfCommands; index++)
               {
                   Console.WriteLine($"Commands # {index + 1}: ");
                   Enum.TryParse(Console.ReadLine().ToUpper(), out commands);
               
               }

                Console.WriteLine();
                Console.WriteLine("The Commands: ");
                for (int index = 0; index < numberOfCommands; index++)
                {
                    Console.WriteLine($"Command #{index + 1}: {commands[index]}");
                }

                DisplayContinuePrompt();

                return commands;
        }

        static int DisplayGetLEDBrightness()
            {
                int LEDBrightness;
                string userResponse;

                DisplayHeader("LED Brightness");
                Console.WriteLine("Enter a light level for the LED [1-255]:");
                userResponse = Console.ReadLine();

                LEDBrightness = int.Parse(userResponse);

                return LEDBrightness;
            }


        static int DisplayGetDelayDuration()
            {
                int delayDuration;

                DisplayHeader("Length of Delay");

                Console.Write("Enter length of delay in miliseconds");
                delayDuration = int.Parse(Console.ReadLine());
                Console.WriteLine($"You have chosen {delayDuration} ms as your time parameter ");

                // userResponse = Console.ReadLine();
                //delayDuration=int.Parse(userResponse);
                //int.TryParse(userResponse, out delayDuration);
                //

                DisplayContinuePrompt();
                return delayDuration;
            }

            /// <summary>
            /// get the number of commands from the user
            /// </summary>
            /// <returns>number of commands</returns>
        static int DisplayGetNumberOfCommands()
            {
                int numberOfCommands;
                string userResponse;

                DisplayHeader("Number of Commands");

                Console.Write("Enter the number of commands:");
                userResponse = Console.ReadLine();

                numberOfCommands = int.Parse(userResponse);

                return numberOfCommands;
            }

        static int DisplayGetMotorSpeed()
            {
                int motorSpeed;
                string userResponse;

                DisplayHeader("Motor Speed");
                Console.WriteLine("Enter the motor speed [1-255]:");
                userResponse = Console.ReadLine();

                motorSpeed = int.Parse(userResponse);

                return motorSpeed;

            }

            /// <summary>
            /// initialize and confirm the finch connects
            /// </summary>
            /// <param name="myFinch"></param>
        static void DisplayInitializeFinch(Finch myFinch)
            {
                DisplayHeader("Initialize the Finch");

                Console.WriteLine("Please plug your Finch Robot into the computer.");
                Console.WriteLine();
                DisplayContinuePrompt();

                while (!myFinch.connect())
                {
                    Console.WriteLine("Please confirm the Finch Robot is connect");
                    DisplayContinuePrompt();
                }

                FinchConnectedAlert(myFinch);
                Console.WriteLine("Your Finch Robot is now connected");

                DisplayContinuePrompt();
            }

            /// <summary>
            /// audio notification that the finch is connected
            /// </summary>
            /// <param name="myFinch">Finch object</param>
        static void FinchConnectedAlert(Finch myFinch)
            {
                myFinch.setLED(0, 255, 0);

                for (int frequency = 17000; frequency > 100; frequency -= 100)
                {
                    myFinch.noteOn(frequency);
                    myFinch.wait(10);
                }

                myFinch.noteOff();
            }

            /// <summary>
            /// display opening screen
            /// </summary>
        static void DisplayOpeningScreen()
            {
                Console.WriteLine();
                Console.WriteLine("\tProgram Commands for Finch Robot");
                Console.WriteLine();

                DisplayContinuePrompt();
            }

            /// <summary>
            /// display closing screen and disconnect finch robot
            /// </summary>
            /// <param name="myFinch">Finch object</param>
        static void DisplayClosingScreen(Finch myFinch)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\t\tThank You!");
                Console.WriteLine();

                myFinch.disConnect();

                DisplayContinuePrompt();
            }

            #region HELPER  METHODS

            /// <summary>
            /// display header
            /// </summary>
            /// <param name="header"></param>
        static void DisplayHeader(string header)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\t" + header);
                Console.WriteLine();
            }

            /// <summary>
            /// display the continue prompt
            /// </summary>
        static void DisplayContinuePrompt()
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            #endregion

        }

    }

}
