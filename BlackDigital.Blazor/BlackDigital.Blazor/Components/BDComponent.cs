namespace BlackDigital.Blazor.Components
{
    public abstract class BDComponent : ComponentBase
    {
        [CascadingParameter]
        public IStringLocalizer StringLocalizer { get; set; }

        protected string Localize(string? message, params object[] arguments)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                    return string.Empty;

                if (StringLocalizer != null)
                    return StringLocalizer[message, arguments];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return string.Format(message, arguments);
        }
    }
}
