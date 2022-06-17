
using GameReview.Domain.Core;
using GameReview.Domain.Models.Enumerations;

namespace GameReview.Domain.Models
{
    public class Game : Register
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Developer { get; set; }
        public GameGender GameGender { get; set; }
        public int IdGenderType { get; set; }
        public decimal Score { get; set; }
        public string Console { get; set; }
        public string imgPath { get; set; }
    }
}
