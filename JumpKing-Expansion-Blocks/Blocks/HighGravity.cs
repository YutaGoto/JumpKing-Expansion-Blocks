using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class HighGravity: IBlock
    {
        private readonly Rectangle m_collider;

        public HighGravity(Rectangle collider)
        {
            m_collider = collider;
        }

        public Rectangle GetRect()
        {
            return m_collider;
        }

        public BlockCollisionType Intersects(Rectangle p_hitbox, out Rectangle p_intersection)
        {
            bool ret = m_collider.Intersects(p_hitbox);

            if (ret)
            {
                p_intersection = Rectangle.Intersect(p_hitbox, m_collider);
                return BlockCollisionType.Collision_NonBlocking;
            }
            else
            {
                p_intersection = new Rectangle(0, 0, 0, 0);
                return BlockCollisionType.NoCollision;
            }
        }
    }
}
