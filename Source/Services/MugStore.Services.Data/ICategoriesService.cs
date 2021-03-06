﻿namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> Get();

        Category Get(string acronym);

        void Create(Category category);
    }
}
