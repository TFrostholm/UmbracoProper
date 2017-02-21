using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmbracoProper.Features.Partials.Header.Navigation.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using UmbracoProper.Business.Constants;

namespace UmbracoProper.Features.Partials.Header.Navigation.Controllers
{
    public class NavigationController : SurfaceController
    {
        // GET: Navigation
        public ActionResult MainNavigation()
        {
            IPublishedContent siteRoot = new UmbracoHelper(UmbracoContext.Current).TypedContent(CurrentPage.Id).AncestorOrSelf(1);
            List<MenuItem> menuItems =
                siteRoot.Children.Where(x => Constants.AllowedNavigationTypes.Contains(x.ContentType.Alias))
                    .Select(x => Set(x, CurrentPage.Id))
                    .ToList();

            return PartialView("/Views/Partials/Header/MainNavigation.cshtml", menuItems);
        }

        private MenuItem Set(IPublishedContent page, int currentPageId)
        {
            bool allowsType = Constants.AllowedNavigationTypes.Contains(page.ContentType.Alias);
            if (!allowsType)
            {
                return null;
            }

            if (page.Children.Any(x => Constants.AllowedNavigationTypes.Contains(x.ContentType.Alias)))
            {
                MenuItem menuItem = new MenuItem(page)
                {
                    IsActive = page.Id == currentPageId,
                    Children = page.Children.Any() ? page.Children.Select(child => Set(child, currentPageId)).ToList() : new List<MenuItem>()
                };

                if (menuItem.Level == 2)
                {
                    menuItem.Children.Insert(0, new MenuItem(page)
                    {
                        Children = new List<MenuItem>(),
                        IsMainItem = false
                    });
                }

                return menuItem;
            }

            return new MenuItem(page)
            {
                IsActive = currentPageId == page.Id,
                Children = new List<MenuItem>(),
                IsMainItem = page.Level == 2 && page.Children.Any(x => Constants.AllowedNavigationTypes.Contains(x.ContentType.Alias))
            };
        }
    }
}