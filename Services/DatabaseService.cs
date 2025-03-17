using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Reserve_iT.Services;
using MySql.Data.MySqlClient;

namespace Reserve_iT.Services
{
  public class DatabaseService
  {
    private static readonly string ConnectionString;

    static DatabaseService()
    {
      ConnectionString = new MySqlConnectionStringBuilder()
      {
        Server = "127.0.0.1",
        Database = "reserve_it",
        UserID = "root",
        Password = "",
        SslMode = MySqlSslMode.Disabled
      }.ConnectionString;
    }

    public static DataTable ExecuteSP(string sp)
    {
      DataTable tbl = new DataTable();
      try
      {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
          MySqlCommand command = new MySqlCommand(sp, connection)
          {
            CommandType = CommandType.StoredProcedure
          };

          using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
          {
            adapter.Fill(tbl);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBoxService.DisplayMessage(ex.Message, MessageBoxImage.Error);
      }
      return tbl;
    }

    public static DataTable ExecuteSP(string sp, Dictionary<string, object> parameters)
    {
      DataTable tbl = new DataTable();
      try
      {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
          MySqlCommand command = new MySqlCommand(sp, connection)
          {
            CommandType = CommandType.StoredProcedure
          };
          foreach (KeyValuePair<string, object> item in parameters)
          {
            command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
          }
          using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
          {
            adapter.Fill(tbl);

            // Fehlermeldungen abfangen
            if (tbl is not null && tbl.Rows.Count == 1)
              if (tbl.Columns.Contains("Nachricht") && tbl.Rows[0]["Nachricht"] != DBNull.Value)
                MessageBoxService.DisplayMessage(tbl.Rows[0]["Nachricht"].ToString() ?? "DB-Fehler", MessageBoxImage.Warning);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBoxService.DisplayMessage(ex.Message, MessageBoxImage.Error);
      }
      return tbl;
    }

    public static DataSet ExecuteSPDataSet(string sp, Dictionary<string, object> parameters)
    {
      DataSet dataSet = new DataSet();
      try
      {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
          MySqlCommand command = new MySqlCommand(sp, connection)
          {
            CommandType = CommandType.StoredProcedure
          };

          foreach (KeyValuePair<string, object> item in parameters)
          {
            command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
          }

          using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
          {
            adapter.Fill(dataSet);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBoxService.DisplayMessage(ex.Message, MessageBoxImage.Error);
      }
      return dataSet;
    }

    public static DataSet ExecuteSPDataSet(string sp)
    {
      DataSet dataSet = new DataSet();
      try
      {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
          MySqlCommand command = new MySqlCommand(sp, connection)
          {
            CommandType = CommandType.StoredProcedure
          };

          using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
          {
            adapter.Fill(dataSet);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBoxService.DisplayMessage(ex.Message, MessageBoxImage.Error);
      }
      return dataSet;
    }
  }
}
