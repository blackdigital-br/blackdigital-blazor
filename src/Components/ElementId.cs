using BlackDigital.Model;

namespace BlackDigital.Blazor.Components
{
    public struct ElementId
    {
        public ElementId()
        {
            Id = Guid.NewGuid();
        }

        public ElementId(Id id)
        {
            Id = id;
        }

        public Id Id { get; set; }
    }
}
