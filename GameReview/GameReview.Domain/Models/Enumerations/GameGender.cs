
using GameReview.Domain.Core;

namespace GameReview.Domain.Models.Enumerations
{
    public class GameGender : Enumeration
    {
        public static GameGender Action = new(1, nameof(Action));
        public static GameGender Race = new(2, nameof(Race));
        public static GameGender FPS = new(3, nameof(FPS));
        public static GameGender RPG = new(4, nameof(RPG));
        public static GameGender Strategy = new(5, nameof(Strategy));

        public GameGender(int id, string name)
            :base (id, name)
        {
        }
    }
}
