using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class DiamondHandsIce: BoxBlock, IBlockDebugColor
    {
        public DiamondHandsIce(Rectangle p_collider): base(p_collider) { }
        public Color DebugColor { get { return Constants.ColorCodes.CODE_DIAMOND_HANDS_ICE; } }
    }
}
