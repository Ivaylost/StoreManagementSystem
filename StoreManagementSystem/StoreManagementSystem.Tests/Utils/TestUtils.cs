using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Tests
{
    public static class TestUtils
    {
        public static DbContextOptions GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
