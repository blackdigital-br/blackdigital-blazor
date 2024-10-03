using BlackDigital.Blazor.Components.Fields;


namespace BlackDigital.Blazor.DataBuilder
{
    public class ComponentDataBuilder
    {
        public ComponentDataBuilder() 
        {
            Components = new();

            Add("Text", typeof(TextField), new() { { "type", "text" } });
            Add("Password", typeof(TextField), new() { { "type", "password" } });
            Add("Integer", typeof(TextField), new() { { "type", "number" } });
            Add("UnsignedInteger", typeof(TextField), new() { { "type", "number" } });
            Add("Decimal", typeof(TextField), new() { { "type", "number" } });
            Add("DateTime", typeof(TextField), new() { { "type", "datetime-local" } });
            Add("Date", typeof(DateField));
            Add("Time", typeof(TimeField));
            Add("Boolean", typeof(BooleanField));
        }

        private Dictionary<string, ComponentType> Components;

        public ComponentDataBuilder Add(string name, Type component, Dictionary<string, object> attributes = null)
        {
            Remove(name);

            Components.Add(name, new(component, attributes));
            return this;
        }

        public ComponentDataBuilder Remove(string name)
        {
            if (Components.ContainsKey(name))
                Components.Remove(name);

            return this;
        }


        public Type GetType(string name)
        {
            if (Components.TryGetValue(name, out ComponentType componentType))
                return componentType.Type;

            return typeof(TextField);
        }

        public Dictionary<string, object>? GetAttributes(string name)
        {
            if (Components.TryGetValue(name, out ComponentType componentType))
                return componentType.Attributes;

            return null;
        }

        public static ComponentDataBuilder New()
        {
            return new ComponentDataBuilder();
        }
    }
}
