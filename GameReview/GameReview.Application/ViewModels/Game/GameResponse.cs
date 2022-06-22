
using GameReview.Application.ViewModels.GameGender;
using GameReview.Domain.Core;

namespace GameReview.Application.ViewModels.Game
{
    public class GameResponse : Register
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Developer { get; set; }
        public int GameGenderId { get; set; }
        public GameGenderResponse GameGender { get; set; }
        public int Score { get; private set; }
        public string Console { get; set; }
    }
}
