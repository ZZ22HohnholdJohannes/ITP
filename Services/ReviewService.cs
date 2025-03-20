using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Reserve_iT.Services;
using Reserve_iT.Model;
using System.Windows;
using MySqlX.XDevAPI.Common;

namespace Reserve_iT.Services
{
  public class ReviewService
  {
    /// <summary>
    /// Loads reviews based on the admin login status.
    /// </summary>
    /// <param name="isAdminLoggedIn">Indicates whether the admin is logged in.</param>
    /// <returns>An ObservableCollection of ReviewModel objects.</returns>
    public ObservableCollection<ReviewModel> LoadReviews(bool isAdminLoggedIn)
    {
      var reviews = new ObservableCollection<ReviewModel>();

      // Choose the stored procedure based on the admin login status.
      string storedProcedure = isAdminLoggedIn ? "showReviewsNichtFreigegeben" : "showReviewsFreigegeben";
      DataTable dt = DatabaseService.ExecuteSP(storedProcedure);

      foreach (DataRow row in dt.Rows)
      {
        var review = new ReviewModel
        {
          ReviewID = Convert.ToInt32(row["bewertung_ID"]),
          OrderID = Convert.ToInt32(row["auftrag_ID"]),
          Vorname = row["Vorname"].ToString(),
          Nachname = row["Nachname"].ToString(),
          ReviewText = row["rezension"].ToString()
        };

        reviews.Add(review);
      }

      return reviews;
    }

    /// <summary>
    /// Accepts a review based on the provided review ID.
    /// </summary>
    /// <param name="reviewId">The ID of the review to accept.</param>
    public void AcceptReview(int reviewId)
    {
      var parameters = new Dictionary<string, object>
    {
      { "bewertung_id_in", reviewId }
    };

      DatabaseService.ExecuteSP("reviewFreigeben", parameters);
    }

    /// <summary>
    /// Denies a review based on the provided review ID.
    /// </summary>
    /// <param name="reviewId">The ID of the review to deny.</param>
    public void DenyReview(int reviewId)
    {
      var parameters = new Dictionary<string, object>
    {
      { "bewertung_id_in", reviewId }
    };

      DatabaseService.ExecuteSP("reviewLoeschen", parameters);
    }

    /// <summary>
    /// Adds a review for a specific order.
    /// </summary>
    /// <param name="orderId">The ID of the order to add the review for.</param>
    /// <param name="reviewText">The text of the review.</param>
    public void AddReview(int orderId, string reviewText)
    {
      if (string.IsNullOrEmpty(reviewText))
      {
        MessageBox.Show("Bitte geben Sie eine Bewertung ein", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      var parameters = new Dictionary<string, object>
      {
      { "auftrag_id_in", orderId },
      { "rezension_in", reviewText }
    };
      var dt = DatabaseService.ExecuteSP("submitReview", parameters);
      foreach (DataRow row in dt.Rows)
      {
        var result = Convert.ToInt32(row["Result"]);
        if (result == 0)
        {
          MessageBox.Show("Bitte gültige Auftragsnummer eingeben", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else if (result == 1)
        {
          MessageBox.Show("Bewertung erfolgreich übermittelt", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
      }
    }
  }
}