using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class AutoJumpCharge: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public AutoJumpCharge(Rectangle p_collider) : base(p_collider) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_AUTO_JUMP_CHARGE; } }
    }
}
