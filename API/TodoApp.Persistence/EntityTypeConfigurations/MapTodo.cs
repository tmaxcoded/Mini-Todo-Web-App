
namespace TodoApp.Persistence.EntityTypeConfigurations
{
    public class MapTodo
    {
        public MapTodo(EntityTypeBuilder<Todo> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.WhatIsToBeDone).IsRequired();
            entityBuilder.Property(t => t.StartDate).IsRequired();
            entityBuilder.Property(t => t.DueDate).IsRequired();
        }
    }
}