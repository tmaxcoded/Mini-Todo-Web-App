using FluentValidation;
using TodoApp.Core.Models;

namespace TodoApp.API.ModelValidation
{
    public class PostTodoAppValidation: AbstractValidator<PostTodoApp>
    {
        public PostTodoAppValidation()
        {
            RuleFor(postTodo => postTodo.WhatIsToBeDone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter what task you want to do");

            RuleFor(postTodo => postTodo.StartDate)
                .NotNull()
                .NotEmpty()
                .Must(DateValidation)
                .WithMessage("Start date cannot be less than todays date");

            RuleFor(postTodo => postTodo.DueDate)
                .NotNull()
                .NotEmpty()
                .Must(DateValidation)
                .WithMessage("Due date date cannot be less than todays date");


        }



        private bool DateValidation(DateTime date)
            => date.Date >= DateTime.Now.Date;
    }
}
