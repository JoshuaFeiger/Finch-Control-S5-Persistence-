using FinchAPI;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FinchControl_Starter
{
    class Program
    {

        public enum Command
        {
            NONE,
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            PLAYSONG,
            DONE
        }

        /// <summary>
        /// basic starter solution for the Finch robot
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // **************************************************
            //
            // Assignment: Finch Control
            // Author: Joshua Caleb Feiger
            // Creation Date: 09/25/2019
            // Last Modified Date: 11/06/2019
            //
            // **************************************************

            // Create (instantiate) a new Finch object.
            Finch FinchRobot;
            FinchRobot = new Finch();


            //FinchPlaySong(FinchRobot);
            Console.Clear();
            DisplayWelcomeScreen();
            DisplayMenu(FinchRobot);
            DisplayClosingScreen();

            //Disconnect the Finch, just in case it's connected.
            FinchRobot.disConnect();
        }
        
        /// <summary>
        /// Display a prompt and retrieve a string from whatever data is entered by the user.
        /// </summary>
        /// <returns></returns>
        static string GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tUser Input: ");
            string UserResponse = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return UserResponse;
        }

        /// <summary>
        /// Display a message prompting the user to continue. When they press a key, do so.
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Display a header containing whatever text is entered between the parentheses
        /// </summary>
        static void DisplayHeader(string HeaderText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + HeaderText);
            Console.WriteLine();
        }

        /// <summary>
        /// Display the welcome screen.
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            DisplayHeader("Welcome");
            Console.WriteLine("Greetings User! My name is Console, and I welcome you to Talent Show!");
            Console.WriteLine("The goal of this application is intended to showcase the various possibilities of the Finch robot.");
            Console.WriteLine("Have fun!");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display a menu so the user can choose between six options.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void DisplayMenu(Finch FinchRobot)
        {
            bool Exit = false;
            while (!Exit)
            {
                FinchRobot.disConnect();
                //Display a list of menu options so the user can know what to input.
                DisplayHeader("Menu");
                Console.WriteLine("Please enter in the number for the option of your choice!");
                Console.WriteLine("1. Connect Finch Robot");
                Console.WriteLine("2. Talent Show");
                Console.WriteLine("3. Data Recorder");
                Console.WriteLine("4. Alarm System");
                Console.WriteLine("5. User Programming");
                //The following code is commented out. Restoring it would add the "disconnect" method as option 6.
                //The "disconnect" method is not necessary as the Finch is auto-disconnected when not in use.
                /*
                Console.WriteLine("6. Disconnect Finch Robot");
                Console.WriteLine("7. Exit");
                */
                //If uncommenting the previous block for some reason, comment this "6 to exit" WriteLine out.
                Console.WriteLine("6. Exit");

                //Get user input and send it through a switch, sending you to different screens based on said input.
                Console.WriteLine();
                string UserResponse = GetUserInput();
                switch (UserResponse)
                {
                    case "1":
                        ConnectFinchRobot(FinchRobot);
                        break;

                    case "2":
                        TalentShow(FinchRobot);
                        break;

                    case "3":
                        DataRecorder(FinchRobot);
                        break;

                    case "4":
                        AlarmSystem(FinchRobot);
                        break;

                    case "5":
                        UserProgramming(FinchRobot);
                        break;

                    //The following code is commented out. Restoring it would add the "disconnect" method as option 6.
                    //The "disconnect" method is not necessary as the Finch is auto-disconnected when not in use.
                    /*case "6":
                        DisconnectFinchRobot(FinchRobot);
                        break;

                    case "7":
                        Exit = true;
                        break;
                    */

                    //If uncommenting the previous block for some reason, comment this "case 6" block out.
                    case "6":
                        Exit = true;
                        break;

                    default:
                        //If the user's input isn't recognized as one of the listed options, present
                        //an error screen and send them back to the menu.
                        DisplayHeader("Error");
                        Console.WriteLine("The data you entered wasn't one of the listed options.");
                        Console.WriteLine("Please try again.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        private static void DisconnectFinchRobot(Finch FinchRobot)
        {
            string UserResponse;
            DisplayHeader("Disconnect finch robot");
            Console.WriteLine("Use this option to safely disconnect your Finch robot. The application may crash if you simply unplug it.");
            Console.WriteLine("Do you want to disconnect the Finch robot? (Yes/No)");
            UserResponse = GetUserInput().ToUpper();
            if (UserResponse == "YES")
            {
                FinchRobot.disConnect();
                Console.WriteLine("Okay! Your Finch robot is now disconnected! He is safe to unplug.");
            }
            else
            {
                Console.WriteLine("Okay. Come back whenever you need to disconnect your Finch!");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Connect a Finch Robot.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void ConnectFinchRobot(Finch FinchRobot)
        {
            bool IsFinchConnected;
            string UserResponse = (" ");
            DisplayHeader("Connect Finch Robot");
            Console.WriteLine("The application will now try to connect to your Finch robot.");
            Console.WriteLine("Please make sure the robot is plugged in to your computer with the included USB cable.");
            DisplayContinuePrompt();
            FinchRobot.disConnect();
            IsFinchConnected = FinchRobot.connect();
            while (!IsFinchConnected && !(UserResponse == "E") && !(UserResponse == "e"))
            {
                Console.Clear();
                DisplayHeader("Connect Finch Robot");
                Console.WriteLine("Connection failed.");
                Console.WriteLine("Please try plugging the Finch in again.");
                Console.WriteLine();
                Console.WriteLine("Press the Enter/Return key to continue, or type \"E\" to exit the connection program.");
                UserResponse = Console.ReadLine();
                FinchRobot.disConnect();
                IsFinchConnected = FinchRobot.connect();
            }
            if (IsFinchConnected)
            {
                IsFinchConnected = false;
                while (!IsFinchConnected)
                {
                    Console.Clear();
                    DisplayHeader("Connect Finch Robot");
                    Console.WriteLine("Finch robot detected. Proceeding to \"Beep Confirmation\".");
                    Console.WriteLine("The Finch should try to speak to you and his nose light should flash blue.");
                    Console.WriteLine("If the Finch does this, it is connected properly. Please type \"finish\".");
                    Console.WriteLine("Otherwise, he is not functioning correctly. Please type... Literally anything else, to try again.");
                    int AVar = 361;
                    while (AVar < 2000)
                    {
                        FinchRobot.wait(1);
                        AVar = AVar + 100;
                        FinchRobot.noteOn(AVar);
                        FinchRobot.setLED(0, 0, (AVar / 10));
                    }
                    FinchRobot.setLED(0, 0, 0);
                    FinchRobot.noteOff();
                    UserResponse = GetUserInput().ToLower();
                    if (UserResponse == "finish")
                    {
                        IsFinchConnected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Do a Talent Show... whatever that means.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void TalentShow(Finch FinchRobot)
        {
            DisplayHeader("Talent Show");
            bool IsFinchConnected = FinchRobot.connect();
            if (IsFinchConnected)
            {
                Console.WriteLine("The Finch will now showcase some of its abilities. He will play sounds and move.");
                Console.WriteLine("Please ensure he has room to do so.");
                DisplayContinuePrompt();

                string UserResponse;
                bool IsResponseValid = false;

                while (!IsResponseValid)
                {
                    DisplayHeader("Talent Show");
                    Console.WriteLine("Do you want Finch to dance, spin, or play a song? (Enter \"dance\", \"spin\", or \"play\")");
                    UserResponse = GetUserInput();
                    if (UserResponse.ToLower() == "dance")
                    {
                        Console.WriteLine("Wow! Look at him go! He's great at... doing that...");
                        FinchDance(FinchRobot);
                        IsResponseValid = true;
                    }
                    else if (UserResponse.ToLower() == "spin")
                    {
                        Console.WriteLine("Look at him go! Look at him go!");
                        FinchSpin(FinchRobot);
                        IsResponseValid = true;
                    }
                    else if (UserResponse.ToLower() == "play")
                    {
                        FinchPlaySong(FinchRobot);
                    }
                    else
                    {
                        Console.WriteLine("Please enter \"dance\", \"spin\", or \"play\".");
                    }
                }
            }
            else
            {
                Console.WriteLine("Your Finch Robot is not connected properly. Please refer to option 1, \"Connect Finch Robot\", to get instructions on how to connect.");
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// Record some form of data.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void DataRecorder(Finch FinchRobot)
        {
            DisplayHeader("Data Recorder");

            bool IsFinchConnected = FinchRobot.connect();
            if (IsFinchConnected)
            {
                Console.WriteLine("The Finch will take a series of measurements based on the inputs you enter.");

                DisplayContinuePrompt();

                DisplayDataRecorder(FinchRobot);
            }
            else
            {
                Console.WriteLine("Your Finch Robot is not connected properly. Please refer to option 1, \"Connect Finch Robot\", to get instructions on how to connect.");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Get a number value from the user for the distance between data measurements.
        /// </summary>
        /// <returns></returns>
        static double DisplayGetDataPointFrequency()
        {
            double DataPointFrequency = 0;
            bool IsResponseValid = false;

            //Ask for the user to enter input until the correct input is entered.

            while (!IsResponseValid)
            {
                DisplayHeader("Get Data Point Frequency");
                Console.WriteLine("Please enter the number of seconds you want between each time data is recorded.");
                //Ask for the user's input and set IsResponseValid based on whether the input can be parsed to a double.
                IsResponseValid = double.TryParse(GetUserInput(), out DataPointFrequency);
                if (IsResponseValid && DataPointFrequency < 0)
                {
                    //Give an error message if the response can be parsed to a double, but is a negative number.
                    //Set IsResponseValid to false, as otherwise the program will continue on and accept the command.
                    Console.WriteLine("Sorry, we can't use negative numbers. If you want us to wait a short time though, try 0!");
                    IsResponseValid = false;
                }
                else if (!(DataPointFrequency < 2000000))
                {
                    //Give an error message for a number too large for the Finch to handle.
                    //Set IsResponseValid to false for the same reason as the "negative" error
                    Console.WriteLine("Sorry, we can't use numbers that large. Please enter one smaller than two million!");
                    IsResponseValid = false;
                }
                else if (!IsResponseValid)
                {
                    //Give an error message if for any other reason the input can't be parsed, such as being a string or being a too long.
                    //No need to set IsResponseValid; it's already false.
                    Console.WriteLine("Sorry, you need to enter a number, using the number keys. We don't know what you just entered.");
                }
                else
                {
                    //If there are no errors, simply echo the user's input. DataPointFrequency has already been set to the user's input, and UserResponseValid is already true.
                    Console.WriteLine($"Okay! The Finch will wait {DataPointFrequency} seconds before each temperature check!");
                }
                //Pause for user so they can see the error or success message.
                DisplayContinuePrompt();
            }
            
            return DataPointFrequency;
        }

        static int DisplayGetNumberOfDataPoints()
        {
            int NumberOfDataPoints = 0;
            bool IsResponseValid = false;

            while (!IsResponseValid)
            {
                DisplayHeader("Get Number of Data Points");
                Console.WriteLine("Please enter the number of times you want data to be recorded.");
                IsResponseValid = int.TryParse(GetUserInput(), out NumberOfDataPoints);
                if (NumberOfDataPoints < 1 || NumberOfDataPoints > 1000000)
                {
                    Console.WriteLine("Sorry, we can't use this number. We need a number between one and one million.");
                    IsResponseValid = false;
                }
                else if (!IsResponseValid)
                {
                    Console.WriteLine("Sorry, you need to enter a number, using the number keys. We don't know what you just entered.");
                }
            }

            DisplayContinuePrompt();
            return NumberOfDataPoints;
        }

        /// <summary>
        ///Record data from the Finch.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void DisplayDataRecorder(Finch FinchRobot)
        {
            double DataPointFrequency;
            int NumberOfDataPoints;

            string DataType = DisplayGetDataType();

            DataPointFrequency = DisplayGetDataPointFrequency();
            NumberOfDataPoints = DisplayGetNumberOfDataPoints();

            double[] Measurements = new double[NumberOfDataPoints];


            DisplayGetData(NumberOfDataPoints, DataPointFrequency, FinchRobot, Measurements, DataType);

            DisplayData(Measurements);
        }


        static string DisplayGetDataType()
        {
            DisplayHeader("Data Type");
            Console.WriteLine("Please enter a type of data to measure. Light or temperature?");
            string UserResponse = GetUserInput().ToLower();
            while (!((UserResponse == "light") || (UserResponse == "temperature")))
            {
                DisplayHeader("Data Type");
                Console.WriteLine("Please enter a type of data to measure. Light or temperature?");
                Console.WriteLine("Invalid entry. Please try again.");
                UserResponse = GetUserInput().ToLower();
            }
            return UserResponse;
        }

        /// <summary>
        /// Record data measurements from the Finch.
        /// </summary>
        /// <param name="NumberOfDataPoints"></param>
        /// <param name="DataPointFrequency"></param>
        /// <param name="FinchRobot"></param>
        /// <param name="Measurements"></param>
        static void DisplayGetData(int NumberOfDataPoints, double DataPointFrequency, Finch FinchRobot, double[] Measurements, string DataType)
        {
            //Let the user know that data is about to be recorded, and show the parameters being used for doing so. 
            DisplayHeader("Get Data Set");
            Console.WriteLine($"Frequency: every {DataPointFrequency} seconds.");
            Console.WriteLine($"Number of measurements: {NumberOfDataPoints} times.");

            if (DataType == "temperature")
            {
                Console.WriteLine("The Finch will now measure the temperature.");
            }
            else
            {
                Console.WriteLine("The Finch will now measure the light level.");
            }
            
            DisplayContinuePrompt();
            FinchRobot.connect();
            Console.WriteLine("Beginning data recording. Please do not unplug your Finch.");

            //Record data the set number of times, with the set number of seconds between each measurement. Print the measured data to the screen.
            for (int index = 0; index < NumberOfDataPoints; index++)
            {
                //Record data.

                if (DataType == "temperature")
                {
                    Measurements[index] = FinchRobot.getTemperature();
                }
                else
                {
                    Measurements[index] = FinchRobot.getLeftLightSensor();
                }
                
                int Milliseconds = (int)(DataPointFrequency * 1000);
                FinchRobot.wait(Milliseconds);

                //Print data.
                Console.WriteLine($"Temperature {index + 1}: {Measurements[index]}");
            }

            DisplayContinuePrompt();
            
        }

        /// <summary>
        /// Display the data that's been recorded.
        /// </summary>
        /// <param name="Measurements"></param>
        static void DisplayData(double[] Measurements)
        {
            DisplayHeader("Temperature Data");

            for (int index = 0; index < Measurements.Length; index++)
            {
                Console.WriteLine($"Temperature {index + 1}: {Measurements[index]}");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Allow the user to set an alarm with the Finch.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void AlarmSystem(Finch FinchRobot)
        {
            DisplayHeader("Alarm System");

            Console.WriteLine("You are about to set an alarm to go off for the Finch.");
            Console.WriteLine("The Finch will measure something, temperature or heat, for a period of time you set. If the level of the measurement goes above a number you set, the alarm will go off.");
            Console.WriteLine("Please ensure your Finch is connected before you begin.");
            DisplayContinuePrompt();

            bool IsFinchConnected = FinchRobot.connect();
            if (IsFinchConnected)
            {
                string AlarmType = DisplayGetAlarmType();
                int MaxSeconds = DisplayGetMaxSeconds();
                double Threshold = DisplayGetThreshhold(AlarmType, FinchRobot);

                bool ThresholdExceeded = MonitorCurrentMeasurementLevel(FinchRobot, Threshold, MaxSeconds, AlarmType);

                if (ThresholdExceeded)
                {
                    Console.WriteLine();
                    switch (AlarmType)
                    {
                        case "light":
                            Console.WriteLine("Maximum light level exceeded!");
                            break;

                        case "temperature":
                            Console.WriteLine("Maximum temperature exceeded!");
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Maximum monitoring time exceeded.");
                }

                DisplayContinuePrompt();
            }
            else
            {
                DisplayHeader("Alarm System");
                Console.WriteLine("Your Finch Robot is not connected properly. Please refer to option 1, \"Connect Finch Robot\", to get instructions on how to connect.");
                DisplayContinuePrompt();
            }
        }

        static string DisplayGetAlarmType()
        {
            DisplayHeader("Alarm System");
            Console.WriteLine("Please enter an alarm type. Light or temperature?");
            string UserResponse = GetUserInput().ToLower();
            while (!((UserResponse == "light") || (UserResponse == "temperature")))
            {
                DisplayHeader("Alarm System");
                Console.WriteLine("Please enter an alarm type. Light or temperature?");
                Console.WriteLine("Invalid entry. Please try again.");
                UserResponse = GetUserInput().ToLower();
            }
            return UserResponse;
        }

        static int DisplayGetMaxSeconds()
        {
            DisplayHeader("Alarm System");
            Console.WriteLine("Please enter the number of seconds the Finch will be monitoring this.");
            bool UserResponseValid = int.TryParse(GetUserInput(), out int MaxSeconds);
            while (!UserResponseValid)
            {
                DisplayHeader("Alarm System");
                Console.WriteLine("Please enter the number of seconds the Finch will be monitoring this.");
                Console.WriteLine("Response invalid. Please try again.");
                UserResponseValid = int.TryParse(GetUserInput(), out MaxSeconds);
            }
            return MaxSeconds;
        }

        static double DisplayGetThreshhold(string AlarmType, Finch FinchRobot)
        {
            double Threshold = 0;

            DisplayHeader("Threshhold Value");

            bool UserResponseValid = false;

            switch (AlarmType)
            {
                case "light":
                    FinchRobot.connect();
                    Console.WriteLine("The current light level is {0}.", FinchRobot.getLeftLightSensor());
                    Console.WriteLine("What do you want the maximum light level to be? (0 - 255)");
                    UserResponseValid = double.TryParse(GetUserInput(), out Threshold);
                    break;

                case "temperature":
                    FinchRobot.connect();
                    Console.WriteLine("The current temperature is {0}.", FinchRobot.getTemperature());
                    Console.WriteLine("What do you want the maximum temperature to be? (0 - 255)");
                    UserResponseValid = double.TryParse(GetUserInput(), out Threshold);
                    break;

                default:
                    throw new FormatException();
            }
            if ((Threshold < 0) || (Threshold > 255)) UserResponseValid = false;

            while (!UserResponseValid)
            {
                DisplayHeader("Threshhold Value");
                switch (AlarmType)
                {
                    case "light":
                        FinchRobot.connect();
                        Console.WriteLine("The current light level is {0}.", FinchRobot.getLeftLightSensor());
                        Console.WriteLine("What do you want the maximum light level to be? (0 - 255)");
                        Console.WriteLine("Error. Please enter a number between 0 and 255.");
                        UserResponseValid = double.TryParse(GetUserInput(), out Threshold);
                        break;

                    case "temperature":
                        FinchRobot.connect();
                        Console.WriteLine("The current temperature is {0}.", FinchRobot.getTemperature());
                        Console.WriteLine("What do you want the maximum temperature to be? (0 - 255)");
                        Console.WriteLine("Error. Please enter a number between 0 and 255.");
                        UserResponseValid = double.TryParse(GetUserInput(), out Threshold);
                        break;

                    default:
                        throw new FormatException();
                }
                if ((Threshold < 0) || (Threshold > 255)) UserResponseValid = false;
            }

            DisplayContinuePrompt();

            return Threshold;
        }

        static bool MonitorCurrentMeasurementLevel(Finch FinchRobot, double Threshold, int MaxSeconds, string AlarmType)
        {
            bool ThresholdExceeded = false;
            int CurrentLightLevel;
            double CurrentTemperature;
            double Seconds = 0;

            while (!ThresholdExceeded && Seconds <= MaxSeconds)
            {
                switch (AlarmType)
                {
                    case "light":
                        CurrentLightLevel = FinchRobot.getLeftLightSensor();
                        DisplayHeader("Monitoring light levels...");
                        Console.WriteLine($"Maximum light level: {Threshold}");
                        Console.WriteLine($"Current light level: {CurrentLightLevel}");

                        if (CurrentLightLevel > Threshold)
                        {
                            ThresholdExceeded = true;
                        }
                        break;


                    case "temperature":
                        CurrentTemperature = FinchRobot.getTemperature();
                        DisplayHeader("Monitoring temperature...");
                        Console.WriteLine($"Maximum temperature: {Threshold}");
                        Console.WriteLine($"Current temperature: {CurrentTemperature}");

                        if (CurrentTemperature > Threshold)
                        {
                            ThresholdExceeded = true;
                        }
                        break;
                    default:
                        throw new FormatException();
                }
                

                FinchRobot.wait(500);
                Seconds += 0.5;
            }

            return ThresholdExceeded;
        }
        
        /// <summary>
        /// 
        /// ______________________________________Under development_________________________________________________
        /// 
        /// Let the user enter data to "program" the Finch.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void UserProgramming(Finch FinchRobot)
        {
            DisplayHeader("User Programming");
            Console.WriteLine("This is a little sandbox for programming the Finch!");
            Console.WriteLine("You can make your own temporary set of basic instructions that the Finch will follow.");
            Console.WriteLine("It's pretty simplified, but you should be able to do some special things with it!");
            Console.WriteLine("You'll be launched into a new menu with its own options. Please ensure your Finch Robot is connected.");
            DisplayContinuePrompt();

            (int MotorSpeed, int LEDBrightness, double WaitSeconds) CommandParameters;
            List<Command> Commands = new List<Command>();
            bool Exit = false;

            CommandParameters.MotorSpeed = 0;
            CommandParameters.LEDBrightness = 0;
            CommandParameters.WaitSeconds = 0;

            while (!Exit)
            {
                FinchRobot.disConnect();
                //Display menu
                DisplayHeader("User Programming Menu");
                Console.WriteLine("Please enter in the number for the option of your choice!");
                Console.WriteLine("1. Command explanations");
                Console.WriteLine("2. Set command parameters");
                Console.WriteLine("3. Enter commands");
                Console.WriteLine("4. View commands entered");
                Console.WriteLine("5. Execute commands");
                Console.WriteLine("6. Save file");
                Console.WriteLine("7. Load file");
                Console.WriteLine("8. Exit");

                //Get user input and send it through a switch, sending you to different screens based on said input
                Console.WriteLine();
                string UserResponse = GetUserInput();
                switch (UserResponse)
                {
                    case "1":
                        DisplayCommandExplanations();
                        break;

                    case "2":
                        CommandParameters = DisplayGetCommandParameters();
                        break;

                    case "3":
                        DisplayGetFinchCommands(Commands);
                        break;

                    case "4":
                        DisplayFinchCommands(Commands, CommandParameters);
                        break;

                    case "5":
                        DisplayExecuteCommands(FinchRobot, Commands, CommandParameters);
                        break;

                    case "6":
                        string ProgramName = DisplayGetProgramName();
                        DisplaySaveProgram(Commands, CommandParameters, ProgramName);
                        break;

                    case "7":
                        //todo: Add these methods.
                        //DisplaySaveProgramScreen();
                        ProgramName = DisplayGetProgramName();
                        Commands = DisplayLoadProgramList(ProgramName);
                        CommandParameters = DisplayLoadProgramParameters(ProgramName);
                        break;
                        
                    case "8":
                        Exit = true;
                        break;

                    default:
                        DisplayHeader("Error");
                        Console.WriteLine("The data you entered wasn't one of the listed options.");
                        Console.WriteLine("Please try again.");
                        DisplayContinuePrompt();
                        break;
                }
            }
            DisplayContinuePrompt();
        }

        static void DisplayCommandExplanations()
        {
            DisplayHeader("User Programming-- Command Explanations");
            Console.WriteLine(
                "\"MoveForward\":    This command will make the Finch move forward at the speed you specify in Option 2, \n" +
                "                    Command Parameters.\n" +
                "\"MoveBackward\":   This command will make the Finch move backwards at the speed you've specified.\n" +
                "\"StopMotors\":     This command will make all of the Finch's motors stop. Use it in combination with \"Delay\"!\n" +
                "\"Delay\":          This command will add space between the execution of commands! It waits a certain numnber of seconds\n" +
                "                    before the next one.\n" +
                "\"TurnRight\":      This command makes the Finch turn right at your specified speed.\n" +
                "\"TurnLeft\":       This command makes the Finch turn left at your specified speed.\n" +
                "\"LEDOn\":          This command will turn the Finch's LED light on.\n" +
                "\"LEDOff\":         This command will turn the Finch's LED light off.\n" +
                "\"Done\":           This command will end the program and close the programming interface, bringing you back to the menu.\n" +
                "\n" +
                "Just type \"Help\" in the programming editor if you need to see this again!");
            DisplayContinuePrompt();
        }

        static (int MotorSpeed, int LEDBrightness, double WaitSeconds) DisplayGetCommandParameters()
        {
            (int MotorSpeed, int LEDBrightness, double WaitSeconds) CommandParameters;
            CommandParameters.MotorSpeed = 0;
            CommandParameters.LEDBrightness = 0;
            CommandParameters.WaitSeconds = 0;

            //todo: validate further
            
            string UserResponse;
            bool UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayHeader("Command Parameters");

                Console.WriteLine("Enter motor speed [0-255].");
                UserResponse = GetUserInput();
                UserResponseValid = int.TryParse(UserResponse, out CommandParameters.MotorSpeed);
                if (!UserResponseValid || CommandParameters.MotorSpeed < 0 || CommandParameters.MotorSpeed > 255)
                {
                    Console.WriteLine("Response was invalid. Please try again.");
                    UserResponseValid = false;
                    DisplayContinuePrompt();
                }
            }

            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayHeader("Command Parameters");

                Console.WriteLine("Enter LED Brightness [1-255].");
                UserResponse = GetUserInput();
                UserResponseValid = int.TryParse(UserResponse, out CommandParameters.LEDBrightness);
                if (!UserResponseValid || CommandParameters.LEDBrightness < 0 || CommandParameters.LEDBrightness > 255)
                {
                    Console.WriteLine("Response was invalid. Please try again.");
                    UserResponseValid = false;
                    DisplayContinuePrompt();
                }
            }

            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayHeader("Command Parameters");

                Console.WriteLine("Enter seconds for Wait Command.");
                UserResponse = GetUserInput();
                UserResponseValid = double.TryParse(UserResponse, out CommandParameters.WaitSeconds);
                UserResponseValid = int.TryParse(Convert.ToString(CommandParameters.WaitSeconds * 1000), out int DoesNothing);
                if (!UserResponseValid)
                {
                    Console.WriteLine("Response was invalid. Please try again.");
                    DisplayContinuePrompt();
                }
            }

            DisplayContinuePrompt();
            return CommandParameters;
        }

        static void DisplayGetFinchCommands (List<Command> Commands)
        {
            Command Command = Command.MOVEFORWARD;
            string UserResponse;

            while (Command != Command.DONE)
            {
                DisplayHeader("Finch Robot Commands");
                Console.WriteLine("Command listing:");
                DisplayWriteFinchCommands(Commands);
                if (Command == Command.NONE)
                {
                    Console.WriteLine();
                    Console.WriteLine("Command invalid. Please try again.");
                }
                Console.WriteLine();
                Console.WriteLine("Enter Command.");
                UserResponse = GetUserInput();
                if (UserResponse.ToUpper() == "HELP")
                {
                    DisplayCommandExplanations();
                }
                else
                {
                    Enum.TryParse(UserResponse.ToUpper(), out Command);
                    if (!(Command == Command.NONE) && !(Command == Command.DONE))
                    {
                        Commands.Add(Command);
                    }
                }
            }
        }

        static void DisplayFinchCommands(List<Command> commands, (int MotorSpeed, int LEDBrightness, double WaitSeconds) CommandParameters)
        {
            DisplayHeader("Display Finch Commands");

            Console.WriteLine($"Motor Speed: {CommandParameters.MotorSpeed}\tLED Brightness: {CommandParameters.LEDBrightness}\tSeconds to wait: {CommandParameters.WaitSeconds}");

            Console.WriteLine();

            DisplayWriteFinchCommands(commands);
            
            DisplayContinuePrompt();
        }

        static void DisplayWriteFinchCommands(List<Command> commands)
        {
            foreach (Command command in commands)
            {
                Console.WriteLine(command);
            }
        }

        static void DisplayExecuteCommands(Finch FinchRobot, List<Command> commands, (int MotorSpeed, int LEDBrightness, double WaitSeconds) CommandParameters)
        {
            int MotorSpeed = CommandParameters.MotorSpeed;
            int LEDBrightness = CommandParameters.LEDBrightness;
            int WaitMilliseconds = Convert.ToInt32(CommandParameters.WaitSeconds * 1000);

            DisplayHeader("Execute Finch Commands");

            bool IsFinchConnected = FinchRobot.connect();
            if (IsFinchConnected)
            {
                FinchRobot.connect();
                bool FinchLEDOn = false;

                foreach (Command command in commands)
                {
                    Console.WriteLine(command);
                    FinchRobot.connect();
                    switch (command)
                    {
                        case Command.NONE:
                            break;
                        case Command.MOVEFORWARD:
                            FinchRobot.setMotors(MotorSpeed, MotorSpeed);
                            break;
                        case Command.MOVEBACKWARD:
                            FinchRobot.setMotors(-MotorSpeed, -MotorSpeed);
                            break;
                        case Command.STOPMOTORS:
                            FinchRobot.setMotors(0, 0);
                            break;
                        case Command.WAIT:
                            FinchRobot.wait(WaitMilliseconds);
                            break;
                        case Command.TURNRIGHT:
                            FinchRobot.setMotors(-MotorSpeed, MotorSpeed);
                            break;
                        case Command.TURNLEFT:
                            FinchRobot.setMotors(MotorSpeed, -MotorSpeed);
                            break;
                        case Command.LEDON:
                            FinchRobot.setLED(LEDBrightness, LEDBrightness, LEDBrightness);
                            FinchLEDOn = true;
                            break;
                        case Command.LEDOFF:
                            FinchRobot.setLED(0, 0, 0);
                            FinchLEDOn = false;
                            break;
                        case Command.PLAYSONG:
                            FinchPlaySong(FinchRobot);
                            break;
                        case Command.DONE:
                            break;
                        default:
                            break;
                    }
                }
                FinchRobot.disConnect();
            }
            else
            {
                Console.WriteLine("Your Finch Robot is not connected properly. Please refer to option 1, \"Connect Finch Robot\", to get instructions on how to connect.");
            }
            
            DisplayContinuePrompt();
        }

        //todo: write more methods below here.

        static string DisplayGetProgramName()
        {
            string ProgramName;

            DisplayHeader("File Name entry");
            Console.WriteLine("Please enter the name of the file you wish to use.");
            ProgramName = GetUserInput();

            return ProgramName;
        }

        static void DisplaySaveProgram(List<Command> Commands, (int MotorSpeed, int LEDBrightness, double WaitSeconds) CommandParameters, string ProgramName)
        {
            string DataPath = (@"Data\" + ProgramName + ".txt");
            List<string> TextToWrite = new List<string>();

            DisplayHeader("Save Commands to File");

            TextToWrite.Add($"{CommandParameters.MotorSpeed}|{CommandParameters.LEDBrightness}|{CommandParameters.WaitSeconds}");

            foreach (Command Command in Commands)
            {
                TextToWrite.Add (Command.ToString());
            }

            Console.WriteLine("Your file is ready for saving.");
            DisplayContinuePrompt();

            try
            {
                File.WriteAllLines(DataPath, TextToWrite.ToArray());
                Console.WriteLine("File saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(ex.Message);
            }
            
            DisplayContinuePrompt();
        }

        static List<Command> DisplayLoadProgramList(string ProgramName)
        {
            string DataPath = (@"Data\" + ProgramName + ".txt");
            List<Command> Commands = new List<Command>();
            string[] CommandsList;

            DisplayHeader("Read Commands from File");
            
            try
            {
                CommandsList = File.ReadAllLines(DataPath);
                Command Command;
                foreach (string CommandString in CommandsList)
                {
                    Enum.TryParse(CommandString, out Command);

                    Commands.Add(Command);
                }
                //removes the first item in the list, as it isn't a command
                try
                {
                    Commands.RemoveAt(0);
                    Console.WriteLine("Data read from file!");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Cannot read from file; your file appears to be empty.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Cannot read from file; your file does not exist.");
            }
            
            DisplayContinuePrompt();
            return Commands;
        }

        static (int MotorSpeed, int LEDBrightness, double WaitSeconds) DisplayLoadProgramParameters(string ProgramName)
        {
            string DataPath = (@"Data\" + ProgramName + ".txt");
            string[] CommandsList;
            (int MotorSpeed, int LEDBrightness, double WaitSeconds) CommandParameters;

            try
            {
                CommandsList = File.ReadAllLines(DataPath);
                string CommandParametersString = CommandsList[0];
                string[] CommandParametersArray = CommandParametersString.Split(Convert.ToChar("|"));
                CommandParameters.MotorSpeed = int.Parse(CommandParametersArray[0]);
                CommandParameters.LEDBrightness = int.Parse(CommandParametersArray[1]);
                CommandParameters.WaitSeconds = double.Parse(CommandParametersArray[2]);
            }
            catch (Exception)
            {
                CommandParameters.MotorSpeed = 0;
                CommandParameters.LEDBrightness = 0;
                CommandParameters.WaitSeconds = 0;
            }

            return CommandParameters;
        }

        /// <summary>
        /// Display a screen to end off the application.
        /// </summary>
        static void DisplayClosingScreen()
        {
            DisplayHeader("-End-");
            Console.WriteLine("Thank you for coming to the Talent Show!");

            //Adjusted ContinuePrompt code.
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Have the Finch Robot dance.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void FinchDance(Finch FinchRobot)
        {
            //Motors can go from -255 to +255.


            //Back-and-forth motion while making one tone.
            FinchRobot.noteOn(462);
            FinchRobot.setLED(0, 100, 0);
            FinchDance_BackAndForth(FinchRobot);

            //Turning left, without any tone playing but with a red LED color.
            FinchRobot.setLED(100, 0, 0);
            FinchRobot.noteOff();
            FinchDance_TurnLeft(FinchRobot);


            //A different color and tone for this back-and-forth motion.
            FinchRobot.setLED(0, 0, 100);
            FinchRobot.noteOn(530);
            FinchDance_BackAndForth(FinchRobot);
            
            //Turning again; rest of the code is kind of an "etc. etc." with different colors and note pitches.
            FinchRobot.setLED(100, 0, 0);
            FinchRobot.noteOff();
            FinchDance_TurnRight(FinchRobot);

            FinchRobot.setLED(0, 100, 0);
            FinchRobot.noteOn(477);
            FinchDance_BackAndForth(FinchRobot);

            FinchRobot.setLED(100, 0, 0);
            FinchRobot.noteOff();
            FinchDance_TurnRight(FinchRobot);

            FinchRobot.setLED(0, 0, 100);
            FinchRobot.noteOn(549);
            FinchDance_BackAndForth(FinchRobot);

            //Shut all the LED and note playing systems off.
            FinchRobot.noteOff();
            FinchRobot.setLED(0, 0, 0);
            FinchDance_TurnLeft(FinchRobot);
        }
        
        /// <summary>
        /// Make the Finch rock forwards and backwards.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void FinchDance_BackAndForth(Finch FinchRobot)
        {
            for (int i = 0; i < 3; i++)
            {
                FinchRobot.setMotors(200, 200);
                FinchRobot.wait(100);
                FinchRobot.setMotors(-200, -200);
                FinchRobot.wait(200);
                FinchRobot.setMotors(200, 200);
                FinchRobot.wait(100);
                FinchRobot.setMotors(0, 0);
                FinchRobot.wait(100);
            }
        }

        /// <summary>
        /// Make the Finch turn left, attempting about 90 degrees though that can't be quite gotten right.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void FinchDance_TurnLeft(Finch FinchRobot)
        {
            FinchRobot.setMotors(-100, 100);
            FinchRobot.wait(600);
            FinchRobot.setMotors(0, 0);
        }

        /// <summary>
        /// Make the Finch turn right, attempting about 90 degrees though that can't be quite gotten right.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void FinchDance_TurnRight(Finch FinchRobot)
        {
            FinchRobot.setMotors(100, -100);
            FinchRobot.wait(600);
            FinchRobot.setMotors(0, 0);
        }

        /// <summary>
        /// Spin the Finch Robot one way, then spin him the other way.
        /// </summary>
        /// <param name="FinchRobot"></param>
        static void FinchSpin(Finch FinchRobot)
        {
            FinchRobot.setLED(100, 0, 0);
            FinchRobot.noteOff();
            FinchRobot.setMotors(100, -100);
            FinchRobot.wait(5600);
            FinchRobot.setLED(0, 100, 0);
            FinchRobot.setMotors(-200, 200);
            FinchRobot.wait(2500);
            FinchRobot.setMotors(0, 0);
            FinchRobot.setLED(0, 0, 0);
        }

        static void FinchPlaySong(Finch FinchRobot)
        {
            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 146.83, 250);
            FinchPlayNote(FinchRobot, 164.81, 250);
            FinchPlayNote(FinchRobot, 98.00, 250);
            FinchPlayNote(FinchRobot, 98.00, 250);
            FinchPlayNote(FinchRobot, 197.00, 500);
            FinchPlayNote(FinchRobot, 174.61, 250);
            FinchPlayNote(FinchRobot, 164.81, 250);
            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 146.83, 400);
            FinchPlayNote(FinchRobot, 164.81, 350);
            FinchPlayNote(FinchRobot, 174.61, 250);
            FinchPlayNote(FinchRobot, 164.81, 250);

            FinchRobot.wait(250);

            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 146.83, 250);
            FinchPlayNote(FinchRobot, 164.81, 250);
            FinchPlayNote(FinchRobot, 98.00, 250);
            FinchPlayNote(FinchRobot, 98.00, 250);
            FinchPlayNote(FinchRobot, 174.61, 500);
            FinchPlayNote(FinchRobot, 164.81, 250);
            FinchPlayNote(FinchRobot, 146.83, 250);
            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 130.81, 500);
            FinchPlayNote(FinchRobot, 123.47, 500);
            FinchPlayNote(FinchRobot, 130.81, 500);
            FinchPlayNote(FinchRobot, 146.83, 500);

            FinchRobot.wait(50);

            FinchPlayNote(FinchRobot, 164.81, 250);
            FinchPlayNote(FinchRobot, 98.00, 250);
            FinchPlayNote(FinchRobot, 98.00, 250);
            FinchPlayNote(FinchRobot, 196.00, 500);
            FinchPlayNote(FinchRobot, 174.61, 250);
            FinchPlayNote(FinchRobot, 164.81, 250);
            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 146.83, 400);
            FinchPlayNote(FinchRobot, 164.81, 350);
            FinchPlayNote(FinchRobot, 174.61, 250);
            FinchPlayNote(FinchRobot, 164.81, 250);

            FinchRobot.wait(350);

            FinchPlayNote(FinchRobot, 146.83, 250);
            FinchPlayNote(FinchRobot, 130.81, 500);
            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 99.00, 250);
            FinchPlayNote(FinchRobot, 164.81, 250);
            FinchPlayNote(FinchRobot, 164.81, 350);
            FinchPlayNote(FinchRobot, 146.83, 500);
            FinchPlayNote(FinchRobot, 130.81, 250);
            FinchPlayNote(FinchRobot, 130.81, 300);
            FinchPlayNote(FinchRobot, 123.47, 300);
            FinchPlayNote(FinchRobot, 98.00, 250);
            FinchPlayNote(FinchRobot, 98.00, 750);
            FinchPlayNote(FinchRobot, 110.00, 250);
            FinchPlayNote(FinchRobot, 110.00, 720);

            FinchRobot.wait(350);

            FinchPlayNote(FinchRobot, 123.47, 250);
            FinchPlayNote(FinchRobot, 123.47, 250);
            FinchPlayNote(FinchRobot, 130.81, 350);
            FinchPlayNote(FinchRobot, 146.83, 250);
            FinchPlayNote(FinchRobot, 146.83, 750);
            FinchPlayNote(FinchRobot, 130.81, 1000);
        }

        static void FinchPlayNote(Finch FinchRobot, double NoteFrequency, int TimeToPlay)
        {
            FinchRobot.connect();
            //Console.Beep(Convert.ToInt32(NoteFrequency * 8), TimeToPlay);
            ////Thread.Sleep(TimeToPlay);
            FinchRobot.noteOn(Convert.ToInt32(NoteFrequency * 8));
            FinchRobot.wait(TimeToPlay);
            FinchRobot.noteOff();
        }
    }
}
