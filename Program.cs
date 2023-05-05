using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Flightreservationsystem
{
    class Program
    {

        class Flight
        {
            public string flight;
            public string companyname;
            public string flightdestination;
            public string flightdeparture;
            public string flighttime;
            public string flightdate;
            // public string flightnumber;
        }
        static void Main(string[] args)
        {
            string n;
            string p;

            int flightcount = 0;
            string flightnumber;
            int option;
            string path = "E:\\2nd semester Material\\OOP Lab\\Flightreservationsystem\\Flightdata.txt";

            List<Flight> flightdata = new List<Flight>();
            readdata(flightdata, ref flightcount, path);

            do
            {
                option = loginmenu();

                if (option == 1)
                {
                    clearScreen();
                    Console.WriteLine("Enter your name: ");
                    n = Console.ReadLine();
                    Console.WriteLine("Enter your password: ");
                    p = Console.ReadLine();

                    if (n == "Admin" && p == "oslo")
                    {
                        clearScreen();

                        int adminoption = adminmenu();
                        if (adminoption == 1)
                        {
                            clearScreen();
                            for (int i = 0; i < 1; i++)
                            {
                                flightdata.Add(addschedule());
                                storeflights(flightdata, path);
                                flightcount++;
                            }
                        }
                        else if (adminoption == 2)
                        {
                            clearScreen();
                            Console.WriteLine("Enter flight number whose info you want to update:");
                            flightnumber = Console.ReadLine();
                            updateschedule(flightdata, flightnumber);
                        }
                        else if (adminoption == 3)
                        {
                            clearScreen();
                            Console.WriteLine("Enter flight number whose data you want to remove:");
                            flightnumber = Console.ReadLine();
                            removeflight(flightdata, flightnumber, ref flightcount);
                        }
                        else if (adminoption == 4)
                        {
                            clearScreen();
                            ViewSchedule(flightdata);
                        }


                    }

                }
            }
            while (option != 2);
            clearScreen();



        }

        static void storeflights(List<Flight> flightdata, string path)
        {
            StreamWriter Flightdata = new StreamWriter(path, true);
            for (int i = 0; i < flightdata.Count; i++)
            {
                Flightdata.WriteLine(flightdata[i].flight + "," + flightdata[i].companyname + "," + flightdata[i].flightdeparture + "," +
                flightdata[i].flightdestination + "," + flightdata[i].flighttime + "," + flightdata[i].flightdate);
            }
            Flightdata.Flush();
            Flightdata.Close();
        }

        static string parseData(string line, int field)
        {
            int comma = 0;
            string data = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    comma++;
                }
                else if (comma == field)
                {
                    data = data + line[i];
                }
            }
            return data;
        }

        static void readdata(List<Flight> flightdata, ref int flightcount, string path)
        {

            StreamReader Flightdata = new StreamReader(path);
            string record;
            while ((record = Flightdata.ReadLine()) != null)
            {
                Flight flights = new Flight();
                flights.flight = parseData(record, 0);
                flights.companyname = parseData(record, 1);
                flights.flightdeparture = parseData(record, 2);
                flights.flightdestination = parseData(record, 3);
                flights.flighttime = parseData(record, 4);
                flights.flightdate = parseData(record, 5);
                flightcount++;
                flightdata.Add(flights);
            }
            Flightdata.Close();
        }


        static int loginmenu()
        {
            int option;
            Console.WriteLine("Press 1 to login");
            Console.WriteLine("Press 2 to Exit");
            option = int.Parse(Console.ReadLine());
            return option;


        }

        static int adminmenu()
        {
            int option;
            Console.WriteLine("1. Add Flight Schedule ");
            Console.WriteLine("2. Update Flight Schedule");
            Console.WriteLine("3. Remove FLight");
            Console.WriteLine("4. View FLight Schedule");
            option = int.Parse(Console.ReadLine());
            return option;
        }

        static void clearScreen()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        static Flight addschedule()
        {
            Flight Obj = new Flight();
            Console.WriteLine("Enter Number of the flight: ");
            Obj.flight = Console.ReadLine();
            Console.WriteLine("Enter Company name of the flight: ");
            Obj.companyname = Console.ReadLine();
            Console.WriteLine("Enter Departure of the flight: ");
            Obj.flightdeparture = Console.ReadLine();
            Console.WriteLine("Enter Destination of the flight: ");
            Obj.flightdestination = Console.ReadLine();
            Console.WriteLine("Enter time of the flight: ");
            Obj.flighttime = Console.ReadLine();
            Console.WriteLine("Enter date of the flight: ");
            Obj.flightdate = Console.ReadLine();
            return Obj;

        }


        static Flight updateschedule(List<Flight> flightdata, string flightnumber)
        {
            for (int i = 0; i < flightdata.Count; i++)
            {
                if (flightdata[i].flight == flightnumber)
                {
                    Console.WriteLine("Enter New Date of the Flight: ");
                    flightdata[i].flightdate = Console.ReadLine();
                    Console.WriteLine("Enter New Time of the Flight: ");
                    flightdata[i].flighttime = Console.ReadLine();
                    Console.WriteLine("Enter New Departure of the Flight: ");
                    flightdata[i].flightdeparture = Console.ReadLine();
                    Console.WriteLine("Enter New Destination of the Flight: ");
                    flightdata[i].flightdestination = Console.ReadLine();
                    return flightdata[i];
                }

            }

            throw new ArgumentException($"Flight {flightnumber} not found.");
        }



        static void removeflight(List<Flight> flightdata, string flightnumber, ref int flightcount)
        {
            for (int i = 0; i < flightdata.Count; i++)
            {
                if (flightdata[i].flight == flightnumber)
                {
                    flightdata.RemoveAt(i);
                    flightcount--;
                    Console.WriteLine("Flight removed successfully.");

                }
            }
            Console.WriteLine("Flight not found.");
        }


        static void ViewSchedule(List<Flight> flightdata)
        {
            Console.WriteLine("Flight Number\t\tCompany Name\t\tFlight Departure\t\tFlight Destination\t\tFlight Date\t\tFlight Time");
            for (int i = 0; i < flightdata.Count; i++)
            {
                Console.WriteLine(flightdata[i].flight + "\t\t" + flightdata[i].companyname + "\t\t" + flightdata[i].flightdeparture + "\t\t" + flightdata[i].flightdestination +
                 "\t\t" + flightdata[i].flightdate + "\t\t" + flightdata[i].flighttime);
            }
        }


    }
}
