﻿using StoreManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Services.Interfaces
{
    public interface ICategoryService
    {
        Category CreateCategory(string name);

        string GetProfitFromCategory(string categoryName);
    }
}
