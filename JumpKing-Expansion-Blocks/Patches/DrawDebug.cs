using EntityComponent;
using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.GameManager;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            ICollisionQuery collisionQuery = LevelManager.Instance;
            Stack<string> texts = new Stack<string>();

            Rectangle hitbox = player.m_body.GetHitbox();
            // AdvCollisionInfo advCollisionInfo = collisionQuery.GetCollisionInfo(hitbox);
            collisionQuery.CheckCollision(hitbox, out Rectangle overlap, out AdvCollisionInfo advCollisionInfo);
            IReadOnlyList<IBlock> collidedBlocks = advCollisionInfo.GetCollidedBlocks();

            foreach (IBlock block in collidedBlocks)
            {
                texts.Push(block.GetType().Name);
            }

            TextHelper.DrawString(
                Game1.instance.contentManager.font.MenuFont,
                //string.Join("\n", texts),
                (advCollisionInfo.SlopeType).ToString(),
                new Vector2(320f, 50f),
                Color.Yellow,
                new Vector2(0f, 0f),
                p_is_outlined: true
            );
        }
    }
}
