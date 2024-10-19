using WEB_253501_Rabets.BlazorWasm.Entities;
using WEB_253501_Rabets.BlazorWasm.Models;

namespace WEB_253501_Rabets.BlazorWasm.Services.DataService;

public interface IDataService
{
    // Событие, генерируемое при изменении данных
    event Action DataLoaded;

    // Список категорий объектов
    List<Category> Categories { get; set; }

    //Список объектов
    List<ElectricProduct> Products { get; set; }

    // Признак успешного ответа на запрос к Api
    bool Success { get; set; }

    // Сообщение об ошибке
    string ErrorMessage { get; set; }

    // Количество страниц списка
    int TotalPages { get; set; }

    // Номер текущей страницы
    int CurrentPage { get; set; }

    // Фильтр по категории
    Category? SelectedCategory { get; set; }

    /// <summary>
    /// Получение списка всех объектов
    /// </summary>
    /// <param name="categoryName">
    /// Название категории товаров
    /// <param name="pageNo">
    /// Номер страницы списка
    /// </param> 
    /// <returns></returns> 
    public Task<ResponseData<ProductListModel<ElectricProduct>>> GetProductListAsync();

    /// <summary> 
    /// Получение списка категорий 
    /// </summary> 
    /// <returns></returns> 
    public Task<ResponseData<List<Category>>> GetCategoryListAsync();
}
