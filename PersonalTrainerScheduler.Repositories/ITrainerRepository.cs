using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalTrainerScheduler.Entities;

namespace PersonalTrainerScheduler.Repositories
{
    public interface ITrainerRepository
    {
        void ModifyTrainerById(int trainerId, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, DateTime startOfWorkTime, DateTime endOfWorkTime);
        void AddNewTrainer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, DateTime startOfWorkTime, DateTime endOfWorkTime, int occupationId);
        void DeleteTrainerById(int trainerId);
        List<Trainer> GetAllTrainers();
        List<Trainer> GetFreeTrainersByDateTime(DateTime desiredDateTime);
    }
}
