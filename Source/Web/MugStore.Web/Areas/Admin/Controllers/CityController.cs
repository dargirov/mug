namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using App_Start;
    using Data.Models;
    using Services.Data;
    using ViewModels.City;
    using Web.Controllers;

    [AuthorizeUser]
    public class CityController : BaseController
    {
        private readonly ICitiesService cities;

        public CityController(ICitiesService cities)
        {
            this.cities = cities;
        }

        public ActionResult Index()
        {
            var cities = this.cities.Get().ToList();
            var viewModel = new IndexViewModel()
            {
                Cities = cities
            };

            return this.View(viewModel);
        }

        public ActionResult Import()
        {
            var path = this.Server.MapPath("~/App_Data");
            using (var reader = new StreamReader(path + @"\speedy_sites.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Replace("\"", string.Empty);
                    var values = line.Split(',');
                    var cityType = CityType.City;
                    if (values[6] == "с.")
                    {
                        cityType = CityType.Village;
                    }

                    var city = new City()
                    {
                        Name = this.UppercaseFirst(values[3]),
                        Type = cityType,
                        Highlight = false,
                        PostCode = int.Parse(values[4])
                    };

                    this.cities.Create(city);
                }
            }

            return this.RedirectToAction("Index", "City");
        }

        public ActionResult EditHighlight(int id)
        {
            var city = this.cities.Get(id);
            if (city == null)
            {
                return this.HttpNotFound();
            }

            city.Highlight = !city.Highlight;
            this.cities.Save();

            return this.RedirectToAction("Index", "City");
        }

        private string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.ToLower().Substring(1);
        }
    }
}
