using System;
using System.Globalization;
using AutoMapper;
using RecipiesModelNS;
using RecipiesMVC.App_Start;
using RecipiesMVC.Models;
using Xunit;

namespace RecipiesMVC.Tests.Automapper
{
    public class AutomapperTest
    {
        [Fact]
        public void MapEmployeeTest()
        {
            // Arrange
            AutoMapperConfig.Execute();
            string firstName = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var employee = new Employee {FirstName = firstName};

            // Act
            var employeeViewModel = Mapper.Map<EmployeeViewModel>(employee);

            // Assert
            Assert.Equal(employee.FirstName, employeeViewModel.FirstName);
        }
    }
}