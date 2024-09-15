namespace WEB_253501_Rabets.Domain.Models;

public class ProductListModel<T>
{
    // запрошенный список объектов
    public List<T> Items { get; set; } = new();
    // номер текущей страницы
    public int CurrentPage { get; set; } = 1;
    // общее количество страниц
    public int TotalPages { get; set; } = 1;
}
