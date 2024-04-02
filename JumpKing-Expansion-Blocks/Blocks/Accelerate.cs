using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class Accelerate: BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public Accelerate(Rectangle p_collider): base(p_collider) { }
    }
}
