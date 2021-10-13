using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationChecker.Models
{
    public class ApiResponse
    {
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string FirstUsedDate { get; set; }
        public string FuelType { get; set; }
        public string PrimaryColour { get; set; }
        public List<MotTest> MotTests { get; set; }
    }

    public class MotTest
    {
        public string CompletedDate { get; set; }
        public string TestResult { get; set; }
        public string ExpiryDate { get; set; }
        public string OdometerValue { get; set; }
        public string OdometerUnit { get; set; }
        public string MotTestNumber { get; set; }
        public List<RfrAndComment> RfrAndComments { get; set; }
    }

    public class RfrAndComment
    {
        public string text { get; set; }
        public string type { get; set; }
    }
}
