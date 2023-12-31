﻿namespace Ploomers_Project_API.Mappers.DTOs.ViewModels
{
    // How the sale data is expected to go back to users
    public class SaleViewModel
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public string EmployeeName { get; set; }
        public string ClientName { get; set; }
        public int Amount { get; set; }
        public int Value { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
    }
}
