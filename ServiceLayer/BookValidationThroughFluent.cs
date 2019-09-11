using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Model;
using FluentValidation;
namespace ServiceLayer
{
    class BookValidationThroughFluent : AbstractValidator<Book>
    { 
            public BookValidationThroughFluent()
            {

            RuleFor(Book => Book).Must(Book => Book.Id >= 0).WithMessage("Id: should be a positive integer.");


            RuleFor(Book => Book.Name).NotNull().WithMessage("Name Not Present").DependentRules ( () => {
                RuleFor(Book => Book).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(Book => Book.Name.Length > 0
                    && Book.Name.All(X => char.IsLetter(X) || X == ' ' || X == '.'))
                .WithMessage("Name: should contain only alphabets.");
            });

            RuleFor(Book => Book.Author).NotNull().WithMessage("Author Not Present").DependentRules(() => {
                RuleFor(Book => Book).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(Book => Book.Author.Length > 0
                    && Book.Author.All(X => char.IsLetter(X) || X == ' ' || X == '.'))
                .WithMessage("Author: should contain only alphabets.");
            });

            RuleFor(Book => Book.Category).NotNull().WithMessage("Category Not Present").DependentRules(() => {
                RuleFor(Book => Book).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(Book => Book.Category.Length > 0
                    && Book.Category.All(X => char.IsLetter(X) || X == ' ' || X == '.'))
                .WithMessage("Category: should contain only alphabets.");
            });
            
            
            RuleFor(Book => Book).Must(Book => Book.Price >= 0).WithMessage("Price: should be a positive number.");
                
        }
        
    }
}
