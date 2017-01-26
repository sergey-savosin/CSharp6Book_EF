using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLotDAL.Models
{
    public partial class Inventory
    {
        public override string ToString()
        {
            return $"{this.PetName ?? "** No Name **"} is a {this.Color} {this.Make} with ID {this.CarId}.";
        }

        [NotMapped]
        public string MakeColor => $"{Make} + ({Color})";
    }
}
