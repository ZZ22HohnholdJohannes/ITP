﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reserve_iT.Essentials
{
  /// <summary>
  /// A proxy to enable binding to a value that is not a dependency property.
  /// </summary>
  public class BindingProxy : Freezable
  {
    protected override Freezable CreateInstanceCore()
    {
      return new BindingProxy();
    }

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    public object Data
    {
      get { return (object)GetValue(DataProperty); }
      set { SetValue(DataProperty, value); }
    }
  }
}
