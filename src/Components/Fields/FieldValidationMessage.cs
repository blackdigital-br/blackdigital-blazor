using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlackDigital.Blazor.Components.Fields
{
    public class FieldValidationMessage : BaseCustomField, IDisposable
    {
        public FieldValidationMessage()
        {
            _validationStateChangedHandler = (sender, eventArgs) => StateHasChanged();
        }

        private EditContext _previousEditContext;
        private FieldIdentifier _fieldIdentifier;
        private readonly EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;

        [CascadingParameter]
        protected EditContext CurrentEditContext { get; set; } = default!;

        protected override void OnParametersSet()
        {
            try
            {
                if (CurrentEditContext == null)
                {
                    throw new InvalidOperationException($"{GetType()} requires a cascading parameter " +
                        $"of type {nameof(EditContext)}. For example, you can use {GetType()} inside " +
                        $"an {nameof(EditForm)}.");
                }

                _fieldIdentifier = new(Model, PropertyBuilder.PropertyName);

                if (CurrentEditContext != _previousEditContext)
                {
                    DetachValidationStateChangedListener();
                    CurrentEditContext.OnValidationStateChanged += _validationStateChangedHandler;
                    _previousEditContext = CurrentEditContext;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            foreach (var message in CurrentEditContext.GetValidationMessages(_fieldIdentifier))
            {
                List<string> messagesFormatted = new();
                var messages = message.Split(';');
                var currentMessage = Localize(messages[0]);

                var fieldName = _fieldIdentifier.FieldName;
                var display = PropertyBuilder.DisplayAttribute;

                if (display != null)
                    fieldName = display.Name;

                messagesFormatted.Add(Localize(fieldName));
                for (int i = 1; i < messages.Length; i++)
                {
                    messagesFormatted.Add(Localize(messages[i]));
                }

                builder.OpenElement(0, "div");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "class", "validation-message");
                builder.AddContent(3, string.Format(currentMessage, messagesFormatted.ToArray()));
                builder.CloseElement();
            }
        }

        void IDisposable.Dispose()
        {
            DetachValidationStateChangedListener();
        }

        private void DetachValidationStateChangedListener()
        {
            if (_previousEditContext != null)
            {
                _previousEditContext.OnValidationStateChanged -= _validationStateChangedHandler;
            }
        }
    }
}
