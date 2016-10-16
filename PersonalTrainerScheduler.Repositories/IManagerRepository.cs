using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalTrainerScheduler.Entities;

namespace PersonalTrainerScheduler.Repositories
{
    public interface IManagerRepository
    {
        Manager GetManagerByLogin(string login, string password);
    }
}
