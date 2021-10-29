using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ahorcado
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] palabraArray;
        String palabraSecreta;
        StringBuilder sb = new StringBuilder();
        String adivinar;
        TextBlock palabra;
        private int estado = 4;
        public MainWindow()
        {

            InitializeComponent();

            //Crear botones


            List<Char> abcedario = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToList<Char>();
            abcedario.Insert(14, 'Ñ');

            foreach (var letra in abcedario)
            {
                Button letras = new Button();
                letras.Tag = letra;
                TextBlock texto = new TextBlock();
                texto.Text = letra.ToString();
                Viewbox box = new Viewbox();
                box.Child = texto;
                letras.Content = box;
                LetrasUniformGrid.Children.Add(letras);
                letras.Click += Button_Click;
                letras.Style = (Style)Application.Current.Resources["botonesLetra"];
            }

            NuevaPartidaButton.Click += Nueva_Partida;
            RendirseButton.Click += Rendirse;

            palabra = new TextBlock();
            ScrollViewer scroll = new ScrollViewer();
            scroll.Content = palabra;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            PalabraWrapPanel.Children.Add(scroll);
            palabra.Style = (Style)Application.Current.Resources["contenedorPalabra"];

            palabraSecreta = PalabraRandom();

            //palabraArray = palabraSecreta.ToCharArray(0, palabraSecreta.Length - 1);
            adivinar = palabraSecreta;

            OfuscaPalabra(palabraSecreta, palabra);
        }
        private String OfuscaPalabra(String cadena, TextBlock contenedor)
        {
            for (int i = 0; i < cadena.Length; i++)
                contenedor.Text = sb.Append("_").ToString();
            return sb.ToString();
        }

        public String PalabraRandom()
        {
            Random gen = new Random();
            List<String> listaPalabras = new List<string>() { "MURCIA", "MOJACA", "LERIDA", "PAMPLONA", "ALBACETE" };
            return listaPalabras[gen.Next(listaPalabras.Count)];
        }


        public void Comprobar(char letra)
        {
            
                for (int i = 0; i < adivinar.Length; i++)
                {
                    if (letra == adivinar[i])
                        sb[i] += letra;

                }

                if (estado <= 9)
                {
                    estado++;
                    EstadoJugadorImage.Source = GetStageImage();
                }
                else
                {
                    FinalizarPartida();
                }

                Console.WriteLine();
            
        }

        public BitmapImage GetStageImage()
        {
            return new BitmapImage(
                new Uri(System.IO.Path.Combine(
                    Environment.CurrentDirectory,
                    "../../../assets/img/" + estado + ".jpg")));
        }

        

        public void GanarPartida()
        {
            MessageBox.Show("La palabra era " + adivinar);
        }
        public void FinalizarPartida()
        {
            estado = 4;
            EstadoJugadorImage.Source = GetStageImage();

            MessageBox.Show("Lo siento era " + adivinar);
        }

        //EVENTOS
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            boton.IsEnabled = false;
            char letraTeclado = (char)boton.Tag;
            Comprobar(letraTeclado);
        }

        private void Nueva_Partida(object sender, RoutedEventArgs e)
        {
            estado = 4;
            EstadoJugadorImage.Source = GetStageImage();

            foreach (Button b in LetrasUniformGrid.Children)
            {
                b.IsEnabled = true;
            }
            MessageBox.Show("Nueva Partida");
        }

        private void Rendirse(object sender, RoutedEventArgs e)
        {
            estado = 10;
            EstadoJugadorImage.Source = GetStageImage();
            MessageBox.Show("Te has rendido \nLa palabra era " + adivinar);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            foreach (Button b in LetrasUniformGrid.Children)
            {
                if (b.Tag.ToString() == e.Key.ToString())
                {
                    b.IsEnabled = false;
                    Comprobar((char)b.Tag);
                }
            }

        }
    }

}







