using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class RestrainedIce: BoxBlock, IBlockDebugColor
    {
        public RestrainedIce(Rectangle p_collider) : base(p_collider) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_RESTRAINED_ICE; } }
    }
}
