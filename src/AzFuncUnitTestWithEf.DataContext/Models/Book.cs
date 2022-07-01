using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzFuncUnitTestWithEf.DataContext.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
