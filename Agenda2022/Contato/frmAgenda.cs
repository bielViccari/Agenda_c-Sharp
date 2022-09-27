using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

//salvar e ler o arquivo
using System.IO;

namespace Contato
{
    public partial class frmAgenda : Form
    {

        public frmAgenda()
        {
            InitializeComponent();            
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(
                "Quer realmente fechar a aplicação?",
                "Informação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if ((txtNome.Text != "") && (txtFone.Text != "") && (cmbSexo.Text != ""))
            {
                lstAgenda.Items.Add(txtNome.Text + "|" + txtFone.Text + "|" + cmbSexo.Text);

                btnNovo.PerformClick();

                //btnNovo_Click(sender, e);

            }
            else
            {
                MessageBox.Show(
                    "Preencha todos os campos!",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtNome.Focus();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtFone.Clear();
            cmbSexo.Text = "";
            txtNome.Focus();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if(lstAgenda.SelectedIndex >= 0)
            {
                string[] campos;
                string selecionado = lstAgenda.SelectedItem.ToString();
                campos = selecionado.Split('|');

                txtNome.Text = campos[0];
                txtFone.Text = campos[1];
                cmbSexo.Text = campos[2];

                lstAgenda.Items.RemoveAt(lstAgenda.SelectedIndex);
            }
            else
            {
                MessageBox.Show(
                    "Selecione um registro da Lista!",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(lstAgenda.SelectedIndex >= 0)
            {
                if(MessageBox.Show(
                    "Deseja realmente excluir o registro selecionado!", 
                    "Informação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    lstAgenda.Items.RemoveAt(lstAgenda.SelectedIndex);
                }
                else
                {
                    MessageBox.Show(
                        "Ação cancelada pelo usuário!",
                        "Informação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (lstAgenda.Items.Count > 0) //verifica se há contatos na agenda
            {                
                FileInfo salvarArquivo = new FileInfo(@".\bk_agenda.txt");

                if (!salvarArquivo.Exists) // verifica o o bk_agenda.txt existe
                {
                    using (StreamWriter sa = salvarArquivo.CreateText())
                    {
                        for (int i = 0; i < lstAgenda.Items.Count; i++)
                        {
                            sa.WriteLine(lstAgenda.Items[i].ToString());
                        }
                    }
                    MessageBox.Show(
                        "Os dados foram exportados com sucesso!",
                        "Informação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    if(MessageBox.Show(
                        "Deseja sobrepor o arquivo de backup?",
                        "Informação",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation)== DialogResult.Yes)
                    {
                        using (StreamWriter sa = salvarArquivo.CreateText())
                        {
                            for (int i = 0; i < lstAgenda.Items.Count; i++)
                            {
                                sa.WriteLine(lstAgenda.Items[i].ToString());
                            }
                        }
                        MessageBox.Show(
                            "Os dados foram exportados com sucesso!",
                            "Informação",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "A agenda está vazia!",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
