using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Reserve_iT.Essentials;

namespace Reserve_iT.Model
{
  public class AdminModel : NotifyObject
  {
    public int OrderID
    {
      get => Get<int>();
      set => Set(value);
    }

    public string FirstName
    {
      get => Get<string>();
      set => Set(value);
    }

    public string Surname
    {
      get => Get<string>();
      set => Set(value);
    }

    public string Category
    {
      get => Get<string>();
      set => Set(value);
    }

    public string Type
    {
      get => Get<string>();
      set => Set(value);
    }
    public DateTime StartDate
    {
      get => Get<DateTime>();
      set => Set(value);
    }
    public DateTime EndDate
    {
      get => Get<DateTime>();
      set => Set(value);
    }
  }
}
