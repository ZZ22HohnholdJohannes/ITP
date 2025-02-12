using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using Reserve_iT.Essentials;
using Reserve_iT.Services;
using Reserve_iT.View;

namespace Reserve_iT.ViewModel
{
  public class MainWindowViewModel : NotifyObject
  {
    #region Properties
    public Frame MainFrame
    {
      get => Get<Frame>();
      set => Set(value);
    }
    //Für Booking Service
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

    public bool Standard
    {
      get => Get<bool>();
      set => Set(value);
    }

    public bool Premium
    {
      get => Get<bool>();
      set => Set(value);
    }

    public bool Luxury
    {
      get => Get<bool>();
      set => Set(value);
    }

    public bool SingleRoom
    {
      get => Get<bool>();
      set => Set(value);
    }

    public bool DoubleRoom
    {
      get => Get<bool>();
      set => Set(value);
    }
    //AdminLogin
    public bool isAdminLoggedIn
      {
      get => Get<bool>();
      set => Set(value);
    }
    public string AdminPassword
    {
      get => Get<string>();
      set => Set(value);
    }
    #endregion Properties

    #region Constructors
    public MainWindowViewModel()
    {
      isAdminLoggedIn = false;
      StartDate = DateTime.Now;
      EndDate = DateTime.Now.AddDays(1);
      CreateCommands();
    }
    #endregion Constructors
    
    #region Commands
    private void CreateCommands()
    {
      //Login
      LoginCommand = new RelayCommand(Login);
      //Navigation
      NavigateToDashboardViewCommand = new RelayCommand(NavigateToDashboardView);
      NavigateToBookingSearchViewCommand = new RelayCommand(NavigateToBookingSearchView);
      NavigateToConfirmationViewCommand = new RelayCommand(NavigateToConfirmationView);
      //Booking Service
      CheckAvailabilityCommand = new RelayCommand(CheckAvailability);
    }

    //Login
    public ICommand LoginCommand { get; private set; }
    //Navigation
    public ICommand NavigateToDashboardViewCommand { get; private set; }
    public ICommand NavigateToBookingSearchViewCommand { get; private set; }
    public ICommand NavigateToConfirmationViewCommand { get; private set; }
    //Booking Service
    public ICommand CheckAvailabilityCommand { get; private set; }

    #endregion Commands
    
    #region Methods
    
    #region Navigation
    // Methoden zur Navigation (DataContext ist das zentrale UserViewModel)
    private void NavigateToDashboardView() => MainFrame?.Navigate(new DashboardView());
    private void NavigateToBookingSearchView() => MainFrame?.Navigate(new BookingSearchView());
    private void NavigateToBookingBookingConfirmationView() => MainFrame?.Navigate(new BookingConfirmationView() { DataContext = this });
    private void NavigateToConfirmationView() => MainFrame?.Navigate(new BookingConfirmationView());

    #endregion Navigation

    public void Login()
    { 
      if(AdminPassword == "Administrator")
      {
        isAdminLoggedIn = true;
      }
      else
      {
        isAdminLoggedIn = false;
      }
    }

    public void CheckAvailability()
    {
      if (StartDate >= EndDate)
      {
        Debug.WriteLine("Das Anreisedatum muss vor dem Abreisedatum liegen.");
        return;
      }

      if (!(Standard || Premium || Luxury))
      {
        Debug.WriteLine("Bitte wählen Sie eine Zimmerkategorie (Standard, Premium oder Luxus).");
        return;
      }

      if (!(SingleRoom || DoubleRoom))
      {
        Debug.WriteLine("Bitte wählen Sie eine Zimmerart (Einzelzimmer oder Doppelzimmer).");
        return;
      }

      //bool isAvailable = bookingService.CheckAvailability(StartDate, EndDate, Standard, Premium, Luxury, SingleRoom, DoubleRoom);
      bool isAvailable = true;
      if (isAvailable)
      {
        Debug.WriteLine("Zimmer verfügbar!");
      }
      else
      {
        Debug.WriteLine("Kein Zimmer verfügbar.");
      }
    }
    
    #endregion Methods

  }
}
