using Microsoft.Extensions.Options;
using WEB_253501_Rabets.Domain.Entities;

namespace WEB_253501_Rabets.API.Data;

public class DbInitializer
{
    public static async Task SeedData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var categories = new List<Category>
        {
            new Category {Name = "Шуруповерты", NormalizedName = "screwdrivers"},
            new Category {Name = "Перфораторы", NormalizedName = "perforators"},
            new Category {Name = "Циркулярные пилы", NormalizedName = "circular_saws"},
            new Category {Name = "Дрели", NormalizedName = "drills"},
            new Category {Name = "Гайковерты", NormalizedName = "wrenches"},
        };

        await context.AddRangeAsync(categories);

        var url = app.Configuration.GetSection("ImagesUrl");

        var products = new List<ElectricProduct>
        {
            new ElectricProduct {Name = "Шуруповерт Makita DF333DWYE", Description = "Профессиональная компактная и легкая дрель-шуруповерт, напряжение-12В, аккум.Li-ion емкость 1,5Ач, патрон бесключевой-10мм, скорость вращения 1700об/мин, крутящий момент на 1-й ск.-30Нм.",
                Price = 467.00M, ImagePath = url + "8026511.282413-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("screwdrivers"))! },
            new ElectricProduct {Name = "Дрель-шуруповерт Werker EWCDL 814", Description = "Аккумуляторная профессиональная дрель-шуруповерт WERKER EWCDL814 предназначена для работы с различными видами крепежа и просверливания отверстий. Развивает от 0-350/1200 оборотов в минуту. Для работы с материалами разной твердости предусмотрены 2 скорости.",
                Price = 159.90M, ImagePath = url + "3404241.244685-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("screwdrivers"))! },
            new ElectricProduct {Name = "Перфоратор Makita HR 2470", Description = "Makita HR 2470 - профессиональный 3-режимный перфоратор. Мощность - 780 Вт, масса - 2,8 кг. Плавное изменение скорости вращения на модели производится через кнопку включения с электронной регулировкой числа оборотов. Имеется возможность установить плоское долото в одно из 40 положений.",
                Price = 603.25M, ImagePath = url + "6701911.149453-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("perforators"))! },
            new ElectricProduct {Name = "Перфоратор DEKO DKH850W", Description = "Перфоратор Deko DKH850W функционирует в трех режимах: сверление, ударное сверление, долбление. Переключение между ними осуществляется при помощи тумблера сбоку на корпусе. Патрон SDS-plus обеспечивает быструю замену оснастки. Дополнительная рукоять способствует уверенному удержанию инструмента в руках.",
                Price = 176.55M, ImagePath = url + "4540811.370152-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("perforators"))! },
            new ElectricProduct {Name = "Дисковая (циркулярная) пила Werker CS 185 L", Description = "Электрическая дисковая пила WERKER CS 185 L подходит для бытового применения. Предназначена для распила древесины в мастерских или на даче. Пила оснащена двигателем с потребляемой мощностью 1500 Вт и развивает скорость до 4700 оборотов в минуту.",
                Price = 264.20M, ImagePath = url + "0813791.264608-medium.png", Category = categories.Find(c => c.NormalizedName.Equals("circular_saws"))! },
            new ElectricProduct {Name = "Дисковая (циркулярная) пила Makita MT M5802", Description = "Профессиональная дисковая пила спроектирована для обеспечения максимального удобства оператора. Эргономичная прорезиненая рукоятка позволяет уменьшить нагрузку на руки, а порт удоления пыли отводит опилки и крошки от оператора. Помимо этого есть возможность подключения пылесоса.",
                Price = 369.50M, ImagePath = url + "1493751.289707-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("circular_saws"))! },
            new ElectricProduct {Name = "Дрель безударная Makita 6413", Description = "Легкая и компактная безударная дрель Makita 6413 мощностью 450 Вт оптимально рассчитана для сверления небольших отверстий в металлах, пластиках, древесных материалах. Быстрозажимной патрон 10 мм позволяет быстро сменить рабочие насадки. Дрель оснащена функцией реверса для вывинчивания шурупов.",
                Price = 250.00M, ImagePath = url + "4765301.191084-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("drills"))! },
            new ElectricProduct {Name = "Дрель ударная Makita HP2051F", Description = "Ударная сетевая дрель Makita HP 2051F предназначена для профессионального использования. Max диаметр сверления в дереве 40 мм, в стали 13 мм. Разгоняется до 2900 об/мин и 58000 уд/мин. Переключение скоростей регулируется 2-мя положениями.",
                Price = 697.44M, ImagePath = url + "8138851.124445-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("drills"))! },
            new ElectricProduct {Name = "Гайковерт акк. DEKO DKIS20", Description = "Гайковерт аккумуляторный бесщеточный ;DEKO ;DKIS20 предназначен для закручивания/ослабления болтов и гаек, сборки деревянных конструкций, сантехнических и общестроительных работ. Этот гайковёрт имеет 3 режима/скорости для затягивания болтов и 2 режима для ослабления болтов.",
                Price = 319.90M, ImagePath = url + "7562241.370172-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("wrenches"))! },
            new ElectricProduct {Name = "Гайковерт акк. Einhell TE-CI 12 Li", Description = "Аккумуляторный ударный винтоверт Einhell TE-CI 12 Li (1x2.0Ah) способен быстро закручивать и выкручивать саморезы. Специальный ударный механизм уменьшает нагрузку на запястья. С помощью винтоверта завинчивание длинных и больших крепежных изделий превращается в легкую работу.",
                Price = 338.00M, ImagePath = url + "3606701.342379-medium.png", Category = categories.Find(c => c.NormalizedName.Equals("wrenches"))! }
        };

        await context.AddRangeAsync(products);

        await context.SaveChangesAsync();
    }
}
