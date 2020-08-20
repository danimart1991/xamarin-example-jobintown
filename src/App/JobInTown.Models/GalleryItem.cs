using System;
using JobInTown.Models.Enums;

namespace JobInTown.Models
{
    public class GalleryItem
    {
        private int _id;
        private User _user;
        private string _title;
        private string _description;
        private CategoryType _category;
        private ContractType _contract;
        private WorkdayType _workday;
        private DateTime _postedDate;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public CategoryType Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public ContractType Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }

        public WorkdayType Workday
        {
            get { return _workday; }
            set { _workday = value; }
        }

        public DateTime PostedDate
        {
            get { return _postedDate; }
            set { _postedDate = value; }
        }
    }
}
