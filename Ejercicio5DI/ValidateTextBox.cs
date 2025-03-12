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
    public partial class ValidateTextBox : UserControl
    {
        public enum eTipo { Numerico, Textual }

        private eTipo tipo = 0;
        [Category("Examen")]
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

        public string Texto
        {
            set { txt.Text = value; }
            get { return txt.Text; }
        }

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
