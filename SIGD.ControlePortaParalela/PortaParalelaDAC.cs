using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PortaParalelaControl
{
    /// <summary>
    /// Classe para controle da Porta Paralela utilizando-se da dll INPOUT32.
    /// </summary>
    class PortaParalelaDAC
    {
        /// <summary>
        /// Construtor Publico Vazio.
        /// </summary>
        public PortaParalelaDAC()
        {
 
        }

        /// <summary>
        /// Construtor que seta o endereço a ser usado para várias aplicações posteriores.
        /// </summary>
        /// <param name="enderecoClass">Endereço na porta paralela.</param>
        public PortaParalelaDAC(int enderecoClass)
        {
            this.enderecoClass = enderecoClass;
        }

        /// <summary>
        /// Váriavel que armazena o endereço a ser usado.
        /// </summary>
        private int enderecoClass = 0;


        /// <summary>
        /// Endereço que dá acesso as portas acess.
        /// Permitem output de dados somente(unidirecionais).
        /// São as portas 2 à 9.
        /// </summary>
        public int PortasData = 888;


        /// <summary>
        /// Endereço que dá acesso as portas de status. 
        /// Permitem a leitura de valores somente (unidireciaonais).
        /// São as portas 10 à 13 e 15.
        /// </summary>
        public int PortasStatus = 889;


        /// <summary>
        /// Endereço que dá acesso as portas de Controle.
        /// Permitem a leitura e gravação de dados (multidirecionais).
        /// sdão as portas 1,14,16 e 17.
        /// </summary>
        public int PortasControl = 890;

        /// <summary>
        /// Método que utiliza a dll INPOUT para controlar a porta paralela, neste caso especifico, enviar dados á ela.
        /// </summary>
        /// <param name="address">Endereço utilizado para enviar os valores para a portas paralela (Data ou Control).</param>
        /// <param name="value">Valor a ser escrito na porta paralela.</param>
        [DllImport("inpout32.dll", EntryPoint = "Out32")]
        private static extern void Output(int address, int value);

        /// <summary>
        /// Método que utiliza a dll INPOUT para ler dados provenientes da porta paralela.
        /// </summary>
        /// <param name="address">Endereço a ser lido na porta paralela(Status).</param>
        /// <returns>retorna o valor encontrado na porta paralela.</returns>
        [DllImport("inpout32.dll", EntryPoint = "Inp32")]
        private static extern int Input(int address);

        /// <summary>
        /// Método que permite acesso direto a todas as portas e endereços disponiveis.
        /// </summary>      
        /// <param name="valor">Valor decimal dos Leds que devemser ligados.</param>
        public void LigarLeds(int valor)
        {
            Output(enderecoClass, valor);
        }


        /// <summary>
        /// Método que permite a leitura de Valores no endereço especificado.
        /// </summary>
        /// <returns>Retorna um valor inteiro que indica quais Leds estão ativados, este valor pode ser convertido para binário.</returns>
        public int lerEndereco()
        {
            return Input(enderecoClass);
        }


        /// <summary>
        /// Método que permite acesso direto a todas as portas e endereços disponiveis.
        /// </summary>
        /// <param name="endereco"> Valores dos endereços da porta, são pré definidos como variaveis da classe.
        /// Use 888,889, ou 890 .</param>
        /// <param name="valor">Valor decimal dos Leds que devemser ligados.</param>
        public void LigarLeds(int endereco,int valor)
        {
            Output(endereco, valor);
        }
        
        
        /// <summary>
        /// Método que permite a leitura de Valores no endereço especificado.
        /// </summary>
        /// <param name="endereco">Endereço dos pinos da porta paralela .</param>
        /// <returns>Retorna um valor inteiro que indica quais Leds estão ativados, este valor pode ser convertido para binário. </returns>
        public int lerEndereco(int endereco)
        {
            return Input(endereco);
        }


     

    }
}
