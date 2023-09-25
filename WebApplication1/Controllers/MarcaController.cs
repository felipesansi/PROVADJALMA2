using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MarcaController : Controller
    {
        //
        public ActionResult Index()
        {
            using (var conexao = new Conexao())
            {
                string sql = "SELECT * FROM marcas;";
                using (var comando = new MySqlCommand(sql, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                    {
                        var lstMarca = new List<Marca>();

                        while (dr.Read())
                        {
                            var marca = new Marca
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Nome = Convert.ToString(dr["nome"]),

                            };

                            lstMarca.Add(marca);
                        }
                        return View(lstMarca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }
                }
            }

        }

        
            


            public ActionResult SalvarMarca(Marca marca)
            {
                using (var conexao = new Conexao())
                {
                    string sql = "INSERT INTO marcas (nome) values(@nome)";
                    using (MySqlCommand comando = new MySqlCommand(sql, conexao.conn))
                    {
                        comando.Parameters.AddWithValue("nome", marca.Nome);
                        comando.ExecuteNonQuery();
                        return RedirectToAction("Index");

                    }


                }

            }

        public ActionResult Edit(int Id)
        {
            using (var conexao = new Conexao())
            {
                string strLogin = "SELECT * FROM marcas " +
                                  "WHERE id = @Id;";

                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@Id", Id);

                    MySqlDataReader dr = comando.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        var marca = new Marca
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nome = Convert.ToString(dr["nome"]),

                        };
                        return View(marca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }
                }
            }
        }



        public ActionResult Visualizar(int Id)
        {
            using (var conexao = new Conexao())
            {
                string strLogin = "SELECT * FROM marcas " +
                                  "WHERE id = @Id;";

                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@Id", Id);

                    MySqlDataReader dr = comando.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        var marca = new Marca
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nome = Convert.ToString(dr["nome"]),

                        };
                        return View(marca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }

                }
            }
        }


        public ActionResult Excluir(int Id)
        {
            using (var conexao = new Conexao())
            {
                string strLogin = "SELECT * FROM marcas " +
                                  "WHERE Id = @Id;";

                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@Id", Id);

                    MySqlDataReader dr = comando.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        var marca = new Marca
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nome = Convert.ToString(dr["nome"]),

                        };
                        return View(marca);
                    }
                    else
                    {
                        ViewBag.ErroLogin = true;
                        return RedirectToAction("Index");
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Excluir(Marca marcas)
        {
            // DELETE DE FATO

            using (var conexao = new Conexao())
            {
                string strLogin = "DELETE FROM marcas " +
                                    "where id = @Id;";

                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@id", marcas.Id);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index", "Marca");
                }
            }



        }

        [HttpPost]
        public ActionResult SalvarAlteracoes(Usuario marcas)
        {
            using (var conexao = new Conexao())
            {
                string sql = "UPDATE marcas SET " +
                                    "nome = @nome " +
                                    "where id = @Id;";


                using (var comando = new MySqlCommand(sql, conexao.conn))
                {
                   
                  
                    comando.Parameters.AddWithValue("@nome",marcas.Nome);
                    
                    comando.Parameters.AddWithValue("@id",marcas.Id);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index","Marca");
                }
            }
        }
        public ActionResult NovaMarca()
        {
            return View();
        }



    }
    }

