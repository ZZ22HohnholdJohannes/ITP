using System;
using System.Collections.Generic;
using System.Data;
using System.Collections.ObjectModel;
using Reserve_iT.Services;
using Reserve_iT.Model;

namespace Reserve_iT.Services
{
  public class BookingService
  {
    /// <summary>
    /// Checks the availability of rooms based on the provided criteria.
    /// </summary>
    /// <param name="startDate">The start date of the booking.</param>
    /// <param name="endDate">The end date of the booking.</param>
    /// <param name="standard">Indicates if a standard room is requested.</param>
    /// <param name="premium">Indicates if a premium room is requested.</param>
    /// <param name="luxury">Indicates if a luxury room is requested.</param>
    /// <param name="singleRoom">Indicates if a single room is requested.</param>
    /// <param name="doubleRoom">Indicates if a double room is requested.</param>
    /// <returns>A DataTable containing the available rooms that match the criteria.</returns>
    public DataTable CheckAvailability(DateTime startDate, DateTime endDate, bool standard, bool premium, bool luxury, bool singleRoom, bool doubleRoom)
    {
      // Determine the room category based on the provided criteria
      var category = -1;
      if (standard)
        category = 1;
      else if (premium)
        category = 2;
      else if (luxury)
        category = 3;

      // Determine the room type based on the provided criteria
      var type = -1;
      if (singleRoom)
        type = 1;
      else if (doubleRoom)
        type = 2;

      var parameters = new Dictionary<string, object>
            {
                { "startDate", startDate },
                { "endDate", endDate },
                { "kategorieZimmer", category },
                { "artZimmer", type }
            };

      DataTable dt = DatabaseService.ExecuteSP("checkAvailability", parameters);
      return dt;
    }
  }
}
