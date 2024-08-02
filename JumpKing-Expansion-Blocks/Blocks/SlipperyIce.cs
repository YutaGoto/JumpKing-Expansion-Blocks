using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class SlipperyIce: BoxBlock, IBlockDebugColor
    {
        public SlipperyIce(Rectangle p_collider) : base(p_collider) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_SLIPPERY_ICE; } }
    }
}
