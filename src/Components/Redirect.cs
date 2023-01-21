using Microsoft.AspNetCore.Components;

namespace BlackDigital.Blazor.Components
{
    public class Redirect : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Url { get; set; } = "/";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Navigation.NavigateTo(Url);
        }
    }
}
