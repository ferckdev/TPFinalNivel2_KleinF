using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace presentacion
{
    public partial class frmAltaArticulo : Form
    {
        private Articulos articulos = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public frmAltaArticulo(Articulos articulos)
        {
            InitializeComponent();
            this.articulos = articulos;
            Text = "Modificar Articulo";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool Validar()
        {
            if (txtPrecio.ToString() == "Numeros")
                return !(soloNumeros(txtPrecio.Text));
            return false;
        }
        private bool soloNumeros(string cadena)
        {
            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulos nuevo = new Articulos();
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (Validar())
                    return;
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.ImagenUrl = txtImagenUrl.Text;
                nuevo.Marca = (Marcas)cboMarca.SelectedItem;
                nuevo.Categoria = (Categorias)cboCategoria.SelectedItem;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                

                negocio.agregar(nuevo);

                MessageBox.Show("agregado exitosamente");
                Close();
            }
            catch (FormatException)
            {

                MessageBox.Show(" Por favor cargar  Solo Numeros al precio.. intente nuevamente");


            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcanegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = marcanegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";


                if (articulos != null)
                {
                    txtCodigo.Text = articulos.Codigo.ToString();
                    txtNombre.Text = articulos.Nombre;
                    txtDescripcion.Text = articulos.Descripcion;
                    txtPrecio.Text = articulos.Precio.ToString();
                    txtImagenUrl.Text = articulos.ImagenUrl;
                    cargarImagen(articulos.ImagenUrl);
                    cboMarca.SelectedValue = articulos.Marca.Id;
                    cboCategoria.SelectedValue = articulos.Categoria.Id;
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ToString());
            }
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pboArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pboArticulo.Load("https://pa1.aminoapps.com/6362/3bb30f21cd66eb5f5fb6c636fc77f0a36b842f6f_hq.gif");
            }
        }

        private void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
