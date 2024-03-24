using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class InfinityJump : BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public InfinityJump(Rectangle p_collider) : base(p_collider) { }
    }
}
