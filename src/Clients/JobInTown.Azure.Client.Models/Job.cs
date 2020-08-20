using System;
using JobInTown.Azure.Client.Models.Enums;

namespace JobInTown.Azure.Client.Models
{
    public class Job
    {
        public int Id { get; set; }

        public User User { get; set; }

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
