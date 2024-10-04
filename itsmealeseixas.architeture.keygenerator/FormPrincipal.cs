namespace itsmealeseixas.architeture.keygenerator
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            panel1.Visible = false;
            panel5.Visible = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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
                    string encryptedText = Program.GenerateKey(textBox1.Text, appToken);
                    panel1.Visible = true;
                    label8.Text = encryptedText;
                }
                else
                {
                    MessageBox.Show("Não foi selecionado nenhum ambiente");
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {

                MessageBox.Show("Não foi selecionado nenhum ambiente");
            }
            else
            {
                string appToken = Program.ObterChaveSecreta(comboBox2.SelectedItem.ToString());
                if (!string.IsNullOrEmpty(appToken))
                {
                    string decrypText = Program.DescriptKey(textBox2.Text, appToken);
                    panel5.Visible = true;
                    label15.Text = decrypText;
                }
                else
                {
                    MessageBox.Show("Não foi selecionado nenhum ambiente");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label15.Text);

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