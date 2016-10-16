using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalTrainerScheduler.Entities;

namespace PersonalTrainerScheduler.Repositories
{
    public interface ITrainingSessionRepository
    {
        void DeleteTrainingSessionById(int sessionId);
        void RegisterTrainingSession(int trainerId, int customerId, DateTime trainingSessionDateTimeStart);
        List<TrainingSession> GetTrainingSessionsByDateAndTrainerId(int trainerId, DateTime date);
        List<TrainingSession> GetAllTrainingSessionsByCustomerId(int customerId);
    }
}
