using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LibreriaDeControles
{

    [DefaultProperty("Tipo")]
    [DefaultEvent("CambiaTexto")]
    public partial class ValidateTextBox : UserControl
    {
        public enum eTipo { Numerico, Textual }

        private eTipo tipo = 0;
        [Category("Properties")]
        [Description("Establece u obtiene la propiedad tipo")]
        public eTipo Tipo
        {
            set
            {
                if (tipo != value)
                {
                    tipo = value;
                }
            }
            get { return tipo; }
        }

        public ValidateTextBox()
        {
            InitializeComponent();
            this.Height = txt.Height + 20;
            txt.Width = this.Width - 20;
        }

        [Category("Properties")]
        [Description("Da acceso a la propiedad Text del textbox del componenete")]
        public string Texto
        {
            set { txt.Text = value; }
            get { return txt.Text; }
        }

        [Category("Properties")]
        [Description("Da acceso a la propiedad Multiline del textbox del componenete")]
        public bool Multilinea
        {
            set { txt.Multiline = value; }
            get { return txt.Multiline; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(new Pen(Comprobar((int)tipo)), 5, 5, this.Width - 10, this.Height - 10);
        }

        private Color Comprobar(int tipo)
        {
            if (tipo == 0)
            {
                return int.TryParse(txt.Text.Trim(), out _) ? Color.Green : Color.Red;
            }

            if (txt.Text == "")
            {
                return Color.Red;
            }

            foreach (char item in txt.Text)
            {
                if (!Char.IsLetter(item) && item != ' ')
                {
                    return Color.Red;
                }
            }
            return Color.Green;
        }

        [Category("Events")]
        [Description("Se lanza al cambiar el texto del componenete")]
        public event EventHandler CambiaTexto;

        protected virtual void OnCambiaTexto(EventArgs e)
        {
            CambiaTexto?.Invoke(this, e);
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            OnCambiaTexto(e);
            Refresh();
        }
    }
}
