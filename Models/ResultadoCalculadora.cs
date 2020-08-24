using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculadoraSalarioLiquido.Models
{
    public class ResultadoCalculadora
    {
        public double SalarioLiquido { get; set; }
        public double SalarioBruto { get; set; }
        public double DescontoInss { get; set; }
        public double DeducaoDependentes { get; set; }
        public double DescontoIRRF { get; set; }

        //Construtor arredonda todos os números para duas casas decimais
        public ResultadoCalculadora( double salarioLiquido, double salarioBruto,
                                          double descontoINSS, double descontoDependentes,
                                          double descontoIRRF)
        {
            SalarioLiquido = Math.Round(salarioLiquido, 2);
            SalarioBruto = Math.Round(salarioBruto, 2);
            DescontoInss = Math.Round(descontoINSS, 2);
            DeducaoDependentes = Math.Round(descontoDependentes, 2);
            DescontoIRRF= Math.Round(descontoIRRF, 2);
        }
    }
}
