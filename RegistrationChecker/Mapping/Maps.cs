using RegistrationChecker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Configuration;

namespace RegistrationChecker.Mapping
{
    public static class Maps
    {
        public static SearchResult MapApiResponseToSearchResult(ApiResponse response)
        {
            var result = new SearchResult
            {
                Success = true,
                Message = $"{ConfigurationManager.AppSettings.Get("TESTS_FOUND")}{response.Registration}:",
                Make = response.Make,
                Model = response.Model,
                Colour = response.PrimaryColour
            };

            // Check MOT data for mileage and expiry details
            if(response.MotTests != null)
            {
                // Get most recent MOT and check for mileage
                MotTest latestMOT = response.MotTests.OrderByDescending(x => x.CompletedDate).First();
                result.Mileage = FetchMileage(latestMOT);

                // To get the MOT expiry date, we need the last MOT that 
                // actually passed, which may not be the latest one.
                if (latestMOT.TestResult == "PASSED" && latestMOT.ExpiryDate != null)
                {
                    result.ExpiryDate = latestMOT.ExpiryDate;
                }
                else
                {
                    result.ExpiryDate = FetchExpiryDate(response.MotTests);                    
                }
            }
            else
            {
                // No MOT data for Mileage & Expiry
                result.Mileage = ConfigurationManager.AppSettings.Get("NO_DATA");
                result.ExpiryDate = ConfigurationManager.AppSettings.Get("NO_DATA");
            }

            return result;
        }

        private static string FetchMileage(MotTest latestMOT)
        {
            // Construct mileage and units
            string units = latestMOT.OdometerUnit == "mi" ? " miles" : " kilometers";
            return $"{latestMOT.OdometerValue}{units}";
        }

        private static string FetchExpiryDate(List<MotTest> motTests)
        {
            // Check for a passing MOT
            if (motTests.Where(x => x.TestResult == "PASSED").Any())
            {
                MotTest lastPassingMOT = motTests.Where(x => x.TestResult == "PASSED")
                                           .OrderByDescending(x => x.CompletedDate).First();

                if (lastPassingMOT.ExpiryDate != null)
                {
                    // Use expiry date from the latest test that passed
                    return lastPassingMOT.ExpiryDate;
                }
                else
                {
                    // MOT passed but no expiry recorded
                    return ConfigurationManager.AppSettings.Get("NO_EXPIRY");
                }
            }
            else
            {
                // We can only find failed tests so assume expired
                return ConfigurationManager.AppSettings.Get("NO_PASS");
            }
        }
    }
}