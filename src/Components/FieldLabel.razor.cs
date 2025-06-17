using BlackDigital.DataBuilder;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;

namespace BlackDigital.Blazor.Components
{
    public partial class FieldLabel<T>
    {
        [CascadingParameter]
        public ElementId FieldId { get; set; }

        [Parameter]
        public Expression<Func<T>> For { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        private string GetDisplayName()
        {
            if (!string.IsNullOrWhiteSpace(Label))
                return Label;

            if (For == null)
                return string.Empty;

            return DisplayHelper.GetDisplayName(For) ?? string.Empty;
        }
    }
}
