using BlackDigital.DataBuilder;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System.Reflection;

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
        public RenderFragment<TModel> CustomForm { get; set; }

        [Parameter]
        public EventCallback<EditContext> OnSubmit { get; set; }

        [Parameter]
        public EventCallback<EditContext> OnValidSubmit { get; set; }

        [Parameter]
        public EventCallback<EditContext> OnInvalidSubmit { get; set; }

        [Parameter]
        public Expression<Func<TModel, object>>? IgnoreProperties { get; set; }

        [Parameter]
        public Expression<Func<TModel, object>>? OnlyProperties { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        public TypeBuilder TypeBuilder { get; private set; }

        private IEnumerable<PropertyInfo> GetPropertiesIgnored()
            => IgnoreProperties?.GetPropertiesInfoFromExpression() ?? [];

        private IEnumerable<PropertyInfo> GetPropertiesOnly()
            => OnlyProperties?.GetPropertiesInfoFromExpression() ?? [];

        private bool ShowProperty(PropertyBuilder property)
        {
            var onlyProperties = GetPropertiesOnly().ToList();

            if (onlyProperties != null && onlyProperties.Any())
                return onlyProperties.Any(p => p.Name == property.PropertyName);

            var ignoredProperties = GetPropertiesIgnored().ToList();

            return property.Show(Model) && !(ignoredProperties?.Any(p => p.Name == property.PropertyName) ?? false);
        }
    }
}
