using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string ?Title { get; set; }  

        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        
        public List<TaskItem> Tasks { get; set; } = new();
    }
}
