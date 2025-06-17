using System.Linq.Expressions;
using System.Reflection.Emit;

namespace BlackDigital.Blazor.Components
{
    public partial class FieldContainer
    {
        [CascadingParameter]
        public PropertyBuilder Property { get; set; }

        [Parameter]
        public RenderFragment<ElementId> ChildContent { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Description { get; set; }

        [Parameter]
        public Expression<Func<object>>? For { get; set; }

        [Parameter]
        public string ContainerClass { get; set; }

        [Parameter]
        public string LabelClass { get; set; }

        [Parameter]
        public string DescriptionClass { get; set; }

        private ElementId FieldId { get; set; } = new ElementId();
    }
}
