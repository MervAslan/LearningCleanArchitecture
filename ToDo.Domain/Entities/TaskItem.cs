using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string ?Title { get; set; }           
        public string ?Description { get; set; }   
        public string ?Status { get; set; }        
        public string? Tag { get; set; }           
        public DateTime? DueDate { get; set; }     

        public int BoardId { get; set; }
        public Board? Board { get; set; }
    }
}
