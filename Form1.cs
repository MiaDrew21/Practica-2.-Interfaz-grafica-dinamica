using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace interfazGrafica
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            CargarImagenes();
        }
        private void CargarImagenes()
        {
            // Obtener el directorio del programa y la carpeta "imag"
            string directorioRaiz = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string rutaCarpetaImagenes = Path.Combine(directorioRaiz, "Practica-2.-Interfaz-grafica-dinamica", "imag");

            // Verificar si la carpeta existe
            if (!Directory.Exists(rutaCarpetaImagenes))
            {
                MessageBox.Show("La carpeta 'imag' no existe.\nRuta: " + rutaCarpetaImagenes, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener imágenes (máximo 5)
            string[] archivos = Directory.GetFiles(rutaCarpetaImagenes, "*.*")
                                         .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".bmp"))
                                         .Take(5)
                                         .ToArray();

            if (archivos.Length == 0)
            {
                MessageBox.Show("No se encontraron imágenes en la carpeta 'imag'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asignar imágenes a los PictureBox y agregar eventos Click
            AsignarImagenAPictureBox(pbImagen1, archivos, 0);
            AsignarImagenAPictureBox(pbImagen2, archivos, 1);
            AsignarImagenAPictureBox(pbImagen3, archivos, 2);
            AsignarImagenAPictureBox(pbImagen4, archivos, 3);
            AsignarImagenAPictureBox(pbImagen5, archivos, 4);
        }
        private void AsignarImagenAPictureBox(PictureBox pictureBox, string[] archivos, int index)
        {
            if (index < archivos.Length)
            {
                pictureBox.Image = Image.FromFile(archivos[index]);
                pictureBox.Tag = archivos[index]; // Guardamos la ruta de la imagen
                pictureBox.Click += PictureBox_Click; // Evento Click
            }
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;

            if (clickedPictureBox != null && clickedPictureBox.Tag != null)
            {
                string rutaImagen = clickedPictureBox.Tag.ToString();
                MostrarImagenTamañoCompleto(rutaImagen);
            }
        }
        private void MostrarImagenTamañoCompleto(string rutaImagen)
        {
            Form ventanaImagen = new Form
            {
                Text = Path.GetFileName(rutaImagen),
                Width = 800,
                Height = 600
            };

            PictureBox pictureBox = new PictureBox
            {
                Image = Image.FromFile(rutaImagen),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill
            };

            ventanaImagen.Controls.Add(pictureBox);
            ventanaImagen.Show();
        }

        private void pbImagen3_Click(object sender, EventArgs e)
        {

        }
    }
}
