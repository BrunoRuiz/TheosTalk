namespace TheosTalk.Dto.Model
{
    public class IgrejaDto
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
        public EnderecoDto Endereco { get; set; }
        public string TagName { get; set; }                    
    }
}
