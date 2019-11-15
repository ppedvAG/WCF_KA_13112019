using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.VollE.Model;

namespace ppedv.VollE.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void EfContext_can_create_database()
        {
            using (var con = new EfContext())
            {
                if (con.Database.Exists())
                    con.Database.Delete();

                con.Database.Create();

                //Assert.IsTrue(con.Database.Exists());
                con.Database.Exists().Should().BeTrue();
            }
        }

        [TestMethod]
        public void EfContext_CRUD_Trainer()
        {
            var t = new Trainer() { Name = $"Fred_{Guid.NewGuid()}" };
            string newName = $"Wilma_{Guid.NewGuid()}";

            using (var con = new EfContext())
            {
                //INSERT
                con.Trainer.Add(t);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check INSERT / READ
                var loaded = con.Trainer.Find(t.Id);
                Assert.IsNotNull(loaded);
                Assert.AreEqual(t.Name, loaded.Name);

                //UPDATE
                loaded.Name = newName;
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check UPDATE
                var loaded = con.Trainer.Find(t.Id);
                Assert.AreEqual(newName, loaded.Name);

                //DELETE
                con.Trainer.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check DELETE
                var loaded = con.Trainer.Find(t.Id);
                Assert.IsNull(loaded);
            }
        }


        [TestMethod]
        public void EfContext_CRUD_Mannschaft_AutoFix()
        {
            var fix = new Fixture();

            fix.Behaviors.Add(new OmitOnRecursionBehavior());

            var mann = fix.Create<Mannschaft>();
            //Spiele dürfen nicht NULL sein, darum alle Mannanschaft auf sich selbst
            foreach (var item in mann.SpielAlsHeim.Union(mann.SpielAlsGast))
            {
                item.GastMannschaft = mann;
                item.HeimMannschaft = mann;
            }

            //INSERT
            using (var con = new EfContext())
            {
                con.Mannschaft.Add(mann);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mannschaft.Find(mann.Id);

                loaded.Should().BeEquivalentTo(mann, o => o.IgnoringCyclicReferences());
            }
        }
    }
}
