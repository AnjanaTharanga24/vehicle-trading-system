using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleTradingSystem.Data;
using VehicleTradingSystem.Models;

namespace VehicleTradingSystem.Services
{
    public class VehicleService
    {
        private readonly AppDbContext _context;

        public VehicleService()
        {
            _context = new AppDbContext();
        }

        public List<Vehicle> GetAllVehicles()
        {
            try
            {
                return _context.Vehicles.OrderBy(v => v.Make).ThenBy(v => v.Model).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving vehicles: {ex.Message}");
            }
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            try
            {
                return _context.Vehicles
                    .Where(v => v.Status == "Available")
                    .OrderBy(v => v.Make)
                    .ThenBy(v => v.Model)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving available vehicles: {ex.Message}");
            }
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            try
            {
                return _context.Vehicles.FirstOrDefault(v => v.VehicleID == vehicleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving vehicle: {ex.Message}");
            }
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            try
            {
                // Check if VIN already exists
                if (_context.Vehicles.Any(v => v.VIN == vehicle.VIN))
                {
                    throw new Exception("A vehicle with this VIN already exists.");
                }

                vehicle.DateAdded = DateTime.Now;
                vehicle.Status = "Available";

                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding vehicle: {ex.Message}");
            }
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                var existingVehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleID == vehicle.VehicleID);
                if (existingVehicle == null)
                {
                    throw new Exception("Vehicle not found.");
                }

                // Check if VIN is being changed and if it conflicts with another vehicle
                if (existingVehicle.VIN != vehicle.VIN)
                {
                    if (_context.Vehicles.Any(v => v.VIN == vehicle.VIN && v.VehicleID != vehicle.VehicleID))
                    {
                        throw new Exception("A vehicle with this VIN already exists.");
                    }
                }

                existingVehicle.Make = vehicle.Make;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.Year = vehicle.Year;
                existingVehicle.VIN = vehicle.VIN;
                existingVehicle.Color = vehicle.Color;
                existingVehicle.Price = vehicle.Price;
                existingVehicle.Status = vehicle.Status;
                existingVehicle.Description = vehicle.Description;
                existingVehicle.FuelType = vehicle.FuelType;
                existingVehicle.Transmission = vehicle.Transmission;
                existingVehicle.Mileage = vehicle.Mileage;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating vehicle: {ex.Message}");
            }
        }

        public bool DeleteVehicle(int vehicleId)
        {
            try
            {
                var vehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleID == vehicleId);
                if (vehicle == null)
                {
                    throw new Exception("Vehicle not found.");
                }

                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting vehicle: {ex.Message}");
            }
        }

        public List<Vehicle> SearchVehicles(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return GetAllVehicles();
                }

                searchTerm = searchTerm.ToLower();
                return _context.Vehicles
                    .Where(v => v.Make.ToLower().Contains(searchTerm) ||
                               v.Model.ToLower().Contains(searchTerm) ||
                               v.VIN.ToLower().Contains(searchTerm) ||
                               v.Color.ToLower().Contains(searchTerm))
                    .OrderBy(v => v.Make)
                    .ThenBy(v => v.Model)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching vehicles: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}