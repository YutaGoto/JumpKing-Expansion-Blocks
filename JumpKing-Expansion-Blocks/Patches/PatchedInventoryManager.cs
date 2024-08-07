using EntityComponent;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Patches
{
    internal class PatchedInventoryManager
    {
        /// <summary>
        /// for SnakeRing item. patch for HasItemEnabled method that is attatch to InventoryManager to disable SnakeRing item
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="p_item"></param>
        public static void HasItemEnabledPostfix(ref bool __result, Items p_item)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            if (player != null)
            {
                if (
                    player.m_body.IsOnBlock<Blocks.SlipperyIce>() ||
                    player.m_body.IsOnBlock<Blocks.ZeroFriction>() ||
                    player.m_body.IsOnBlock<Blocks.RestrainedIce>() ||
                    player.m_body.IsOnBlock<Blocks.CursedIce>() ||
                    player.m_body.IsOnBlock<Blocks.InfinityJump>()
                )
                {
                    if (p_item == Items.SnakeRing)
                    {
                        __result = false;
                    }
                }
            }
        }
    }
}
