using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class RevokeWalking : BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public RevokeWalking(Rectangle p_collider) : base(p_collider) { }
        public Color DebugColor { get { return Constants.ColorCodes.CODE_REVOKE_WALKING; } }
    }
}
