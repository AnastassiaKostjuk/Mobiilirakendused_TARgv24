using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics.Text;
using Microsoft.Maui.Layouts;

namespace Mobiilirakendused_TARgv24;

public partial class SnowmanPage : ContentPage
{
    // Список с текстами для кнопок
    public List<string> tekstid = new List<string>() { "Peida", "Näita", "Muuda värvi", "Sulata", "Tantsi" };
    BoxView eyeLeft, eyeRight, button1, button2, button3, nose;
    Picker picker;
    Slider slider;
    Stepper stepper;
    Frame head, bodyBottom, bodyMiddle;
    Label lbl;
    VerticalStackLayout buttonPanel; // контейнер для кнопок


    public SnowmanPage()
    {
        // Создаём фон
        var bgImage = new Image
        {
            // Aspect — это свойство, которое определяет, как изображение будет вписано в контейнер (по размеру и по соотношению сторон)
            Aspect = Aspect.AspectFill, // картинка растянута на весь фон страницы
            Source = "frozen.jpg"
        };

        // вертикальный контейнер для кнопок
        buttonPanel = new VerticalStackLayout { IsVisible = false }; // спрятан по умолчанию

        //Генерация кнопок
        //Цикл проходит по всем страницам.
        //Для каждой страницы создаётся кнопка.
        //Свойства кнопки:
        for (int i = 0; i < tekstid.Count; i++)
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


        picker = new Picker
        {
            FontSize = 20,
            BackgroundColor = Colors.Transparent, // фон прозрачный, рамку даёт Frame
            FontFamily = "Anta-Regular",
            ItemsSource = tekstid
        };


        // Оборачиваем во Frame
        var pickerFrame = new Frame
        {
            Content = picker,
            CornerRadius = 25,         // закруглённые края
            Padding = new Thickness(10, 5), // слева, сверху, справа и снизу
            BackgroundColor = Colors.White,
            BorderColor = Colors.Black,
            HasShadow = false
        };




        var layout = new AbsoluteLayout();

        // Сначала добавляем фон
        AbsoluteLayout.SetLayoutBounds(bgImage, new Rect(0, 0, 1, 1));
        AbsoluteLayout.SetLayoutFlags(bgImage, AbsoluteLayoutFlags.All);
        layout.Children.Add(bgImage);

        //picker и lbl

        AbsoluteLayout.SetLayoutBounds(lbl, new Rect(0.3, 0.01, 350, 100)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(lbl, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(lbl);


        AbsoluteLayout.SetLayoutBounds(pickerFrame, new Rect(0.45, 0.15, 250, 50)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(pickerFrame, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(pickerFrame);

        // Нижний шар
        var bodyBottom = new Frame
        {
            WidthRequest = 200,
            HeightRequest = 200,
            BackgroundColor = Colors.White,
            BorderColor = Colors.DarkBlue,
            CornerRadius = 100,
            HasShadow = false
        };
        AbsoluteLayout.SetLayoutBounds(bodyBottom, new Rect(0.5, 0.8, 200, 200)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(bodyBottom, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(bodyBottom);

        // Средний шар
        var bodyMiddle = new Frame
        {
            WidthRequest = 150,
            HeightRequest = 150,
            BackgroundColor = Colors.White,
            BorderColor = Colors.DarkBlue,
            CornerRadius = 75,
            HasShadow = false
        };
        AbsoluteLayout.SetLayoutBounds(bodyMiddle, new Rect(0.5, 0.55, 150, 150)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(bodyMiddle, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(bodyMiddle);

        // Голова
        var head = new Frame
        {
            WidthRequest = 120,
            HeightRequest = 120,
            BackgroundColor = Colors.White,
            BorderColor = Colors.DarkBlue,
            CornerRadius = 60,
            HasShadow = false
        };
        AbsoluteLayout.SetLayoutBounds(head, new Rect(0.5, 0.40, 120, 120)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(head, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(head);

        // Левый глаз
        var eyeLeft = new Frame
        {
            WidthRequest = 30,
            HeightRequest = 30,
            BackgroundColor = Colors.White,
            BorderColor = Colors.Black,
            CornerRadius = 15,
            HasShadow = false
        };
        AbsoluteLayout.SetLayoutBounds(eyeLeft, new Rect(0.45, 0.38, 30, 30)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(eyeLeft, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(eyeLeft);

        var pupilLeft = new BoxView
        {
            WidthRequest = 10,
            HeightRequest = 10,
            Color = Colors.Black,
            CornerRadius = 5
        };
        AbsoluteLayout.SetLayoutBounds(pupilLeft, new Rect(0.45, 0.38, 10, 10)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(pupilLeft, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(pupilLeft);

        // Правый глаз
        var eyeRight = new Frame
        {
            WidthRequest = 30,
            HeightRequest = 30,
            BackgroundColor = Colors.White,
            BorderColor = Colors.Black,
            CornerRadius = 15,
            HasShadow = false
        };
        AbsoluteLayout.SetLayoutBounds(eyeRight, new Rect(0.55, 0.38, 30, 30)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(eyeRight, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(eyeRight);

        var pupilRight = new BoxView
        {
            WidthRequest = 10,
            HeightRequest = 10,
            Color = Colors.Black,
            CornerRadius = 5
        };
        AbsoluteLayout.SetLayoutBounds(pupilRight, new Rect(0.55, 0.38, 10, 10)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(pupilRight, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(pupilRight);

        // Нос (морковка)
        var nose = new BoxView
        {
            WidthRequest = 50,
            HeightRequest = 15,
            Color = Colors.Orange,
            CornerRadius = 5
        };
        AbsoluteLayout.SetLayoutBounds(nose, new Rect(0.5, 0.43, 50, 15)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(nose, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(nose);

        // Пуговицы
        var button1 = new BoxView { Color = Colors.Black, WidthRequest = 20, HeightRequest = 20, CornerRadius = 10 };
        AbsoluteLayout.SetLayoutBounds(button1, new Rect(0.5, 0.56, 20, 20)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(button1, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(button1);

        var button2 = new BoxView { Color = Colors.Black, WidthRequest = 20, HeightRequest = 20, CornerRadius = 10 };
        AbsoluteLayout.SetLayoutBounds(button2, new Rect(0.5, 0.70, 20, 20)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(button2, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(button2);

        var button3 = new BoxView { Color = Colors.Black, WidthRequest = 20, HeightRequest = 20, CornerRadius = 10 };
        AbsoluteLayout.SetLayoutBounds(button3, new Rect(0.5, 0.78, 20, 20)); // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(button3, AbsoluteLayoutFlags.PositionProportional);
        layout.Children.Add(button3);

        layout.Children.Add(buttonPanel);
        Content = layout;


        slider = new Slider
        {
            Minimum = 0,
            Maximum = 1,
            Value = 50,
            ThumbImageSource = "snowflake.png",
            BackgroundColor = Color.FromRgba(200, 200, 100, 0),
            MinimumTrackColor = Colors.Green,
            MaximumTrackColor = Colors.Blue
        };

        slider.ValueChanged += Sl_ValueChanged;
    }

    private void Sl_ValueChanged(object sender, ValueChangedEventArgs e)
    {

    }

    //Обработчик нажатия на кнопку
    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        Button nupp = (Button)sender;
        await DisplayAlert("Info", $"Ты нажал: {tekstid[nupp.ZIndex]}", "OK");
        //sender приводится к Button.
        //Берётся её ZIndex, чтобы понять, какой номер у кнопки.
        //По этому индексу из списка lehed достаётся страница.
        //Navigation.PushAsync открывает её (переход вперёд в стеке навигации).
    }

}