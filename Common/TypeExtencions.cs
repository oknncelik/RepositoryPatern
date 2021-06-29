using System;

namespace Common
{
    public static class TypeExtencions
    {
        public static bool IsNotNullOrEmpty(this string value)
        {
            try
            {
                return value != null && value.Trim().Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}