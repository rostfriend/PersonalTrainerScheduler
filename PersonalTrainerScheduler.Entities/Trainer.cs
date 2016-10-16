using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTrainerScheduler.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public TimeSpan StartOfWorkTime { get; set; }
        public TimeSpan EndOfWorkTime { get; set; } 
        public List<Occupation> Occupations { get; set; }
    }
}
