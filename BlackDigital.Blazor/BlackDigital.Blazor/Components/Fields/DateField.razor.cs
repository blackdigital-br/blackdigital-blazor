using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Blazor.Components.Fields
{
    public partial class DateField
    {
        public override object? GetValue()
        {
            var value = base.GetValue();
            var type = GetPropertyType();

            if (type == typeof(DateOnly)
                && value is DateOnly)
            {
                return ((DateOnly)value).ToString("yyyy-MM-dd");
            }

            return value;
        }

        protected override void OnInput(ChangeEventArgs args)
        {
            var type = GetPropertyType();

            if (type == typeof(DateOnly)) 
            {
                if (DateOnly.TryParseExact(args.Value.ToString(), "yyyy-MM-dd", out DateOnly dateOnly))
                    SetValue(dateOnly);
            }
        }
    }
}
