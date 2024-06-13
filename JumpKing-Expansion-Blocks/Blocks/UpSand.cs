using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class UpSand : BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public UpSand(Rectangle p_collider) : base(p_collider) { }
    }
}
