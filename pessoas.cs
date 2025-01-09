using System.Threading.Tasks;

namespace CRUD_Dapper1.Models
{
    public class Pessoas
    {
        public int PessoaID { get; set; }

        public required string Nome { set; get; }

        public int idade { get; set; }  

        public float peso{ get; set; }
    }
}
