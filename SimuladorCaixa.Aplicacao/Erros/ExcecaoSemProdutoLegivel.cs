using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.Erros
{
    public sealed class ExcecaoSemProdutoElegivel : Exception
    {
        public ExcecaoSemProdutoElegivel()
            : base("Nenhum produto de investimento elegível foi encontrado para os critérios informados.")
        {
        }
    }
}
