using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class AntiGiantBoots : IBlock, IBlockDebugColor
    {
        private readonly Rectangle m_collider;

        public AntiGiantBoots(Rectangle collider) => m_collider = collider;

        public Color DebugColor{ get { return Constants.ColorCodes.CODE_ANTI_GIANT_BOOTS; } }

        public Rectangle GetRect() => m_collider;

        public BlockCollisionType Intersects(Rectangle hitbox, out Rectangle intersection)
        {
            if (m_collider.Intersects(hitbox))
            {
                intersection = Rectangle.Intersect(hitbox, m_collider);
                return BlockCollisionType.Collision_NonBlocking;
            }
            else
            {
                intersection = new Rectangle(0, 0, 0, 0);
                return BlockCollisionType.NoCollision;
            }
        }
    }
}
