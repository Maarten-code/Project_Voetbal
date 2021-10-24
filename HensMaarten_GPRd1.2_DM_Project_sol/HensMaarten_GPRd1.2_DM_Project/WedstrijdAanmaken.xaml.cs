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
    /// Interaction logic for WedstrijdAanmaken.xaml
    /// </summary>
    public partial class WedstrijdAanmaken : Window
    {
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // combobox opvullen bij het laden van het window
            cmbTeams.ItemsSource = DatabaseOperations.OphalenTeams();
            cmbUitTeams.ItemsSource = DatabaseOperations.OphalenTeams();
        }
        Wedstrijd tebewerkenWestrijd;
        public WedstrijdAanmaken(Wedstrijd wedstrijd = null)
        {
            // window opvullen en wedstrijd bewerken knop laten zien indien wedstrijd niet null is
            // anders de wedstrijd aanmaken knop laten zien 
            InitializeComponent();
            if (wedstrijd == null)
            {
                btnWedstrijdAanmaken.Visibility = Visibility.Visible;
                btnWedstrijdBewerken.Visibility = Visibility.Collapsed;
            }
            else
            {
                tebewerkenWestrijd = wedstrijd;
                btnWedstrijdAanmaken.Visibility = Visibility.Collapsed;
                btnWedstrijdBewerken.Visibility = Visibility.Visible;
                VulOp(wedstrijd);
            }
        }
        private void btnWedstrijdBewerken_Click(object sender, RoutedEventArgs e)
        {
            // wedstrijd bewerken 
            if (cmbTeams.SelectedItem is Team team && cmbUitTeams.SelectedItem is Team uitTeam)
            {
                if (int.TryParse(txtThuisScore.Text, out int thuis) && int.TryParse(txtUitScore.Text, out int uit))
                {
                    if (txtDatum.SelectedDate is DateTime Date)
                    {
                        tebewerkenWestrijd.teamId = team.id;
                        tebewerkenWestrijd.tegenstanderId = uitTeam.id;
                        tebewerkenWestrijd.datum = Date;
                        tebewerkenWestrijd.uitslag = tebewerkenWestrijd.MaakScore(ref thuis, ref uit);
                        if (tebewerkenWestrijd.IsGeldig())
                        {
                            int ok = DatabaseOperations.Aanpassenwedstrijd(tebewerkenWestrijd);
                            if (ok > 0)
                            {
                                MessageBox.Show("Wedstrijd is aangepast", "info"
                        , MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Wedstrijd is niet aangepast", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }                       
                    }
                    else MessageBox.Show("Selecteer een datum!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("De velden thuis en uitscore mogen enkel een numerieke waarde bevatten!"
                   ,"Foutmelding" , MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Duid een team aan!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void btnWedstrijdAanmaken_Click(object sender, RoutedEventArgs e)
        {
            List<Wedstrijd> wedstrijden = new List<Wedstrijd>();
            // nieuwe wedstrijd aanmaken 
            if (cmbTeams.SelectedItem is Team thuisteam && cmbUitTeams.SelectedItem is Team uitTeam)
            {
                if (txtDatum.SelectedDate is DateTime date)
                {
                    if (int.TryParse(txtThuisScore.Text, out int thuisScore)
                        && int.TryParse(txtUitScore.Text, out int uitScore))
                    {
                        Wedstrijd wedstrijd = new Wedstrijd();
                        wedstrijd.datum = date;
                        wedstrijd.uitslag = wedstrijd.MaakScore(ref thuisScore, ref uitScore);
                        wedstrijd.teamId = thuisteam.id;
                        wedstrijd.tegenstanderId = uitTeam.id;
                        wedstrijd.id = bepaalWedstrijdId();
                        wedstrijden = DatabaseOperations.OphalenWedstrijden();
                        if (wedstrijd.IsGeldig())
                        {
                            if (!wedstrijden.Contains(wedstrijd))
                            {
                                int ok = DatabaseOperations.ToevoegenWedstrijd(wedstrijd);
                                if (ok > 0)
                                {
                                    MessageBox.Show("wedstrijd is toegevoegd", "info"
                        , MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("wedstrijd is niet toegevoegd", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else MessageBox.Show("Deze wedstrijd bestaat al!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else MessageBox.Show("Vul een geheel getal in als thuis en uitscore", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Selecteer een datum", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Selecteer een thuisteam en een uitteam", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void VulOp(Wedstrijd wedstrijd)
        {
            // window opvullen 
            int thuis = 0;
            int uit = 0;
            wedstrijd.GeefScore(ref thuis, ref uit);
            txtThuisScore.Text = thuis.ToString();
            txtUitScore.Text = uit.ToString();
            cmbTeams.ItemsSource = DatabaseOperations.OphalenTeams();
            cmbUitTeams.ItemsSource = DatabaseOperations.OphalenTeams();
            txtDatum.SelectedDate = wedstrijd.datum;
            cmbTeams.SelectedItem = wedstrijd.Team;
            cmbUitTeams.SelectedItem = wedstrijd.Tegenstander;
        }

        private int bepaalWedstrijdId()
        {
            // wedstrijdId bepalen
            List<int> ids = new List<int>();
            int defId = 1;
            List<Wedstrijd> wedstrijden = DatabaseOperations.OphalenWedstrijden();
            foreach (Wedstrijd wedstrijd in wedstrijden)
            {
                ids.Add(wedstrijd.id);
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
    }
}
