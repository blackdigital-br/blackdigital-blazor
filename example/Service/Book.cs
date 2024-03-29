﻿namespace Example.Service
{
    public class Author
    {
        public string key { get; set; }
    }

    public class Created
    {
        public string type { get; set; }
        public DateTime value { get; set; }
    }

    public class FirstSentence
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Identifiers
    {
        public List<string> goodreads { get; set; }
        public List<string> librarything { get; set; }
        public List<string> amazon { get; set; }
    }

    public class Language
    {
        public string key { get; set; }
    }

    public class LastModified
    {
        public string type { get; set; }
        public DateTime value { get; set; }
    }

    public class Book
    {
        public List<string> publishers { get; set; }
        public int number_of_pages { get; set; }
        public List<string> isbn_10 { get; set; }
        public List<int> covers { get; set; }
        public string key { get; set; }
        public List<Author> authors { get; set; }
        public string ocaid { get; set; }
        public List<string> contributions { get; set; }
        public List<Language> languages { get; set; }
        public List<string> source_records { get; set; }
        public string title { get; set; }
        public Identifiers identifiers { get; set; }
        public List<string> isbn_13 { get; set; }
        public List<string> local_id { get; set; }
        public string publish_date { get; set; }
        public List<Work> works { get; set; }
        public Type type { get; set; }
        public FirstSentence first_sentence { get; set; }
        public int latest_revision { get; set; }
        public int revision { get; set; }
        public Created created { get; set; }
        public LastModified last_modified { get; set; }
    }

    public class Type
    {
        public string key { get; set; }
    }

    public class Work
    {
        public string key { get; set; }
    }


}
