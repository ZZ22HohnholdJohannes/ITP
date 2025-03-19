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
    public void CreateBooking(PaymentModel payment, DateTime startDate, DateTime endDate, string category, string type)
    {
      var gender = "";
      if (payment.Male)
      {
        gender = "M";
      }
      else if (payment.Female)
      {
        gender = "W";
      }
      else if (payment.Diverse)
      {
        gender = "D";
      }

      var parameters = new Dictionary<string, object>
    {
      { "geschlecht_in", gender },
      { "geburtsdatum_in", payment.BirthDate },
      { "vorname_in", payment.FirstName },
      { "nachname_in", payment.Surname },
      { "straße_in", payment.Street },
      { "hausnummer_in", payment.HouseNumber },
      { "plz_in", payment.ZipCode },
      { "ort_in", payment.City },
      { "land_in", payment.Country },
      { "startdatum_in", startDate },
      { "enddatum_in", endDate },
      { "kategorie_in", category },
      { "art_in", type },
    };
      DataTable dt = DatabaseService.ExecuteSP("createBooking", parameters);
    }
  }
}
