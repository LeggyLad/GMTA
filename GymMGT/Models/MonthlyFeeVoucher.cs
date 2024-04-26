using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMGT.Models
{
    // Model class representing the Monthly Fee Voucher entity
    public class MonthlyFeeVoucher
    {

        //Primary Key
        [Key]
        public int MonthlyFeeID { get; set; }

        //[Timestamp]
        [DataType(DataType.Date)]
        public DateTime FeeDate { get; set; }=DateTime.Now;

        //Remarks on fee voucher
        public string Remarks { get; set; }

        //Status of fee voucher
        public string Status { get; set; }

        //Foreign key and navigation property
        [ForeignKey("GymTrainee")]
        public int TraineeId { get; set; }
        public GymTrainee GymTrainee { get; set; }
    }
}
