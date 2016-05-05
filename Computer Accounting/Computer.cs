using System.ComponentModel.DataAnnotations;

namespace Computer_Accounting
{
    public class Computer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }
    }
}
