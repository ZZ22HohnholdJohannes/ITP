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
    public void ShowBooking(int orderID)
    {
      var parameters = new Dictionary<string, object>
      {
        { "orderID", orderID }
      };
      DataTable dt = DatabaseService.ExecuteSP("showBookings", parameters);
      var admins = new ObservableCollection<AdminModel>();

      foreach (DataRow row in dt.Rows)
      {
        var admin = new AdminModel
        {
          FirstName = Convert.ToString(row["vorname"]),
          Surname = Convert.ToString(row["nachname"]),
          Category = Convert.ToString(row["kategorie"]),
          Type = Convert.ToString(row["zimmerart"]),
          StartDate = Convert.ToDateTime(row["startdatum"]),
          EndDate = Convert.ToDateTime(row["enddatum"]),
        };
        admins.Add(admin);
      }
      //showBookings
      //deleteBooking
    }
  }
}
