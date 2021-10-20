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
            char[] abecedario = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();

            foreach (var letra in abecedario)
            {
                Button letras = new Button();
                letras.Tag = letra;
                TextBlock texto = new TextBlock();
                texto.Text = letra.ToString();
                Viewbox box = new Viewbox();
                box.Child = texto;
                letras.Content = box;
                LetrasUniformGrid.Children.Add(letras);
                Style = (Style)this.Resources["botonesLetra"];
            }

            //Crear contenedor ?? NO uniform grid JAVI


            

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    ResultadoTextBlock.Text += ((Button)sender).Tag.ToString();
        //}
    }






}








