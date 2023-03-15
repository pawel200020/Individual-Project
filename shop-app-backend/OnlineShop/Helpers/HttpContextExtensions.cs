using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParamtersPanginationInHeader<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null) {throw new ArgumentNullException(nameof(httpContext));}

            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmountOfRecords",count.ToString());
        }
    }
}
