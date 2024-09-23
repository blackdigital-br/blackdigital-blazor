using System.Linq.Expressions;

namespace BlackDigital.Blazor.Components
{
    public partial class FieldDescription<T>
    {
        [CascadingParameter]
        public ElementId FieldId { get; set; }

        [Parameter]
        public Expression<Func<T>> For { get; set; }

        [Parameter]
        public string? Description { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        private string GetDescription()
        {
            if (!string.IsNullOrWhiteSpace(Description))
                return Description;

            return DisplayHelper.GetDescription(For) ?? string.Empty;
        }
    }
}
