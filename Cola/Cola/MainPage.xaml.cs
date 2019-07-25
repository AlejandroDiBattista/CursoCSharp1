using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cola {

    public enum Estados {
        Parado,
        Configurando,
        Esperando
    }

    [DesignTimeVisible(false)]
    public static class ViewExtensions {
        public static void OnTap(this View view, Action accion) {
            var tocar = new TapGestureRecognizer();
            tocar.Tapped += (s, e) => accion();
            view.GestureRecognizers.Add(tocar);
        }

        public static void OnDoubleTap(this View view, Action accion) {
            var tocar = new TapGestureRecognizer();
            tocar.Tapped += (s, e) => accion();
            tocar.NumberOfTapsRequired = 2;
            view.GestureRecognizers.Add(tocar);
        }

        public static void OnSwipe(this View view, Action accion, SwipeDirection direccion = SwipeDirection.Down) {
            var swipe = new SwipeGestureRecognizer();
            swipe.Direction = direccion;
            swipe.Swiped += (s, e) => accion();
            view.GestureRecognizers.Add(swipe);
        }

        public delegate void AlCambiar(int delta);

        public static void OnSwipeUp(this View view, Action accion)    => view.OnSwipe(accion, SwipeDirection.Up);
        public static void OnSwipeDown(this View view, Action accion)  => view.OnSwipe(accion, SwipeDirection.Down);
        public static void OnSwipeLeft(this View view, Action accion)  => view.OnSwipe(accion, SwipeDirection.Left);
        public static void OnSwipeRight(this View view, Action accion) => view.OnSwipe(accion, SwipeDirection.Right);

        public static void OnPan(this View view, AlCambiar cambiar, bool vertical = true, double paso = 20) {
            var pan = new PanGestureRecognizer();
            var inicio = 0.0;
            pan.PanUpdated += (a, b) => {
                if(b.StatusType == GestureStatus.Started) {
                    inicio = 0;
                }
                if(b.StatusType == GestureStatus.Running) {
                    var delta = (vertical ? b.TotalY : b.TotalX) - inicio;
                    while(Math.Abs(delta) >= paso) {
                        if(delta < 0) {
                            cambiar(+1);
                            delta  += paso;
                            inicio -= paso;
                        } else {
                            cambiar(-1);
                            delta  -= paso;
                            inicio += paso;
                        }
                    }
                }
            };
            view.GestureRecognizers.Add(pan);           
        }
    }

    public class Cantidad : Label {
        int valor  =  0, minimo =  0, maximo = 10;
        public int Valor  { get => valor;  set => Set(value, minimo, maximo); }
        public int Minimo { get => minimo; set => Set(valor, value,  maximo); }
        public int Maximo { get => maximo; set => Set(valor, minimo, value ); }

        public bool Fijo { get; set; } = false;
        public bool Incremento { get; set; } = true;

        public Cantidad(int inicial, int minimo = 0, int maximo = 10) {
            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
            this.VerticalOptions   = LayoutOptions.Center;
            this.TextColor = Color.Cyan;
            this.FontSize = 160;

            this.OnTap(Avanzar);
            this.OnPan(Cambiar);

            Set(inicial, minimo, maximo);
        }

        void Sumar()   => Cambiar(+1);
        void Restar()  => Cambiar(-1);
        void Avanzar() => Cambiar(Incremento ? +1 : -1);

        void Cambiar(int delta) => Valor += Fijo ? 0 : delta;

        private void Set(int val, int min, int max) {
            minimo = Math.Max(0, min);
            maximo = Math.Max(0, max);
            valor = val;
            valor = Math.Max(valor, minimo);
            valor = Math.Min(valor, maximo);
            Text = $"{valor}";
        }
    }

    public partial class MainPage : ContentPage {
        static Label Descripcion;
        static Cantidad A;
        static Cantidad B;
        static Label Reloj;

        static int Mayor = 1;
        static int Menor  = 1;
        //static int Final = 0;

        static bool Doble = false;

        static DateTime Inicio = DateTime.MinValue;
        static DateTime Primer = DateTime.MinValue;
        static DateTime Ultimo = DateTime.MinValue;

        static Estados Estado = Estados.Parado;

        public MainPage() {
            BackgroundColor = Color.FromHex("E0FFFF");
            Padding = new Thickness(0, 20, 0, 0);

            Content = Vertical(
                Descripcion = Texto("", 20, color: Color.Gray),
                A = new Cantidad(Mayor, 1, 100),
                B = new Cantidad(Menor,  1, 100),
                Reloj = Texto("", 20, color: Color.Red)
            ); ;

            AlParar();

            Device.StartTimer(TimeSpan.FromSeconds(0.5), () => {
                Actualizar();
                return true;
            });

            Content.OnSwipeLeft(CambiarModo);
            Content.OnSwipeRight(CambiarModo);
        }

        static void CambiarModo() {
            Doble = !Doble;
            Actualizar();
        }

        static void AlParar() {
            Estado = Estados.Parado;
            A.Valor = Mayor = 1;
            A.IsVisible = false;
            A.TextColor = Color.Red;
            A.FontSize = 160;
            A.Fijo = false;

            B.Valor = Menor = 1;
            B.IsVisible = true;
            B.TextColor = Color.Red;
            B.FontSize = 120;
            B.Incremento = true;
            B.Minimo = 1;

            Reloj.Text = "Tocar para comenzar \n Parado";
            Reloj.FontSize = 40;
        }

        static void AlConfigurar() {
            Estado = Estados.Configurando;
            Inicio = DateTime.Now;

            Mayor = A.Valor;
            Menor = B.Valor;

            A.IsVisible = true;
            A.TextColor = Color.Blue;
            A.Fijo = false;

            B.TextColor = Color.Blue;
            B.Fijo = false;
            B.Incremento = true;

            Reloj.Text = "...\nConfigurar";
            Reloj.TextColor = Color.Blue;
            Reloj.FontSize = 40;
        }

        static void AlEsperar() {
            Menor = Math.Min(A.Valor, B.Valor);
            Mayor = Math.Max(A.Valor, B.Valor);

            if(Menor == 1) {
                A.IsVisible = false;
                B.Minimo = 0;
                B.Maximo = Mayor;
                B.Valor = Mayor;
                B.Incremento = false;
            } else {
                A.Valor = Mayor;
                A.Fijo = true;

                B.Minimo = Menor;
                B.Maximo = Mayor;
                B.Valor = Menor;
                B.Incremento = true;
            }

            A.TextColor = Color.Green;
            B.TextColor = Color.Green;

            Reloj.TextColor = Color.Green;
            Reloj.FontSize = 40;
        }

        static void Actualizar() {
            switch(Estado) {
                case Estados.Parado:
                    Descripcion.Text = String.Format("{0:h\\:mm\\:ss}", DateTime.Now);

                    if(A.Valor > 1 || B.Valor > 1) AlConfigurar();
                    break;

                case Estados.Configurando:
                    var m = DateTime.Now.Subtract(Ultimo);
                    Descripcion.Text = String.Format("{0:h\\:mm\\:ss}  »  {1:m\\:ss}", Inicio, m);

                    if(A.Valor > 1 || B.Valor > 1) {
                        Ultimo = DateTime.Now;
                        if(m.TotalSeconds >= 5.0) AlEsperar();
                    }

                    break;

                case Estados.Esperando:
                    if(Menor == 1 && Mayor != B.Valor || Menor != 1 && Menor != B.Valor) {
                        var f = Falta();
                        Reloj.Text = String.Format("{0:m\\:ss}\nEsperando", f);
                        Reloj.TextColor  = f.TotalSeconds < 0.0 ? Color.Red : Color.Green;
                        Descripcion.Text = String.Format("{0:h\\:mm\\:ss}  »  {1:m\\:ss}  »  {2:h\\:mm\\:ss} ", Inicio, Duracion(), Final());
                    } else {
                        Reloj.Text = "\nEsperando";// String.Format("{0:m\\:ss}", Duracion());
                        Descripcion.Text = String.Format("{0:h\\:mm\\:ss}  »  {1:m\\:ss}", Inicio, Duracion());
                    }

                    if(Menor == 1 && B.Valor == 0 || Menor != 1 && B.Valor == Mayor) AlParar();

                    break;
            }
        }

        static TimeSpan Duracion() {
            if(Menor == Mayor) {
                return DateTime.Now.Subtract(Inicio);
            } else {
                return TimeSpan.FromSeconds(Ultimo.Subtract(Inicio).TotalSeconds / (Mayor - Menor));
            }
        }

        static DateTime Final() {
            return Inicio.Add(TimeSpan.FromSeconds(Duracion().TotalSeconds * Mayor));
        }

        static TimeSpan Falta() {
            return Final().Subtract(DateTime.Now);
        }

        static Label Texto(string texto, double tamano = 20, FontAttributes atributos = FontAttributes.None, Color? color = null) {
            var l = new Label();
            l.Text = texto;
            l.HorizontalOptions = LayoutOptions.Center;
            l.VerticalOptions = LayoutOptions.Center;
            l.TextColor = color ?? Color.Default;
            l.FontSize = tamano;
            l.FontAttributes = atributos;
            return l;
        }

        static Label Cantidad(int n) => Texto($"{n}", 240, FontAttributes.None, Color.Green);
        static Label Modo(int i) => Texto(i > 0 ? "+" : i < 0 ? "-" : "", 96, color: Color.Green);

        static Button Boton(string titulo, Action click) {
            var b = new Button();
            b.Text = titulo;
            b.FontSize = 40;
            b.Clicked += (object s, EventArgs e) => click();
            return b;
        }

        static StackLayout Vertical(params View[] views) {
            var s = new StackLayout();
            s.Spacing = 10;
            s.HorizontalOptions = LayoutOptions.CenterAndExpand;
            s.VerticalOptions = LayoutOptions.CenterAndExpand;
            foreach(var v in views)
                s.Children.Add(v);
            return s;
        }

        static StackLayout Horizontal(params View[] views) {
            var s = Vertical(views);
            s.Orientation = StackOrientation.Horizontal;
            return s;
        }
    }
}
