using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


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
            var meta = new EmptyModelMetadataProvider()
                .GetMetadataForProperty(typeof(TClass), name);

            return new HtmlString(meta.DisplayName);
        }
    }
}
