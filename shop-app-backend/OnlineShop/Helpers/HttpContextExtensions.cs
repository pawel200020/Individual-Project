using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace ShopPortal.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertParametersPaginationInHeader<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null) {throw new ArgumentNullException(nameof(httpContext));}

            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmountOfRecords",count.ToString(CultureInfo.InvariantCulture));
        }
        public static void InsertParametersPaginationInHeader(this HttpContext httpContext, int count)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            httpContext.Response.Headers.Add("totalAmountOfRecords", count.ToString(CultureInfo.InvariantCulture));
        }
    }
}
