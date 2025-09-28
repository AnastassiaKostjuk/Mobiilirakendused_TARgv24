using System;

namespace Mobiilirakendused_TARgv24;

public partial class Valgusfoor : ContentPage
{
    // Список с текстами для кнопок
    public List<string> tekstid = new List<string>() { "SISSE", "VÄLJA", "ÖÖ", "PÄEV" };

    // Цвета для ламп: красный, жёлтый, зелёный, серый (неактивный)
    public Color punane, kollane, roheline, hall;

    // Контейнер, который позволяет прокручивать содержимое
    ScrollView sv;

    // Кружки для светофора
    BoxView punaneLamp, kollaneLamp, rohelineLamp;

    bool päev;
    bool öö;
    bool running = false; // флаг работы светофора / работает ли сейчас светофор (чтобы можно было остановить)

    public Valgusfoor()
    {
        Title = "Valgusfoor";

        // Цвета
        punane = Color.FromRgb(255, 0, 0);
        kollane = Color.FromRgb(255, 255, 0);
        roheline = Color.FromRgb(0, 255, 0);
        hall = Color.FromRgb(80, 80, 80);

        // Создаём фон
        var bgImage = new Image
        {
            // Aspect — это свойство, которое определяет, как изображение будет вписано в контейнер (по размеру и по соотношению сторон)
            Aspect = Aspect.AspectFill, // картинка дороги растянута на весь фон страницы
            Source = "dayroad.png" // картинка для дня
        };

        // Создаём "лампы" светофора
        // Три кружка серого цвета (гаснутые лампы)
        punaneLamp = new BoxView
        {
            Color = hall,
            CornerRadius = 100, // CornerRadius = 100 → делает круги круглыми
            WidthRequest = 100, // размер
            HeightRequest = 100, // размер
            HorizontalOptions = LayoutOptions.Center // центр по горизонтали
        };

        kollaneLamp = new BoxView
        {
            Color = hall,
            CornerRadius = 100,
            WidthRequest = 100,
            HeightRequest = 100,
            HorizontalOptions = LayoutOptions.Center
        };

        rohelineLamp = new BoxView
        {
            Color = hall,
            CornerRadius = 100,
            WidthRequest = 100,
            HeightRequest = 100,
            HorizontalOptions = LayoutOptions.Center
        };

       
        // Frame (чёрный прямоугольник) изображает корпус светофора
        var valgusfoorBody = new Frame 
        {
            BackgroundColor = Colors.Black,
            CornerRadius = 20,
            Padding = 10,
            Content = new StackLayout
            // Внутри вертикальный StackLayout с тремя лампами
            {
                Spacing = 15,
                Children = { punaneLamp, kollaneLamp, rohelineLamp }
            },
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        // Кнопка переключения день/ночь
        var switchDayNightButton = new Button
        {
            Text = öö ? tekstid[3] : tekstid[2], // если ночь - показывает "PÄEV", если день - "ÖÖ"
            // // VerticalOptions — это свойство, которое говорит системе разметки, как элемент должен располагаться по вертикали внутри контейнера
            VerticalOptions = LayoutOptions.End // End означает, что элемент будет прижат к нижнему краю доступного пространства
        };
        switchDayNightButton.Clicked += (s, e) => // (s, e) — это параметры обработчика события
        // s (sender) → объект, который вызвал событие(кнопка switchDayNightButton)
        // e (EventArgs e) → аргументы события
        {
            päev = !päev; // ! — это логическое отрицание (NOT)
            öö = !päev;
            // Если переменная была true → станет false
            // Если была false → станет true

            // меняем фон
            bgImage.Source = päev ? "dayroad.png" : "nightroad.png";

            // меняем текст кнопки
            switchDayNightButton.Text = öö ? tekstid[3] : tekstid[2];

            // сразу сбрасываем лампы, чтобы режим поменялся мгновенно
            punaneLamp.Color = hall;
            kollaneLamp.Color = hall;
            rohelineLamp.Color = hall;
        };

        // Кнопка запуска
        var startButton = new Button
        {
            Text = tekstid[0] // "SISSE"
        };
        startButton.Clicked += StartTrafficLight;

        var stopButton = new Button
        {
            Text =  tekstid[1] // "VALJA"
        };
        stopButton.Clicked += StopTrafficLight;


        // Всё содержимое
        sv = new ScrollView // ScrollView → контейнер
        {
            // // Здесь в sv.Content мы кладём всё, что должно отображаться на странице
            Content = new Grid // Grid — это контейнер для размещения элементов в «слоях» или таблице
            {
                // свойство Children, куда добавляются все элементы
                Children = 
                    {
                        bgImage, // фон / Первый ребёнок → bgImage (картинка дороги)
                        new StackLayout // Второй ребёнок → StackLayout со светофором и кнопками / StackLayout — это контейнер, который раскладывает элементы один под другим (вертикально)
                        // Значит, фон будет внизу, а интерфейс светофора поверх картинки
                        {
                            // VerticalOptions = LayoutOptions.Center — весь StackLayout будет располагаться по центру экрана (по вертикали)
                            VerticalOptions = LayoutOptions.Center,
                            Children = { valgusfoorBody, switchDayNightButton, startButton, stopButton }
                        }
                    }
            }
        };

        // У любой ContentPage есть свойство Content
        // Оно определяет, что именно будет показано на странице
        Content = sv; // Содержимое страницы = ScrollView (с Grid внутри)
    }


    private void StopTrafficLight(object sender, EventArgs e)
    {
        running = false; // цикл в StartTrafficLight останавливается
        punaneLamp.Color = hall;
        kollaneLamp.Color = hall;
        rohelineLamp.Color = hall;
    }

    private async void StartTrafficLight(object sender, EventArgs e)
    {
        if (running) return; // если уже работает — не запускать второй раз
        running = true;

        while (running)
        {
            if (öö) // ночной режим
            {
                punaneLamp.Color = hall;
                rohelineLamp.Color = hall;

                kollaneLamp.Color = kollane;
                await Task.Delay(700);
                if (!running) break; // проверяем, не оставновили ли светофор

                kollaneLamp.Color = hall;
                await Task.Delay(700);
                if (!running) break;
            }
            else // дневной режим
            {
                punaneLamp.Color = punane;
                kollaneLamp.Color = hall;
                rohelineLamp.Color = hall;
                await Task.Delay(2000);
                if (!running) break;

                punaneLamp.Color = hall;
                kollaneLamp.Color = kollane;
                rohelineLamp.Color = hall;
                await Task.Delay(1000);
                if (!running) break;

                punaneLamp.Color = hall;
                kollaneLamp.Color = hall;
                rohelineLamp.Color = roheline;
                await Task.Delay(2000);
                if (!running) break;
            }
        }
    }
}