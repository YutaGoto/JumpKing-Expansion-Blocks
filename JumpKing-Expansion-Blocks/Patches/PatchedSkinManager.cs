using EntityComponent;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Patches
{
    internal class PatchedSkinManager
    {
        /// <summary>
        /// for CursedIce block. patch for IsWearingSkin method that is attatch to Skin to wearing GiantBoots
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="p_item"></param>
        public static void IsWearingSkinPostfix(ref bool __result, Items p_item)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            if (player != null)
            {
                if (player.m_body.IsOnBlock<Blocks.RestrainedIce>() && p_item == Items.GiantBoots)
                {
                    __result = true;
                }

                if (player.m_body.IsOnBlock<Blocks.CursedIce>() && p_item == Items.GiantBoots)
                {
                    __result = true;
                }

                if (player.m_body.IsOnBlock<Blocks.AntiGiantBoots>() && p_item == Items.GiantBoots)
                {
                    __result = false;
                }
            }
        }
    }
}
