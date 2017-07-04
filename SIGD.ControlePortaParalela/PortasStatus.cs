using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortaParalelaControl
{
    public class PortasStatus
    {
        PortaParalelaDAC portaDAC = null;



        /// <summary>
        /// Método que converte byte em um int de valores 0 e 1, como se fosse binário.
        /// </summary>
        /// <param name="porta">Porta a ser utilizada para a leitura</param>
        /// <returns>Se a porta for a 889: retorna um int de 5 caracteres equivalendo a valores binarios. Se as portas forem a 890 ou  888: retorna um int de 8 caracteres com valores entre 0 e 1. </returns>
        private int ConverterLeitura(int porta)
        {
            portaDAC = new PortaParalelaDAC();

            if (porta == 889)
            {
                return Convert.ToInt32(Convert.ToString(portaDAC.lerEndereco(porta), 2)) / 1000;

            }
            else
            {
                return Convert.ToInt32(Convert.ToString(portaDAC.lerEndereco(porta), 2));
            }
        }
         /// <summary>
         /// Método que retorna True or False para dizer se o led está ligado.
         /// </summary>
         /// <param name="posicao">posição do led na porta paralela.</param>
         /// <returns>Retorna se o led está lgiado ou desligado.</returns>
        private bool CalcRetornoLer(int posicao)
        {
            string bintmp = leitura.ToString();

            if (posicao < 0)
                return false;
            else
            {
                string numero = bintmp.ToCharArray().GetValue(posicao).ToString();
                int retorno = Convert.ToInt32(numero);
                return Convert.ToBoolean(retorno);
            }
        }

        #region Leitura_Sensores
        /// <summary>
        /// váriavel que armazena o valor lido
        /// </summary>
        private int leitura = 0111;

        /// <summary>
        /// Variavel que representa o valor lido na porta 10
        /// </summary>
        bool sensor1;

        public bool Sensor1
        {
            get
            {
                leitura = ConverterLeitura(889);
                return !CalcRetornoLer(leitura.ToString().Count() - 5);
            }

        }

        /// <summary>
        /// Variavel que representa o valor lido na porta 11
        /// </summary>
        bool sensor2;

        public bool Sensor2
        {
            get
            {
                leitura = ConverterLeitura(889);
                return CalcRetornoLer(leitura.ToString().Count() - 4);
            }

        }

        /// <summary>
        /// Variavel que representa o valor lido na porta 12
        /// </summary>
        bool sensor3;

        public bool Sensor3
        {
            get
            {
                leitura = ConverterLeitura(889);
                return CalcRetornoLer(leitura.ToString().Count() - 3);
            }

        }

        /// <summary>
        /// Variavel que representa o valor lido na porta 13
        /// </summary>
        bool sensor4;

        public bool Sensor4
        {
            get
            {
                leitura = ConverterLeitura(889);
                return CalcRetornoLer(leitura.ToString().Count() - 2);
            }

        }

        /// <summary>
        /// Variavel que representa o valor lido na porta 15
        /// </summary>
        bool sensor5;

        public bool Sensor5
        {
            get
            {
                leitura = ConverterLeitura(889);
                return CalcRetornoLer(leitura.ToString().Count() - 1);
            }

        }

        #endregion
    }
}
