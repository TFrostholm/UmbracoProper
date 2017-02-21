using System.Collections.Generic;

namespace UmbracoProper.Business.Constants
{
    public class Constants
    {
        public static class PageTypes
        {
            public const string StartPage = "startPage";
            public const string SiteSettingsPage = "siteSettingsPage";
            public const string ContentPage = "contentPage";
            public const string NewsPage = "newsPage";
            public const string NewsFolder = "newsFolder";
            public const string NewsOverview = "newsOverview";
        }
        public static class Properties
        {
            public const string HideInNavi = "hideInNavigation";
            public const string NameInNavi = "nameInNavigation";
            public const string UrlName = "umbracoUrlName";
        }

        public static List<string> AllowedNavigationTypes
        {
            get {
                return new List<string>()
                {
                    {PageTypes.ContentPage},
                    {PageTypes.NewsOverview}
                };
            }
        }
    }
}