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
using System.Windows.Shapes;

namespace HensMaarten_GPRd1._2_DM_Project
{
    /// <summary>
    /// Interaction logic for StadionAanmaken.xaml
    /// </summary>
    public partial class StadionAanmaken : Window
    {
        Stadion tebewerkenStadion;
        StadionTeam tebewerkenStadionTeam;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // vult comboboxen op bij het laden van het window
            cmbTeams.ItemsSource = DatabaseOperations.OphalenTeams();
            cmbExtrateam.ItemsSource = DatabaseOperations.OphalenTeams();
        }
        public StadionAanmaken(StadionTeam stadionTeam = null)
        {
            //Als stadionteam niet null is, vullen we het window op, anders doen we dit niet
            // de knop bewerken is niet zichtbaar als stadionteam null is, de knop aanmaken is dan wel zichtbaar.
            InitializeComponent();
            if (stadionTeam == null)
            {
                btnStadionAanmaken.Visibility = Visibility.Visible;
                btnStadionBewerken.Visibility = Visibility.Collapsed;
                btnVerwijderStadion.Visibility = Visibility.Collapsed;
                btnVoegLinkToe.Visibility = Visibility.Collapsed;
                cmbExtrateam.Visibility = Visibility.Collapsed;
            }
            else
            {
                tebewerkenStadionTeam = stadionTeam;
                tebewerkenStadion = DatabaseOperations.OphalenStadionOpStadionid(stadionTeam.stadionId);
                btnStadionAanmaken.Visibility = Visibility.Collapsed;
                btnStadionBewerken.Visibility = Visibility.Visible;
                LoadOrEmpty(tebewerkenStadion);
                cmbTeams.SelectedItem = DatabaseOperations.OphalenTeamOpTeamid(stadionTeam.teamId);
            }
        }
        private void btnStadionBewerken_Click(object sender, RoutedEventArgs e)
        {
            // stadion bewerken
            if (cmbTeams.SelectedItem is Team team)
            {
                List<StadionTeam> stadionTeams = DatabaseOperations.OphalenStadionTeamsOpStadionID(tebewerkenStadion.id);
                StadionTeam stadionTeam = tebewerkenStadionTeam;
                int id = tebewerkenStadionTeam.teamId;
                tebewerkenStadion.gemeente = txtGemeente.Text;
                tebewerkenStadion.naam = txtNaam.Text;
                tebewerkenStadion.straat = txtStraat.Text;
                tebewerkenStadion.straatNummer = txtStraatNummer.Text;
                stadionTeam.teamId = team.id;
                if (tebewerkenStadion.IsGeldig())
                {
                    int ok = DatabaseOperations.AanpassenStadion(tebewerkenStadion);
                    if (ok > 0)
                    {
                        string boodschap = "stadion is aangepast";
                        // controleert of het id van het stadionteam is gewijzigd door de gebruiker
                        // als dit niet het geval is gaan we verder met de databaseoperatie
                        // anders controleren we eerst of deze link al bestaat. Indien deze
                        // al bestaat geven we een foutmelding
                        bool check1 = true;
                        bool check2 = false;
                        if (id != stadionTeam.teamId)
                        {
                            check2 = stadionTeams.Contains(stadionTeam);
                            check1 = false;
                        }
                        else check1 = true;
                        if (!check1)
                        {
                            if (!check2)
                            {
                                int ok2 = DatabaseOperations.AanpassenStadionTeam(stadionTeam);
                                if (ok2 > 0)
                                {
                                    boodschap += "Link met stadion is aangepast";
                                    this.Close();
                                }
                                else boodschap += "Link met stadion is niet aangepast";
                                MessageBox.Show(boodschap, "info"
                                , MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Dit team speelt al in dit stadion", "Foutmelding"
                            , MessageBoxButton.OK, MessageBoxImage.Error);
                                cmbTeams.SelectedItem = tebewerkenStadionTeam.Team;
                                tebewerkenStadionTeam.teamId = id;
                            }
                           
                        }
                        else MessageBox.Show(boodschap, "info"
                               , MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("stadion is niet aangepast ", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("stadion is niet aangepast kijk na of alle waarden correct zijn ingevuld", "Foutmelding"
                       , MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Duid een team aan!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void btnVerwijderStadion_Click(object sender, RoutedEventArgs e)
        {
            // stadion verwijderen
            if (tebewerkenStadion.IsGeldig())
            {
                int ok = DatabaseOperations.VerwijderenStadion(tebewerkenStadion);
                if (ok > 0)
                {
                    MessageBox.Show("Stadion is verwijderd", "info"
                        , MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanStadionTeams();
                    this.Close();
                }
            }
        }
        private void btnVoegLinkToe_Click(object sender, RoutedEventArgs e)
        {
            // link met extra team toevoegen 
            if (cmbExtrateam.SelectedItem is Team team)
            {
                StadionTeam stadionTeam = new StadionTeam();
                stadionTeam.stadionId = tebewerkenStadion.id;
                stadionTeam.teamId = team.id;
                stadionTeam.id = bepaalStadionTeamId();
                List<StadionTeam> stadionTeams = DatabaseOperations.OphalenStadionTeamsOpStadionID
                    (tebewerkenStadion.id);
                if (!stadionTeams.Contains(stadionTeam))
                {
                    int ok = DatabaseOperations.ToevoegenStadionTeam(stadionTeam);
                    if (ok > 0)
                    {
                        MessageBox.Show("link met stadion is toegevoegd", "info"
                        , MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else MessageBox.Show("link met stadion is niet toegevoegd", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Dit team speelt al in dit stadion", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Selecteer eerst een extra team!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void btnStadionAanmaken_Click(object sender, RoutedEventArgs e)
        {
            List<Stadion> stadions = DatabaseOperations.OphalenStadions();
            List<StadionTeam> stadionTeams = DatabaseOperations.OphalenStadionTeams();
            // stadion aanmaken 
            if (cmbTeams.SelectedItem is Team team)
            {
                Stadion stadion = new Stadion();
                StadionTeam stadionTeam = new StadionTeam();
                stadion.gemeente = txtGemeente.Text;
                stadion.naam = txtNaam.Text;
                stadion.straat = txtStraat.Text;
                stadion.straatNummer = txtStraatNummer.Text;
                stadion.id = bepaalStadionId();
                stadionTeam.stadionId = stadion.id;
                stadionTeam.teamId = team.id;
                stadionTeam.id = bepaalStadionTeamId();
                if (stadion.IsGeldig())
                {
                    if (!stadions.Contains(stadion))
                    {
                        int ok = DatabaseOperations.ToevoegenStadion(stadion);
                        if (ok > 0)
                        {
                            string boodschap = "stadion is toegevoegd";
                            int ok2 = DatabaseOperations.ToevoegenStadionTeam(stadionTeam);
                            if (ok2 > 0)
                            {
                                boodschap += "Link met stadion is toegevoegd";
                                this.Close();
                            }
                            else boodschap += "Link met stadion is niet toegevoegd";
                            MessageBox.Show(boodschap, "info"
                        , MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        else
                        {
                            MessageBox.Show("stadion is niet toegevoegd", "Foutmelding"
                            , MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else MessageBox.Show("dit stadion bestaat al!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("stadion is niet aangepast kijk na of alle waarden correct zijn ingevuld", "Foutmelding"
                       , MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else MessageBox.Show("Duid een team aan!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private int bepaalStadionId()
        {
            // de id bepalen van een nieuw stadion
            List<int> ids = new List<int>();
            int defId = 1;
            List<Stadion> stadions = DatabaseOperations.OphalenStadions();
            foreach (Stadion stadion in stadions)
            {
                ids.Add(stadion.id);
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
        private int bepaalStadionTeamId()
        {
            // de id bepalen van een nieuw stadionteam 
            List<int> ids = new List<int>();
            int defId = 1;
            List<StadionTeam> stadionTeams = DatabaseOperations.OphalenStadionTeams();
            foreach (StadionTeam stadionTeam in stadionTeams)
            {
                ids.Add(stadionTeam.id);
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
        private void LoadOrEmpty(Stadion stadion = null)
        {
            // window opvullen bij een niet null waarde, niet opvullen bij een null waarde 
            if (stadion != null)
            {
                txtGemeente.Text = stadion.gemeente;
                txtNaam.Text = stadion.naam;
                txtStraat.Text = stadion.straat;
                txtStraatNummer.Text = stadion.straatNummer;
            }
            if (stadion == null)
            {
                txtGemeente.Text = "";
                txtNaam.Text = "";
                txtStraat.Text = "";
                txtStraatNummer.Text = "";
                cmbTeams.SelectedIndex = -1;
            }
        }
        private void CleanStadionTeams()
        {
            // als er een stadion verwijdered word komt er een null waarde te staan bij stadionid
            // bij de stadionteam entiteit. Om te voorkomen dat onze database oneindig groot
            // word verwijderen we daarom alle stadionteams waar de stadionid waarde null is. 
            // Dit had ik eigenlijk al bij datamodellering moeten oplossen door DTC in plaats
            // van DTN te gebruiken 
            List<StadionTeam> stadionTeams = DatabaseOperations.OphalenStadionTeams();
            foreach (StadionTeam stadionTeam in stadionTeams)
            {
                if (stadionTeam.stadionId == null)
                {
                    int ok = DatabaseOperations.VerwijderenStadionTeam(stadionTeam);
                    if (ok > 0)
                    {
                        MessageBox.Show("Link is verwijderd!", "info"
                        , MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}

