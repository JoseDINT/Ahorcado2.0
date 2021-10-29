using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
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
        List<Char> abcedario;
        List<BitmapImage> imagenes;
        List<TextBlock> camposTexto;
        string palabra;
        int fallos = 0;


        public MainWindow()
        {

            InitializeComponent();
            CrearBotones();
            imagenes = new List<BitmapImage>();
            EmpezarPartida();

        }



        public void CrearBotones()
        {
            abcedario = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToList<Char>();
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
                letras.Style = (Style)this.Resources["botonesLetra"];
            }
        }

        private void EmpezarPartida()
        {

            CargarImagen();
            PalabraWrapPanel.Children.Clear();
            OfuscarPalabra();
        }

        private void SonidoAcierto()
        {

            SoundPlayer player = new SoundPlayer(@"../../../assets/mp3/acierto.wav");
            player.Load();
            player.Play();
        }

        private void CargarImagen()
        {
            for (int i = 4; i < 11; i++)
            {
                var imagen = new BitmapImage(
                new Uri(System.IO.Path.Combine(
                    Environment.CurrentDirectory,
                    "../../../assets/img/" + i.ToString() + ".jpg")));
                imagenes.Add(imagen);
            }
        }

        private string PalabraRandom()
        {
            Random gen = new Random();
            List<string> listaPalabras = System.IO.File.ReadAllLines(@"../../../assets/palabras.txt").ToList();
            return listaPalabras[gen.Next(listaPalabras.Count)];
        }

        private void OfuscarPalabra()
        {
            fallos = 0;
            this.palabra = PalabraRandom();
            EstadoJugadorImage.Source = imagenes[0];
            camposTexto = new List<TextBlock>();

            for (int i = 0; i < this.palabra.Length; i++)
            {
                TextBlock textBlock = new TextBlock()
                {
                    Text = "_",
                };
                textBlock.Style = (Style)this.Resources["letrasTextBlock"];
                PalabraWrapPanel.Children.Add(textBlock);
                camposTexto.Add(textBlock);
            }
        }

        public void Comprobar(string letra)
        {
            bool acierto = false;

            for (int i = 0; i < this.palabra.Length; i++)
            {
                if (this.palabra[i].ToString() == letra)
                {
                    SonidoAcierto();
                    acierto = true;
                    camposTexto[i].Text = letra;
                }
            }
            if (acierto == false)
            {

                fallos++;
                EstadoJugadorImage.Source = imagenes[fallos];
            }
            if (fallos == 6)
            {
                PerderPartida();
            }
            int contador = 0;
            for (int i = 0; i < this.palabra.Length; i++)
            {
                if (camposTexto[i].Text != "_")
                    contador++;
            }
            if (contador == this.palabra.Length)
            {
                GanarPartida();
            }

        }

        public void GanarPartida()
        {
            RendirseButton.IsEnabled = false;
            MessageBox.Show("Correcto la palabra era " + palabra, "Ganaste", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        public void PerderPartida()
        {

            EstadoJugadorImage.Source = imagenes[6];
            MessageBox.Show("Lo siento era " + palabra, "Perdiste", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            string letra = boton.Tag.ToString();

            Comprobar(letra);
            boton.IsEnabled = false;
        }

        private void Nueva_Partida(object sender, RoutedEventArgs e)
        {
            EmpezarPartida();

            if (RendirseButton.IsEnabled == false) RendirseButton.IsEnabled = true;

            foreach (Button b in LetrasUniformGrid.Children)
            {
                b.IsEnabled = true;
            }
            MessageBox.Show("Generando Nueva Palabra", "Nueva Partida", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Rendirse(object sender, RoutedEventArgs e)
        {
            RendirseButton.IsEnabled = false;
            EstadoJugadorImage.Source = imagenes[6];
            MessageBox.Show("La palabra era " + palabra, "Te has rendido", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            foreach (Button b in LetrasUniformGrid.Children)
            {
                if (b.Tag.ToString() == e.Key.ToString())
                {
                    b.IsEnabled = false;
                    Comprobar(b.Tag.ToString());
                }
            }

        }
    }

}







