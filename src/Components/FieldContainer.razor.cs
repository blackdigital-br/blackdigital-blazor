using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace BlackDigital.Blazor.Components
{
    public partial class FieldContainer
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Description { get; set; }

        [Parameter]
        public Expression<Func<object>>? For { get; set; }
    }
}
