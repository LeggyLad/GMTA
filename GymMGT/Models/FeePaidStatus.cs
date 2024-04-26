namespace GymMGT.Models
{
    // Model class representing the FeePaidStatus entity. This deals with what the client's status is regarding their monthly payment.
    public class FeePaidStatus 
    {


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
