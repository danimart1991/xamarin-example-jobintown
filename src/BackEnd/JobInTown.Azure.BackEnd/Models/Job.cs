using System;
using System.ComponentModel.DataAnnotations;
using JobInTown.Azure.BackEnd.Models.Enums;

namespace JobInTown.Azure.BackEnd.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public CategoryType Category { get; set; }

        public ContractType Contract { get; set; }

        public WorkdayType WorkDay { get; set; }

        public DateTime PostedDate { get; set; }

        public string ImageUrl { get; set; }

        public string Location { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}