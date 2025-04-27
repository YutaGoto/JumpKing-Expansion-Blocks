using EntityComponent;
using HarmonyLib;
using JumpKing;
using JumpKing.GameManager;
using JumpKing.Player;
using JumpKing.Util;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Patches
{
    internal class DrawDebug
    {
        public DrawDebug(Harmony harmony)
        {
            harmony.Patch(typeof(GameLoop).GetMethod("Draw"), null, new HarmonyMethod(AccessTools.Method(typeof(DrawDebug), nameof(Draw))));
        }

        private static void Draw(GameLoop __instance)
        {
            // PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();

            TextHelper.DrawString(
                Game1.instance.contentManager.font.MenuFont,
                string.Join("\n", PatchedJumpState.t_timer),
                new Vector2(320f, 50f),
                Color.Yellow,
                new Vector2(0f, 0f),
                p_is_outlined: true
            );
        }
    }
}
