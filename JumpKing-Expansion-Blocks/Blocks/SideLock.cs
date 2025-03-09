using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class SideLock: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;

        public int Direction { get; private set; }
        public Rectangle m_collider { get; private set; }

        public SideLock(Rectangle p_collider, int direction) : base(p_collider)
        {
            m_collider = p_collider;
            Direction = direction;
        }

        public Color DebugColor {
            get
            {
                return new Color(Direction, Constants.SideLockColorCodes.SIDE_LOCK_G, Constants.SideLockColorCodes.SIDE_LOCK_B);
            }
        }
    }
}
