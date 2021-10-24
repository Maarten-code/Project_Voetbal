using Microsoft.VisualStudio.TestTools.UnitTesting;
using HensMaarten_GPRd1._2_DM_Project_DAL;
using HensMaarten_GPRd1._2_DM_Project_models;
using System;

namespace HensMaarten_GPRd1._2_DM_Project_UnitTests
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void IsGeldig_TeamIsGeldig_True()
        {
            // arrange
            Team team = new Team();
            //act
             
            team.naam = "testteam";
            team.oprichtingsDatum = DateTime.Now;
            team.website = "www.test.be";
            // assert
            Assert.IsTrue(team.IsGeldig());
        }
        [TestMethod]
        public void IsGeldig_TeamIsNietGeldig_False()
        {
            // arrange
            Team team = new Team();
            //act
            team.oprichtingsDatum = DateTime.Now;
            team.website = "www.test.be";
            // assert
            Assert.IsFalse(team.IsGeldig());
        }
    }
}
