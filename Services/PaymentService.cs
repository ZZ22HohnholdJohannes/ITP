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
    /// <summary>
    /// Creates a booking based on the provided payment and booking details.
    /// </summary>
    /// <param name="payment">The payment details.</param>
    /// <param name="startDate">The start date of the booking.</param>
    /// <param name="endDate">The end date of the booking.</param>
    /// <param name="category">The category of the room.</param>
    /// <param name="type">The type of the room.</param>
    public void CreateBooking(PaymentModel payment, DateTime startDate, DateTime endDate, string category, string type)
    {
      // Determine gender based on the payment details
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

      // Determine category number based on the room category
      var category_num = 0;
      if (category == "Standard")
      {
        category_num = 1;
      }
      else if (category == "Premium")
      {
        category_num = 2;
      }
      else if (category == "Luxus")
      {
        category_num = 3;
      }

      // Determine type number based on the room type
      var type_num = 0;
      if (type == "Einzelzimmer")
      {
        type_num = 1;
      }
      else if (type == "Doppelzimmer")
      {
        type_num = 2;
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
      { "kategorie_in", category_num },
      { "art_in", type_num },
    };
      DataTable dt = DatabaseService.ExecuteSP("createBooking", parameters);
    }
  }
}
