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
        StringBuilder sb = new StringBuilder();
        public MainWindow()
        {

            InitializeComponent();

            //Crear botones

            //Hacer un bitmap de imagenes para que nos muestre los cambios

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

            NuevaPartidaButton.Click += Reiniciar_Botones;
            RendirseButton.Click += Perder;

            //this es para la ventana actual y si queremos hacer referencia al
            //app.xmal usaremos Application.Current.Rs

            TextBlock palabra = new TextBlock();
            ScrollViewer scroll = new ScrollViewer();
            scroll.Content = palabra;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            //Para los scrollview
            PalabraWrapPanel.Children.Add(scroll);
            palabra.Style = (Style)Application.Current.Resources["contenedorPalabra"];

            String palabraSecreta = PalabraRandom();
            
            palabraArray = palabraSecreta.ToCharArray(0, palabraSecreta.Length);
            StringBuilder adivinar = new StringBuilder(palabraSecreta);

            OfuscaPalabra(palabraSecreta, palabra);
        }

        private int Stage { get; set; }
        public void Comprobar(char letra)
        {

            if (palabraArray.Contains(letra))
            {
                if (Stage <= 9)
                {
                    Stage++;
                    EstadoJugadorImage.Source = GetStageImage();
                }
                else
                {
                    MessageBox.Show("Has perdido");
                }
            }

            //if acertado compara el texto del textblock vs la.
        }

        public BitmapImage GetStageImage()
        {
            return new BitmapImage(
                new Uri(System.IO.Path.Combine(
                    Environment.CurrentDirectory,
                    "../../../assets/img/" + Stage + ".jpg")));
        }

        private String OfuscaPalabra(String cadena, TextBlock contenedor)
        {
            for (int i = 0; i < cadena.Length; i++)
                contenedor.Text = sb.Append('_').ToString();
            return sb.ToString();
        }

        public String PalabraRandom()
        {
            Random gen = new Random();
            List<String> listaPalabras = new List<string>() { "MURCIA", "MOJACA", "LERIDA", "PAMPLONA" };
            return listaPalabras[gen.Next(0, listaPalabras.Count)];
        }

        //EVENTOS
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            boton.IsEnabled = false;
            letraTeclado = (char)boton.Tag;
            Comprobar(letraTeclado);
        }

        private void Reiniciar_Botones(object sender, RoutedEventArgs e)
        {
            //Hay que hacer un foreach de los botones que tenemos del children del contenedor 
            foreach (Button b in LetrasUniformGrid.Children)
            {
                b.IsEnabled = true;
            }
        }

        private void Perder(object sender, RoutedEventArgs e)
        {
            Stage = 10;
            EstadoJugadorImage.Source = GetStageImage();
            MessageBox.Show("Te has rendido");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Button b in LetrasUniformGrid.Children)
            {
                if (b.Tag.ToString() == e.Key.ToString())
                    b.IsEnabled = false;
                Comprobar((char)b.Tag);
            }
        }
    }

}







