using System;
using BibliotecaAcervo;
try
{
    // Cria um novo empréstimo
    EmprestimoLivro emprestimo = new EmprestimoLivro(
        idLivro: 1,
        idUsuario: 100,
        dataEmprestimo: DateTime.Now,
        dataDevolucao: DateTime.Now.AddDays(30),
        multa: 0
    );

    Console.WriteLine("Empréstimo criado com sucesso!");
    Console.WriteLine(emprestimo.ToString());

    // Atualiza o status (ainda deve estar Ativo)
    emprestimo.AtualizarStatus();
    Console.WriteLine("\nStatus atualizado:");
    Console.WriteLine(emprestimo.ToString());

    // Simula devolução
    emprestimo.RegistrarDevolucao();
    Console.WriteLine("\nApós devolução:");
    Console.WriteLine(emprestimo.ToString());
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}
