using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace POO_3C1_25.DAL
{
    class DAL_Livro
    {

            //Projeto desenvolvido por alunos da turma 3C1:
            //Eduardo Oliveira - 07 && Lucas Mendonça - 18

            private MySqlConnection conexao;
            private string string_conexao = "Persist security info = false; " +
                                            "server = localhost; " +
                                            "database = bd_Livraria; " +
                                            "user = root ; pwd=; " +
                                            "port = 3306";
            public void conectar()
            {
                try
                {
                    conexao = new MySqlConnection(string_conexao);
                    conexao.Open();
                }
                catch (MySqlException e)
                {
                    throw new Exception("Problemas ao conectar com o banco de dados. \nErro: " + e.Message);
                }

            }

            public void executarComando(string sql)
            {
                try
                {
                    conectar();
                    MySqlCommand comando = new MySqlCommand(sql, conexao);
                    comando.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    throw new Exception("Não foi possivel executar o comando no Banco de Dados. \nErro: " + e.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }

            public DataTable executarConsulta(string sql)
            {
                try
                {
                    conectar();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter dados = new MySqlDataAdapter(sql, conexao);
                    dados.Fill(dt);
                    return dt;
                }
                catch (MySqlException e)
                {
                    throw new Exception("Não foi possivel selecionar os registros no Banco de Dados. \nErro: " + e.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }

    }
}
