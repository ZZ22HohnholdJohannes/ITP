﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reserve_iT.Essentials;

namespace Reserve_iT.Model
{
  public class ReviewModel : NotifyObject
  {
    public int ReviewID
    {
      get => Get<int>();
      set => Set(value);
    }
    
    public int OrderID
    {
      get => Get<int>();
      set => Set(value);
    }

    public bool Published
    {
      get => Get<bool>();
      set => Set(value);
    }
    
    public string ReviewText
    {
      get => Get<string>();
      set => Set(value);
    }

    /*public ObservableCollection<ReviewModel> Reviews
    {
      get => Get<ObservableCollection<ReviewModel>>();
      set => Set(value);
    }*/ 
  }
}
