using MapaAndMaya.Services.Models;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Areas.Mapa.Pages;

public partial class CumFums
{
    private bool _isLoading;
    
    private RadzenDataGrid<CumFum> _grid = new();
    
    private ICollection<CumFum> CumFumCollection { get; set; } = new List<CumFum>();
    
    private IList<CumFum>? SelectedCumFums { get; set; } = new List<CumFum>();

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        CumFumCollection = await CumFumService.FindAll();
        _isLoading = false;

        await base.OnInitializedAsync();
    }

    private async void AddItem(){}
    
    private async void AddFormSubmit(){}
    
    private async void EditItem(CumFum item){}
    
    private async void EditFormSubmit(){}
    private async void DeleteItems(){}
    
    private async void NotifyOk(string title){}
    
    private  void NotifyErrors( string title , List<string> errors){}
    
}