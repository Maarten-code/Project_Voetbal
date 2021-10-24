using HensMaarten_GPRd1._2_DM_Project_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HensMaarten_GPRd1._2_DM_Project_DAL
{
    public partial class Stadion : Basisklasse
    {
        public override string ToString()
        {
            return this.naam;
        }
        public override bool Equals(object obj)
        {
            return obj is Stadion stadion &&
                   naam == stadion.naam;
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
                else if (columnName == nameof(straat) && string.IsNullOrEmpty(straat))
                {
                    return "Straat mag niet leeg zijn";
                }
                else if (columnName == nameof(straatNummer) && string.IsNullOrEmpty(straatNummer))
                {
                    return "Straatnummer mag niet leeg zijn";
                }
                else if (columnName == nameof(gemeente) && string.IsNullOrEmpty(gemeente))
                {
                    return "Gemeente mag niet leeg zijn";
                }
                else return "";
            }
        }
    }
}
