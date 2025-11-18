using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class MultiWarp: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;

        private readonly Rectangle m_collider;
        public int ToScreenNo { get; private set; }
        public int Offset { get;  private set;  }

		public MultiWarp(Rectangle collider, byte toScreenNo = 1, byte offset = 0): base (collider)
        { 
            m_collider = collider;
            ToScreenNo = (int)toScreenNo;
            Offset = (int)offset;
		}

        public Color DebugColor { get { return new Color(ToScreenNo, Offset, Constants.MultiWarpColorCodes.MULTI_WARP_B); } }

        public Rectangle GetRect()
        {
            return m_collider;
        }
    }
}
