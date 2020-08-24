using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using CalculadoraSalarioLiquido.Models;

namespace CalculadoraSalarioLiquido.Utils
{
    public class Calculadora
    {
        //Tetos das faixas salariais do INSS
        public List<double> FaixasINSS { get; set; }
        public List<double> AliquotasINSS { get; set; }
        public double TetoInss { get; set; }

        public double DeducaoPorDependente { get; set; }

        public List<double> FaixasIRRF { get; set; }
        public List<double> AliquotasIRRF { get; set; }

        public List<double> ParcelasDedutiveis { get; set; }

        //Inicia a calculadora com valores padrão.
        public Calculadora()
        {
            FaixasINSS = new List<double>();
            FaixasINSS.AddRange(new double[] { 1045, 2089.6, 3134.4, 6101.06 });
            
            AliquotasINSS = new List<double>();
            AliquotasINSS.AddRange(new double[] { 0.075, 0.09, 0.12, 0.14 });

            TetoInss = 713.1;
            
            DeducaoPorDependente = 189.59;
            
            FaixasIRRF = new List<double>();
            FaixasIRRF.AddRange(new double[] { 1903.98, 2826.65, 3751.05, 4664.68 });

            AliquotasIRRF = new List<double>();
            AliquotasIRRF.AddRange(new double[] { 0, 0.075, 0.15, 0.225, 0.275 });

            ParcelasDedutiveis = new List<double>();
            ParcelasDedutiveis.AddRange(new double[] { 0, 142.8, 354.8, 636.13, 869.36 });
        }

        //Requer: salario >= 0
        //Requer: dependentes >= 0
        public ResultadoCalculadora CalculaSalarioLiquido(double salario, int dependentes)
        {
            double salarioLiquido = salario;
            
            double descontoInss = CalculaDescontoInss(salario);
            salarioLiquido -= descontoInss;

            double deducaoDependentesTotal = CalculaDeducaoPorDependentes(salario, dependentes);
            double salarioBaseIRRF = salarioLiquido - deducaoDependentesTotal;

            double descontoIRRF = CalculaDescontoIRRF(salarioBaseIRRF);
            salarioLiquido -= descontoIRRF;

            return new ResultadoCalculadora (
                salarioLiquido,
                salario,
                descontoInss,
                deducaoDependentesTotal,
                descontoIRRF
            );
        }

        public double CalculaDescontoInss(double salario)
        {
            //Teto de contribuição.
            if (salario > FaixasINSS[FaixasINSS.Count - 1]) return TetoInss;

            //Sobe nas faixas salariais somando os decontos de cada uma:
            double faixaPiso = 0;
            double desconto = 0;
            for (int i = 0; i < FaixasINSS.Count; i++)
            {
                if (salario <= FaixasINSS[i])
                {
                    desconto += (salario - faixaPiso) * AliquotasINSS[i];
                    //Encontrou a faixa salarial na qual o valor do salario bruto se encaixa. Encerra o laço.
                    break;
                }
                else
                {
                    desconto += (FaixasINSS[i] - faixaPiso) * AliquotasINSS[i];
                    //Antes de passar para a faixa seguinte, define o novo piso.
                    faixaPiso = FaixasINSS[i];
                }
            }

            return desconto;
        }

        public double CalculaDeducaoPorDependentes(double salario, int dependentes)
        {
            return Math.Min(dependentes * DeducaoPorDependente, salario);
        }

        //Entrada salario deve ter sido previamente deduzida de contribuição ao INSS e desconto por dependentes.
        public double CalculaDescontoIRRF(double salario)
        {
            for (int i = 0; i<FaixasIRRF.Count; i++)
            {
                if (salario <= FaixasIRRF[i])
                {
                    return salario * AliquotasIRRF[i] - ParcelasDedutiveis[i];
                }
            }

            //Aliquota máxima
            return salario * AliquotasIRRF[AliquotasIRRF.Count - 1] - ParcelasDedutiveis[ParcelasDedutiveis.Count - 1];
        }
    }
}
