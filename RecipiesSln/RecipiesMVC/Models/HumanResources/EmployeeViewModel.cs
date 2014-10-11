using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using RecipiesModelNS;

namespace RecipiesMVC.Models
{
    public class EmployeeViewModel
    {
        [Key]
        public int EmployeeId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string Country { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        public string PreviousExperience { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        public EmployeeViewModel ConvertFromEntity(Employee entity)
        {
            Mapper.Map(entity, this);
            return this;
        }

        public Employee ConvertToEntity(Employee entity)
        {
            Mapper.Map(this, entity);
            return entity;
        }
    }
}