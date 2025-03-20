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
using Reserve_iT.Model;
using System.Data;
using CommunityToolkit.Mvvm.Input;
using Org.BouncyCastle.Security;
using Mysqlx.Crud;
using System.Dynamic;
using System.Windows.Media.Animation;

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

    public bool isAdminLoggedIn
    {
      get => Get<bool>();
      set => Set(value);
    }

    //Admin
    public int OrderID
    {
      get => Get<int>();
      set => Set(value);
    }

    public AdminModel adminModel
    {
      get => Get<AdminModel>();
      set => Set(value);
    }

    public bool IsBookingFound
    {
      get => Get<bool>();
      set => Set(value);
    }

    //Booking
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

    public string AdminPassword
    {
      get => Get<string>();
      set => Set(value);
    }
    //Payment
    public PaymentModel Payment { get; set; } = new PaymentModel();

    //Review
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
      LoginCommand = new RelayCommand(Login);
      //Navigation
      NavigateToDashboardViewCommand = new RelayCommand(NavigateToDashboardView);
      NavigateToBookingSearchViewCommand = new RelayCommand(NavigateToBookingSearchView);
      NavigateToBookingConfirmationViewCommand = new RelayCommand(NavigateToBookingConfirmationView);
      NavigateToBookingPaymentViewCommand = new RelayCommand(NavigateToBookingPaymentView);
      NavigateToReviewViewCommand = new RelayCommand(NavigateToReviewView);
      NavigateToAdminViewCommand = new RelayCommand(NavigateToAdminView);
      NavigateBackCommand = new RelayCommand(NavigateBack);
      //Booking
      CheckAvailabilityCommand = new RelayCommand(CheckAvailability);
      //Payment
      CreateBookingCommand = new RelayCommand(CreateBooking);
      //Review
      AcceptReviewCommand = new RelayCommand<ReviewModel>(AcceptReview);
      DenyReviewCommand = new RelayCommand<ReviewModel>(DenyReview);
      AddReviewCommand = new RelayCommand(AddReview);
      LoadReviewsCommand = new RelayCommand(LoadReviews);
      //Admin
      ShowBookingCommand = new RelayCommand(ShowBooking);
      DeleteBookingCommand = new RelayCommand(DeleteBooking);
    }

    public ICommand LoginCommand { get; private set; }
    //Navigation
    public ICommand NavigateToDashboardViewCommand { get; private set; }
    public ICommand NavigateToBookingSearchViewCommand { get; private set; }
    public ICommand NavigateToBookingConfirmationViewCommand { get; private set; }
    public ICommand NavigateToBookingPaymentViewCommand { get; private set; }
    public ICommand NavigateToReviewViewCommand { get; private set; }
    public ICommand NavigateToAdminViewCommand { get; private set; }
    public ICommand NavigateBackCommand { get; private set; }
    //Booking
    public ICommand CheckAvailabilityCommand { get; private set; }
    //Payment
    public ICommand CreateBookingCommand { get; private set; }
    //Review
    public ICommand AcceptReviewCommand { get; private set; }
    public ICommand DenyReviewCommand { get; private set; }
    public ICommand AddReviewCommand { get; private set; }
    public ICommand LoadReviewsCommand { get; private set; }
    //Admin
    public ICommand ShowBookingCommand { get; private set; }
    public ICommand DeleteBookingCommand { get; private set; }


    #endregion Commands

    #region Methods

    #region Navigation
    private void NavigateToDashboardView() => MainFrame?.Navigate(new DashboardView()); //Initial loading
    private void NavigateToBookingSearchView() => MainFrame?.Navigate(new BookingSearchView() { DataContext = this }); //Naviagation from DashboardView to BookingSearchView
    private void NavigateToBookingConfirmationView() => MainFrame?.Navigate(new BookingConfirmationView() { DataContext = this }); //Navigation from BookingSearchView to BookingConfirmationView
    private void NavigateToBookingPaymentView() => MainFrame?.Navigate(new BookingPaymentView() { DataContext = this }); //Navigation from BookingConfirmationView to BookingPaymentView
    private void NavigateToReviewView() //Navigation von DashboardView zu ReviewView
    {
      MainFrame?.Navigate(new ReviewView() { DataContext = this });
      LoadReviews();
    }
    private void NavigateToAdminView() => MainFrame?.Navigate(new AdminView() { DataContext = this }); //Navigate from DashboardView to AdminView when Admin is logged in
    private void NavigateBack() //Remember the current view and go back
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
      //Check that the StartDate is before the Enddate and not the same
      if (StartDate > EndDate)
      {
        MessageBox.Show("Das Anreisedatum muss vor dem Abreisedatum liegen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      //Check if a room category is selected
      if (!(Standard || Premium || Luxury))
      {
        MessageBox.Show("Bitte wählen Sie eine Zimmerkategorie", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        return;
      }
      //Check if a room type is selected
      if (!(SingleRoom || DoubleRoom))
      {
        MessageBox.Show("Bitte wählen Sie eine Zimmerart", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      var bookingService = new BookingService();
      DataTable dt = bookingService.CheckAvailability(StartDate, EndDate, Standard, Premium, Luxury, SingleRoom, DoubleRoom);
      //Check if a room is available
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
    //Calculate the total cost of the booking
    private decimal CalculateTotalCost(DateTime startDate, DateTime endDate, decimal costPerNight)
    {
      int numberOfDays = (endDate.Date - startDate.Date).Days;
      return numberOfDays * costPerNight;
    }
    #endregion Booking

    #region Payment
    private bool CanCreateBooking()
    {
      return true;
    }
    //Create a booking with the BookingProperties and the PaymentProperties
    public void CreateBooking()
    {
      var paymentService = new PaymentService();
      DateTime startDate = StartDate;
      DateTime endDate = EndDate;
      string category = RoomCategory;
      string type = RoomType;
      string standardDate = "0001-01-01";

      if (Payment.Male == false && Payment.Female == false && Payment.Diverse == false)
      {
        MessageBox.Show("Bite geben Sie ein Geschlecht an!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if ((String.Compare(Payment.BirthDate.ToString("yyyy-MM-dd"), standardDate)) == 0)
      {
        MessageBox.Show("Bitte geben Sie einen Geburtstag an!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if (string.IsNullOrEmpty(Payment.FirstName) || string.IsNullOrEmpty(Payment.Surname))
      {
        MessageBox.Show("Bitte geben Sie Ihren vollen Namen an!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if (string.IsNullOrEmpty(Payment.Street) || string.IsNullOrEmpty(Payment.HouseNumber) || string.IsNullOrEmpty(Payment.ZipCode) || string.IsNullOrEmpty(Payment.City) || string.IsNullOrEmpty(Payment.Country))
      {
        MessageBox.Show("Bitte geben Sie Ihre volle Adresse an!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      paymentService.CreateBooking(Payment, startDate, endDate, category, type);
      MessageBox.Show("Vielen Dank für Ihre Bestellung", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
      NavigateBack();
      NavigateBack();
      NavigateBack();
    }
    #endregion Payment

    #region Review
    //Initial loading of the Reviews from database or after a review was accepted or denied
    public void LoadReviews()
    {
      var reviewService = new ReviewService();
      Reviews = reviewService.LoadReviews(isAdminLoggedIn); //Update ObservableCollection<ReviewModel> in ViewModel-Property
    }
    //Set the "istFreigegeben" Parameter in the database to true and reload the reviews
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
    //Delete the review from the database and reload the reviews
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
    //Add a review with "istFreigegeben" Parameteter to false that can only be seen by the admin
    public void AddReview()
    {
      var reviewService = new ReviewService();
      reviewService.AddReview(ReviewOrderId, ReviewText);
    }
    #endregion Review

    #region Admin
    //Show the booking by searching with the OrderID
    public void ShowBooking()
    {
      var adminService = new AdminService();
      var result = adminService.ShowBooking(OrderID);
      //Check if a booking was found
      if (result != null)
      {
        adminModel = result;
        adminModel.OrderID = OrderID;
        IsBookingFound = true;
      }
      else
      {
        MessageBox.Show("Kein Auftrag gefunden", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        adminModel = null;
        IsBookingFound = false;
      }
    }
    //When a booking is found, delete the booking from the database
    public void DeleteBooking()
    {
      if (adminModel == null)
      {
        MessageBox.Show("Kein Auftrag zum Löschen ausgewählt.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      var adminService = new AdminService();
      adminService.DeleteBooking(adminModel.OrderID);

      MessageBox.Show("Auftrag erfolgreich gelöscht.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
      adminModel = null;
      IsBookingFound = false;
      OrderID = 0; //Searchbox will be emptied
    }

    #endregion Admin

    #endregion Methods
  }
}
