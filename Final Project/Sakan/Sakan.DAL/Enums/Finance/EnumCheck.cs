using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Enums.Finance
{
    internal  class EnumCheck
    {
        public static string In<TEnum>(string column) where TEnum : struct, Enum =>
            $"[{column}] IN ({string.Join(",", Enum.GetNames<TEnum>().Select(n => $"'{n}'"))})";
    }
}
