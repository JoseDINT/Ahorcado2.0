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

            //this es para la ventana actual y si queremos hacer referencia al
            //app.xmal usaremos Application.Current.Rs

            TextBlock palabra = new TextBlock();
            ScrollViewer scroll = new ScrollViewer();
            scroll.Content = palabra;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            //Para los scrollview debemos poner los
            PalabraWrapPanel.Children.Add(scroll);
            palabra.Style = (Style)Application.Current.Resources["contenedorPalabra"];

            List<String> listaPalabras = new List<string>() {"Murcia", "Mojaca", "Lerida", "Pamplona", "Valencia" };

            Random seed = new Random();


            //List<Char> palabras = new List<Char>(); 

            StringBuilder sb = new StringBuilder();


            for (int i = 0; i < pAdivina.Length; i++)
            {
                palabra.Text = sb.Append(" _ ").ToString();

                if (pAdivina.Equals(letrillas))
                {
                    sb.Replace( '_', letrillas);

                }
                palabra.FontSize = 40;
            }
        }

        //Clicar sobre botón
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button boton = (Button)sender;
            boton.IsEnabled = false;
        }

        private void Reiniciar_Botones(object sender, RoutedEventArgs e)
        {

            //Hay que hacer un foreach de los botones que tenemos del children del contenedor 
            foreach (Button b in LetrasUniformGrid.Children)
            {
                b.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            foreach (Button b in LetrasUniformGrid.Children)
            {
                if (b.Tag.ToString() == e.Key.ToString())
                    b.IsEnabled = false;
            }
        }

        //Añadir letras y letras está en letras , letras acertadas


    }






}








