using System;
using System.Threading.Tasks;
using RegistrationChecker.Helpers;
using RegistrationChecker.Models;

namespace RegistrationChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            do
            {
                // Setup console for user input
                DisplayHeader();

                // Take and clean the user input
                string registrationNumber = StringHelper.GetFormattedRegistration(Console.ReadLine());

                // Validate the input to prevent unnecessary calls to the api
                bool isValidRegistration = ValidationHelper.ValidateRegistration(registrationNumber);
                if (!isValidRegistration)
                {
                    DisplayInvalidRegistrationError();
                }
                else
                {
                    // Send the request
                    var searchResult = HttpHelper.SendRequest(registrationNumber);
                    searchResult.Wait();

                    // Display the output
                    DisplayOutput(searchResult.Result);           
                }
            } while ((_ = Console.ReadKey().Key) != ConsoleKey.Escape); // User can keep entering number plates until they decide to exit

            return;
        }

        private static void DisplayHeader()
        {
            // Setup console for input
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********************************************");
            Console.WriteLine("***              MOT Checker             ***");
            Console.WriteLine("********************************************");
            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please enter a registration number to check:");
        }

        private static void DisplayInvalidRegistrationError()
        {
            // Display error message 
            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*** The registration number you have entered appears to be invalid. ***");
            

            // Prompt user for next action
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Escape (Esc) to exit or any other key to try again.");
        }

        private static void DisplayOutput(SearchResult result)
        {
            // Display the output 
            Console.ForegroundColor = (result.Success) ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"***   {result.Message}   ***");            
            if (result.Success)
            {
                // Display any details we have retrived
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("__________________________________________________");
                Console.WriteLine(Environment.NewLine);
                if (result.Make != null) { Console.WriteLine($"Make: {result.Make}"); }
                if (result.Model != null) { Console.WriteLine($"Model: {result.Model}"); }
                if (result.Colour != null) { Console.WriteLine($"Colour: {result.Colour}"); }
                if (result.ExpiryDate != null) { Console.WriteLine($"Expiry Date: {result.ExpiryDate}"); }
                if (result.Mileage != null) { Console.WriteLine($"Mileage: {result.Mileage}"); }
                Console.WriteLine("__________________________________________________");                
            }


            // Prompt user for next action
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Escape (Esc) to exit or any other key to try again.");
        }
    }
}
