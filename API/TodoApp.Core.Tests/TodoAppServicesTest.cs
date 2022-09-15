
namespace TodoApp.Core.Tests;

public class TodoAppServicesTest
{

    private TodoAppService _toodServices;

    private PostTodoApp _createTodo;

    private IEnumerable<Todo> _todos;
    private GenericResponse<IEnumerable<GetTodoApp>> _getTodos;

    private Mock<ITodoAppRepository> _todoRepositoryMock;
    private IMapper mapper;

    public TodoAppServicesTest()
    {
        _createTodo = new PostTodoApp()
        {

            WhatIsToBeDone = "Walking Treadmill",
            StartDate = new DateTime(2022, 09, 10),
            DueDate = new DateTime(2022, 09, 15),

        };

        _todos = new List<Todo>() { new Todo()
        {
            Id = 1,
            WhatIsToBeDone = "Walking Treadmill",
            StartDate = new DateTime(2022, 09, 10),
            DueDate = new DateTime(2022, 09, 15),

            DateCreated = DateTime.Now,
            CreatedBy = "Tobi Adeogun",
        } };

        _todoRepositoryMock = new Mock<ITodoAppRepository>();
        _todoRepositoryMock.Setup(q => q.GetAll())
            .ReturnsAsync(_todos);

        //auto mapper configuration
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapModelProfile());
        });
        mapper = mockMapper.CreateMapper();


        _toodServices = new TodoAppService(_todoRepositoryMock.Object, mapper);
    }
    [Fact]
    public async Task Should_Return_List_of_Generic_GetTodo_Response()
    {
        //Arrarange

        //act
        _getTodos = await _toodServices.GetAll();

        _getTodos.IsSuccess.ShouldBe(true);
        _getTodos.Data.Count().ShouldBeGreaterThanOrEqualTo(0);

    }

    [Fact]
    public async Task Should_Return_A_Generic_Single_Response_When_GetById()
    {
        //Arrarange

        var _oldtodos = new Todo()
        {
            Id = 1,
            WhatIsToBeDone = "Walking Treadmill",
            StartDate = new DateTime(2022, 09, 10),
            DueDate = new DateTime(2022, 09, 15),

            DateCreated = DateTime.Now,
            CreatedBy = "Tobi Adeogun",
        };

        _todoRepositoryMock = new Mock<ITodoAppRepository>();
        _todoRepositoryMock.Setup(q => q.GetById(_oldtodos.Id))
            .ReturnsAsync(_oldtodos);

        _toodServices = new TodoAppService(_todoRepositoryMock.Object, mapper);

        //act
        var getTodoById = await _toodServices.GetById(1);

        getTodoById.IsSuccess.ShouldBe(true);
        getTodoById.Data.ShouldNotBeNull();
        getTodoById.ResponseCode.ShouldBeOfType<int>();
        getTodoById.ResponseCode.ShouldBe(200);

        //Should.Throw

    }


    [Fact]
    public async Task Should_Return_A_Generic_Single_Response_With_Null_Data_When_GetById()
    {
        //Arrarange

        var _oldtodos = new Todo()
        {
            Id = 1,
            WhatIsToBeDone = "Walking Treadmill",
            StartDate = new DateTime(2022, 09, 10),
            DueDate = new DateTime(2022, 09, 15),

            DateCreated = DateTime.Now,
            CreatedBy = "Tobi Adeogun",
        };

        _todoRepositoryMock = new Mock<ITodoAppRepository>();
        _todoRepositoryMock.Setup(q => q.GetById(_oldtodos.Id))
            .ReturnsAsync(_oldtodos);

        _toodServices = new TodoAppService(_todoRepositoryMock.Object, mapper);

        //act
        var getTodoById = await _toodServices.GetById(0);

        getTodoById.IsSuccess.ShouldBe(false);
        getTodoById.Data.ShouldBeNull();
        getTodoById.ResponseCode.ShouldBeOfType<int>();
        getTodoById.ResponseCode.ShouldBe(400);



    }

    [Fact]
    public async Task Should_Save_Todo()
    {
        //Arrarange

        var _oldtodos = new Todo()
        {
            Id = 1,
            WhatIsToBeDone = "Walking Treadmill",
            StartDate = new DateTime(2022, 09, 10),
            DueDate = new DateTime(2022, 09, 15),

            DateCreated = DateTime.Now,
            CreatedBy = "Tobi Adeogun",
        };

        _todoRepositoryMock = new Mock<ITodoAppRepository>();
        _todoRepositoryMock.Setup(q => q.Save(It.IsAny<Todo>()))
            .ReturnsAsync(_oldtodos);


        _toodServices = new TodoAppService(_todoRepositoryMock.Object, mapper);

        _oldtodos.WhatIsToBeDone = "Walking at nearby GYM";
        _oldtodos.ModifiedDate = DateTime.Now;

        //act
        var saveTodo = new PostTodoApp()
        {

            WhatIsToBeDone = "Walking at nearby GYM",
            StartDate = new DateTime(2022, 09, 10),
            DueDate = new DateTime(2022, 09, 15),

        };
        var result = await _toodServices.Save(saveTodo);

        result.IsSuccess.ShouldBe(true);
        result.Data.ShouldNotBeNull();







    }


}