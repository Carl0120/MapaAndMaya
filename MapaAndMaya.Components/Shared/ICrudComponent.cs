using MapaAndMaya.Services.ViewModels;
using Radzen.Blazor;

namespace MapaAndMaya.Components.Shared;

public interface ICrudComponent<TE>
{
    protected bool _isLoading { get; set; }

    protected RadzenDataGrid<TE> _grid { get; set; }

    protected IEnumerable<TE> ItemsCollection { get; set; }

    protected IList<TE>? SelectedItems { get; set; }

    protected GenericViewModel ViewModel { get; set; }

    void AddItem();

    void AddFormSubmit();

    void EditItem(TE item);

    void EditFormSubmit();

    void DeleteItems();

    void ReloadGridButton();

    void NotifyOk(string title);

    void NotifyErrors(string title, List<string> errors);
}