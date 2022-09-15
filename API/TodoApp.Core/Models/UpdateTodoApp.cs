using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Models
{
    public class UpdateTodoApp
    {
        public long Id { get; set; }
        public string? WhatIsToBeDone { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

       

        public bool IsCompleted { get; set; }
    }
}
