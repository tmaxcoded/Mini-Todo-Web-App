

namespace TodoApp.Core.Models
{
    public partial class GetTodoApp
    {
        public long Id { get; set; }
        public string? WhatIsToBeDone { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        

        public bool IsCompleted { get; set; }

        public string? CompletedBy { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
