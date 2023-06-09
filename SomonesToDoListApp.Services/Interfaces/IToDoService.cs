﻿using SomeonesToDoListApp.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeonesToDoListApp.Services.Interfaces
{
    public interface IToDoService : IDisposable
    {
        Task<bool> CreateToDoAsync(ToDoViewModel viewModel);

        Task<bool> UpdateToDoAsync(ToDoViewModel viewModel);

        Task<bool> RemoveToDoAsync(int id);

        Task<IEnumerable<ToDoViewModel>> GetToDoItemsAsync();

    }
}
