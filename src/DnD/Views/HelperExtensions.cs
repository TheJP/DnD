using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnD
{
    public static class HelperExtensions
    {
        public static string IsActiveAction(this IHtmlHelper html, string actions = null, string controllers = null, string cssClass = "active")
        {
            RouteValueDictionary routeValues = html.ViewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            actions = actions ?? currentAction;
            controllers = controllers ?? currentController;

            if (!actions.Split(',').Select(a => a.Trim()).Contains(currentAction)) { return string.Empty; }
            if (!controllers.Split(',').Select(c => c.Trim()).Contains(currentController)) { return string.Empty; }
            return cssClass;
        }

        private static readonly Dictionary<string, string> genderIcon = new Dictionary<string, string>()
        {
            ["male"] = "man",
            ["female"] = "woman",
            ["agender"] = "genderless",
            ["other"] = "other gender"
        };

        public static string GenderIcon(this IHtmlHelper html, string gender)
        {
            gender = gender.ToLower();
            if (genderIcon.ContainsKey(gender)) { return genderIcon[gender]; }
            else { return "other gender"; }
        }
    }
}
