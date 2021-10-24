using Microsoft.VisualStudio.TestTools.UnitTesting;
using HensMaarten_GPRd1._2_DM_Project_DAL;
using HensMaarten_GPRd1._2_DM_Project_models;
using System;

namespace HensMaarten_GPRd1._2_DM_Project_UnitTests
{
    [TestClass]
    public class StadionTests
    {
        [TestMethod]
        public void IsGeldig_StadionIsGeldig_True()
        {
            // arrange
            Stadion stadion = new Stadion();
            //act
            stadion.gemeente = "Genk";
            stadion.naam = "TestStadion";
            stadion.straat = "Teststraat";
            stadion.straatNummer = "5a";
            // assert
            Assert.IsTrue(stadion.IsGeldig());
        }
        [TestMethod]
        public void IsGeldig_StadionIsNietGeldig_False()
        {
            // arrange
            Stadion stadion = new Stadion();
            //act
            stadion.naam = "TestStadion";
            stadion.straat = "Teststraat";
            stadion.straatNummer = "5a";
            // assert
            Assert.IsFalse(stadion.IsGeldig());
        }
    }
}
