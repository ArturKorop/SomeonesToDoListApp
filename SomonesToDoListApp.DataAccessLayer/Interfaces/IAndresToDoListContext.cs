using SomeonesToDoListApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeonesToDoListApp.DataAccessLayer.Interfaces
{
    public interface ISomeonesToDoListContext 
    {
        DbSet<ToDo> ToDos { get; set; }
    }
}
