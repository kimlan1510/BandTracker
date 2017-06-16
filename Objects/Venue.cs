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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, location, capacity) OUTPUT INSERTED.id VALUES (@name,  @location, @capacity);", conn);

      SqlParameter namePara = new SqlParameter("@name", this.GetName());
      SqlParameter locationPara = new SqlParameter("@location", this.GetLocation());
      SqlParameter capacityPara = new SqlParameter("@capacity", this.GetCapacity());

      cmd.Parameters.Add(namePara);
      cmd.Parameters.Add(locationPara);
      cmd.Parameters.Add(capacityPara);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @id;", conn);
      SqlParameter IdPara = new SqlParameter("@id", id.ToString());
      cmd.Parameters.Add(IdPara);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundid = 0;
      string name = null;
      string location = null;
      int capacity = 0;

      while(rdr.Read())
      {
        foundid = rdr.GetInt32(0);
        name = rdr.GetString(1);
        location = rdr.GetString(2);
        capacity = rdr.GetInt32(3);
      }
      Venue foundVenue = new Venue(name, location, capacity, foundid);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
     return foundVenue;
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
