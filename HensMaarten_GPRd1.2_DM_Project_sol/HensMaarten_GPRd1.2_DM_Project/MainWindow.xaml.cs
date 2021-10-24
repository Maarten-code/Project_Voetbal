using HensMaarten_GPRd1._2_DM_Project_DAL;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HensMaarten_GPRd1._2_DM_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrEmpty();         
            cmbTeams.ItemsSource = LaadTeams();
        }
        private void cmbTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // vult window op als het team in de combobox wijzigt 
            if (cmbTeams.SelectedItem is Team team)
            {
                LoadOrEmpty(team);
            }
            else LoadOrEmpty();
        }

        private void cmbstadionTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // vult stadion gegevens op als 1 team in meerdere stadions speelt 
            if (cmbstadionTeams.SelectedItem is Stadion stadion)
            {
                LoadStadion(stadion);
            }
            else LoadStadion();
        }
        private void btnBevestig_Click(object sender, RoutedEventArgs e)
        {
            // controleert of de waarde in het vak datum een dateTime is.
            // de is geldig methode controleert daarna of de waarde van het veld website niet leeg is
            // als dit klop word het team gewijzigt en de gegevens van het team teruggeladen anders
            // geven we een foutmelding. 
            if (DateTime.TryParse(txtOprichtingsjaar.Text, out DateTime oprichting))
            {
                bool check = false;
                List<Team> teams = DatabaseOperations.OphalenTeams();
                Team team = cmbTeams.SelectedItem as Team;
                string initialName = team.naam;
                teams.Remove(team);
                team.oprichtingsDatum = oprichting;
                team.naam = txtNaam.Text;
                team.website = txtWebsite.Text;
                if (initialName != team.naam)
                {
                    check = teams.Contains(team);
                }
                if (team.IsGeldig())
                {
                    if (!check)
                    {
                        int ok = DatabaseOperations.AanpassenTeam(team);
                        if (ok > 0)
                        {
                            LoadOrEmpty(team);
                            cmbTeams.Items.Refresh();
                            MessageBox.Show("Team is aangepast", ""
                            , MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else MessageBox.Show("Team is niet aangepast", "Foutmelding"
                            , MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Er bestaat al een team met deze naam!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                        txtNaam.Text = initialName;
                        team.naam = initialName;
                    }
                }
                else MessageBox.Show("Vul een website in!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Duid een correcte datum aan!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);

        }
        private void btnWedstrijden_Click(object sender, RoutedEventArgs e)
        {
            // opent wedstrijden window
            WedstrijdenOverzicht wedstrijdenOverzicht = new WedstrijdenOverzicht();
            wedstrijdenOverzicht.ShowDialog();
            resetwindow();
        }
        private void btnTeamAanmaken_Click(object sender, RoutedEventArgs e)
        {
            // opent het teamAanmaken window
            TeamAanmaken teamAanmaken = new TeamAanmaken();
            teamAanmaken.ShowDialog();
            resetwindow();
        }
        private void btnNieuwStadionAanmaken_Click(object sender, RoutedEventArgs e)
        {
            // opent een window om een nieuw stadion aan te maken 
            StadionAanmaken stadionAanmaken = new StadionAanmaken();
            stadionAanmaken.ShowDialog();
            resetwindow();
        }

        private void btnStadionBewerken_Click(object sender, RoutedEventArgs e)
        {
            // als er een team met een stadion geselecteerd is openen
            // we een window waar het stadion kunnen bewerken
            StadionTeam stadionTeam = null;
            if (cmbTeams.SelectedItem is Team team)
            {
                if (team.StadionTeams.Count > 0)
                {
                    if (cmbstadionTeams.Visibility == Visibility.Visible)
                    {
                        if (cmbstadionTeams.SelectedItem is Stadion stadion)
                        {
                            stadionTeam = DatabaseOperations.OphalenStadionTeam(stadion.id, team.id);
                        }
                        else MessageBox.Show("Selecteer eerst een een stadion uit de combobox!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else stadionTeam = team.StadionTeams.First();
                    if (stadionTeam != null)
                    {
                        StadionAanmaken stadionAanmaken = new StadionAanmaken(stadionTeam);
                        stadionAanmaken.ShowDialog();
                        resetwindow();
                    }
                }
                else
                {
                    MessageBox.Show("Selecteer eerst een team met een stadion!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een team!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void resetwindow()
        {
            // herlaad de combobox en maakt het window leeg, methode word vooral gebruikt na
            // een bewerking op een ander scherm.
            LoadOrEmpty();
            cmbTeams.ItemsSource = LaadTeams();
            cmbstadionTeams.Visibility = Visibility.Hidden;
            btnWijzig.IsEnabled = false;
        }
        private List<Team> LaadTeams()
        {
            // kortere methode om teams op te halen
            return DatabaseOperations.OphalenTeamsMetStadions();
        }
        private void LoadStadion(Stadion stadion = null)
        {
            if (stadion != null)
            {
                lblGemeente.Content = stadion.gemeente;
                lblStraat.Content = stadion.straat;
                lblStraatnummer.Content = stadion.straatNummer;
                lblStadion.Content = stadion.naam;
            }
            else
            {
                lblStadion.Content = "";
                lblGemeente.Content = "";
                lblStraat.Content = "";
                lblStraatnummer.Content = "";
            }
        }
        private void VulWedstrijdenOp(int teamid)
        {
            // laad de wedstrijden van een team op. Als het team geen wedstrijden heeft dan 
            // disablen we de expander. 
            List<Wedstrijd> wedstrijden = DatabaseOperations.OphalenWedstrijdenViaTeamid(teamid);
            List<string> datums = new List<string>();
            foreach (Wedstrijd wedstrijd in wedstrijden)
            {
                datums.Add(wedstrijd.datum.ToShortDateString());
            }
            if (wedstrijden.Count == 0)
            {
                exWedstrijden.IsEnabled = false;
            }
            else exWedstrijden.IsEnabled = true;
            lijstWedstrijden.ItemsSource = wedstrijden;
            lijstDatums.ItemsSource = datums;
        }
        private void LoadOrEmpty(Team team = null)
        {
            // vult de gegevens van het window op als team een waarde heeft
            // bij geen waarde maakt de methode het window leeg 
            if (team != null)
            {
                lblNaamTeam.Content = team.naam;
                lblOprichtingsJaar.Content = team.oprichtingsDatum.ToLongDateString();
                lbwebsite.Content = team.website;
                txtOprichtingsjaar.Text = team.oprichtingsDatum.ToShortDateString();
                txtWebsite.Text = team.website;
                btnWijzig.IsEnabled = true;
                txtNaam.Text = team.naam;
                cmbTeams.SelectedItem = team;
                VulWedstrijdenOp(team.id);
                List<Stadion> stadions = new List<Stadion>();
                if (team.StadionTeams.Count > 0)
                {
                    if (team.StadionTeams.Count > 1)
                    {
                        foreach (StadionTeam stadionTeam in team.StadionTeams)
                        {
                            stadions.Add(stadionTeam.Stadion);
                        }
                        cmbstadionTeams.Visibility = Visibility.Visible;
                        cmbstadionTeams.ItemsSource = stadions;
                        cmbstadionTeams.SelectedIndex = -1;
                        LoadStadion();
                    }
                    else
                    {
                        cmbstadionTeams.Visibility = Visibility.Collapsed;
                        StadionTeam stadionTeam = team.StadionTeams.First();
                        LoadStadion(stadionTeam.Stadion);
                    }

                }
                else
                {
                    cmbstadionTeams.Visibility = Visibility.Collapsed;
                    LoadStadion();
                }
            }
            if (team == null)
            {
                cmbTeams.SelectedIndex = -1;
                lblNaamTeam.Content = "";
                lblOprichtingsJaar.Content = "";
                lbwebsite.Content = "";
                txtOprichtingsjaar.Text = "";
                txtWebsite.Text = "";
                lblStadion.Content = "";
                lblGemeente.Content = "";
                lblStraat.Content = "";
                lblStraatnummer.Content = "";
                exWedstrijden.IsEnabled = false;
            }
        }
    }
}
