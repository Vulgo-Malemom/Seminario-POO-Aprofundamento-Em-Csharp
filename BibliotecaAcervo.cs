namespace BibliotecaAcervo
{
    public class EmprestimoLivro
    {
        public enum EstadoValido { Ativo, Atrasado, Devolvido }
        public EstadoValido Status { get; private set; }

        public int IdLivro { get; private set; }
        public int IdUsuario { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public decimal Multa { get; private set; }

        public EmprestimoLivro(int idLivro, int idUsuario, DateTime dataEmprestimo, DateTime dataDevolucao, decimal multa)
        {
            if (string.IsNullOrWhiteSpace(idLivro.ToString()))
                throw new ArgumentException("O ID do livro não pode ser nulo ou vazio.", nameof(idLivro));
            if (string.IsNullOrWhiteSpace(idUsuario.ToString()))
                throw new ArgumentException("O ID do usuário não pode ser nulo ou vazio.", nameof(idUsuario));
            if (dataEmprestimo == default)
                throw new ArgumentException("A data de empréstimo é inválida.", nameof(dataEmprestimo));
            if (dataDevolucao <= dataEmprestimo)
                throw new ArgumentException("A data de devolução deve ser posterior à data de empréstimo.", nameof(dataDevolucao));
            if (multa <= 0)
                throw new ArgumentException("A multa não pode ser negativa.", nameof(multa));

            IdLivro = idLivro;
            IdUsuario = idUsuario;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = dataEmprestimo.AddDays(30); // Padrão de 30 dias para devolução
            Multa = 0;

            Status = EstadoValido.Ativo;

        }
        public void AtualizarStatus()
        {
            if (Status == EstadoValido.Devolvido)
                return;

            if (DateTime.Now > DataDevolucao)
                Status = EstadoValido.Atrasado;
            else
                Status = EstadoValido.Ativo;
        }
        public void RegistrarDevolucao()
        {
            if (Status == EstadoValido.Devolvido)
                throw new InvalidOperationException("O livro já foi devolvido.");

            AtualizarStatus();

            if (Status == EstadoValido.Atrasado)
            {
                int diasAtraso = (DateTime.Now - DataDevolucao).Days;
                Multa = diasAtraso * 2; // Exemplo: R$2 por dia de atraso
            }

            Status = EstadoValido.Devolvido;
        }
        public override string ToString()
        {
            return $"Empréstimo Livro [ID Livro: {IdLivro}, ID Usuário: {IdUsuario}, Data Empréstimo: {DataEmprestimo}, Data Devolução: {DataDevolucao}, Multa: {Multa}, Status: {Status}]";
        }
    }
}