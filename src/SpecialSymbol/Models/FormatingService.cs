using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecialSymbol.Models
{
    public class FormatingService
    {
        public string AsReadableDate(DateTime datetime)
        {
            return datetime.ToString("m");
        }
    }
}
