﻿using System;
using System.Windows.Forms;
using Impacta.Infra.Apoio;
using Impacta.Infra.Repositorios.SistemaDeArquivos;
using Impacta.Dominio;
using System.IO;

namespace CSharp1.Capitulo04.Fretes
{
    public partial class FretesForm : Form
    {
        public FretesForm()
        {
            InitializeComponent();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                Calcular();
                
                // ToDo: implementar cathes
                try
                {
                    Gravar();

                    MessageBox.Show("Gravação realizada com sucesso!");
                    LimparFormulario();
                }
                catch
                {
                    
                }
            }
        }

        private void Gravar()
        {
            //var diretorioDeExecucao = Application.StartupPath;
            var meusDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //var caminho = Path.Combine(meusDocumentos, "Fretes.txt");
            var caminho = "c:\\Fretes.txt";
            var registro = string.Format("{0};{1};{2};{3};{4}",
                nomeClienteTextBox.Text.Trim(),
                ufComboBox.Text,
                valorTextBox.Text,
                freteLabel.Text,
                totalLabel.Text);

            using (var arquivoTexto = new StreamWriter(caminho, true))
            {
                arquivoTexto.WriteLine(registro);
            }
        }

        private void Calcular()
        {
            var percentual = 0.0m;
            var valor = Convert.ToDecimal(valorTextBox.Text);

            switch (ufComboBox.Text.ToUpper())
            {
                case "SP":
                    percentual = 0.2M;
                    break;
                case "RJ": percentual = 0.3M; break;
                case "BA":
                case "MG":
                    percentual = 0.35M;
                    break;
                case "AM":
                    percentual = 0.6m;
                    break;
                default:
                    percentual = 0.75M;
                    break;
            }

            freteLabel.Text = percentual.ToString("P1");
            totalLabel.Text = (valor * (1 + percentual)).ToString("C");
        }

        private bool ValidarFormulario()
        {
            var validacao = true;

            if (nomeClienteTextBox.Text.Trim() == string.Empty)
            {
                validacao = false;
                MessageBox.Show("Digite o nome do cliente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (ufComboBox.SelectedIndex == -1)
            {
                validacao = false;
                MessageBox.Show("Selecione o Estado.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (valorTextBox.Text.Trim() == string.Empty)
            {
                validacao = false;
                MessageBox.Show("Digite o valor do frete.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    Convert.ToDecimal(valorTextBox.Text);
                }
                catch
                {
                    validacao = false;
                    MessageBox.Show("Digite o valor do frete no formato numérico.", "Validação", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }

            return validacao;
        }

        private void gravarButtonOo_Click(object sender, EventArgs e)
        {
            if (!Formulario.ValidarCamposObrigatorios(this, fretesErrorProvider) ||
                !Formulario.ValidarTipoDosDados(this, fretesErrorProvider))
            {
                return;
            }

            var frete = new Frete();
            //frete.Cliente = clienteComboBox.SelectedItem as Cliente;
            frete.Valor = Convert.ToDecimal(valorTextBox.Text);
            frete.Uf = ufComboBox.Text;

            frete.Calcular(ufComboBox.Text, Convert.ToDecimal(valorTextBox.Text));
            freteLabel.Text = frete.Percentual.ToString("P2");
            totalLabel.Text = frete.Total.ToString("c");

            //var repositorio = new FreteRepositorio();
            //repositorio.Inserir(frete);
            new FreteRepositorio().Inserir(frete);
            MessageBox.Show("Frete gravado com sucesso!");

            Formulario.Limpar(this);
            freteLabel.Text = "0,0%";
            totalLabel.Text = "0,00";
            //clienteComboBox.Focus();
        }

        private void FretesForm_Load(object sender, EventArgs e)
        {
            //var repositorio = new ClienteRepositorio();
            //var listaClientes = repositorio.Selecionar();
            //clienteBindingSource.DataSource = listaClientes;

            //clienteBindingSource.DataSource = new ClienteRepositorio().Selecionar();

            //clienteComboBox.SelectedIndex = -1;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void LimparFormulario()
        {
            nomeClienteTextBox.Clear();
            valorTextBox.Clear();
            ufComboBox.SelectedIndex = -1;
            freteLabel.Text = string.Empty;
            totalLabel.Text = null;

            nomeClienteTextBox.Focus();
        }
    }
}