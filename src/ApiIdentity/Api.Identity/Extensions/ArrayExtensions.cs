using System;

namespace Api.Identity.Extensions
{
    public static class ArrayExtensions
    {
        public static bool Compare(this Array array, byte[] a1, byte[] a2, int a1Index, int a2Index, int length)
        {
            for (var i = 0; i < length; i++)
            {
                if (a1[i + a1Index] != a2[i + a2Index])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
