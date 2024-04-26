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

   //Almost the same as GymTrainee model, focus is on the Nav Properties
    public class GymTrainee_result
    {

    

        [Key]
        [Column(TypeName = "int")]
        public int TraineeId { get; set; }


        [Required]
        [Column(TypeName ="varchar(50)")]
        [DisplayName("First Name")]
        public String FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Last Name")]
        public String LastName { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Contact No")]
        public String ContactNo { get; set; }


        [Required]
        [Column(TypeName = "int")]
        [DisplayName("Age")]
        public int Age { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Height")]
        public String Height { get; set; } = "0";



        [Required]
        [Column(TypeName = "int")]
        [DisplayName("Weight")]
        public int Weight { get; set; }
       
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Gender")]
        public String Gender { get; set; } = "Male";

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Address")]
        public String Address { get; set; }
        

       
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }


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
}
