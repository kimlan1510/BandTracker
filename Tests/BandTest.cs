using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=band_tracker_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_BandEmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveBandToDatabase()
    {
      //Arrange
      Band testBand = new Band("Reo", "rock");
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Update_UpdatesBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("Reo", "rock");
      testBand.Save();
      string newName = "journey";
      //Act
      testBand.Update("journey", "rock");
      string result =testBand.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_AddVenue_AddsVenueToBand()
    {
      //Arrange
      Band testBand = new Band("reo", "rock");
      testBand.Save();

      Venue testVenue1 = new Venue("Rose Quarter", "Portland", 20000);
      testVenue1.Save();

      Venue testVenue2 = new Venue("Moda", "mars", 10000);
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue1);
      testBand.AddVenue(testVenue2);

      List<Venue> result = testBand.GetVenue();
      List<Venue> testList = new List<Venue>{testVenue1, testVenue2};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("Reo", "rock");
      testBand.Save();
      //Act
      Band foundBand = Band.Find(testBand.GetId());
      //Assert
      Assert.Equal(testBand, foundBand);
    }

    [Fact]
   public void Delete_DeletesBandAssociationsFromDatabase_BandList()
   {
     //Arrange
     Band testBand = new Band("reo", "rock");
     testBand.Save();

     Venue testVenue = new Venue("Moda", "mars", 10000);
     testVenue.Save();

     //Act
     testBand.AddVenue(testVenue);
     testBand.Delete();

     List<Band> resultVenueBand = testVenue.GetBand();
     List<Band> testVenueBand = new List<Band> {};

     //Assert
     Assert.Equal(testVenueBand, resultVenueBand);
   }



    public void Dispose()
    {
      Band.DeleteAll();

    }
  }
}
