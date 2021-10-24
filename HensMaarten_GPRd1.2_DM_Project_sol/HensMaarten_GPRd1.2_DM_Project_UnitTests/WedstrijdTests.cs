using Microsoft.VisualStudio.TestTools.UnitTesting;
using HensMaarten_GPRd1._2_DM_Project_DAL;
using HensMaarten_GPRd1._2_DM_Project_models;
using System;

namespace HensMaarten_GPRd1._2_DM_Project_UnitTests
{
    [TestClass]
    public class WedstrijdTests
    {
        [TestMethod]
        public void MaakScore_Thuis0Uit2_02()
        {
            // arrange
            Wedstrijd wedstrijd = new Wedstrijd();
            //act
            int thuis = 0;
            int uit = 2;
            // assert
            Assert.AreEqual(wedstrijd.MaakScore(ref thuis, ref uit),"0-2");
        }
        [TestMethod]
        public void IsGeldig_WedstrijdIsGeldig_True()
        {
            // arrange
            Wedstrijd wedstrijd = new Wedstrijd();
            //act
            int thuis = 1;
            int uit = 2;
            wedstrijd.uitslag = wedstrijd.MaakScore(ref thuis, ref uit);
            wedstrijd.datum = DateTime.Now;
            // assert
            Assert.IsTrue(wedstrijd.IsGeldig());
        }
        [TestMethod]
        public void IsGeldig_WedstrijdIsNietGeldig_False()
        {
            // arrange
            Wedstrijd wedstrijd = new Wedstrijd();
            //act
            int thuis = 1;
            int uit = 2;
            wedstrijd.uitslag = wedstrijd.MaakScore(ref thuis, ref uit);
            // assert
            Assert.IsFalse(wedstrijd.IsGeldig());
        }
    }
}
