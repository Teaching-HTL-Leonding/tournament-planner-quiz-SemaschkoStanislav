using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentPlanner.Data
{
    public class Player
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements
    }
}
