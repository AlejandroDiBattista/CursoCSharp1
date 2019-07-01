using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cola {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {


        static Label texto;
        static Label cantidad;
        static Label modo;

        public MainPage() {
            //InitializeComponent();
            Padding = new Thickness(0, 20, 0, 0);

            Content = Vertical(
                        Horizontal(
                            cantidad = Cantidad(10),
                            modo = Modo(+1)
                        ),
                        Texto("10:30", 40, color: Color.Red)
                    );
        }

        static void Actualizar() {
            texto.Text = "Holis";
        }

        static Label Texto(string texto, double tamano = 20, FontAttributes atributos = FontAttributes.None, Color? color = null) {
            var l = new Label();
            l.Text = texto;
            l.HorizontalOptions = LayoutOptions.Center;
            l.VerticalOptions   = LayoutOptions.Center;
            l.TextColor = color ?? Color.Default;
            l.FontSize = tamano;
            l.FontAttributes = atributos;
            return l;
        }

        static Label Cantidad(int n) => Texto($"{n}", 120, FontAttributes.Bold, Color.Green);
        static Label Modo(int i) => Texto(i > 0 ? "+" : i < 0 ? "-" : "", 96, color : Color.Green );

        static Button Boton(string titulo, Action click) {
            var b =new Button();
            b.Text = titulo;
            b.Clicked += (object s, EventArgs e) => click();
            return b;
        }

        static StackLayout Vertical(params View[] views) {
            var s = new StackLayout();
            s.Spacing = 10;
            s.HorizontalOptions = LayoutOptions.CenterAndExpand;
            foreach(var v in views) s.Children.Add(v);
            return s ;
        }

        static StackLayout Horizontal(params View[] views) {
            var s = Vertical(views);
            s.Orientation = StackOrientation.Horizontal;
            return s;
        }
    }
}
