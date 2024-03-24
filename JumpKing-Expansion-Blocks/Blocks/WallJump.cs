using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class WallJump: BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public WallJump(Rectangle p_collider) : base(p_collider) { }
    }
}
