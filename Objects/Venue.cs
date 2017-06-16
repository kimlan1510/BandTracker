using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;
    private string _location;
    private int _capacity;

    public Venue(string name, string location, int capacity, int id = 0)
    {
      _id = id;
      _name = name;
      _location = location;
      _capacity = capacity;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetLocation()
    {
      return _location;
    }
    public int GetCapacity()
    {
      return _capacity;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if(!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        bool locationEquality = (this.GetLocation() == newVenue.GetLocation());
        bool capacityEquality = (this.GetCapacity() == newVenue.GetCapacity());
        return (idEquality && nameEquality && capacityEquality && locationEquality);
      }
    }

    public static List<Venue> GetAll()
    {
      List<Venue> AllVenue = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string location = rdr.GetString(2);
        int capacity = rdr.GetInt32(3);
        Venue newVenue = new Venue(name, location,capacity, id);
        AllVenue.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllVenue;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues; DELETE FROM venues_bands", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
