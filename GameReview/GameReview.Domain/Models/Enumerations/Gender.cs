
using GameReview.Domain.Core;

namespace GameReview.Domain.Models.Enumerations
{
    public class Gender : Enumeration
    {
        public static Gender Action = new(1, nameof(Action));
        public static Gender Race = new(2, nameof(Race));
        public static Gender FPS = new(3, nameof(FPS));
        public static Gender RPG = new(4, nameof(RPG));
        public static Gender Strategy = new(5, nameof(Strategy));

        public Gender(int id, string name)
            :base (id, name)
        {
        }
    }
}
