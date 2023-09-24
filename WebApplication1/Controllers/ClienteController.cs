using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index(Cliente cli)
        {
            var lstVendedores = new List<Usuario>();
            using (var conexao = new Conexao())
            {
                string strVendedores = "SELECT * FROM usuarios where isExcluido = false order by nome;";
                using (var comando = new MySqlCommand(strVendedores, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var usario = new Usuario
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"])
                            };

                            lstVendedores.Add(usario);
                        }
                    ViewBag.ListaVendedores = lstVendedores;
                }
            }

            using (var conexao = new Conexao())
            {

                string strClientes = "SELECT * FROM clientes " +
                "WHERE nome like @nome and " +
                "isExcluido = false;";

                using (var comando = new MySqlCommand(strClientes, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@nome", cli.Nome + "%");

                    MySqlDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var lstClientes = new List<Cliente>();

                        while (dr.Read())
                        {
                            var cliente = new Cliente
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"]),
                                Telefone = Convert.ToString(dr["telefone"]),
                                EMail = Convert.ToString(dr["email"]),
                                DataNasc = Convert.ToDateTime(dr["dataNasc"]).ToString("dd/MM/yyyy")
                            };

                            lstClientes.Add(cliente);
                        }
                        ViewBag.ListaClientes = lstClientes;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
            }
        }

        public ActionResult NovoCliente()
        {
            var lstVendedores = new List<Usuario>();
            using (var conexao = new Conexao())
            {
                string strVendedores = "SELECT * FROM usuarios where isExcluido = false order by nome;";
                using (var comando = new MySqlCommand(strVendedores, conexao.conn))
                {
                    MySqlDataReader dr = comando.ExecuteReader();
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var usario = new Usuario
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["nome"])
                            };
                            lstVendedores.Add(usario);
                        }
                    ViewBag.ListaVendedores = lstVendedores;
                }
            }
            return View();
        }

        public ActionResult EditarCliente()
        {

            return View();
        }

        //public ActionResult Edit(int Id)
        //{
        //    using (var conexao = new Conexao())
        //    {
        //        string strLogin = "SELECT * FROM clientes WHERE Id = @Id;";

        //        using (var comando = new MySqlCommand(strLogin, conexao.conn))
        //        {
        //            comando.Parameters.AddWithValue("@Id", Id);

        //            MySqlDataReader dr = comando.ExecuteReader();
        //            dr.Read();
        //            if (dr.HasRows)
        //            {
        //                var cliente = new Cliente
        //                {
        //                    Id = Convert.ToInt32(dr["Id"]),
        //                    Nome = Convert.ToString(dr["nome"]),
        //                    EMail = Convert.ToString(dr["email"]),
        //                    DataNasc = Convert.ToString(dr["dataNasc"]),
        //                    Telefone = Convert.ToString(dr["telefone"])

        //                };
        //                return View(cliente);
        //            }
        //            else
        //            {
        //                ViewBag.ErroLogin = true;
        //                return RedirectToAction("Index");
        //            }
        //        }
        //    }
        //}
        [HttpPost]
        public ActionResult SalvarAlteracoes(Cliente cliente)
        {
            using (var conexao = new Conexao())
            {
                string strLogin = "UPDATE clientes SET nome = @nome, email = @email,dataNasc = @dataNasc, telefone = @telefone, vendedor = @vendedor  where id = @Id;";


                using (var comando = new MySqlCommand(strLogin, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@email", cliente.EMail);
                    comando.Parameters.AddWithValue("@dataNasc", cliente.DataNasc);
                    comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    comando.Parameters.AddWithValue("@vendedor", cliente.Vendedor);
                    comando.Parameters.AddWithValue("@id", cliente.Id);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index, Cliente");
                }
            }
        }

        public ActionResult Excluir(int id)
        {
            using (var conexao = new Conexao())
            {
                string sql = "SELECT * FROM clientes WHERE  id = @id";

                using (MySqlCommand comando = new MySqlCommand(@sql, conexao.conn))
                {
                    comando.Parameters.AddWithValue(@"id", id);

                    MySqlDataReader dr = comando.ExecuteReader();

                    dr.Read();
                    if (dr.HasRows)
                    {
                        var cliente = new Cliente
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            EMail = Convert.ToString(dr["email"]),
                            DataNasc = Convert.ToDateTime(dr["dataNasc"]).ToString("dd/MM/yyyy"),
                            Telefone = Convert.ToString(dr["telefone"]),
                            Vendedor = Convert.ToString(dr["vendedor"])
                        };
                        return View(cliente);
                    }
                    else
                    {
                        ViewBag.ErroCliente = true;
                        return RedirectToAction("Index", "Cliente");
                    }
                }
            }


        }
        public ActionResult Excluir(Cliente cliente)
        {
            using (var conexao = new Conexao())
            {
                string sql = "UPDATE cliente SET isExcluido = true where id = @Id;";

                using (var comando = new MySqlCommand(sql, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@id", cliente.Id);
                    comando.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }

            }
        }
        public ActionResult Salvarcliente( Cliente cliente)
        {
            using(Conexao conexao = new Conexao())
            {
                string sql = "INSERT INTO clientes (nome, email, dataNasc, telefone, vendedor) values (@nome, @email, @dataNasc, @telefone, @vendedor)";

                using (var comando = new MySqlCommand (sql, conexao.conn))
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@email", cliente.EMail);
                    comando.Parameters.AddWithValue("@dataNasc", cliente.DataNasc);
                    comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    comando.Parameters.AddWithValue("@vendedor", cliente.Vendedor);
                    comando.ExecuteNonQuery();
                    return RedirectToAction("Index", "Cliente");
                }
            }
        }
    }
}
