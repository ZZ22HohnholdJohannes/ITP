using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reserve_iT.Model;

namespace Reserve_iT.Services
{
  public class PaymentService
  {
    public void CreateBooking(bool male, bool female, bool diverse, DateTime birthDate, string firstName, string surname, string street, string houseNumber, string zipCode, string city, string country, DateTime startDate, DateTime endDate, string category, string type)
    {
      var payment = new PaymentModel();
      var gender = "";
      if (male == true)
      {
        gender = "M";
      }
      else if (female == true)
      {
        gender = "W";
      }
      else if (diverse == true)
      {
        gender = "D";
      }
      var parameters = new Dictionary<string, object>
    {
        { "geschlecht_in", gender },
        { "geburtsdatum_in", birthDate },
        { "vorname_in", firstName },
        { "nachname_in", surname },
        { "straße_in", street },
        { "hausnummer_in", houseNumber },
        { "plz_in", zipCode },
        { "ort_in", city },
        { "land_in", country },
        { "startdatum_in", startDate },
        { "enddatum_in", endDate },
        { "kategorie_in", category },
        { "art_in", type },
        };
      DataTable dt = DatabaseService.ExecuteSP("createBooking", parameters);
    }
  }
}
