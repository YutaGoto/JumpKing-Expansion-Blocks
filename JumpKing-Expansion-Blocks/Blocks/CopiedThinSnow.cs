using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class CopiedThinSnow: BoxBlock
    {
        private readonly Rectangle m_collider;
        protected override bool canBlockPlayer => true;

        public CopiedThinSnow(Rectangle p_collider): base(p_collider)
        {
            m_collider = p_collider;
        }
    }
}
