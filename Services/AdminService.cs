using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reserve_iT.Model;

namespace Reserve_iT.Services
{
  public class AdminService
  {
    public AdminModel ShowBooking(int orderID)
    {
      var parameters = new Dictionary<string, object>
    {
        { "auftrag_id_in", orderID }
    };
      DataTable dt = DatabaseService.ExecuteSP("showBookings", parameters);

      if (dt.Rows.Count > 0)
      {
        DataRow row = dt.Rows[0];
        var admin = new AdminModel
        {
          FirstName = Convert.ToString(row["vorname"]),
          Surname = Convert.ToString(row["nachname"]),
          Category = Convert.ToString(row["kategorie"]),
          Type = Convert.ToString(row["zimmerart"]),
          StartDate = Convert.ToDateTime(row["startdatum"]),
          EndDate = Convert.ToDateTime(row["enddatum"])
        };
        return admin;
      }
      else
      {
        return null;
      }
    }

    public void DeleteBooking(int orderID)
    {
      var parameters = new Dictionary<string, object>
    {
        { "auftrag_id_in", orderID }
    };
      DatabaseService.ExecuteSP("deleteBooking", parameters);
    }
  }
}
