﻿namespace WebServer.ByTheCakeApplication.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(6), MaxLength(100)]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
