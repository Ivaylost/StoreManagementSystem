using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands.Categories
{
    public class ProfitByGivenCategoryCommand : ICommand
    {
        private readonly ICategoryService categoryService;
        private readonly IWriter writer;
        private readonly IPramaterValidator validator;

        public ProfitByGivenCategoryCommand(ICategoryService categoryService, IWriter writer, IPramaterValidator validator)
        {
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();
            writer.WriteLine("Enter category name:");
            var categoryName = validator.Validate();
            parameters.Add(categoryName);
            return parameters;
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            var categoryName = parameters[0];
            var profit = this.categoryService.GetProfitFromCategory(categoryName);
            

            var result = new StringBuilder();
            result.Append($"Porfit: {profit}");

            return result.ToString();

        }
    }

    }
