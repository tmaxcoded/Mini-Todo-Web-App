


namespace TodoApp.Persistence.Repository
{
    public class TodoAppRepository : ITodoAppRepository
    {
        private readonly TodoAppDbContext _context;

        public TodoAppRepository(TodoAppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(long Id)
        {
            var res = await GetById(Id);
            _context.Todos.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await _context.Todos.AsNoTracking().ToListAsync();
        }

        public async Task<Todo> GetById(long Id)
        {
            return await _context.Todos.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Todo> Save(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo;

        }

        public async Task Update(Todo todo)
        {

          
                
           _context.Update(todo);
          await _context.SaveChangesAsync();
           
           
        }
    }
}