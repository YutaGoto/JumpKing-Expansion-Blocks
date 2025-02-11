using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class Conveyor: BoxBlock, IBlockDebugColor
    {
        public readonly Rectangle collider;
        public int Speed { get; private set; }
        public int Direction { get; private set; }

        public Rectangle GetRect()
        {
            return collider;
        }

        public Conveyor(Rectangle p_collider, byte speed = 30, byte direction = 30) : base(p_collider)
        {
            collider = p_collider;
            Speed = (int)speed;
            Direction = (int)direction;
        }

        public Color DebugColor
        {
            get { return new Color(Speed, Constants.ConveyorSpeedCodes.CONVEYOR_G, Direction); }
        }
    }
}
