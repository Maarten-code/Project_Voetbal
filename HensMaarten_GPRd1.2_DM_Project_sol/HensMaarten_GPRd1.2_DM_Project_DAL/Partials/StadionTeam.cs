using HensMaarten_GPRd1._2_DM_Project_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HensMaarten_GPRd1._2_DM_Project_DAL
{
    public partial class StadionTeam : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(teamId) && teamId <= 0)
                {
                    return "teamId moet groter dan 0 zijn!";
                }
                else if (columnName == nameof(stadionId) && stadionId <= 0)
                {
                    return "stadionId moet groter dan 0 zijn!";
                }
                else if (columnName == nameof(id) && id <= 0)
                {
                    return "id moet groter dan 0 zijn!";
                }
                else return "";
            }

        }

        public override bool Equals(object obj)
        {
            return obj is StadionTeam stadionTeam &&
                  teamId == stadionTeam.teamId && stadionId == stadionTeam.stadionId;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}
