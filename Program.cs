using DataAccess;
using Dominio;

public partial class Program
{
    public static Contexto contexto = new Contexto();
    public static ClienteRepositorio clienteRepositorio = new ClienteRepositorio(contexto);
    public static ProdutoRepositorio produtoRepositorio = new ProdutoRepositorio(contexto);
    public static PedidoRepositorio pedidoRepositorio = new PedidoRepositorio(contexto);
    public static PedidoProdutoRepositorio pedidoProdutoRepositorio = new PedidoProdutoRepositorio(contexto);
    public static Cliente cliente = new Cliente();
    public static Produto produto = new Produto();
    public static Pedido pedido = new Pedido();
    public static PedidoProduto pedidoProduto = new PedidoProduto();
    public static string nomeCliente = string.Empty;
    public static string nomeProduto = string.Empty;
    public static Cliente clienteConsultaDoBanco;
    public static Produto produtoConsultaDoBanco;
    public static int opcao;


    public static void Main(string[] args)
    {
        using (contexto)
        {
            Menu();
        }
    }

    static void Menu()
    {
        Console.WriteLine("Onde você deseja acessar? \n 1- Clientes \n 2- Produtos \n 3- Pedidos \n 0- Sair");
        int opcao = int.Parse(Console.ReadLine());

        switch ((MenuEnum)opcao)
        {
            case MenuEnum.Clientes:
                Clientes();
                break;

            case MenuEnum.Produtos:
                Produtos();
                break;

            case MenuEnum.Pedidos:
                Pedidos();
                break;

            case MenuEnum.Sair:
                Console.WriteLine("Obrigada e volte sempre!");
                break;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    static void Clientes()
    {
        do
        {
            Console.WriteLine("O que você deseja fazer? \n 1- Adicionar \n 2- Atualizar Email \n 3- Consultar \n 4- Remover \n 5- Menu Inicial \n 0- Sair");
            opcao = int.Parse(Console.ReadLine());

            switch ((ClientesEnum)opcao)
            {
                case ClientesEnum.Adicionar:

                    Console.WriteLine("Digite o nome do cliente:");
                    cliente.Nome = Console.ReadLine();

                    Console.WriteLine("Digite o email do cliente:");
                    cliente.Email = Console.ReadLine();

                    cliente.Ativo = true;

                    clienteRepositorio.Salvar(cliente);

                    clienteRepositorio.Listar(cliente.Ativo);

                    Console.WriteLine("Cliente adicionado com sucesso!");
                    break;

                case ClientesEnum.Atualizar:

                    Console.WriteLine("Digite o nome do usuário que deseja atualizar:");
                    nomeCliente = Console.ReadLine();

                    var clienteDoBanco = clienteRepositorio.ObterClientePorNome(nomeCliente);

                    if (clienteDoBanco != null)
                    {
                        Console.WriteLine($"Cliente encontrado com sucesso! O cliente {clienteDoBanco.Nome} possui o e-mail {clienteDoBanco.Email}");

                        Console.WriteLine("Digite um novo e-mail:");
                        var novoEmail = Console.ReadLine();

                        clienteDoBanco.Email = novoEmail;

                        clienteRepositorio.Atualizar(clienteDoBanco);

                        Console.WriteLine("Cliente atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado!");
                    }

                    break;

                case ClientesEnum.Consultar:

                    Console.WriteLine("Digite o nome do usuário que deseja consultar:");
                    nomeCliente = Console.ReadLine();

                    var clienteDoBanco2 = clienteRepositorio.ObterClientePorNome(nomeCliente);

                    if (clienteDoBanco2 != null)
                    {
                        Console.WriteLine($"Cliente encontrado com sucesso! O cliente {clienteDoBanco2.Nome} possui o e-mail {clienteDoBanco2.Email}");
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado!");
                    }

                    break;

                case ClientesEnum.Remover:

                    Console.WriteLine("Digite o nome do usuário que deseja remover:");
                    nomeCliente = Console.ReadLine();

                    var clienteRemoverDoBanco = clienteRepositorio.ObterClientePorNome(nomeCliente);

                    if (clienteRemoverDoBanco != null)
                    {
                        clienteRepositorio.RemoverCliente(clienteRemoverDoBanco);

                        Console.WriteLine("Cliente removido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado.");
                    }

                    break;

                case ClientesEnum.MenuInicial:
                    Menu();
                    break;

                case ClientesEnum.Sair:
                    Console.WriteLine("Obrigada e volte sempre!");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 0);
    }
    static void Produtos()
    {
        do
        {
            Console.WriteLine("O que você deseja fazer? \n 1- Adicionar \n 2- Atualizar \n 3- Consultar \n 4- Remover \n 5- Menu Inicial \n 0- Sair");
            opcao = int.Parse(Console.ReadLine());

            switch ((ProdutosEnum)opcao)
            {
                case ProdutosEnum.Adicionar:
                    Console.WriteLine("Digite o nome do produto:");
                    produto.NomeProduto = Console.ReadLine();

                    Console.WriteLine("Informe o preço do produto:");
                    produto.Preco = decimal.Parse(Console.ReadLine());

                    produtoRepositorio.SalvarProduto(produto);

                    Console.WriteLine("Produto adicionado com sucesso!");
                    break;

                case ProdutosEnum.Atualizar:
                    Console.WriteLine("Digite o nome do produto que deseja atualizar:");
                    nomeProduto = Console.ReadLine();

                    var produtoDoBanco = produtoRepositorio.ObterProduto(nomeProduto);

                    if (produtoDoBanco != null)
                    {
                        Console.WriteLine("Produto encontrado com sucesso!");

                        Console.WriteLine("Digite o nome do novo produto:");
                        string novoNomeProduto = Console.ReadLine();

                        produtoDoBanco.NomeProduto = novoNomeProduto;

                        Console.WriteLine("Digite o novo preço do produto:");
                        decimal novoPreco = decimal.Parse(Console.ReadLine());

                        produtoDoBanco.Preco = novoPreco;

                        produtoRepositorio.AtualizarProduto(produtoDoBanco);

                        Console.WriteLine("Produto atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado!");
                    }

                    break;

                case ProdutosEnum.Consultar:
                    Console.WriteLine("Digite o nome do produto que deseja consultar:");
                    nomeProduto = Console.ReadLine();

                    produtoDoBanco = produtoRepositorio.ObterProduto(nomeProduto);

                    if (produtoDoBanco != null)
                    {
                        Console.WriteLine("Produto encontrado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado!");
                    }
                    break;

                case ProdutosEnum.Remover:
                    Console.WriteLine("Digite o nome do produto que deseja remover:");
                    nomeProduto = Console.ReadLine();

                    var produtoRemoverDoBanco = produtoRepositorio.ObterProduto(nomeProduto);

                    if (produtoRemoverDoBanco != null)
                    {
                        produtoRepositorio.RemoverProduto(produtoRemoverDoBanco);

                        Console.WriteLine("Produto removido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado.");
                    }
                    break;

                case ProdutosEnum.MenuInicial:
                    Menu();
                    break;

                case ProdutosEnum.Sair:
                    Console.WriteLine("Obrigada e volte sempre!");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 0);
    }
    static void Pedidos()
    {
        do
        {
            Console.WriteLine("O que você deseja fazer? \n 1- Adicionar \n 2- Remover \n 3- Menu Inicial \n 0- Sair");
            opcao = int.Parse(Console.ReadLine());

            switch ((PedidosEnum)opcao)
            {
                case PedidosEnum.Adicionar:

                    Console.WriteLine("Digite o nome do produto que deseja adicionar o pedido:");
                    nomeProduto = Console.ReadLine();

                    produtoConsultaDoBanco = produtoRepositorio.ObterProduto(nomeProduto);

                    if (produtoConsultaDoBanco != null)
                    {
                        Console.WriteLine($"Produto encontrado: {produtoConsultaDoBanco.NomeProduto} | Preço: {produtoConsultaDoBanco.Preco}");

                        Console.WriteLine("Digite o nome do usuário que deseja adicionar o pedido:");
                        nomeCliente = Console.ReadLine();

                        clienteConsultaDoBanco = clienteRepositorio.ObterClientePorNome(nomeCliente);

                        if (clienteConsultaDoBanco != null)
                        {
                            Console.WriteLine($"Cliente encontrado com sucesso! O cliente {clienteConsultaDoBanco.Nome} possui o e-mail {clienteConsultaDoBanco.Email}");

                            Console.WriteLine($"Data do Pedido: {pedido.Data = DateTime.Now}");

                            Console.WriteLine("Digite o valor total do pedido:");
                            pedido.ValorTotal = decimal.Parse(Console.ReadLine());

                            pedido.ClienteID = clienteConsultaDoBanco.Id;

                            int idPedido = pedidoRepositorio.SalvarPedido(pedido);

                            pedidoProduto.PedidoId = idPedido;
                            pedidoProduto.ProdutoId = produtoConsultaDoBanco.Id;

                            pedidoProdutoRepositorio.Salvar(pedidoProduto);
                            Console.WriteLine("Pedido adicionado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado.");
                    }

                    break;


                case PedidosEnum.Remover:
                    Console.WriteLine("Digite o nome do usuário que deseja remover o pedido:");
                    nomeCliente = Console.ReadLine();

                    clienteConsultaDoBanco = clienteRepositorio.ObterClientePorNome(nomeCliente);

                    if (clienteConsultaDoBanco != null)
                    {
                        Console.WriteLine($"Cliente encontrado com sucesso! O cliente {clienteConsultaDoBanco.Nome} possui o e-mail {clienteConsultaDoBanco.Email}");

                        // Obtém os pedidos do cliente
                        var pedidos = pedidoRepositorio.ObterPedidosPorCliente(clienteConsultaDoBanco.Id);

                        if (pedidos.Any())
                        {
                            // Lista os pedidos para o usuário
                            Console.WriteLine("Pedidos do cliente:");
                            foreach (var pedido in pedidos)
                            {
                                Console.WriteLine($"ID: {pedido.Id}, Data: {pedido.Data}, Valor: {pedido.ValorTotal}");
                            }

                            // Solicita o ID do pedido para remoção
                            Console.WriteLine("Digite o ID do pedido que deseja remover:");
                            if (int.TryParse(Console.ReadLine(), out int idPedido))
                            {
                                var pedidoParaRemover = pedidos.FirstOrDefault(p => p.Id == idPedido);

                                if (pedidoParaRemover != null)
                                {
                                    pedidoRepositorio.RemoverPedido(pedidoParaRemover);
                                    Console.WriteLine("Pedido removido com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Pedido não encontrado.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("ID do pedido inválido.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("O cliente não possui pedidos.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado.");
                    }
                    break;

                case PedidosEnum.MenuInicial:
                    Menu();
                    break;

                case PedidosEnum.Sair:
                    Console.WriteLine("Obrigada e volte sempre!");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 0);
    }

    // static void PedidosProdutos()
    // {
    //     do
    //     {
    //         Console.WriteLine("O que você deseja fazer? \n 1- Adicionar \n 2- Remover \n 3- Listar \n 4- Menu Inicial \n 0- Sair");
    //         opcao = int.Parse(Console.ReadLine());

    //         switch ((PedidosProdutosEnum)opcao)
    //         {
    //             case PedidosProdutosEnum.Adicionar:
    //                 break;

    //             case PedidosProdutosEnum.Remover:
    //                 break;

    //                 case PedidosProdutosEnum.Listar:
    //                 break;

    //             case PedidosProdutosEnum.MenuInicial:
    //                 Menu();
    //                 break;

    //             case PedidosProdutosEnum.Sair:
    //                 Console.WriteLine("Obrigada e volte sempre!");
    //                 break;

    //             default:
    //                 Console.WriteLine("Opção inválida.");
    //                 break;
    //         }
    //     } while (opcao != 0);
    // }
}

