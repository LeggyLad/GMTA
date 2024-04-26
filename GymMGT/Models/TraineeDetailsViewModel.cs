using System.Collections.Generic;
using System.Net;

namespace GymMGT.Models
{
    // ViewModel class representing details of a trainee
    public class TraineeDetailsViewModel
    {
        // Properties representing associated entities and data for a trainee
        public GymTrainee GymTraineeVM { get; set; }
        public  BloodGroup BloodGroupVM { get; set; }
        public TrainingLevel TrainingLevelVM { get; set; }
        public MonthlyFeeVoucher MonthlyFeeVoucherVM { get; set; }

        // Properties representing page title and header
        public string PageTitle { get; set; }
        public string PageHeader { get; set; }
    }
}
