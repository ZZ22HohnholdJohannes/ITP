using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reserve_iT.Essentials;

namespace Reserve_iT.Model
{
  public class PaymentModel : NotifyObject
  {
    public bool Male
    {
      get => Get<bool>();
      set => Set(value);
    }

    public bool Female
    {
      get => Get<bool>();
      set => Set(value);
    }

    public bool Diverse
    {
      get => Get<bool>();
      set => Set(value);
    }

    public DateTime BirthDate
    {
      get => Get<DateTime>();
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

    public string Street
    {
      get => Get<string>();
      set => Set(value);
    }

    public string HouseNumber
    {
      get => Get<string>();
      set => Set(value);
    }
    public string ZipCode
    {
      get => Get<string>();
      set => Set(value);
    }

    public string City
    {
      get => Get<string>();
      set => Set(value);
    }

    public string Country
    {
      get => Get<string>();
      set => Set(value);
    }
  }
}
