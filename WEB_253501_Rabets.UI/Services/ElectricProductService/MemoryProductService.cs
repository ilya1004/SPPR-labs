using Microsoft.AspNetCore.Mvc;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Services.CategoryService;

namespace WEB_253501_Rabets.UI.Services.ElectricProductService;

public class MemoryProductService : IProductService
{
    private readonly IConfiguration _configuration;
    List<ElectricProduct> products;
    List<Category> categories;
    public MemoryProductService([FromServices]IConfiguration configuration, ICategoryService categoryService)
    {
        _configuration = configuration;
        categories = categoryService.GetCategoryListAsync().Result.Data!;

        SetUpData();
    }

    private void SetUpData()
    {
        products = new List<ElectricProduct>
        {
            new ElectricProduct {Id = 1, Name = "Шуруповерт Makita DF333DWYE", Description = "Профессиональная компактная и легкая дрель-шуруповерт, напряжение-12В, аккум.Li-ion емкость 1,5Ач, патрон бесключевой-10мм, скорость вращения 1700об/мин, крутящий момент на 1-й ск.-30Нм.", 
                Price = 467.00M, ImagePath = "8026511.282413-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("screwdrivers"))! },
            new ElectricProduct {Id = 2, Name = "Дрель-шуруповерт Werker EWCDL 814", Description = "Аккумуляторная профессиональная дрель-шуруповерт WERKER EWCDL814 предназначена для работы с различными видами крепежа и просверливания отверстий. Развивает от 0-350/1200 оборотов в минуту. Для работы с материалами разной твердости предусмотрены 2 скорости.",
                Price = 159.90M, ImagePath = "3404241.244685-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("screwdrivers"))! },
            new ElectricProduct {Id = 3, Name = "Перфоратор Makita HR 2470", Description = "Makita HR 2470 - профессиональный 3-режимный перфоратор. Мощность - 780 Вт, масса - 2,8 кг. Плавное изменение скорости вращения на модели производится через кнопку включения с электронной регулировкой числа оборотов. Имеется возможность установить плоское долото в одно из 40 положений.",
                Price = 603.25M, ImagePath = "6701911.149453-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("perforators"))! },
            new ElectricProduct {Id = 4, Name = "Перфоратор DEKO DKH850W", Description = "Перфоратор Deko DKH850W функционирует в трех режимах: сверление, ударное сверление, долбление. Переключение между ними осуществляется при помощи тумблера сбоку на корпусе. Патрон SDS-plus обеспечивает быструю замену оснастки. Дополнительная рукоять способствует уверенному удержанию инструмента в руках.",
                Price = 176.55M, ImagePath = "4540811.370152-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("perforators"))! },
            new ElectricProduct {Id = 5, Name = "Дисковая (циркулярная) пила Werker CS 185 L", Description = "Электрическая дисковая пила WERKER CS 185 L подходит для бытового применения. Предназначена для распила древесины в мастерских или на даче. Пила оснащена двигателем с потребляемой мощностью 1500 Вт и развивает скорость до 4700 оборотов в минуту.",
                Price = 264.20M, ImagePath = "0813791.264608-medium.png", Category = categories.Find(c => c.NormalizedName.Equals("circular_saws"))! },
            new ElectricProduct {Id = 6, Name = "Дисковая (циркулярная) пила Makita MT M5802", Description = "Профессиональная дисковая пила спроектирована для обеспечения максимального удобства оператора. Эргономичная прорезиненая рукоятка позволяет уменьшить нагрузку на руки, а порт удоления пыли отводит опилки и крошки от оператора. Помимо этого есть возможность подключения пылесоса.",
                Price = 369.50M, ImagePath = "1493751.289707-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("circular_saws"))! },
            new ElectricProduct {Id = 7, Name = "Дрель безударная Makita 6413", Description = "Легкая и компактная безударная дрель Makita 6413 мощностью 450 Вт оптимально рассчитана для сверления небольших отверстий в металлах, пластиках, древесных материалах. Быстрозажимной патрон 10 мм позволяет быстро сменить рабочие насадки. Дрель оснащена функцией реверса для вывинчивания шурупов.",
                Price = 250.00M, ImagePath = "4765301.191084-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("drills"))! },
            new ElectricProduct {Id = 8, Name = "Дрель ударная Makita HP2051F", Description = "Ударная сетевая дрель Makita HP 2051F предназначена для профессионального использования. Max диаметр сверления в дереве 40 мм, в стали 13 мм. Разгоняется до 2900 об/мин и 58000 уд/мин. Переключение скоростей регулируется 2-мя положениями.",
                Price = 697.44M, ImagePath = "8138851.124445-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("drills"))! },
            new ElectricProduct {Id = 9, Name = "Гайковерт акк. DEKO DKIS20", Description = "Гайковерт аккумуляторный бесщеточный ;DEKO ;DKIS20 предназначен для закручивания/ослабления болтов и гаек, сборки деревянных конструкций, сантехнических и общестроительных работ. Этот гайковёрт имеет 3 режима/скорости для затягивания болтов и 2 режима для ослабления болтов.",
                Price = 319.90M, ImagePath = "7562241.370172-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("wrenches"))! },
            new ElectricProduct {Id = 10, Name = "Гайковерт акк. Einhell TE-CI 12 Li", Description = "Аккумуляторный ударный винтоверт Einhell TE-CI 12 Li (1x2.0Ah) способен быстро закручивать и выкручивать саморезы. Специальный ударный механизм уменьшает нагрузку на запястья. С помощью винтоверта завинчивание длинных и больших крепежных изделий превращается в легкую работу.",
                Price = 338.00M, ImagePath = "3606701.342379-medium.png", Category = categories.Find(c => c.NormalizedName.Equals("wrenches"))! }
        };
    }

    public Task<ResponseData<ElectricProduct>> CreateProductAsync(ElectricProduct product, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<ElectricProduct>> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<ProductListModel<ElectricProduct>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        var itemsPerPage = Convert.ToInt32(_configuration.GetRequiredSection("ItemsPerPage").Value);

        var items = products.Where(p => categoryNormalizedName == null || p.Category.NormalizedName.Equals(categoryNormalizedName)).ToList();

        var itemsCount = items.Count;

        items = items.Skip(itemsPerPage * (pageNo - 1)).Take(itemsPerPage).ToList();

        var totalPages = (int)Math.Floor((double)((categoryNormalizedName == null ? itemsCount : items.Count) / itemsPerPage));

        totalPages += ((categoryNormalizedName == null ? itemsCount : items.Count) % itemsPerPage == 0) ? 0 : 1;

        return Task.FromResult(new ResponseData<ProductListModel<ElectricProduct>>
        {
            Data = new ProductListModel<ElectricProduct>
            {
                Items = items,
                CurrentPage = pageNo,
                TotalPages = totalPages,
            }
        });

    }

    public Task UpdateProductAsync(int id, ElectricProduct product, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
