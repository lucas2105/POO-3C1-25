using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POO_3C1_25.DTO;
using POO_3C1_25.BLL;


namespace POO_3C1_25
{
    public partial class Form1 : Form
    {
        BLL_Livro bllLivro = new BLL_Livro();
        DTO_Livro dtoLivro = new DTO_Livro();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Projeto desenvolvido por alunos da turma 3C1:
            //Eduardo Oliveira - 07 && Lucas Mendonça - 18
            dtg_Livros.DataSource = bllLivro.ListarLivros();
            this.PreencheIdAutor();
            this.PreencheIdEditora();
        }

        private void btn_Pesquisar_Click(object sender, EventArgs e)
        {
            string condicao = "titulo like '%" + txt_Pesquisar.Text + "%'";

            dtg_Livros.DataSource = bllLivro.PesquisarLivros(condicao);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                // Passagem dos dados da UI para o DTO
                dtoLivro.IdAutor = int.Parse(cbx_idAutor.Text);
                dtoLivro.IdEditora = int.Parse(cbx_idEditora.Text);
                dtoLivro.Titulo = txt_Titulo.Text.ToString();
                dtoLivro.NumPaginas = int.Parse(txt_Numpag.Text);
                dtoLivro.Valor = double.Parse(txt_Valor.Text);


                bllLivro.InserirLivros(dtoLivro);
                MessageBox.Show("Livro inserido com Sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtg_Livros.DataSource = bllLivro.ListarLivros();

                new LimpaForm(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Passagem dos dados da UI para o DTO
                dtoLivro.IdLivro = int.Parse(txt_idLivro.Text);
                dtoLivro.IdAutor = int.Parse(cbx_idAutor.Text);
                dtoLivro.IdEditora = int.Parse(cbx_idEditora.Text);
                dtoLivro.Titulo = txt_Titulo.Text.ToString();
                dtoLivro.NumPaginas = int.Parse(txt_Numpag.Text);
                dtoLivro.Valor = double.Parse(txt_Valor.Text);


                bllLivro.AlterarLivros(dtoLivro);
                dtg_Livros.DataSource = bllLivro.ListarLivros();
                MessageBox.Show("Livro alterado com Sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                new LimpaForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Está ação irá deletar o registro selecionado e não poderá ser desfeita, deseja continuar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    dtoLivro.IdLivro = Convert.ToInt32(txt_idLivro.Text);
                    bllLivro.ExcluirLivros(dtoLivro);
                    dtg_Livros.DataSource = bllLivro.ListarLivros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtg_Livros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_idLivro.Text = dtg_Livros.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbx_idAutor.Text = dtg_Livros.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbx_idEditora.Text = dtg_Livros.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_Titulo.Text = dtg_Livros.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_Numpag.Text = dtg_Livros.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_Valor.Text = dtg_Livros.Rows[e.RowIndex].Cells[5].Value.ToString();
            // Habilitar o botao Excluir 
            this.btnDeletar.Enabled = true;
            this.btnEditar.Enabled = true;
        }

        public void PreencheIdAutor()
        {
            cbx_idAutor.DataSource = bllLivro.ListarLivros();
            // Indicar o campo que o usuario verá no combo
            cbx_idAutor.DisplayMember = "idAutor";
            //Indicar o campo que será gravado no banco
            cbx_idAutor.ValueMember = "idAutor";
        }

        public void PreencheIdEditora()
        {
            cbx_idEditora.DataSource = bllLivro.ListarLivros();
            // Indicar o campo que o usuario verá no combo
            cbx_idEditora.DisplayMember = "idEditora";
            //Indicar o campo que será gravado no banco
            cbx_idEditora.ValueMember = "idEditora";
        }

    }
}
