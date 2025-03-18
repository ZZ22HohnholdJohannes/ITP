using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Reserve_iT.Services;
using Reserve_iT.Model;

namespace Reserve_iT.Services
{
  public class ReviewService
  {
    public ObservableCollection<ReviewModel> LoadReviews(bool isAdminLoggedIn)
    {
      var reviews = new ObservableCollection<ReviewModel>();

      string storedProcedure = isAdminLoggedIn ? "showReviewsNichtFreigegeben" : "showReviewsFreigegeben";
      DataTable dt = DatabaseService.ExecuteSP(storedProcedure);

      foreach (DataRow row in dt.Rows)
      {
        var review = new ReviewModel
        {
          ReviewID = Convert.ToInt32(row["bewertung_ID"]),
          OrderID = Convert.ToInt32(row["auftrag_ID"]),
          Published = Convert.ToBoolean(row["istFreigegeben"]),
          ReviewText = row["rezension"].ToString()
        };

        reviews.Add(review);
      }

      return reviews;
    }

    public void AcceptReview(int reviewId)
    {
      var parameters = new Dictionary<string, object>
    {
      { "bewertung_id_in", reviewId }
    };

      DatabaseService.ExecuteSP("reviewFreigeben", parameters);
    }

    public void DenyReview(int reviewId)
    {
      var parameters = new Dictionary<string, object>
    {
      { "bewertung_id_in", reviewId }
    };

      DatabaseService.ExecuteSP("reviewLoeschen", parameters);
    }

    public void AddReview(int orderId, string reviewText)
    {
      var parameters = new Dictionary<string, object>
    {
      { "auftrag_id_in", orderId },
      { "rezension_in", reviewText }
    };

      DatabaseService.ExecuteSP("submitReview", parameters);
    }
  }
}