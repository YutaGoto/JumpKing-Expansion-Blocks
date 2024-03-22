using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class Conveyor: BoxBlock
    {
        public readonly Rectangle collider;
        public int Speed { get; private set; }
        public int Direction { get; private set; }

        public Conveyor(Rectangle p_collider, byte speed = 30, byte direction = 30) : base(p_collider)
        {
            collider = p_collider;
            Speed = (int)speed;
            Direction = (int)direction;
        }
    }
}
