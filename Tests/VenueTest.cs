using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=band_tracker_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_VenueEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveVenueToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Rose Quarter", "Portland", 20000);
      testVenue.Save();

      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Rose Quarter", "Portland", 20000);
      testVenue.Save();
      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());
      //Assert
      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
      //Arrange
      Venue testVenue = new Venue("Rose Quarter", "Portland", 20000);
      testVenue.Save();

      Band testBand = new Band("reo", "rock");
      testBand.Save();

      Band testBand2 = new Band("journey", "rock");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);

      List<Band> result = testVenue.GetBand();
      List<Band> testList = new List<Band>{testBand, testBand2};

      //Assert
      Assert.Equal(testList, result);
    }


    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();

    }

  }
}
