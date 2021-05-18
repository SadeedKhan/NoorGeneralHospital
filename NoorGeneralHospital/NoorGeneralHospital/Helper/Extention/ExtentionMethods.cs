using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Helper.Extention
{
    public static class ExtentionMethods
    {
        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }
    }
}