using System;
using System.ComponentModel.DataAnnotations;

namespace MatchScore.Helpers
{
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute() : base(typeof(DateTime),
           DateTime.Now.ToShortDateString(),
           DateTime.Now.AddYears(5).ToShortDateString())
        { }
    }
}
