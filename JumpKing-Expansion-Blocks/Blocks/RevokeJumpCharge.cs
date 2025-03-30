using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class RevokeJumpCharge: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public RevokeJumpCharge(Rectangle p_collider) : base(p_collider) { }
        public Color DebugColor { get { return Constants.ColorCodes.CODE_REVOKE_JUMP_CHARGE; } }
    }
}
