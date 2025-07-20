using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleTradingSystem.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }

        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string VIN { get; set; } // Vehicle Identification Number

        [Required]
        [StringLength(50)]
        public string Color { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Available, Sold, Reserved

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public DateTime? DateSold { get; set; }

        [StringLength(50)]
        public string FuelType { get; set; }

        [StringLength(20)]
        public string Transmission { get; set; }

        public int? Mileage { get; set; }
    }
}