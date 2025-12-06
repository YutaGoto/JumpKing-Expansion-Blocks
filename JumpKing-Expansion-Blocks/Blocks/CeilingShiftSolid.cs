using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class CeilingShiftSolid: BoxBlock, IBlockDebugColor
    {
        private readonly Rectangle m_collider;

        public CeilingShiftSolid(Rectangle p_collider): base(p_collider)
        {
            m_collider = p_collider;
        }

        protected override bool canBlockPlayer => false;

        public Color DebugColor { get { return Constants.ColorCodes.CODE_CEILING_SHIFT_SOLID; } }
        public Rectangle GetRect()
        {
            return m_collider;
        }
    }
}
