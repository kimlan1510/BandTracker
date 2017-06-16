using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;
    private string _type;

    public Band(string name, string type, int id = 0)
    {
      _name = name;
      _type = type;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetType()
    {
      return _type;
    }

    public override bool Equals(System.Object otherBand)
    {
      if(!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());
        bool typeEquality = (this.GetType() == newBand.GetType());
        return (idEquality && nameEquality && typeEquality);
      }
    }

    public static List<Band> GetAll()
    {
      List<Band> AllBand = new List<Band>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string type = rdr.GetString(2);
        Band newBand = new Band(name, type, id);
        AllBand.Add(newBand);
      }
        if (rdr != null)
      {
        rdr.Close();
      }
        if (conn != null)
      {
        conn.Close();
      }
      return AllBand;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, type) OUTPUT INSERTED.id VALUES (@name,  @type);", conn);

      SqlParameter namePara = new SqlParameter("@name", this.GetName());
      SqlParameter typePara = new SqlParameter("@type", this.GetType());

      cmd.Parameters.Add(namePara);
      cmd.Parameters.Add(typePara);
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

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @id;", conn);
      SqlParameter IdPara = new SqlParameter("@id", id.ToString());
      cmd.Parameters.Add(IdPara);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundid = 0;
      string name = null;
      string type = null;

      while(rdr.Read())
      {
        foundid = rdr.GetInt32(0);
        name = rdr.GetString(1);
        type = rdr.GetString(2);
      }
      Band foundBand = new Band(name, type, foundid);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
     return foundBand;
    }

    // public List<Venue> GetVenue()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venues_bands ON (bands.id = venues_bands.bands_id) JOIN venues ON (venues_bands.venues_id = venues.id) WHERE bands.id = @bandsId;", conn);
    //   SqlParameter bandIdParam = new SqlParameter("@bandId", this.GetId().ToString());
    //
    //   cmd.Parameters.Add(BandIdParam);
    //
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   List<Venue> venues = new List<Venue>{};
    //
    //   while(rdr.Read())
    //   {
    //     int venuesId = rdr.GetInt32(0);
    //     string name = rdr.GetString(1);
    //     string location =rdr.GetString(2);
    //     int capacity = rdr.GetInt32(3);
    //     Venue newVenue = new Venue(name, location, capacity, venuesId);
    //     venues.Add(newVenue);
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return venues;
    // }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (venues_id, bands_id) VALUES (@VenueId, @BandId);", conn);

      SqlParameter venueIdParameter = new SqlParameter("@VenueId", newVenue.GetId());
      SqlParameter bandIdParameter = new SqlParameter( "@BandId", this.GetId());

      cmd.Parameters.Add(venueIdParameter);
      cmd.Parameters.Add(bandIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string name, string type)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @name, type = @type WHERE id = @Id;", conn);

      SqlParameter namePara = new SqlParameter("@name", name);
      SqlParameter typePara = new SqlParameter("@type", type);
      SqlParameter idPara = new SqlParameter("@Id", this.GetId());

      cmd.Parameters.Add(namePara);
      cmd.Parameters.Add(typePara);
      cmd.Parameters.Add(idPara);

      this._name = name;
      this._type = type;
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    // public void Delete()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @bandId; DELETE FROM venues_bands WHERE bands_id = @bandId;", conn);
    //   SqlParameter bandIdParameter = new SqlParameter("@bandId", this.GetId());
    //
    //   cmd.Parameters.Add(bandIdParameter);
    //   cmd.ExecuteNonQuery();
    //
    //   if (conn != null)
    //   {
    //    conn.Close();
    //   }
    // }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands; DELETE FROM venues_bands", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }



  }
}
