using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;


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
        string name = Request.Form["name"];
        name.ToLower();
        string nameTitleCasename = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
        Band newBand = new Band(nameTitleCasename, Request.Form["type"]);
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
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedBand = Band.Find(parameters.id);
        var venues = selectedBand.GetVenue();
        var AllVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("venues", venues);
        model.Add("AllVenues", AllVenues);
        return View["band.cshtml", model];
      };
      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedVenue = Venue.Find(parameters.id);
        var bandsAtVenue = selectedVenue.GetBand();
        var AllBands = Band.GetAll();
        model.Add("venue", selectedVenue);
        model.Add("bands", bandsAtVenue);
        model.Add("allBands", AllBands);
        return View["venue.cshtml", model];
      };
      Get["bands/{id}/venue"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var AllVenues = Venue.GetAll();
        var selectedBand = Band.Find(parameters.id);
        model.Add("AllVenues", AllVenues);
        model.Add("band", selectedBand);
        return View["band_venue_form.cshtml", model];
      };
      Post["bands/{id}/venue"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue selectedVenue = Venue.Find(int.Parse(Request.Form["venue"]));
        var selectedBand = Band.Find(parameters.id);
        selectedBand.AddVenue(selectedVenue);
        var venues = selectedBand.GetVenue();
        var AllVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("venues", venues);
        model.Add("AllVenues", AllVenues);
        return View["band.cshtml", model];
      };
      Get["venues/{id}/band"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var AllBands = Band.GetAll();
        var selectedVenue = Venue.Find(parameters.id);
        model.Add("AllBands", AllBands);
        model.Add("venue", selectedVenue);
        return View["venue_band_form.cshtml", model];
      };
      Post["venues/{id}/band"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(int.Parse(Request.Form["band"]));
        var selectedVenue = Venue.Find(parameters.id);
        selectedVenue.AddBand(selectedBand);
        var bands = selectedVenue.GetBand();
        var AllBands = Band.GetAll();
        model.Add("venue", selectedVenue);
        model.Add("bands", bands);
        model.Add("AllBands", AllBands);
        return View["venue.cshtml", model];
      };
      Get["venues/edit/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["venue_edit.cshtml", selectedVenue];
      };
      Patch["/venues/edit/{id}"] = parameters =>{
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Update(Request.Form["name"], Request.Form["location"], int.Parse(Request.Form["capacity"]));
        return View["success.cshtml"];
      };
      Get["venues/delete/{id}"] = parameters => {
       Venue SelectedVenue = Venue.Find(parameters.id);
       return View["venue_delete.cshtml", SelectedVenue];
      };
      Delete["venues/delete/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Delete();
        return View["success.cshtml"];
      };

      Post["/bands/clear"] = _ => {
        Band.DeleteAll();
        List<Band> AllBand = Band.GetAll();
        return View["bands.cshtml", AllBand];
      };
      Post["/venues/clear"] = _ => {
        Venue.DeleteAll();
        List<Venue> AllVenue = Venue.GetAll();
        return View["venues.cshtml", AllVenue];
      };

    }
  }
}
