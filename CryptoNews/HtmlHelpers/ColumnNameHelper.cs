using CryptoNews.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CryptoNews.HtmlHelpers
{
    public static class ColumnNameHelper
    {
        public static HtmlString DisplayColumnNameFor<TModel, TClass, TProperty>
            (this IHtmlHelper<TModel> hh, 
            IEnumerable<TClass> model, 
            Expression<Func<TClass, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            name = hh.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var metadata = ModelMetadataProviders.Current.GetMetadataForProperty(
                () => Activator.CreateInstance<TClass>(), typeof(TClass), name);

            return new HtmlString(metadata.DisplayName);
        }
    }
}
