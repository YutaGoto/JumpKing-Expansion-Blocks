using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class ReversedCharge: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public ReversedCharge(Rectangle p_collidar) :  base(p_collidar) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_REVERSED_CHARGE; } }
    }
}
