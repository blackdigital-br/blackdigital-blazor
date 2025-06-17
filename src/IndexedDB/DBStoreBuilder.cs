
namespace BlackDigital.Blazor.IndexedDB
{
    public class DBStoreBuilder
    {
        internal DBStoreBuilder(Type type)
        {
            Name = type.Name;
            Indexes = new List<DBIndexBuilder>();
        }

        public string Name { get; set; }

        public string? KeyPath { get; set; }

        public List<DBIndexBuilder> Indexes { get; set; }

        public DBStoreBuilder AddKeyPath(string name)
        {
            this.KeyPath = name;
            return this;
        }

        public DBIndexBuilder AddIndex(string name, string? keyPath = null)
        {
            var builder = new DBIndexBuilder(name, keyPath ?? name);
            Indexes.Add(builder);

            return builder;
        }

        public DBStoreBuilder WithKeyPath(string keyPath)
        {
            KeyPath = keyPath;
            return this;
        }
    }
}
