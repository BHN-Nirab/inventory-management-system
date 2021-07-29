using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Model
{
    public class Log
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public int TransactionType { get; set; } // {0-Purchase, 1-Sale}
        public int Unit { get; set; }

    }
}
