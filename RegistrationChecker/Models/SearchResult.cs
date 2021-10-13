using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistrationChecker.Models
{
    public class SearchResult 
    {
        [Required]
        public bool Success { get; set; }

        [Required]
        public string Message { get; set; }
        
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string ExpiryDate { get; set; }
        public string Mileage { get; set; }
    }
}
