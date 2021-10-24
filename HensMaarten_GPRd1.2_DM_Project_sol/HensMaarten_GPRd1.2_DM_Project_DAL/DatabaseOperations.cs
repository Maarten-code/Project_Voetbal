using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace HensMaarten_GPRd1._2_DM_Project_DAL
{
    public static class DatabaseOperations
    {
        public static Team OphalenTeamOpTeamid(int? teamid) 
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Team
                      .Where(x => x.id == teamid)
                      .SingleOrDefault();
            }
        }
        public static StadionTeam OphalenStadionTeam(int stadionid, int teamid)
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.StadionTeam
                      .Where(x => x.stadionId == stadionid)
                      .Where(x => x.teamId == teamid)
                      .SingleOrDefault();
            }
        }
        public static Stadion OphalenStadionOpStadionid(int? stadionid) 
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Stadion
                      .Where(x => x.id == stadionid)
                      .SingleOrDefault();
            }
        }
        public static List<Team> OphalenTeams()
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Team
                        .OrderBy(x => x.naam)
                        .ToList();
            }
        }
        public static List<Team> OphalenTeamsOpId()
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Team
                        .OrderBy(x => x.id)
                        .ToList();
            }
        }
        public static List<Stadion> OphalenStadions()
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Stadion
                        .OrderBy(x => x.id)
                        .ToList();
            }
        }
        public static List<StadionTeam> OphalenStadionTeams()
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.StadionTeam
                        .ToList();
            }
        }
       
        public static List<Team> OphalenTeamsMetStadions()
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Team
                        .Include(x => x.StadionTeams.Select(sub => sub.Stadion))
                        .OrderBy(x => x.naam)
                        .ToList();
            }
        }
        public static List<Wedstrijd> OphalenWedstrijden()
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Wedstrijd
                        .OrderBy(x => x.id)
                        .ToList();

            }
        }
        public static List<Wedstrijd> OphalenWedstrijdenMetTeams()
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Wedstrijd
                        .Include(x => x.Team)
                        .Include(x => x.Tegenstander)
                        .OrderBy(x => x.datum)
                        .ToList();

            }
        }
        public static List<Wedstrijd> OphalenWedstrijdenViaTeamid(int teamId)
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Wedstrijd
                        .Include(x => x.Team)
                        .Include(x => x.Tegenstander)
                        .Where(x => x.Team.id == teamId || x.Tegenstander.id == teamId)
                        .OrderBy(x => x.datum)
                        .ToList();

            }
        }
        public static List<Wedstrijd> OphalenWedstrijdenViaTerm(string term)
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Wedstrijd
                        .Include(x => x.Team)
                        .Include(x => x.Tegenstander)
                        .Where(x => x.Team.naam.Contains(term))
                        .OrderBy(x => x.datum)
                        .ToList();

            }
        }
        public static List<Wedstrijd> OphalenWedstrijdenOpDatumMetTeams(DateTime dateTime)
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.Wedstrijd
                        .Include(x => x.Team)
                        .Include(x => x.Tegenstander)
                        .Where(x => x.datum == dateTime)
                        .ToList();
            }
        }
        public static List<StadionTeam> OphalenStadionTeamsOpStadionID(int stadionid)
        {
            using (VoetbalEntities voetbalEntities = new VoetbalEntities())
            {
                return voetbalEntities.StadionTeam
                                       .Include(x => x.Stadion)
                                       .Include(x => x.Team)
                                       .Where(x => x.stadionId == stadionid)
                                       .ToList();
            }
        }
        public static int ToevoegenStadion(Stadion stadion)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.Stadion.Add(stadion);
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int ToevoegenStadionTeam(StadionTeam stadionTeam)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.StadionTeam.Add(stadionTeam);
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int ToevoegenWedstrijd(Wedstrijd wedstrijd)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.Wedstrijd.Add(wedstrijd);
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int ToevoegenTeam(Team team)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.Team.Add(team);
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int AanpassenTeam(Team Team)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.Entry(Team).State = EntityState.Modified;
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int AanpassenStadion(Stadion stadion)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.Entry(stadion).State = EntityState.Modified;
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int AanpassenStadionTeam(StadionTeam stadionTeam)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    stadionTeam.Stadion = null;
                    stadionTeam.Team = null;
                    voetbalEntities.Entry(stadionTeam).State = EntityState.Modified;
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int Aanpassenwedstrijd(Wedstrijd wedstrijd)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    wedstrijd.Team = null;
                    wedstrijd.Tegenstander = null;
                    voetbalEntities.Entry(wedstrijd).State = EntityState.Modified;
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int VerwijderenStadionTeam(StadionTeam stadionTeam)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.Entry(stadionTeam).State = EntityState.Deleted;
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
        public static int VerwijderenStadion(Stadion stadion)
        {
            try
            {
                using (VoetbalEntities voetbalEntities = new VoetbalEntities())
                {
                    voetbalEntities.Entry(stadion).State = EntityState.Deleted;
                    return voetbalEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
    }
}
