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
    /// <summary>
    /// Retrieves booking information based on the provided order ID.
    /// </summary>
    /// <param name="orderID">The ID of the order to retrieve.</param>
    /// <returns>An AdminModel object containing the booking information, or null if no booking is found.</returns>
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
    /// <summary>
    /// Deletes a booking based on the provided order ID.
    /// </summary>
    /// <param name="orderID">The ID of the order to delete.</param>
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
