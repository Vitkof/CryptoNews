using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Linq.Expressions;


namespace CryptoNews.HtmlHelpers
{
    public static class ExpressionHelper
    {
        private static readonly ModelExpressionProvider model = 
            new(new EmptyModelMetadataProvider());

        public static string GetExpressionText<TEntity, TProperty>(
            this Expression<Func<TEntity, TProperty>> expression)
        {
            return model.GetExpressionText(expression);
        }
    }
}
