using WEB_253501_Rabets.Domain.Entities;

namespace WEB_253501_Rabets.Domain.Models;

public class Cart
{

    /// <summary>
    /// Список объектов в корзине
    /// key - идентификатор объекта
    /// </summary>
    public Dictionary<int, CartItem> CartItems { get; set; } = [];

    /// <summary>
    /// Добавить объект в корзину
    /// </summary>
    /// <param name="product">Добавляемый объект</param>
    public virtual void AddToCart(ElectricProduct product)
    {
        if (product == null) return;

        // Если товар уже в корзине, увеличиваем количество
        if (CartItems.TryGetValue(product.Id, out CartItem? value))
        {
            value.Count++;
        }
        else
        {
            CartItems[product.Id] = new CartItem { Product = product, Count = 1 };
        }
    }

    /// <summary>
    /// Удалить объект из корзины
    /// </summary>
    /// <param name="id"> id удаляемого объекта</param>
    public virtual void RemoveItem(int id)
    {
        CartItems.Remove(id);
    }

    ///<summary>
    /// Очистить корзину
    /// </summary>
    public virtual void ClearAll()
    {
        CartItems.Clear();
    }

    ///<summary>
    /// Количество объектов в корзине
    ///</summary>
    public int Count 
    { 
        get => CartItems.Sum(item => item.Value.Count); 
    }

    ///// <summary>
    ///// Общее количество калорий
    ///// </summary>
    //public double TotalCalories
    //{
    //    get => CartItems.Sum(item => item.Value.Item.Calories * item.Value.Count);
    //}
}
