using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class MagicSand: BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public MagicSand(Rectangle p_collider) : base(p_collider) { }
    }
}
