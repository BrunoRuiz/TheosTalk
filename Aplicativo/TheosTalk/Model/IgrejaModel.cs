namespace TheosTalk.Model
{
    public class IgrejaModel
    {
        public string UrlImage { get; set; }
        public string NomeIgreja { get; set; }
        public string BreveDescricao { get; set; }
        public string Telefone { get; set; }
        public double Altura { get; set; }
        public int Inauguracao { get; set; }
        public string Arquiteto { get; set; }
        public string EstiloArquitetonico { get; set; }
        public double Capacidade { get; set; }
        public Endereco EnderecoIgreja { get; set; }
        public string TagName { get; set; }
    }

    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
    }

}


