using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ReportManagementService.Controllers;
using ReportManagementService.Dtos;
using ReportManagementService.Interfaces;
using ReportManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReportService.BaseFixtures
{
    public class BaseFixture : IDisposable
    {

        public ReportController reportController { get; private set; }

        public IReportService dataStore { get; private set; }


        public BaseFixture()
        {
            dataStore = A.Fake<IReportService>();
            reportController = new ReportController(dataStore);

        }
        public void Dispose()
        {

        }
    }
}
