using System.Collections.Generic;
using System.Linq;
using Sample.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization; // for serialize and deserialize  
using System.IO; // for File operation 
using System.Text.Json;

namespace Sample.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService()
        {

        }

        private string JsonFilePath
        {
            get { return HttpContext.Current.Server.MapPath("~/App_Data/products.json"); ; }
        }

        public IEnumerable<Product> GetProducts()
        {
            string jsonString = File.ReadAllText(JsonFilePath);
            return JsonSerializer.Deserialize<Product[]>(jsonString);

        }

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFilePath))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }
    }

}
