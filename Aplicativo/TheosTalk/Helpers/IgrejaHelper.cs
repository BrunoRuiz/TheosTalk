using TheosTalk.Dto.Model;

namespace TheosTalk.Helpers
{
    public class IgrejaHelper
    {
        public static IgrejaDto RetornaIgreja(string tagName)
        {
            switch (tagName)
            {
                case "CatedralMaringa" :
                return new IgrejaDto
                    {
                        UrlImage = "https://static.ricmais.com.br/uploads/2020/11/resultado-prefeito-maringa-2020.jpg",
                        NomeIgreja = "Catedral Metropolitana Basílica Menor Nossa Senhora da Glória",
                        BreveDescricao = "Catedral de Maringá ou Catedral Metropolitana Basílica Menor Nossa Senhora da Glória é a catedral da Arquidiocese de Maringá, no estado do Paraná, no Brasil. Localizada na Avenida Tiradentes, é considerada símbolo da cidade de Maringá.",
                        Endereco = new EnderecoDto { Logradouro = "Praça da Catedral", Numero = "S/N", Bairro = "Zona 02", Cidade = "Maringá", UF = "PR", CEP = "87010-530" },
                        Telefone = "(44) 3227-1993",
                        Altura = 124,
                        Inauguracao = 1972,
                        Arquiteto = "José Augusto Bellucci",
                        EstiloArquitetonico = "Arquitetura moderna",
                        Capacidade = 4.500,
                        TagName = "CatedralMaringa"
                    };

                case "CatedralBrasilia":
                    return new IgrejaDto
                    {
                        UrlImage = "https://www.curtamais.com.br/uploads/midias/cccc27dd112edaa0a43f6cbeb0acdbf0.jpg",
                        NomeIgreja = "Catedral Metropolitana Nossa Senhora Aparecida",
                        BreveDescricao = "A Catedral Metropolitana - Nossa Senhora Aparecida, mais conhecida como Catedral de Brasília, é um templo católico brasileiro, na qual se encontra a cátedra da Arquidiocese de Brasília, localizada na capital federal, ao sul da S1, no Eixo Monumental, região da Esplanada dos Ministérios.",
                        Endereco = new EnderecoDto { Logradouro = "Esplanada dos Ministérios", Numero = "Lote 12", Cidade = "Brasília", UF = "DF", CEP = "70050-000" },
                        Telefone = "(61) 3224-4073",
                        Altura = 40,
                        Inauguracao = 1970,
                        Arquiteto = "Oscar Niemeyer",
                        EstiloArquitetonico = "Arquitetura moderna, Arquitetura futurista",
                        Capacidade = 4.000,
                        TagName = "CatedralBrasilia"
                    };

                case "BasilicaSantaMariaRoma":
                    return
                    new IgrejaDto
                    {
                        UrlImage = "https://colosseumrometickets.com/wp-content/uploads/2018/10/The-Basilica-di-Santa-Maria-Maggiore-the-largest-Roman-Catholic-Marian-church-in-Rome-Italy.jpg",
                        NomeIgreja = "Basílica de Santa Maria Maggiore",
                        BreveDescricao = "Santa Maria Maggiore ou Basílica de Santa Maria Maior é uma das quatro basílicas maiores, uma das sete igrejas de peregrinação e a maior igreja mariana de Roma — motivo pelo qual ela recebeu o epíteto de Maior",
                        Endereco = new EnderecoDto { Logradouro = "P.za di Santa Maria Maggiore", Cidade = "ROMA", UF = "RM", CEP = "00100" },
                        Telefone = "+39(06) 6988-6800",
                        Altura = 75,
                        Inauguracao = 440,
                        Arquiteto = "Ferdinando Fuga",
                        EstiloArquitetonico = "Arquitetura barroca, Arquitetura da Roma Antiga, Arquitetura da Idade Média, Arquitetura românica",
                        TagName = "BasilicaSantaMariaRoma"
                    };


                default:
                    return null;
            }                   
        }
    }
}
