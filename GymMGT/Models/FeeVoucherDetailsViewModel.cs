using System.Collections.Generic;

namespace GymMGT.Models
{
    // ViewModel class to represent details of the fee voucher
    public class FeeVoucherDetailsViewModel
    {
        public GymTrainee GymTraineeVM { get; set; }
        public MonthlyFeeVoucher MonthlyFeeVoucherVM { get; set; }


        public IEnumerable<GymTrainee> list_GymTrainee { get; set; }

        public IEnumerable<MonthlyFeeVoucher> list_MonthlyFeeVoucher { get; set; }

    }
}
