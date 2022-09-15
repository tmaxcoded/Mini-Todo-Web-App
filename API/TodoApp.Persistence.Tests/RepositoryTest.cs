using System.Diagnostics.CodeAnalysis;

namespace TodoApp.Persistence.Tests;

public class RepositoryTest
{
    Todo newTodo;
    public RepositoryTest()
    {
        newTodo = new Todo()
        {

            WhatIsToBeDone = "Walking Treadmill",
            StartDate = new DateTime(2022, 09, 10),
            DueDate = new DateTime(2022, 09, 15),
            DateCreated = DateTime.Now,
            CreatedBy = "Tobi Adeogun",
            
        };


    }
    [Fact]
    public async Task Should_Return_List_Of_Todos()
    {
        var dboptions = new DbContextOptionsBuilder<TodoAppDbContext>().UseInMemoryDatabase("Todo.db").Options;

        var context = new TodoAppDbContext(dboptions);
        context.Add(newTodo);

        context.SaveChanges();

        var todoRepo = new TodoAppRepository(context);

        var result = await todoRepo.GetAll();

        result.Count().ShouldBeGreaterThanOrEqualTo(0);

       
    }


    [Fact]
    public async Task Should_Save_Todos()
    {
       
        var dboptions = new DbContextOptionsBuilder<TodoAppDbContext>().UseInMemoryDatabase("Todo.db").Options;

        var context = new TodoAppDbContext(dboptions);
        //context.Add();

       

        var todoRepo = new TodoAppRepository(context);

        var result = await todoRepo.Save(newTodo);

        result.ShouldNotBeNull();

        var getAllTodos =context.Todos.ToList();

        getAllTodos.Count().ShouldBeGreaterThan(0);

       
    }

    [Fact]
    public async Task Should_Find_Todos_By_Id_And_Return_Single_Todo()
    {
        
        var dboptions = new DbContextOptionsBuilder<TodoAppDbContext>().UseInMemoryDatabase("Todo.db").Options;

        var context = new TodoAppDbContext(dboptions);
        //context.Add();



        var todoRepo = new TodoAppRepository(context);

        var result = await todoRepo.Save(newTodo);

        // find Todo
        var Result_Founded = await todoRepo.GetById(newTodo.Id);

        Result_Founded.ShouldNotBeNull();

        Result_Founded.Id.ShouldBe(newTodo.Id);

    }

   

    [Fact]
    public async Task Should_Delete_Todos_By_Id_()
    {
      
        var dboptions = new DbContextOptionsBuilder<TodoAppDbContext>().UseInMemoryDatabase("Todo.db").Options;

        var context = new TodoAppDbContext(dboptions);
        //context.Add();



        var todoRepo = new TodoAppRepository(context);

        var result = await todoRepo.Save(newTodo);

        // find Todo
        await todoRepo.Delete(newTodo.Id);

        var Result_Founded = await todoRepo.GetById(newTodo.Id);

        Result_Founded.ShouldBeNull();

        


    }


    [Fact]
    [ExcludeFromCodeCoverage]
    public async Task Should_Update_Todos()
    {
        //Arrange

       

        var dboptions = new DbContextOptionsBuilder<TodoAppDbContext>().UseInMemoryDatabase("Todo.db").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;

        var context = new TodoAppDbContext(dboptions);
      



        var todoRepo = new TodoAppRepository(context);

        var result = await todoRepo.Save(newTodo);


        // Act
        // update todos

        newTodo.WhatIsToBeDone = "Look for a new job in Australia";

        newTodo.ModifiedBy = "Tobi Adeogun";

        await todoRepo.Update(newTodo);

       var _final_result = await todoRepo.GetAll();

        //Assert
       _final_result.Count().ShouldBeGreaterThan(0);
       _final_result.ToList()[0].WhatIsToBeDone.ShouldNotBe(newTodo.WhatIsToBeDone);


    }
}