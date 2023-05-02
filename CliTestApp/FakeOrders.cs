using Skyware.Arenal.Model;

namespace CliTestApp;


public static class FakeOrders
{

    public class Product
    {
        public string Code = string.Empty;
        public string Name = string.Empty;
        public string SampleCode = string.Empty;
        public string SampleName = string.Empty;
    }


    #region "Constants"

    public static readonly string[] MALE_NAMES = new string[] {
        "Андрей",
        "Ангел",
        "Антон",
        "Бисер",
        "Борис",
        "Борислав",
        "Божидар",
        "Боян",
        "Валери",
        "Васил",
        "Георги",
        "Дамян",
        "Делян",
        "Добри",
        "Емил",
        "Желязко",
        "Здравко",
        "Иван",
        "Ивайло",
        "Калин",
        "Калоян",
        "Любомир",
        "Манол",
        "Милен",
        "Мирослав",
        "Михаил",
        "Недялко",
        "Николай",
        "Огнян",
        "Петър",
        "Павел",
        "Радослав",
        "Рашко",
        "Ростислав",
        "Румен",
        "Симеон",
        "Стоян",
        "Стефан",
        "Тихомир",
        "Теодор",
        "Христо",
        "Християн",
        "Цветан",
        "Чавдар",
        "Щерьо",
        "Юлиян",
        "Янчо",
    };

    public static readonly string[] FEMALE_NAMES = new string[] {
        "Ана",
        "Ангелина",
        "Антоанета",
        "Биляна",
        "Боряна",
        "Виолета",
        "Габриела",
        "Гергина",
        "Диляна",
        "Дима",
        "Димитрина",
        "Елисавета",
        "Елица",
        "Жасмин",
        "Жанет",
        "Здравка",
        "Зорница",
        "Иванка",
        "Ивайла",
        "Камелия",
        "Калина",
        "Корнелия",
        "Лили",
        "Лиляна",
        "Мария",
        "Милена",
        "Михаела",
        "Недялка",
        "Николета",
        "Оля",
        "Петя",
        "Пламена",
        "Румяна",
        "Росица",
        "Силвия",
        "Таня",
        "Татяна",
        "Теодора",
        "Христина",
        "Цветанка",
        "Юлияна",
        "Яна",
    };

    public static readonly string[] MALE_SURNAMES = new string[] {
        "Андреев",
        "Ангелов",
        "Бисеров",
        "Борисов",
        "Боянов",
        "Василев",
        "Георгиев",
        "Дамянов",
        "Емилов",
        "Желязков",
        "Здравков",
        "Иванов",
        "Ивайлов",
        "Калинов",
        "Калоянов",
        "Любомиров",
        "Миленов",
        "Михайлов",
        "Манолов",
        "Неделчев",
        "Николаев",
        "Огнянов",
        "Петров",
        "Павлов",
        "Попов",
        "Руменов",
        "Рашков",
        "Стоянов",
        "Стефанов",
        "Тихомиров",
        "Тодоров",
        "Христов",
        "Цветанов",
        "Щерев",
        "Янков",
    };

    public static readonly string[] FEMALE_SURNAMES = new string[] {
        "Андреева",
        "Ангелова",
        "Боянова",
        "Василева",
        "Георгиева",
        "Дамянова",
        "Емилова",
        "Желязкова",
        "Здравкова",
        "Иванова",
        "Ивайлова",
        "Калинова",
        "Калоянова",
        "Любомирова",
        "Михайлова",
        "Манолова",
        "Неделчева",
        "Николаева",
        "Огнянова",
        "Петрова",
        "Павлова",
        "Руменова",
        "Рашкова",
        "Стоянова",
        "Стефанова",
        "Тихомирова",
        "Тодорова",
        "Христова",
        "Цветанова",
        "Щерева",
        "Янкова",
    };

