using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocktailAlmanac.Helpers
{
    public static class HtmlHelpers
    {

        /// <summary>
        /// Extends MvcHtml to conditionaly display a value or empty string
        /// </summary>
        /// <param name="value">Value to be displayed if 'evaluation' is true</param>
        /// <param name="evaluation"></param>
        /// <returns></returns>
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }

        /// <summary>
        /// Extends MvcHtml to conditionaly display one of two possible values
        /// </summary>
        /// <param name="value">Value to be displayed if 'evaluation' is true</param>
        /// <param name="evaluation"></param>
        /// <param name="valueIfFalse">Value to be displayed if 'evaluation' is false</param>
        /// <returns></returns>
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation, MvcHtmlString valueIfFalse)
        {
            return evaluation ? value : valueIfFalse;
        }
    }

}