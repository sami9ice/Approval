using System;

namespace Application.Common.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object item)
        {
            if (item == null) return 0.00M;

            if (decimal.TryParse(item.ToString(), out decimal result))
                return result;

            return 0.00M;
        }
        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static DateTimeOffset? ToDate(this object item)
        {
            if (item == null) return null;

            if (DateTimeOffset.TryParse(item.ToString(), out DateTimeOffset result))
                return result;

            return null;
        }
        /// <summary>
        /// Converts the string representation of a number to an integer.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static int? ToInt(this object item)
        {
            if (item == null) return null;

            if (int.TryParse(item.ToString(), out int result))
                return result;

            return null;
        }
    }
}