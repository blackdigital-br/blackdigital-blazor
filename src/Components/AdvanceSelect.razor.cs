using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BlackDigital.Blazor.Components
{
    public partial class AdvanceSelect<TModel>
    {
        [Parameter]
        public List<TModel> Options { get; set; }

        [Parameter]
        public RenderFragment<TModel> ChildContent { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public bool IsMultiple { get; set; }

        [Parameter]
        public TModel? Value { get; set; }

        [Parameter]
        public bool Search { get; set; } = false;

        [Parameter]
        public bool IncludeBlank { get; set; } = false;

        [Parameter]
        public List<TModel> Values { get; set; } = new();

        [Parameter]
        public EventCallback<TModel?> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<List<TModel>> ValuesChanged { get; set; }

        [Parameter]
        public string Filter { get; set; } = "";

        [Parameter]
        public bool ReadOnly { get; set; } = false;

        [Parameter]
        public bool Card { get; set; } = false;

        protected string EmptyPlaceholder
        {
            get
            {
                return Localize(Placeholder ?? "Select an item");
            }
        }

        protected object MultipleItensPlaceholder
        {
            get
            {
                if (Values.Count == 1)
                    return GetChildContent(Values.First());

                return Localize("{0} and {1} others itens", GetChildContent(Values.First()), Values.Count);
            }
        }

        private bool Show { get; set; }

        private int? LastIndexClick = null;

        private bool AllSelected => Values?.Count == Options?.Count();

        private void OnOpenCloseSelect(MouseEventArgs args)
        {
            if (!ReadOnly)
                Show = !Show;
        }

        private async Task OnLostFocus(FocusEventArgs args)
        {
            await Task.Delay(150);

            if (!IsMultiple)
                Show = false;
        }

        private async void OnSelected(MouseEventArgs mouseEvent, TModel? key)
        {
            if (IsMultiple && key != null)
            {
                if ((mouseEvent?.ShiftKey ?? false)
                    && LastIndexClick.HasValue)
                {
                    bool insert = !Values.Contains(key);
                    int currentIndex = Options.IndexOf(key);
                    int startIndex = Math.Min(LastIndexClick.Value, currentIndex);
                    int endIndex = Math.Max(LastIndexClick.Value, currentIndex);

                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        var option = Options.ElementAt(i);
                        if (insert && !Values.Contains(option))
                            Values?.Add(option);
                        else if (!insert && Values.Contains(option))
                            Values?.Remove(option);

                        await ValuesChanged.InvokeAsync(Values);
                    }

                    LastIndexClick = default;
                }
                else
                {
                    LastIndexClick = Options.IndexOf(Options.FirstOrDefault(x => Equals(x, key)));

                    if (Values?.Contains(key) ?? false)
                        RemoveValue(key);
                    else
                        AddValue(key);
                }
            }
            else
            {
                Show = !Show;
                SetValue(key);
            }
        }

        private string GetClassSelect(TModel key)
        {
            if (!IsMultiple)
                return "";

            return Values?.Contains(key) ?? false ? "selected" : "no-selected";
        }

        private async void SelectAll()
        {
            if (AllSelected)
            {
                Values.Clear();
            }
            else
            {
                Values.Clear();
                Values.AddRange(Options.ToArray());
            }

            await ValuesChanged.InvokeAsync(Values);
        }

        protected async void AddValue(TModel value)
        {
            if (Values != null)
            {
                Values.Add(value);
                await ValuesChanged.InvokeAsync(Values);
            }
        }

        protected async void RemoveValue(TModel value)
        {
            if (Values != null)
            {
                Values.Remove(value);
                await ValuesChanged.InvokeAsync(Values);
            }
        }

        protected async void SetValue(TModel? value)
        {
            if (!Equals(Value, value))
            {
                Value = value;
                await ValueChanged.InvokeAsync(value);
            }
        }

        protected object GetChildContent(TModel? value)
        {
            if (ChildContent == null)
            {
                return value;
            }
            else
            {
                return ChildContent(value);
            }
        }

        
    }
}
