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

        public ActionResult Index(Produto produto)
        {
            var lstMarca = new List<Marca>();
            using (var conexao = new Conexao())
            {
                string strMarca = "SELECT * FROM marcas ;";
                using (var comando = new MySqlCommand(strMarca, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var marca = new Marca
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"])
                            };

                            lstMarca.Add(marca);
                        }
                    ViewBag.ListaMarca = lstMarca;
                }
            }

            using (var conexao = new Conexao())
            {

                string strClientes = "SELECT * FROM produtos " +
                "WHERE nome like @nome " +
                ";";

                using (var comando = new MySqlCommand(strClientes, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@nome", produto.Nome);
                    comando.Parameters.AddWithValue("@descricao", produto.Descricao);
                    comando.Parameters.AddWithValue("@codigo_produto", produto.Descricao);
                    comando.Parameters.AddWithValue("@@preco_venda", produto.Descricao);
                    comando.Parameters.AddWithValue("@estoque_atual", produto.Descricao);
                    comando.Parameters.AddWithValue("@marca", produto.Marca);

                    MySqlDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var lstProduto = new List<Produto>();

                        while (dr.Read())
                        {
                            var produto1 = new Produto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"]),
                                Marca = Convert.ToString(dr["marca"]),

                            };

                            lstProduto.Add(produto1);
                        }
                        ViewBag.ListaProduto = lstProduto;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
            }
        }
     public ActionResult NovoProduto()
        {
            return View();
        }
            [HttpPost]
        public ActionResult SalvarProduto(Produto produto)
        {
            using(var conexao = new Conexao())
            {
                string SQL = "INSERT INTO produtos (nome,descricao, codigo_produto, preco_venda, estoque_atual, marca) Values" +
                    " (@nome,@descricao, @codigo_produto, @preco_venda, @estoque_atual, @marca)";

                using (var comando = new MySqlCommand(SQL, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@nome", produto.Nome);
                    comando.Parameters.AddWithValue("@descricao",produto.Descricao);
                    comando.Parameters.AddWithValue("@codigo_produto", produto.Descricao); 
                    comando.Parameters.AddWithValue("@@preco_venda", produto.Descricao);
                    comando.Parameters.AddWithValue("@estoque_atual", produto.Descricao);
                    comando.Parameters.AddWithValue("@marca", produto.Marca);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index","Produto");
                }

            }
        }
        

        }
           
        }
    
