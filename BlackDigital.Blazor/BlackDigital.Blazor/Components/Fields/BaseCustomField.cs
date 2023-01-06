using BlackDigital.DataBuilder;

namespace BlackDigital.Blazor.Components.Fields
{
    public abstract class BaseCustomField : BDComponent
    {
        [CascadingParameter]
        public TypeBuilder TypeBuilder { get; set; }

        [CascadingParameter]
        public PropertyBuilder PropertyBuilder { get; set; }

        [CascadingParameter(Name = "Model")]
        public object Model { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        public virtual object? GetValue()
        {
            object? value = PropertyBuilder?.GetValue(Model);

            return value;
        }

        public virtual void SetValue(object? value) => PropertyBuilder?.SetValue(Model, value);

        protected virtual void OnInput(ChangeEventArgs args)
        {
            try
            {
                var type = Nullable.GetUnderlyingType(PropertyBuilder.PropertyType);

                if (type == null)
                    type = PropertyBuilder.PropertyType;

                object? value = Convert.ChangeType(args.Value, type);
                SetValue(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                /*if (PropertyBuilder.PropertyType.IsValueType)
                    SetValue(Activator.CreateInstance(PropertyBuilder.PropertyType));
                else
                    SetValue(null);*/
            }
        }

        protected Type GetPropertyType()
        {
            var type = Nullable.GetUnderlyingType(PropertyBuilder.PropertyType);

            if (type == null)
                type = PropertyBuilder.PropertyType;

            return type;
        }
    }
}
