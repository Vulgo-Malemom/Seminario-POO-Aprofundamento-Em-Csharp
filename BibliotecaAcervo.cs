namespace BibliotecaAcervo
{
    public class EmprestimoLivro
    {
        public enum EstadoValido //Tipo especial para definir um conjunto de constantes nomeadas
        {
            //O livro foi emprestado
            Ativo,
            //O livro está atrasado
            Atrasado,
            //O livro foi devolvido
            Devolvido
        }
        //Adicionado a Propriedade para o status do empréstimo, com private set.
        public EstadoValido Status { get; private set; }

        //Propriedades do Empréstimo Encapsuladas com private set
        public int IdLivro { get; private set; }
        public int IdUsuario { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public decimal Multa { get; private set; }

        public EmprestimoLivro(int idLivro, int idUsuario, DateTime dataEmprestimo, DateTime dataDevolucao, decimal multa)
        {
            //Realizando as validações básicas necessárias
            if (string.IsNullOrWhiteSpace(idLivro.ToString()))
                throw new ArgumentException("O ID do livro não pode ser nulo ou vazio.", nameof(idLivro));
            if (string.IsNullOrWhiteSpace(idUsuario.ToString()))
                throw new ArgumentException("O ID do usuário não pode ser nulo ou vazio.", nameof(idUsuario));
            if (dataEmprestimo == default)
                throw new ArgumentException("A data de empréstimo é inválida.", nameof(dataEmprestimo));
            if (dataDevolucao <= dataEmprestimo)
                throw new ArgumentException("A data de devolução deve ser posterior à data de empréstimo.", nameof(dataDevolucao));
            if (multa < 0)
                throw new ArgumentException("A multa não pode ser negativa.", nameof(multa));

            //Atribuindo os valores às propriedades    
            IdLivro = idLivro;
            IdUsuario = idUsuario;
            DataEmprestimo = DateTime.Now; //Pega a data atual do sistema
            DataDevolucao = dataEmprestimo.AddDays(30); // Padrão de 30 dias para devolução
            Multa = 0;

            Status = EstadoValido.Ativo;

        }
        public void AtualizarStatus()
        {
            if (Status == EstadoValido.Devolvido)//Se o livro já foi devolvido → não altera o status.
                return;

            if (DateTime.Now > DataDevolucao)//Se passou da data de devolução → marca como atrasado.
                Status = EstadoValido.Atrasado;
            else
                Status = EstadoValido.Ativo;//Se ainda está dentro do prazo → mantém como ativo
        }
        public void RegistrarDevolucao()
        {
            if (Status == EstadoValido.Devolvido)
                throw new InvalidOperationException("O livro já foi devolvido.");

            AtualizarStatus(); //Atualiza o status antes de registrar a devolução

            if (Status == EstadoValido.Atrasado)
            {
                int diasAtraso = (DateTime.Now - DataDevolucao).Days;//Calcula os dias de atraso
                Multa = diasAtraso * 2; // Exemplo: R$2 por dia de atraso
            }

            Status = EstadoValido.Devolvido;//Marca o empréstimo como devolvido
        }
        public override string ToString()
        {
            //Usamos interpolação de string para construir uma saída legível
            //Formatos de data (d) para apresentação concisa
            return $"Empréstimo Livro [ID Livro: {IdLivro}, ID Usuário: {IdUsuario}, Data Empréstimo: {DataEmprestimo:d}, Data Devolução: {DataDevolucao:d}, Multa: {Multa}, Status: {Status}]";
        }
    }
}