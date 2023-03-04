

namespace BlackDigital.Blazor.IndexedDB
{
    public class DBIndexBuilder
    {
        internal DBIndexBuilder(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public bool Unique { get; set; }

        public bool MultiEntry { get; set; }

        public DBIndexBuilder IsUnique()
        {
            Unique = true;
            return this;
        }

        public DBIndexBuilder IsMultiEntry()
        {
            MultiEntry = true;
            return this;
        }
    }
}
