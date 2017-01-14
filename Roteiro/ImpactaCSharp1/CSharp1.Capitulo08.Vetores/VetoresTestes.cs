﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.VisualBasic.Devices;

namespace CSharp1.Capitulo08.Vetores
{
    [TestClass]
    public class VetoresTestes
    {
        [TestMethod]
        public void InicializacaoTeste()
        {
            var strings = new string[10];
            
            var inteiros = new int[5];
            inteiros[0] = 57;
            inteiros[1] = 18;
            Console.WriteLine("Posição 2: ", inteiros[2]); // null, sem representação textual.

            var decimais = new decimal[/*pode ser omitido - 2 e 4 quebram o build*/] { 0.5m, 0.9m, 1.59m };
            
            // Se informado, confere a quantidade informada
            //var decimais = new decimal[3] { 0.5m, 0.9m, 1.59m };
            
            // /*var não promove a inferência*/ decimal[] decimais = { 0.5m, 0.9m, 1.59m };

            // Erro de execução
            strings[-1] = "Teset";
            strings[10] = "Teset9";

            foreach (var numeroDecimal in decimais)
            {
                Console.WriteLine(numeroDecimal);
            }

            Console.WriteLine("Tamanho do vetor: {0}", decimais.Length);
        }

        [TestMethod]
        public void RedimensionamentoTeste()
        {
            var decimais = new decimal[] { 0.5m, 0.9m, 1.59m };

            //ref: aula 12
            Array.Resize(ref decimais, 5);
            
            decimais[4] = 4.4m;
        }

        [TestMethod]
        public void OrdenacaoTeste()
        {
            var decimais = new decimal[] { 1.5m, 0.9m, 1.59m };

            Array.Sort(decimais);

            Assert.AreEqual(decimais[0], 0.9m);
        }

        [TestMethod]
        public void ParamsTeste()
        {
            var decimais = new decimal[] { 1.5m, 0.9m, 1.59m };

            Console.WriteLine(Media(decimais));
            //Console.WriteLine(CalcularMedia(1.5m, 0.9m, 1.59m)); // descomentar os params abaixo.
            // Citar string.Format();
            // Só pode haver um e na última posição.
        }

        private decimal Media(/*params*/ decimal[] valores)
        {
            return valores.Average();
        }

        [TestMethod]
        public void MatrizTeste()
        {
           /*int[,]*/ var matriz = new int[3, 3];

            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    matriz[linha, coluna] = linha * coluna;
                    Console.Write("| {0} ", matriz[linha, coluna]);
                }
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void VetorDeVetoresTeste()
        {
            /*int[][]*/ var vetorDeVetores = new int[2][];

            vetorDeVetores[0] = new int[5] { 1, 3, 5, 7, 9 };
            vetorDeVetores[1] = new int[4] { 2, 4, 6, 8 };

            for (int i = 0; i < vetorDeVetores.Length; i++)
            {
                Console.Write("Elemento({0}): ", i);

                for (int j = 0; j < vetorDeVetores[i].Length; j++)
                {
                    System.Console.Write("{0} ", vetorDeVetores[i][j]);
                }
                
                System.Console.WriteLine();
            }
        }

        [TestMethod]
        public void TodaStringEhUmVetorTeste()
        {
            const string nome = "Hejlsberg";

            Assert.AreEqual(nome[0], 'H');

            //nome[2] = 'l'; // Erro

            foreach (var @char in nome)
            {
                Console.Write(@char);
            }
        }

        [TestMethod]
        public void TamanhoMaximoTeste()
        {
            // sizeof: tamanho em bytes. 
            // Console.WriteLine(sizeof(bool));           

            var vetorDeStrings = new string[0]; // bisonho, mas possível.
            //vetorDeStrings[0] = "teste"; // erro de execução.

            //var outroVetorDeStrings = new string[-1]; // build quebrado.

            var vetorDeBooleanos = new bool[int.MaxValue]; // OutOfMemoryException.

            var memoriaDisponivel = new ComputerInfo().AvailablePhysicalMemory * 0.65m;
            Console.WriteLine((memoriaDisponivel/1024m/1024m/1024m).ToString("n2"));

            var vetorDeBytes = new byte[Math.Min(int.MaxValue, Convert.ToInt32(memoriaDisponivel))]; // comentar o vetorDeBooleanos.
            var vetorDeInteiros = new int[Math.Min(int.MaxValue, Convert.ToInt32(memoriaDisponivel/4m))]; // comentar o vetorDeBytes.
        }
    }
}