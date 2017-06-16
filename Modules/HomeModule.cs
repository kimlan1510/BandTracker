using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;
using System.Text.RegularExpressions;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ => {
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venues"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      Get["/bands/new"] = _ => {
        return View["band_form.cshtml"];
      };
      Get["/venues/new"] = _ => {
        return View["venue_form.cshtml"];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["name"], Request.Form["type"]);
        newBand.Save();
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["name"], Request.Form["location"], Request.Form["capacity"]);
        newVenue.Save();
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      Get["/bands/{id}"] = parameters => {
        
      };
    }
  }
}
