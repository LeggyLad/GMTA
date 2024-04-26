using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace GymMGT.Models
{
    // Model class representing a Blood Group entity
    public class BloodGroup
    {
        [Key]
        public int BloodGroupID { get; set; }
        public string BloodGroupName { get; set; }
        public virtual ICollection<GymTrainee> GymTrainees { get; set; }


    }
}
