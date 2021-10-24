using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HensMaarten_GPRd1._2_DM_Project_DAL;
using HensMaarten_GPRd1._2_DM_Project_models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HensMaarten_GPRd1._2_DM_Project_UnitTests
{
    [TestClass]
    public class StadionTeamTests
    {
        [TestMethod]
        public void IsGeldig_StadionTeamIsGeldig_True()
        {
            // arrange
            StadionTeam stadionTeam = new StadionTeam();
            //act
            stadionTeam.id = 5;
            stadionTeam.teamId = 4;
            stadionTeam.stadionId = 6;
            // assert
            Assert.IsTrue(stadionTeam.IsGeldig());
        }
        [TestMethod]
        public void IsGeldig_StadionTeamIsNietGeldig_False()
        {
            // arrange
            StadionTeam stadionTeam = new StadionTeam();
            //act
            stadionTeam.id = 5;
            stadionTeam.stadionId = 6;
            // assert
            Assert.IsFalse(stadionTeam.IsGeldig());
        }
    }
}
