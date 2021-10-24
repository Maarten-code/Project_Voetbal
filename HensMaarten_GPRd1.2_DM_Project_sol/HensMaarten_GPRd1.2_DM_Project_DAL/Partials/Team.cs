using HensMaarten_GPRd1._2_DM_Project_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HensMaarten_GPRd1._2_DM_Project_DAL
{
    public partial class Team : Basisklasse
    {
        public override string ToString()
        {
            return naam;
        }

        public override bool Equals(object obj)
        {
            return obj is Team team &&
                   naam.ToLower() == team.naam.ToLower();
        }
        public override int GetHashCode()
        {
            return -1562492934 + EqualityComparer<string>.Default.GetHashCode(naam);
        }
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(naam) && string.IsNullOrEmpty(naam))
                {
                    return "Naam mag niet leeg zijn";
                }
                if (columnName == nameof(oprichtingsDatum) && oprichtingsDatum == DateTime.MinValue)
                {
                    return "oprichtingsdatum moet ingevuld zijn!";
                }
                else if (columnName == nameof(website) && string.IsNullOrEmpty(website))
                {
                    return "Website mag niet leeg zijn";
                }
                else return "";
            }
        }
    }
}
