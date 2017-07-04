using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortaParalelaControl
{
    /// <summary>
    /// Utilizam todas os 8 bits do byte para dar saida de dados para a porta paralela.
    /// </summary>
    public  class PortasData
    {
        private int valor = 00000000;

        PortaParalelaDAC portaDAC = null;

        public PortasData()
        {
 
        }

        /// <summary>
        /// Método para coventer um inteiro formado por 8 numeros, que simbolizam binários(1 e 0), que rão se tornar um valor em byte.
        /// </summary>
        /// <param name="bin">É o valor que será covnertido para byte. Este valor deve conter 8 numeros inteiros ,porém devem possuir apenas valores 1 e 0. </param>
        /// <returns>Retorna um inteiro que se encontra entre 0-255 e será usado para controlar a porta</returns>
        private  int ToByte(int bin)
        {
            string binario = bin.ToString();
            long l = Convert.ToInt32(binario, 2);
            int i = (int)l;
            return i;
        }
         

      

         

        /// <summary>
        /// Método para Analisar a booleana a ser retornada pelo método get das váriaveis. 
        /// </summary>
        /// <param name="posicao">Posicao do Led na porta Paralela.</param>
        /// <returns>Retorna se o Led está Ligado ou desligado a partir da posição na porta paralela.</returns>
        private bool CalcRetorno(int posicao)
        {
           string bintmp = valor.ToString();
           if (valor == 0 || valor.ToString().Count() < 8 - posicao)
           {
               return false;
           }
           else
           {
               if (8 - posicao == valor.ToString().Count())
               {
                   string numero = bintmp.ToCharArray().GetValue(0).ToString();
                   int retorno = Convert.ToInt32(numero);
                   return Convert.ToBoolean(retorno);
               }
               else
               {
                   string numero = bintmp.ToCharArray().GetValue(posicao).ToString();
                   int retorno = Convert.ToInt32(numero);
                   return Convert.ToBoolean(retorno);
               }
           }
        }

        


        #region LEDS

        /// <summary>
        /// Simboliza o pino 2 e é representado como D0.
        /// </summary>
        bool led1;
        public bool Led1
        {
            get {return CalcRetorno(0);}
            set
            {
                led1 = value;
                if (CalcRetorno(0))
                    valor -= 10000000;
                else
                {
                    valor += 10000000;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }
                
            }         

        }



        /// <summary>
        /// Simboliza o pino 3 e é representado como D1.
        /// </summary>
        bool led2;       
        public bool Led2
        {
            get { return CalcRetorno(1); }
            set
            {
                led2 = value;
                if (CalcRetorno(1))
                    valor -= 1000000;
                else
                {
                    valor += 1000000;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }

            }
        }


        /// <summary>
        /// Simboliza o pino 4 e é representado como D2.
        /// </summary>
        bool led3;
        public bool Led3
        {
            get { return CalcRetorno(2); }
            set
            {
                led3 = value;
                if (CalcRetorno(2))
                    valor -= 100000;
                else
                {
                    valor += 100000;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }

            }
        }



        /// <summary>
        /// Simboliza o pino 5 e é representado como D3.
        /// </summary>
        bool led4;
        public bool Led4
        {
            get { return CalcRetorno(3); }
            set
            {
                led4 = value;
                if (CalcRetorno(3))
                    valor -= 10000;
                else
                {
                    valor += 10000;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }

            }
        }



        /// <summary>
        /// Simboliza o pino 6 e é representado como D4.
        /// </summary>
        bool led5;
        public bool Led5
        {
            get { return CalcRetorno(4); }
            set
            {
                led5 = value;
                if (CalcRetorno(4))
                    valor -= 1000;
                else
                {
                    valor += 1000;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }

            }
        }




        /// <summary>
        /// Simboliza o pino 7 e é representado como D5.
        /// </summary>
        bool led6;
        public bool Led6
        {
            get { return CalcRetorno(5); }
            set
            {
                led6 = value;
                if (CalcRetorno(5))
                    valor -= 100;
                else
                {
                    valor += 100;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }

            }
        }




        /// <summary>
        /// Simboliza o pino 8 e é representado como D6.
        /// </summary>
        bool led7;
        public bool Led7
        {
            get { return CalcRetorno(6); }
            set
            {
                led7 = value;
                if (CalcRetorno(6))
                    valor -= 10;
                else
                {
                    valor += 10;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }

            }
        }




        /// <summary>
        /// Simboliza o pino 9 e é representado como D7.
        /// </summary>
        bool led8;
        public bool Led8
        {
            get { return CalcRetorno(7); }
            set
            {
                led8 = value;
                if (CalcRetorno(7))
                    valor -= 1;
                else
                {
                    valor += 1;
                    portaDAC.LigarLeds(portaDAC.PortasData, ToByte(valor));
                }

            }
        }



       

       
        #endregion



       



    }
}
