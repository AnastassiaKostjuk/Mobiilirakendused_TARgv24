using System.Threading.Tasks; //Подключает библиотеку для работы с асинхронными задачами

namespace Mobiilirakendused_TARgv24;

public partial class StartPage : ContentPage
//Определяется класс StartPage
//partial означает, что часть его реализации может находиться в другом файле.
//Наследуется от ContentPage → это базовый класс MAUI-страницы.
{
    public List<ContentPage> lehed = new List<ContentPage>() { new TekstPage(), new FigurePage(), new TimerPage(), new DateTimePage(), new PulsePage() };
    //lehed — список страниц (ContentPage), которые можно будет открыть при нажатии на кнопки.
    public List<string> tekstid = new List<string>() { "Tekst page", "Figure page", "Timer page", "DateTime page", "Pusle page" };
    //tekstid — список строк (подписи для кнопок).
    //Каждая строка соответствует странице из списка lehed.
    ScrollView sv; //sv — скролл-контейнер.
    //VerticalStackLayout vsl; //vsl — вертикальный контейнер (располагает элементы друг под другом).
    VerticalStackLayout buttonPanel; // контейнер для кнопок
    Image menuIcon; // иконка (картинка)


    public StartPage()
	{
		Title = "Avaleht";
		BackgroundImageSource = "spaceman.jpg";

        //vsl = new VerticalStackLayout {  }; //Создаётся пустой вертикальный контейнер.


        // вертикальный контейнер для кнопок
        buttonPanel = new VerticalStackLayout { IsVisible = false }; // спрятан по умолчанию

        //Генерация кнопок
        //Цикл проходит по всем страницам.
        //Для каждой страницы создаётся кнопка.
        //Свойства кнопки:
        for (int i = 0; i < lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = tekstid[i], //Text → берётся из tekstid.
                FontSize = 20, //FontSize, TextColor, FontFamily → оформление текста.
                TextColor = Colors.White,
				Padding = 10, //Padding, Margin, CornerRadius → отступы и скругления.
                CornerRadius = 20,
				FontFamily = "Texturina-VariableFont",
				Margin = 10,
				WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 4, //WidthRequest → ширина кнопки (четверть ширины экрана).
                ZIndex = i, //ZIndex → сохраняется индекс кнопки (используется потом, чтобы понять, какую страницу открыть).
                Shadow = new Shadow { Opacity = 0.35f, Offset = new Point(4, 4), Radius = 8 } //Shadow → добавляется тень.
            };

			nupp.Background = new LinearGradientBrush(
				new GradientStopCollection
				{
                    new GradientStop(Color.FromRgba(0, 32, 96, 230), 1.0f),   // тёмно-синий (почти сапфир)
                    new GradientStop(Color.FromRgba(0, 128, 64, 200), 0.6f), // изумрудный
                    new GradientStop(Color.FromRgba(0, 255, 200, 120), 0.0f) // светло-зелёный с "свечением"
				},
				new Point(0, 0),
				new Point(0, 1)
			);

			//vsl.Add(nupp); //Кнопка добавляется в вертикальный контейнер.
            nupp.Clicked += Nupp_Clicked; //Привязывается обработчик события Clicked → метод Nupp_Clicked.
            buttonPanel.Add(nupp);
        }

        //Весь вертикальный контейнер (vsl) помещается в скролл (sv).
        //sv = new ScrollView { Content = vsl };
        //Content = sv;
        //Content = sv; → у страницы теперь содержимое — это скролл со всеми элементами.

        // иконка меню
        menuIcon = new Image
        {
            Source = "heart.png",
            HeightRequest = 80,
            WidthRequest = 80,
            Margin = new Thickness(10),
            HorizontalOptions = LayoutOptions.Start, // ← прижимает к левому краю
            VerticalOptions = LayoutOptions.Start    // ← прижимает к верху
        };

        // делаем картинку кликабельной
        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) =>
        {
            buttonPanel.IsVisible = !buttonPanel.IsVisible; // показать/спрятать кнопки
        };
        menuIcon.GestureRecognizers.Add(tap);

        // собираем макет
        Content = new Grid
        {
            Children =
            {
                new VerticalStackLayout
                {
                    Children =
                    {
                        menuIcon,   // иконка сверху
                        buttonPanel // скрытая панель с кнопками
                    }
                }
            }
        };
    }

    //Обработчик нажатия на кнопку
    private async void Nupp_Clicked (object? sender, EventArgs e)
	{
		Button nupp = (Button)sender;
        await Navigation.PushAsync(lehed[nupp.ZIndex]);
        //sender приводится к Button.
        //Берётся её ZIndex, чтобы понять, какой номер у кнопки.
        //По этому индексу из списка lehed достаётся страница.
        //Navigation.PushAsync открывает её (переход вперёд в стеке навигации).
    }
}