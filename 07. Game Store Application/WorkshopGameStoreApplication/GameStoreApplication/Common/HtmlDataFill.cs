namespace GameStoreApplication.Common
{
    using System.Collections.Generic;
    using ViewModels.Admin;

    public class HtmlDataFill
    {
        public void Game(IDictionary<string, string> viewData, GameViewModel model)
        {
            viewData["title"] = model.Title;
            viewData["description"] = model.Description;
            viewData["thumbnail"] = model.Image;
            viewData["price"] = model.Price.ToString();
            viewData["size"] = model.Size.ToString();
            viewData["videoId"] = model.Trailer;
            viewData["release-date"] = model.ReleaseDate.ToString("yyyy-MM-dd");
            viewData["description"] = model.Description;
        }
    }
}
