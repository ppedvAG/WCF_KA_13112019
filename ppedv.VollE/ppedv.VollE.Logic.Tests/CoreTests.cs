using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ppedv.VollE.Model;
using ppedv.VollE.Model.Contracts;

namespace ppedv.VollE.Logic.Tests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void Core_GetMannschaftMitMeistenGewinnenOfYear()
        {
            var uowMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IRepository<Mannschaft>>();

            repoMock.Setup(x => x.Query()).Returns(() =>
            {
                var m1 = new Mannschaft() { Name = "M1" };
                var m2 = new Mannschaft() { Name = "M2" };
                var s = new Spiel()
                {
                    GastMannschaft = m1,
                    HeimMannschaft = m2,
                    PunkteGast = 17,
                    PunkteHeim = 2
                };
                m1.SpielAlsGast.Add(s);
                m2.SpielAlsHeim.Add(s);
                return new[] { m1, m2 }.AsQueryable();
            });
            uowMock.Setup(x => x.GetRepo<Mannschaft>()).Returns(repoMock.Object);

            var core = new Core(uowMock.Object);

            var result = core.GetMannschaftMitMeistenGewinnenOfYear(2000);

            Assert.AreEqual("M1", result.Name);
        }

        [TestMethod]
        public void Core_GetMannschaftMitMeistenGewinnenOfYear_no_spiele_in_this_year_should_throw()
        {
            var uowMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IRepository<Mannschaft>>();

            repoMock.Setup(x => x.Query()).Returns(() =>
            {
                var m1 = new Mannschaft() { Name = "M1" };
                var m2 = new Mannschaft() { Name = "M2" };
                var s = new Spiel()
                {
                    GastMannschaft = m1,
                    HeimMannschaft = m2,
                    PunkteGast = 0,
                    PunkteHeim = -0
                };
                m1.SpielAlsGast.Add(s);
                m2.SpielAlsHeim.Add(s);
                return new[] { m1, m2 }.AsQueryable();
            });
            uowMock.Setup(x => x.GetRepo<Mannschaft>()).Returns(repoMock.Object);

            var core = new Core(uowMock.Object);

            var result = core.GetMannschaftMitMeistenGewinnenOfYear(2000);

            Assert.AreEqual("M1", result.Name);
        }
    }
}
