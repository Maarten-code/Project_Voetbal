using Microsoft.VisualStudio.TestTools.UnitTesting;
using HensMaarten_GPRd1._2_DM_Project_DAL;
using HensMaarten_GPRd1._2_DM_Project_models;
using System;
using System.Collections.Generic;

namespace HensMaarten_GPRd1._2_DM_Project_UnitTests
{
    [TestClass]
    public class DatabaseOperationsTests
    {
        [TestMethod]
        public void OphalenTeams_MeerDan0TeamsInDatabase_lijstGroterDan0()
        {
            // arrange
            List<Team> teams = new List<Team>();
            // act
            teams = DatabaseOperations.OphalenTeams();
            // assert
            Assert.IsTrue(teams.Count > 0);
        }
        [TestMethod]
        public void OphalenTeamOpTeamid_teamidBestaatInDatabase_True()
        {
            // arrange
            Team team;
            // act
            team = DatabaseOperations.OphalenTeamOpTeamid(1);
            //   Team team1 = new Team();
            //   team1.naam = "KRC Genk";
            // assert
            Assert.IsNotNull(team);
            //  Assert.AreEqual(team1, team);
        }
        [TestMethod]
        public void OphalenStadionOpStadionid_teamidBestaatInDatabase_True()
        {
            // arrange
            Stadion stadion;
            // act
            stadion = DatabaseOperations.OphalenStadionOpStadionid(1);
            // assert
            Assert.IsNotNull(stadion);
        }
    }
}
