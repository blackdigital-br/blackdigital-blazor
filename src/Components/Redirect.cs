namespace BlackDigital.Blazor.Components
{
    public class Redirect : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Url { get; set; } = "/";

        [Parameter]
        public bool ForceLoad { get; set; } = false;

        [Parameter]
        public bool Replace { get; set; } = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Navigation.NavigateTo(Url, ForceLoad, Replace);
        }
    }
}
