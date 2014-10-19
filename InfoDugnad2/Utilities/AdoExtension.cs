using System;

namespace Skattedugnad.Utilities
{
   public static class AdoExtensions
   {
      public static bool IsNullOrDbNull(this object value)
      {
         return value == null || value == DBNull.Value;
      }
   }
}