using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFindingDotnetCore.Controllers;
using PathFindingDotnetCore.Models;
using PathFindingDotnetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFindingDotnetCore.Controllers.Tests
{
    [TestClass()]
    public class GridsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // component test
            var repo = new GridRepository();
            var ctrl = new GridsController(repo);
            int actual = ctrl.Get().ToList().Count;
            Assert.IsTrue(actual > 0);
        }
    }
}