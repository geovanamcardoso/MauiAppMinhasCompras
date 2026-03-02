using MauiAppMinhasCompras.Models; //As classes Models e Helpers vão trabalhar juntas, então é necessário importar o namespace Models para acessar as classes de modelo de dados.
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn; //Armazena conexão com SQLite, ou seja, o 'arquivo aberto' do banco de dados.
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path); //Inicializa a conexão com o banco de dados usando o caminho fornecido.

            _conn.CreateTableAsync<Produto>().Wait(); //Cria a tabela 'Produto' no banco de dados, se ela ainda não existir. O método Wait() é usado para aguardar a conclusão da operação assíncrona.
        }


        //Método resumido
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p); //Insere um novo registro do tipo 'Produto' no banco de dados. O método retorna uma Task<int> que representa a operação assíncrona de inserção, onde o resultado é o número de linhas afetadas.
        }

        //Método com comando sql escrito manualmente
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id = ?"; //Define a consulta SQL para atualizar um registro na tabela 'Produto', usando parâmetros para evitar injeção de SQL.

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
                );
        }
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(p => p.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'"; //Define a consulta SQL para buscar registros na tabela 'Produto' onde a descrição contém a string de busca, usando um parâmetro para evitar injeção de SQL.

            return _conn.QueryAsync<Produto>(sql);
        }
    }
}

