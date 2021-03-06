﻿using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System;

namespace CSharp1.Capitulo04.Fretes
{
    public partial class ListagemFretesForm : Form
    {
        public ListagemFretesForm()
        {
            InitializeComponent();

            // Preferível, pois no construtor, os controles são criados todos em um único lote. Assim, o layout dos 
            // controles, por exemplo, é tratado de uma vez.
            PopularListBoxFretes();
            this.ActiveControl = nomeClienteToolStripTextBox.Control;
        }

        private void PopularListBoxFretes()
        {
            try
            {
                var fretes = Selecionar(nomeClienteToolStripTextBox.Text);

                var cabecalho = string.Concat("Cliente".PadRight(50),
                    "Estado".PadRight(10),
                    "Valor".PadLeft(20),
                    "Percentual".PadLeft(20),
                    "Total".PadLeft(20));

                fretes.Insert(0, cabecalho);

                fretesListBox.DataSource = fretes;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("O caminho do arquivo de fretes não foi encontrado. A gravação não foi realizada.");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("O arquivo Fretes.txt não tem permissão de gravação.");
                //File.SetAttributes("C:\\Fretes.txt", FileAttributes.Normal);
            }
            catch (Exception excecao)
            {
                MessageBox.Show("Ooops! Houve um erro e a gravação não foi realizada. O suporte já foi comunicado.");
                //_log.Error(excecao);
            }
            finally
            {
                // Opcional - se presente, é executado sempre, independente de sucesso, erro ou qualquer return.
            }
        }

        private List<string> Selecionar(string nomeCliente = null)
        {
            var fretes = new List<string>();
            var caminho = ConfigurationManager.AppSettings["caminhoArquivoFretes"];
            var arquivoFretes = new StreamReader(caminho);

            //using (var arquivoFretes = new StreamReader(arquivoFretes))
            //{
            while (!arquivoFretes.EndOfStream)
            {
                var registro = arquivoFretes.ReadLine();
                
                //var propriedades = registro.Split(';');

                //if (!string.IsNullOrEmpty(nomeCliente))
                //{
                //    if (!registro[0].ToUpper().Contains(nomeCliente.ToUpper()))
                //    {
                //        continue;
                //    }
                //}

                //var frete = string.Concat(
                //    registro[0].PadRight(50),/*Nome*/
                //    registro[1].PadRight(10),
                //    registro[2].PadLeft(20),
                //    registro[3].PadLeft(20),
                //    registro[4].PadLeft(20));

                //fretes.Add(frete);
                
                fretes.Add(registro);
            }
            //}

            arquivoFretes.Close();

            return fretes;
        }

        private void pesquisarToolStripButton_Click(object sender, System.EventArgs e)
        {
            PopularListBoxFretes();

            // Se usar tab para focar o botão Pesquisar, nem o bloco abaixo não tem efeito.
            //ActiveControl = nomeClienteToolStripTextBox.Control;
            //nomeClienteToolStripTextBox.Focus();
        }

        private void nomeClienteToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            PopularListBoxFretes();
        }
    }
}