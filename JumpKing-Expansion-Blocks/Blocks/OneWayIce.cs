using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class OneWayIce: BoxBlock, IBlockDebugColor
    {

        public OneWayIce(Rectangle p_collider): base(p_collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor { get { return Constants.ColorCodes.CODE_ONE_WAY_ICE_BLOCK; } }

    }
}
