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
    /// Interaction logic for WedstrijdenOverzicht.xaml
    /// </summary>
    public partial class WedstrijdenOverzicht : Window
    {
        public WedstrijdenOverzicht()
        {
            InitializeComponent();
        }
        List<DateTime> datums = new List<DateTime>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWindow();
        }
        private void btnToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            // als de toggle button links staat (unchecked) laten we een weergave van wedstrijden op
            // datum zien en collapsen we de elementen die nodig zijn voor de filterview
            FilterView.Visibility = Visibility.Collapsed;
            DatumView.Visibility = Visibility.Visible;
            BtnsFilterView.Visibility = Visibility.Collapsed;
            setDate();
        }

        private void btnToggle_Checked(object sender, RoutedEventArgs e)
        {
            // als de toggle button rechts staat (checked) laten we de filterfiew zien
            // en collapsen we de elementen van de datumview
            FilterView.Visibility = Visibility.Visible;
            DatumView.Visibility = Visibility.Collapsed;
            BtnsFilterView.Visibility = Visibility.Visible;
            datagridWedstrijden.ItemsSource = DatabaseOperations.OphalenWedstrijdenMetTeams();
        }

        private void btnBewerkWedstrijd_Click(object sender, RoutedEventArgs e)
        {
            // controleren of er een wedstrijd geselecteerd is voor we deze gaan bewerken
            if (datagridWedstrijden.SelectedItem is Wedstrijd wedstrijd)
            {
                WedstrijdAanmaken wedstrijdBewerken = new WedstrijdAanmaken(wedstrijd);
                wedstrijdBewerken.ShowDialog();
                datums.Clear();
                datagridWedstrijden.SelectedItem = null;
                btnToggle.IsChecked = false;
                LoadWindow();
            }
            else MessageBox.Show("Selecteer eerst een wedstrijd", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void btnNieuweWedstrijd_Click(object sender, RoutedEventArgs e)
        {
            // naar het venster om een nieuwe wedstrijd aan te maken 
            WedstrijdAanmaken wedstrijdAanmaken = new WedstrijdAanmaken();
            wedstrijdAanmaken.ShowDialog();
            datums.Clear();
            btnToggle.IsChecked = false;
            LoadWindow();
        }
        private void BtnVolgendeDatum_Click(object sender, RoutedEventArgs e)
        {
            // datum verhogen 
            setDate(1);
        }
        private void BtnVorigeDatum_Click(object sender, RoutedEventArgs e)
        {
            // datum verlagen
            setDate(-1);
        }
        private void btnFilterTerm_Click(object sender, RoutedEventArgs e)
        {
            // wedstrijden filteren op een term (string) die de gebruiker zelf ingeeft
            string term = txtTerm.Text;
            if (!string.IsNullOrEmpty(term))
            {
                datagridWedstrijden.ItemsSource = DatabaseOperations.OphalenWedstrijdenViaTerm(term);
            }
            else MessageBox.Show("Veld term is niet ingevuld!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnNFilterTeam_Click(object sender, RoutedEventArgs e)
        {
            // wedstrijden filteren op een team dat de gebruiker selecteerd uit de combobox
            if (CmbTeams.SelectedItem is Team team)
            {
                datagridWedstrijden.ItemsSource = DatabaseOperations.OphalenWedstrijdenViaTeamid(team.id);
            }
            else MessageBox.Show("Selecteer een team!", "Foutmelding"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
        }
        int start = 0;
        private void setDate(int value = 0)
        {
            // juiste wedstrijden weergeven bij de juiste datum en controleren dat we niet onder de minimum
            // of boven de maximum datum gaan
            int valideer = start + value;
            if (valideer >= 0)
            {
                int controle = datums.Count();
                if (valideer < controle)
                {
                    start = valideer;
                    lblDatum.Content = datums[start].ToLongDateString();
                    datagridWedstrijden.ItemsSource = DatabaseOperations.OphalenWedstrijdenOpDatumMetTeams(datums[start]);
                }                
            }
        }
        private void LoadWindow()
        {
            //combobox, lijst met datums opvullen en de juiste datum laten zien 
            CmbTeams.ItemsSource = DatabaseOperations.OphalenTeams();
            List<Wedstrijd> wedstrijden = DatabaseOperations.OphalenWedstrijdenMetTeams();
            foreach (Wedstrijd wedstrijd in wedstrijden)
            {
                if(!datums.Contains(wedstrijd.datum)) datums.Add(wedstrijd.datum);
            }
            setDate();
        }
    }
}