    public static readonly Product[] PRODUCTS = new Product[] {
        new Product() {Code = "82477-1", Name = "СУЕ  ( ESR )", SampleCode = "WB", SampleName = "Blood"},
        new Product() {Code = "4548-4", Name = "Гликиран Хемоглобин (Glycated hemoglobin, HbA1c)", SampleCode = "WB", SampleName = "Blood"},
        new Product() {Code = "14749-6", Name = "Глюкоза (Glucose)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "19239-3", Name = "Лактат ( lactate )", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "1963-8", Name = "Бикарбонат (Bicarbonate)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2885-2", Name = "Общ белтък (Total protein)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "1751-7", Name = "Албумин (Albumin)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "13980-8", Name = "Албумин фракция (Albumin/Total Protein)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "13978-2", Name = "Алфа 1 (Alpha 1/Total Protein)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "13981-6", Name = "Алфа 2 (Alpha 2/Total Protein)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "32732-0", Name = "Бета 1 (Beta 1/Total Protein)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "32733-8", Name = "Бета 2 (Beta 2/Total Protein)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "13983-2", Name = "Гама (Gamma/Total protein)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14682-9", Name = "Креатинин (Creatinine)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "22664-7", Name = "Урея (Urea)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14933-6", Name = "Пикочна киселина (Uric acid)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "33863-2", Name = "Цистатин C (Cystatin C)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14631-6", Name = "Билирубин - общ (Total Bilirubin)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14629-0", Name = "Билирубин - директен (Direct Bilirubin)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14647-2", Name = "Холестерол - общ (Total Cholesterol)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14927-8", Name = "Триглицериди (Triglycerides)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "1920-8", Name = "АсАТ (GOT; AST)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "1742-6", Name = "АлАТ (GPT; ALT)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2324-2", Name = "Гама ГТ (GGT)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "6768-6", Name = "Алкална фосфатаза (Alkaline Phosphatase)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "1715-2", Name = "Кисела Фосфатаза (Acid Phosphatase)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "1798-8", Name = "Алфа - амилаза (Amylase)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "3040-3", Name = "Липаза (Lipase)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14798-3", Name = "Желязо (Fe)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "22753-8", Name = "Свободен ЖСК (UIBC)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14800-7", Name = "Тотален ЖСК (TIBC)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14928-6", Name = "FT 3", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14920-3", Name = "FT 4", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14297-6", Name = "TSH", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14866-8", Name = "Паратхормон (PTH)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "8098-6", Name = "TSH рецепторни антитела", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14918-7", Name = "Тиреоглобулин (Tg)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "1992-7", Name = "Калцитонин (Calcitonin)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "57780-9", Name = "TAT (Anti TG)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "32042-4", Name = "MAT (Anti TPO)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "15081-3", Name = "Пролактин (Prolactin)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2579-1", Name = "LH", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2279-1", Name = "FSH", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14715-7", Name = "Естрадиол (Estradiol)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14890-8", Name = "Прогестерон (Progesterone)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14913-8", Name = "Тестостерон (Testosterone)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14688-6", Name = "DHEA-S", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "38476-8", Name = "AMH (анти Мюлеров хормон) II generation", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "19180-9", Name = "free βhCG", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2857-1", Name = "Total PSA", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "19201-3", Name = "Free PSA", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "19166-8", Name = "CEA", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "19177-5", Name = "AFP", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2010-7", Name = "CA-19-9", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2119-6", Name = "Общ Бета ЧХГ", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "10334-1", Name = "CA-125", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "55180-4", Name = "HE4", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2007-3", Name = "CA 15-3", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "2015-6", Name = "CA 72-4", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "19182-5", Name = "Cyfra 21-1", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "9679-2", Name = "SCC", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "47275-3", Name = "Маркер S-100", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "19193-2", Name = "NSE (невронспецифична енолаза)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "14685-2", Name = "Витамин В12", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "13965-9", Name = "Хомоцистеин", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "60493-4", Name = "Витамин D total (25-OH Витамин D)", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "5388-4", Name = "Токсоплазмоза IgG – ELISA", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "47308-2", Name = "Ехинококоза IgG – ELISA", SampleCode = "SER", SampleName = "Serum"},
        new Product() {Code = "6563-1", Name = "Трихинелоза IgG – ELISA", SampleCode = "SER", SampleName = "Serum"},
    };


    #endregion

    public static Order Generate(string placerId, string provierId)
    {
        Random rnd = new();

        int rGender = rnd.Next(0, 100);
        Patient patient = new();
        if (rGender > 50)
        {
            //Male
            patient.GivenName = MALE_NAMES[rnd.Next(0, MALE_NAMES.Length - 1)];
            patient.FamilyName = MALE_SURNAMES[rnd.Next(0, MALE_SURNAMES.Length - 1)];
            if (rnd.Next(0, 5) > 0) patient.MiddleName = MALE_SURNAMES[rnd.Next(0, MALE_SURNAMES.Length - 1)];
            patient.IsMale = true;
        }
        else
        {
            //Female
            patient.GivenName = FEMALE_NAMES[rnd.Next(0, FEMALE_NAMES.Length - 1)];
            patient.FamilyName = FEMALE_SURNAMES[rnd.Next(0, FEMALE_SURNAMES.Length - 1)];
            if (rnd.Next(0, 5) > 0) patient.MiddleName = FEMALE_SURNAMES[rnd.Next(0, FEMALE_SURNAMES.Length - 1)];
            patient.IsMale = false;
        }
        patient.DateOfBirth = DateTime.Now.AddYears(-1 * rnd.Next(15, 60)).AddMonths(-1 * rnd.Next(0, 8)).AddDays(-1 * rnd.Next(0, 25));
        patient.ExactDoB = false;

        int rNumProds = rnd.Next(1, 10);
        List<Product> productsToOrder = new();
        for (int i = 0; i < rNumProds; i++)
        {
            productsToOrder.Add(PRODUCTS[rnd.Next(0, PRODUCTS.Length)]);
        }
        productsToOrder = productsToOrder.Distinct().ToList();

        KeyValuePair<string, string>[] samplesToOrder;

        samplesToOrder = productsToOrder
            .Select(p => p.SampleCode)
            .Distinct()
            .Select(x => new KeyValuePair<string, string>(x, productsToOrder.First(z => z.SampleCode == x).SampleName))
            .ToArray();

        return new Order(
            Workflows.LAB_SCO, placerId,
            patient,
            productsToOrder.Select(p => new Service(p.Code, p.Name)).ToArray(),
            samplesToOrder.Select(s => new Sample(s.Key, null, $"SXA{rnd.Next(1, 9)}{rnd.Next(1, 9)}{rnd.Next(1, 9)}", DateTime.Now.AddMinutes(-1 * rnd.Next(1, 50)))).ToArray(),
            provierId);
    }

    public static Order GetFixedDemoOrder(string placerId, string? providerId)
    {
        Patient p = new("Борис", "Иванов", true, new DateTime(1975, 5, 5).ToUniversalTime());
        p.AddIdentifier(Authorities.BG_GRAO, "7505051234").AddIdentifier(Authorities.LOCAL, "523647");
        p.AddPhone("0878005006");

        Service svc = new Service("14749-6", "Глюкоза")
            .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
            .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
            .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

        return p.CreateOrder(Workflows.LAB_SCO, placerId, providerId)
            .AddService(svc)
            .AddSample("SER", null, "S05FT9");

    }

}
