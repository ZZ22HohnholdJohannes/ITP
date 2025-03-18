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
    public DataTable CheckAvailability(DateTime startDate, DateTime endDate, bool standard, bool premium, bool luxury, bool singleRoom, bool doubleRoom)
    {
      int category = -1;
      if (standard)
        category = 1;
      else if (premium)
        category = 2;
      else if (luxury)
        category = 3;

      int type = -1;
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

    //createBooking
    //saveGuestData
  }
}
