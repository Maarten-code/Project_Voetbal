using HensMaarten_GPRd1._2_DM_Project_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HensMaarten_GPRd1._2_DM_Project_DAL
{
    public partial class Wedstrijd : Basisklasse
    {
        public override string ToString()
        {
            return $"{Team} {uitslag} {Tegenstander}";
        }
        public void GeefScore(ref int thuisScore, ref int uitScore)
        {
            int split = this.uitslag.IndexOf('-');
            string thuis = this.uitslag.Substring(0, split);
            string uit = this.uitslag.Substring(split + 1);
            thuisScore = int.Parse(thuis);
            uitScore = int.Parse(uit);
        }
        public string MaakScore(ref int thuisScore, ref int uitScore)
        {
            return thuisScore + "-" + uitScore;
        }
        public override bool Equals(object obj)
        {
            return obj is Wedstrijd wedstrijd &&
                wedstrijd.teamId == teamId && wedstrijd.tegenstanderId == tegenstanderId
                && wedstrijd.datum == datum;
        }
        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(uitslag) && string.IsNullOrEmpty(uitslag))
                {
                    return "uitslag mag niet leeg zijn";
                }
                else if (columnName == nameof(datum) && datum == DateTime.MinValue)
                {
                    return "datum mag niet leeg zijn";
                }
                else return "";
            }
        }
    }
}
