using Microsoft.AspNetCore.Mvc;
using System;

namespace catdance.Controllers
{
    public class HomeController : Controller
    {
         private readonly string[] _images = new string[] {
            "https://media1.giphy.com/media/yFQ0ywscgobJK/giphy.webp",
            "https://img.huffingtonpost.com/asset/5b9d69e02500003200367360.gif",
            "https://media0.giphy.com/media/zWuSfeDJkqj0A/200w.webp",
            "https://media2.giphy.com/media/mJqQXx8vK9kD6/200w.webp",
            "https://media2.giphy.com/media/12mWfQYoxRqslq/200.webp",
            "https://media3.giphy.com/media/Vj5ZgHbXa3kWY/200w.webp",
            "https://media0.giphy.com/media/40Fpxgn6Yq640/giphy.webp",
            "https://media3.giphy.com/media/jpPTyP6YghtiU/200.webp",
            "https://media2.giphy.com/media/3oriO0OEd9QIDdllqo/200.webp",
            "https://media0.giphy.com/media/OmK8lulOMQ9XO/200w.webp",            
        };

        public HomeController()
        { }

        public IActionResult Index()
        {
            var rng = new Random();
            var url = _images[rng.Next(_images.Length)];
            
            var model = new CatUrlModel()
            {
                Url = url
            };

            return View(model);
        }
    }

    public class CatUrlModel
    {
        public string Url { get; set; }
    } 
}
