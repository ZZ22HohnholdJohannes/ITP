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
    public ObservableCollection<ReviewModel> LoadReviews()
    {
      var reviews = new ObservableCollection<ReviewModel>();

      DataTable dt = DatabaseService.ExecuteSP("showReviewsFreigegeben");

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
  }
}
