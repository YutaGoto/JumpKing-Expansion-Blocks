using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class MultiWarp: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;

        private readonly Rectangle m_collider;
        public int ToScreenNo { get; private set; }

        public MultiWarp(Rectangle collider, byte toScreenNo = 1): base (collider)
        { 
            m_collider = collider;
            ToScreenNo = (int)toScreenNo;
        }

        public Color DebugColor { get { return new Color(ToScreenNo, Constants.MultiWarpColorCodes.MULTI_WARP_G, Constants.MultiWarpColorCodes.MULTI_WARP_B); } }

        public Rectangle GetRect()
        {
            return m_collider;
        }
    }
}
