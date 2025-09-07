public class EmprestimoLivro
{
    public enum EstadoEmprestimo { Ativo, Concluido, Atrasado }
    private DateTime DataEmprestimo;
    private DateTime DataDevolucaoPrevista;
    private DateTime? DataDevolucaoReal;
    private decimal Multa;
    private EstadoEmprestimo Estado;
    private string IdLivro;
    private string IdUsuario;

    // Construtor
    public EmprestimoLivro(string idLivro, string idUsuario, DateTime dataEmprestimo, DateTime dataDevolucaoPrevista)
    {
        if (dataEmprestimo > dataDevolucaoPrevista)
            throw new EmprestimoInvalidoException("Data de empréstimo deve ser anterior à devolução prevista."); // pesquisar como criar essa exception sem herdar de Exception

        IdLivro = idLivro;
        IdUsuario = idUsuario;
        DataEmprestimo = dataEmprestimo;
        DataDevolucaoPrevista = dataDevolucaoPrevista;
        Estado = EstadoEmprestimo.Ativo;
        Multa = 0;
    }

    public void RegistrarDevolucao(DateTime dataDevolucao)
    {
        if (dataDevolucao < DataEmprestimo)
            throw new EmprestimoInvalidoException("Data de devolução não pode ser anterior ao empréstimo."); // pesquisar como criar essa exception sem herdar de Exception

        DataDevolucaoReal = dataDevolucao;

        if (dataDevolucao > DataDevolucaoPrevista)
        {
            CalcularMulta(dataDevolucao);
            Estado = EstadoEmprestimo.Atrasado;
        }
        else
        {
            Estado = EstadoEmprestimo.Concluido;
        }
    }

    private void CalcularMulta(DateTime dataDevolucao)
    {
        int diasAtraso = (dataDevolucao - DataDevolucaoPrevista).Days;
        Multa = diasAtraso * 2; // R$2 por dia de atraso
    }

}