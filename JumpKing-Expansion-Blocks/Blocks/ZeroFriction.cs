using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class ZeroFriction: BoxBlock, IBlockDebugColor
    {
        public ZeroFriction(Rectangle p_collider): base(p_collider) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_ZERO_FRICTION; } }
    }
}
