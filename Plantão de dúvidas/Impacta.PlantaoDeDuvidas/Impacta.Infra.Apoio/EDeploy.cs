﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impacta.Infra.Apoio
{
    public class EDeploy
    {
        public List<int> _caminhoNohs = new List<int>();
        public bool _nohEncontrado;

        public static void BuzzBizz()
        {
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine(DestacarMultiplos(i));
            }
        }

        private static string DestacarMultiplos(int i)
        {
            if (i % 15 == 0)
            {
                return i + "BUZZBIZZ";
            }
            else if (i % 3 == 0)
            {
                return i + "BUZZ";
            }
            else if (i % 5 == 0)
            {
                return i + "BIZZ";
            }
            else return i.ToString();
        }

        public static int SomaDeQuadrados(int[] lista)
        {
            var soma = 0;
            foreach (var elemento in lista)
            {
                soma += elemento * elemento;
            }
            return soma;
        }

        public static int Fibonacci(int quantidadeDigitos)
        {
            var fibonacci = new Stack<int>();
            fibonacci.Push(1);
            fibonacci.Push(1);

            while (fibonacci.Peek().ToString().Length < quantidadeDigitos)
            {
                var ultimo = fibonacci.Pop();
                var penultimo = fibonacci.Pop();
                var proximo = ultimo + penultimo;

                fibonacci.Push(penultimo);
                fibonacci.Push(ultimo);
                fibonacci.Push(proximo);
            }
            return fibonacci.Peek();
        }

        public void CaminhoArvore(ref Arvore arvore, int idProcurado)
        {
            _caminhoNohs.Add(arvore.Id);
            _nohEncontrado = arvore.Id == idProcurado;

            if (arvore.Filhos[0] != null && !_nohEncontrado)
                CaminhoArvore(ref arvore.Filhos[0], idProcurado);
            if (arvore.Filhos[1] != null && !_nohEncontrado)
                CaminhoArvore(ref arvore.Filhos[1], idProcurado);

            if (!_nohEncontrado)
            {
                if (arvore.Filhos[0] == null && arvore.Filhos[1] == null)
                {
                    _caminhoNohs.Remove(arvore.Id);
                    arvore = null;
                }
            }
        }
    }
}