using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymMGT.Models
{

    // Model class representing the Gym Trainee entity (here is how I got the tables set up for migration to my local SQL server (I used my run-down laptop to keep the server up while you grade this)
    public class GymTrainee
    {

    
        //Primary Key
        [Key]
        [Column(TypeName = "int")]
        public int TraineeId { get; set; }

        //First Name
        [Required]
        [Column(TypeName ="varchar(50)")]
        [DisplayName("First Name")]
        public String FirstName { get; set; }

        //Last Name
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Last Name")]
        public String LastName { get; set; }

        //ContactNo (forgot to add functionality to it)
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Contact No")]
        public String ContactNo { get; set; } = "0";

        //Age
        [Required]
        [Column(TypeName = "int")]
        [DisplayName("Age")]
        public int Age { get; set; }

        //Height
        [Required]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Height")]
        public String Height { get; set; } = "0";

        //Weigtht
        [Required]
        [Column(TypeName = "int")]
        [DisplayName("Weight")]
        public int Weight { get; set; }
       
        //Gender
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Gender")]
        public String Gender { get; set; } = "Male";

        //Address
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Address")]
        public String Address { get; set; }
       
        //Image Name
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        //Creation Date
        public DateTime CreationDate { get; set; }

        //Upload File
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }



        //Navigation Property 001
        public int BloodGroupID { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }


        //Navigation Property 002
        public int TrainingLevelID { get; set; }
        public virtual TrainingLevel TrainingLevel { get; set; }
        public int MonthlyFee { get; set; }


        private string _feepaid_status = "unknown";

        [NotMapped]
        public string Feepaid_Status
        {
            get
            {
                return _feepaid_status;
            }
            set
            {
                _feepaid_status = value;
            }
        }

    }

    // Enum to represent fee payment filter options
    public enum feefilter
    {
        Paid,
        UnPaid
    }
}
