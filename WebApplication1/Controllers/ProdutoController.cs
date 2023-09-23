using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {


            using (var conexao_bd = new Conexao())
            {

                string sql = "select * from produtos  where excluido = false";
                using (MySqlCommand comando = new MySqlCommand(sql, conexao_bd.conn))
                {
                   
                    MySqlDataReader dr = comando.ExecuteReader();
                  
                        if (dr.HasRows) 
                       {
                        var lstClinte = new List<Cliente>();
                        while (dr.Read())
                        {

                            var produto = new Produto
                            {
                                Id = Convert.ToInt32(dr["id"]),

                                Descricao = Convert.ToString(dr["descricao"]),

                                codigo_produto = Convert.ToString(dr["codigo_produto"]),

                                Preco_venda = Convert.ToDouble(dr["preco_venda"]),

                                Estoque_atual = Convert.ToInt32(dr["estoque_atual"])
                            };
                        }
                            //lstClinte.Add(produto);
                        }
                        return View();

                        


                    
                    }

                }
            }
        [HttpPost]
        public ActionResult SalvarProduto(Produto produto)
        {
            using(var conexao = new Conexao())
            {
                string SQL = "INSERT INTO produtos (descricao, codigo_produto, preco_venda, estoque_atual, marca) Values" +
                    " (@descricao, @codigo_produto, @preco_venda, @estoque_atual, @marca)";

                using (var comando = new MySqlCommand(SQL, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@descricao",produto.Descricao);
                    comando.Parameters.AddWithValue("@codigo_produto", produto.Descricao); 
                    comando.Parameters.AddWithValue("@@preco_venda", produto.Descricao);
                    comando.Parameters.AddWithValue("@estoque_atual", produto.Descricao);
                    comando.Parameters.AddWithValue("@marca", produto.Descricao);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }

            }
        }
        

        }
           
        }
    
