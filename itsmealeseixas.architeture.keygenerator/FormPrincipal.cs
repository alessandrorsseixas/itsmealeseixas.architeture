namespace itsmealeseixas.architeture.keygenerator
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            //roundedComboBox1.SelectedIndex = 0;
            //roundedComboBox2.SelectedIndex = 0;
        }

        private void btDecCopy_Click(object sender, EventArgs e)
        {
            //var result = this.lblDecRes.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (roundedComboBox1.SelectedIndex == 0)
            {

                MessageBox.Show("Não foi selecionado nenhum ambiente");
            }
            else
            {
                string appToken = Program.ObterChaveSecreta(roundedComboBox1.SelectedItem.ToString());
                if (!string.IsNullOrEmpty(appToken))
                {
                    string encryptedText = Program.GenerateKey(roundedTextBox1.Text, appToken);
                    panel1.Visible = true;
                    label7.Text = encryptedText;
                }
                else
                {
                    MessageBox.Show("Não foi selecionado nenhum ambiente");
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (roundedComboBox2.SelectedIndex == 0)
            {

                MessageBox.Show("Não foi selecionado nenhum ambiente");
            }
            else
            {
                string appToken = Program.ObterChaveSecreta(roundedComboBox2.SelectedItem.ToString());
                if (!string.IsNullOrEmpty(appToken))
                {
                    string decrypText = Program.DescriptKey(roundedTextBox2.Text, appToken);
                    panel2.Visible = true;
                    label8.Text = decrypText;
                }
                else
                {
                    MessageBox.Show("Não foi selecionado nenhum ambiente");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label7.Text);

            // Exibe uma mensagem de confirmação
            MessageBox.Show("Texto copiado para a área de transferência!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label8.Text);

            // Exibe uma mensagem de confirmação
            MessageBox.Show("Texto copiado para a área de transferência!");
        }


    }
}