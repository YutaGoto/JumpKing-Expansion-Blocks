using HarmonyLib;
using JumpKing;
using JK = JumpKing.GameManager.TitleScreen;

using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpKing_Expansion_Blocks.Patches
{
    public class FollyPlayer
    {
        public FollyPlayer (Harmony harmony)
        {
            Type type = AccessTools.TypeByName("JumpKing.GameManager.TitleScreen.FollyPlayer");
            MethodInfo Draw = type.GetMethod("Draw");
            harmony.Patch(
                Draw,
                transpiler: new HarmonyMethod(AccessTools.Method(typeof(FollyPlayer), nameof(transpileDraw)))
            );
        }

        private static IEnumerable<CodeInstruction> transpileDraw(IEnumerable<CodeInstruction> instructions , ILGenerator generator) {
            CodeMatcher matcher = new CodeMatcher(instructions , generator);

            matcher.MatchStartForward(
                // m_center.ToVector2()
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldflda, AccessTools.Method("JumpKing.GameManager.TitleScreen.FollyPlayer:m_center")),
                new CodeMatch(OpCodes.Call, AccessTools.Method("Microsoft.Xna.Framework.Point:ToVector2"))
            ).ThrowIfInvalid($"Cant find code in {nameof(FollyPlayer)}");
            matcher.Advance(3);
            matcher.InsertAndAdvance(
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(FollyPlayer), nameof(fixPosition)))
            );

            matcher.MatchStartForward(
                // m_effect
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld,  AccessTools.Method("JumpKing.GameManager.TitleScreen.FollyPlayer:m_effect"))
            ).ThrowIfInvalid($"Cant find code in {nameof(FollyPlayer)}");
            matcher.Advance(2);
            matcher.InsertAndAdvance(
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(FollyPlayer), nameof(flipSpiritV)))
            );

            return matcher.Instructions();
        }

        private static Vector2 fixPosition(Vector2 origin) {
            return origin + (ModEntry.IsUpsideDown() ? new Vector2(0f, 23f) : Vector2.Zero);
        }

        private static int flipSpiritV(int effect) {
            return effect | (ModEntry.IsUpsideDown() ? (int)SpriteEffects.FlipVertically : 0);
        }
    }
}
