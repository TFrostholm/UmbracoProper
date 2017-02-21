using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbracoProper.Business.Constants;

namespace UmbracoProper.Features.Partials.Header.Navigation.Models
{
    public class MenuItem
    {
        public MenuItem() { }

        public MenuItem(IPublishedContent page)
        {
            Name = NameInNavigation(page);
            IsMainItem = page.Level == 2;
            Level = page.Level;
            Url = page.Url;
            Hide = page.IsDraft || page.GetPropertyValue<bool>(Constants.Properties.HideInNavi);
        }

        public string Name { get; set; }
        public int Level { get; set; }
        public bool Hide { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public List<MenuItem> Children { get; set; }
        public bool IsMainItem { get; set; }

        private string NameInNavigation(IPublishedContent page)
        {
            string menuName = page.GetPropertyValue<string>(UmbracoProper.Business.Constants.Constants.Properties.NameInNavi);
            if (!string.IsNullOrEmpty(menuName))
            {
                //Best result
                return menuName;
            }

            menuName = page.GetPropertyValue<string>(UmbracoProper.Business.Constants.Constants.Properties.UrlName);

            if (!string.IsNullOrEmpty(menuName))
            {
                //Second best result
                return menuName;
            }

            //Least favorite
            return page.Name;

        }
    }
}