namespace BlackDigital.Blazor.DataBuilder
{
    public class ComponentType
    {
        public ComponentType(Type type, Dictionary<string, object>? attributes) 
        { 
            Type = type;
            Attributes = attributes;
        }

        public Type Type { get; set; }

        public Dictionary<string, object>? Attributes { get; set; }
    }
}
