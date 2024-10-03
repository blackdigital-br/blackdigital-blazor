
using Microsoft.AspNetCore.Components;

namespace BlackDigital.Blazor.Components.Fields
{
    public partial class TimeField
    {
        public override object? GetValue()
        {
            var value = base.GetValue();
            var type = GetPropertyType();

            if (type == typeof(TimeOnly)
                && value is TimeOnly)
            {
                return ((TimeOnly)value).ToString("HH:mm:ss");
            }

            return value;
        }

        protected override void OnInput(ChangeEventArgs args)
        {
            var type = GetPropertyType();

            if (type == typeof(TimeOnly))
            {
                if (TimeOnly.TryParseExact(args.Value.ToString(), "HH:mm:ss", out TimeOnly timeOnly))
                    SetValue(timeOnly);
            }
        }
    }
}
