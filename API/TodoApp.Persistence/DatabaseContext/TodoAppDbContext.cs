


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TodoApp.Persistence.DatabaseContext
{
    public class TodoAppDbContext : IdentityDbContext<User>
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options):base(options)
        {
            
        }
        


        public DbSet<Todo> Todos {get;set;}
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Todo>().HasData(
            new Todo{Id=1,WhatIsToBeDone ="Walking Treadmill",
            StartDate = new DateTime(2022,09,10),
            DueDate = new DateTime(2022,09,15),
            DateCreated = DateTime.Now,
            CreatedBy = "Tobi Adeogun",
           });

           

            new MapTodo(modelBuilder.Entity<Todo>());
            

            modelBuilder.ApplyConfiguration(new RoleConfiguration());



        }
    }
}