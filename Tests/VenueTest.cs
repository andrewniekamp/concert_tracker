using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Venue.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Venue.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfVenueIsSame()
    {
      Venue firstVenue = new Venue("Paramount Theatre");
      Venue secondVenue = new Venue("Paramount Theatre");

      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToVenue()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();

      Venue savedVenue = Venue.GetAll()[0];
      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsVenueInDB()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.GetId());

      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void T6_Update_UpdatesVenueInDB()
    {
      Venue testVenue = new Venue("Epicodus Classroom");
      testVenue.Save();
      string newName = "Paramount Theatre";

      testVenue.Update(newName);
      string result = testVenue.GetName();

      Assert.Equal(newName, result);
    }
  }
}
