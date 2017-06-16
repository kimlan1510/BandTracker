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
