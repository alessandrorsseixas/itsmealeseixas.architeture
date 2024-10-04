namespace itsmealeseixas.architeture.keygenerator
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            panel1.Visible = false;
            comboBox1.SelectedIndex = 0;
            //roundedComboBox2.SelectedIndex = 0;
        }



        private void button5_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0)
            {

                MessageBox.Show("Não foi selecionado nenhum ambiente");
            }
            else
            {
                string appToken = Program.ObterChaveSecreta(comboBox1.SelectedItem.ToString());
                if (!string.IsNullOrEmpty(appToken))
                {
                    string encryptedText = Program.GenerateKey(comboBox1.Text, appToken);
                    panel1.Visible = true;
                    label8.Text = encryptedText;
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

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}