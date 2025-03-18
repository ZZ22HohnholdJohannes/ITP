﻿using System;
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
using Reserve_iT.Model;
using System.Data;
using CommunityToolkit.Mvvm.Input;
using Org.BouncyCastle.Security;

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

    public string RoomType
    {
      get => Get<string>();
      set => Set(value);
    }

    public string RoomCategory
    {
      get => Get<string>();
      set => Set(value);
    }

    public decimal CostPerNight
    {
      get => Get<decimal>();
      set => Set(value);
    }

    public decimal TotalCost
    {
      get => Get<decimal>();
      set => Set(value);
    }

    public string PictureRoom
    {
      get => Get<string>();
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
    //Alles für Review
    public ObservableCollection<ReviewModel> Reviews
    {
      get => Get<ObservableCollection<ReviewModel>>();
      set => Set(value);
    }

    public int ReviewOrderId
    {
      get => Get<int>();
      set => Set(value);
    }

    public string ReviewText
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
      //Navigation zur Buchung
      NavigateToDashboardViewCommand = new RelayCommand(NavigateToDashboardView);
      NavigateToBookingSearchViewCommand = new RelayCommand(NavigateToBookingSearchView);
      NavigateToBookingConfirmationViewCommand = new RelayCommand(NavigateToBookingConfirmationView);
      NavigateToBookingPaymentViewCommand = new RelayCommand(NavigateToBookingPaymentView);
      //Navigation zu Bewertung
      NavigateToReviewViewCommand = new RelayCommand(NavigateToReviewView);
      //Navigation zur Administation
      NavigateToAdminViewCommand = new RelayCommand(NavigateToAdminView);
      //Navigation zurück
      NavigateBackCommand = new RelayCommand(NavigateBack);
      //Booking Service
      CheckAvailabilityCommand = new RelayCommand(CheckAvailability);
      //Bewertung
      AcceptReviewCommand = new RelayCommand<ReviewModel>(AcceptReview);
      DenyReviewCommand = new RelayCommand<ReviewModel>(DenyReview);
      AddReviewCommand = new RelayCommand(AddReview);
      LoadReviewsCommand = new RelayCommand(LoadReviews);
      //Administration
      ShowBookingCommand = new RelayCommand(ShowBooking);
      DeleteBookingCommand = new RelayCommand(DeleteBooking);
      //Ab hier schreibe ich Commands, die noch implementiert werden müssen, aber noch nicht fertig sind
      CreateBookingCommand = new RelayCommand(CreateBooking);
      SaveGuestDataCommand = new RelayCommand(SaveGuestData);
    }

    //Login
    public ICommand LoginCommand { get; private set; }
    //Navigation
    //Navigation zur Buchung
    public ICommand NavigateToDashboardViewCommand { get; private set; }
    public ICommand NavigateToBookingSearchViewCommand { get; private set; }
    public ICommand NavigateToBookingConfirmationViewCommand { get; private set; }
    public ICommand NavigateToBookingPaymentViewCommand { get; private set; }
    //Navigation zu Bewertung
    public ICommand NavigateToReviewViewCommand { get; private set; }
    //Navigation zur Administation
    public ICommand NavigateToAdminViewCommand { get; private set; }
    //Navigation zurück
    public ICommand NavigateBackCommand { get; private set; }
    //Booking Service
    public ICommand CheckAvailabilityCommand { get; private set; }
    //Bewertung
    public ICommand AcceptReviewCommand { get; private set; }
    public ICommand DenyReviewCommand { get; private set; }
    public ICommand AddReviewCommand { get; private set; }
    public ICommand LoadReviewsCommand { get; private set; }
    //Administration
    public ICommand ShowBookingCommand { get; private set; }
    public ICommand DeleteBookingCommand { get; private set; }
    //Ab hier schreibe ich Commands, die noch implementiert werden müssen, aber noch nicht fertig sind (Gehören zur PaymentView)
    public ICommand CreateBookingCommand { get; private set; }
    public ICommand SaveGuestDataCommand { get; private set; }


    #endregion Commands

    #region Methods

    #region Navigation
    //Navigation zur Buchung
    private void NavigateToDashboardView() => MainFrame?.Navigate(new DashboardView()); //Initiales Laden der Anwendung
    private void NavigateToBookingSearchView() => MainFrame?.Navigate(new BookingSearchView() { DataContext = this }); //Naviagation nach Button Zimmer buchen
    private void NavigateToBookingConfirmationView() => MainFrame?.Navigate(new BookingConfirmationView() { DataContext = this }); //Navigation von BookingSearchView zu BookingConfirmationView
    private void NavigateToBookingPaymentView() => MainFrame?.Navigate(new BookingPaymentView());
    //Navigation zu Bewertung
    private void NavigateToReviewView()
    {
      MainFrame?.Navigate(new ReviewView() { DataContext = this });
      LoadReviews();
    }

    //Navigation zur Administation
    private void NavigateToAdminView() => MainFrame?.Navigate(new AdminView());
    //Navigation zurück
    private void NavigateBack()
    {
      if (MainFrame.NavigationService.CanGoBack)
        MainFrame.NavigationService.GoBack();
    }

    #endregion Navigation

    #region Dashboard
    public void Login()
    {
      if (AdminPassword == "Admin")
      {
        isAdminLoggedIn = true;
      }
      else
      {
        isAdminLoggedIn = false;
      }
    }
    #endregion Dashboard

    #region Booking
    public void CheckAvailability()
    {
      if (StartDate >= EndDate)
      {
        MessageBox.Show("Das Anreisedatum muss vor dem Abreisedatum liegen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if (!(Standard || Premium || Luxury))
      {
        MessageBox.Show("Bitte wählen Sie eine Zimmerkategorie", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        return;
      }

      if (!(SingleRoom || DoubleRoom))
      {
        MessageBox.Show("Bitte wählen Sie eine Zimmerart", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      var bookingService = new BookingService();
      DataTable dt = bookingService.CheckAvailability(StartDate, EndDate, Standard, Premium, Luxury, SingleRoom, DoubleRoom);

      if (dt.Rows.Count > 0)
      {
        var row = dt.Rows[0];
        RoomType = row["zimmerart"].ToString();
        RoomCategory = row["kategorie"].ToString();
        CostPerNight = Convert.ToDecimal(row["preis_pro_nacht"]);
        TotalCost = CalculateTotalCost(StartDate, EndDate, CostPerNight);
        PictureRoom = "pack://application:,,,/Images/" + RoomCategory + "_" + RoomType + ".png";

        MessageBox.Show("Zimmer verfügbar", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        NavigateToBookingConfirmationView();
      }
      else
      {
        MessageBox.Show("Es ist kein Zimmer verfügbar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
    //TODO: Berechnung ausreichend testen
    private decimal CalculateTotalCost(DateTime startDate, DateTime endDate, decimal costPerNight)
    {
      int numberOfDays = (endDate.Date - startDate.Date).Days;
      return numberOfDays * costPerNight;
    }
    #endregion Booking

    #region Reviews
    public void LoadReviews()
    {
      var reviewService = new ReviewService();
      Reviews = reviewService.LoadReviews(isAdminLoggedIn); // Hier wird die ObservableCollection<ReviewModel> in der ViewModel-Property aktualisiert.
    }
    public void AcceptReview(object? review)
    {
      if (review is not null && review is ReviewModel reviewModel)
      {
        var reviewService = new ReviewService();
        reviewService.AcceptReview(reviewModel.ReviewID);
        LoadReviews();
        MessageBox.Show("Bewertung erfolgreich freigegeben", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        
      }
    }

    public void DenyReview(object? review)
    {
      if (review is not null && review is ReviewModel reviewModel)
      {
        var reviewService = new ReviewService();
        reviewService.DenyReview(reviewModel.ReviewID);
        LoadReviews();
        MessageBox.Show("Bewertung erfolgreich gelöscht", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }

    public void AddReview()
    {
      var reviewService = new ReviewService();
      reviewService.AddReview(ReviewOrderId, ReviewText);
    }
    #endregion Review

    #region Admin
    public void ShowBooking()
    {

    }

    public void DeleteBooking()
    {

    }

    #endregion Admin

    #endregion Methods

    public void CreateBooking()
    {

    }

    public void SaveGuestData()
    {

    }
  }
}
