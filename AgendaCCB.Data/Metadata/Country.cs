using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public class CountryMetadata
    {
        [Display(Description="Código")]
        public int Id { get; set; }

        [Display(Description = "Nome")]
        public string Name { get; set; }
    }
}
