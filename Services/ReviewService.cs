using System;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;
using Reserve_iT.Model;

namespace Reserve_iT.Services
{
  public class ReviewService
  {
    private readonly string _connectionString = "Server=localhost;Database=reserve_it;User=root;Password=;";

    public ObservableCollection<ReviewModel> LoadReviews()
    {
      var reviews = new ObservableCollection<ReviewModel>();

      using (var connection = new MySqlConnection(_connectionString))
      {
        connection.Open();

        using (var command = new MySqlCommand("showReviewsFreigegeben", connection))
        {
          command.CommandType = CommandType.StoredProcedure;

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              var review = new ReviewModel
              {
                ReviewID = reader.GetInt32("bewertung_ID"),
                OrderID = reader.GetInt32("auftrag_ID"),
                Published = reader.GetBoolean("istFreigegeben"),
                ReviewText = reader.GetString("rezension"),
                // Falls du in jedem Review eine eigene Collection benötigst (z.B. für verschachtelte Reviews), kannst du sie initialisieren:
                //Reviews = new ObservableCollection<ReviewModel>()
              };

              reviews.Add(review);
            }
          }
        }
      }

      return reviews;
    }
  }
}
