using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jochum.SocialVillageSimulator.Parsers
{
    public static class ParserHelper
    {
        public static TEnum GetEnumValue<TEnum>(string value) where TEnum : struct
        {
            TEnum parsedType;
            var successful = Enum.TryParse(value, true, out parsedType);
            if (!successful)
            {
                throw new ArgumentException($"Cannot parse {value}");
            }

            return parsedType;
        }
    }
}
