using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class DeepWater: BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public DeepWater(Rectangle p_collider) : base(p_collider) { }
    }
}
