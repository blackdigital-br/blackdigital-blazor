using BlackDigital.DataBuilder;
using Microsoft.AspNetCore.Components.Forms;

namespace BlackDigital.Blazor.Components
{
    public partial class ModelForm<TModel>
    {
        public ModelForm()
            : base()
        {
            TypeBuilder = TypeBuilder.Create<TModel>();
        }

        [Parameter]
        public TModel Model { get; set; }

        [Parameter]
        public RenderFragment Loading { get; set; }

        [Parameter]
        public RenderFragment<TModel> Header { get; set; }

        [Parameter]
        public RenderFragment<TModel> Footer { get; set; }

        [Parameter]
        public EventCallback<EditContext> OnSubmit { get; set; }

        [Parameter]
        public EventCallback<EditContext> OnValidSubmit { get; set; }

        [Parameter]
        public EventCallback<EditContext> OnInvalidSubmit { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        public TypeBuilder TypeBuilder { get; private set; }

    }
}
