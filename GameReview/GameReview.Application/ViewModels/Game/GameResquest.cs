
using GameReview.Application.ViewModels.GameGender;

namespace GameReview.Application.ViewModels.Game
{
    public class GameResquest
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Developer { get; set; }
        public GameGenderResponse GameGenderResponse { get; set; }
        public int IdGenderType { get; set; }
        public decimal Score { get; set; }
        public string Console { get; set; }
    }
}
