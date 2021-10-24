using System;
using HensMaarten_GPRd1._2_DM_Project_DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HensMaarten_GPRd1._2_DM_Project
{
    /// <summary>
    /// Interaction logic for TeamAanmaken.xaml
    /// </summary>
    public partial class TeamAanmaken : Window
    {
        public TeamAanmaken()
        {
            InitializeComponent();
        }

        private void btnTeamAanmaken_Click(object sender, RoutedEventArgs e)
        {
            // team aanmaken 
            List<Team> teams = DatabaseOperations.OphalenTeamsOpId();
            if (DateTime.TryParse(txtDatum.Text, out DateTime Oprichting))
            {
                Team team = new Team();
                team.naam = txtNaam.Text;
                team.website = txtWebsite.Text;
                team.oprichtingsDatum = Oprichting;
                team.id = bepaalId(teams);
                if (team.IsGeldig())
                {
                    if (ValidateURL(team.website))
                    {
                        if (!teams.Contains(team))
                        {
                            int ok = DatabaseOperations.ToevoegenTeam(team);
                            if (ok > 0)
                            {
                                MessageBox.Show("team is toegevoegd", "info"
                            , MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Team is niet toegevoegd", "Foutmelding"
                            , MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else MessageBox.Show("De naam van het team moet verschillend zijn van een al bestaand team!",
                           "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Het veld website bevat geen geldige url!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("De velden website en naam mogen niet leeg zijn!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Duid een datum aan!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private int bepaalId(List<Team> teams)
        {
            // id van team bepalen 
            List<int> ids = new List<int>();
            int defId = 1;
            foreach (Team team in teams)
            {
                ids.Add(team.id);
            }
            foreach (int id in ids)
            {
                if (id != defId)
                {
                    return defId;
                }
                else defId++;
            }
            return defId++;
        }
        private bool ValidateURL(string url)
        {
            // methode gebruikt om na te kijken of de ingevoerde waarde effectief een url is 
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute)) return true;
            else return false;
        }
    }
}
