using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTrainerScheduler.Entities
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TrainingDateTimeStart { get; set; }
        public Trainer TrainerEntity { get; set; }
        public Customer CustomerEntity { get; set; }
    }
}
