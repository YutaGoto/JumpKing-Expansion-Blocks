using HarmonyLib;
using JK = JumpKing.BodyCompBehaviours;

using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

using System;

namespace JumpKing_Expansion_Blocks.Patches
{
    public class ApplyGravityBehaviour
    {
        public ApplyGravityBehaviour (Harmony harmony)
        {
            Type type = typeof(JK.ApplyGravityBehaviour);
            MethodInfo ExecuteBehaviour = type.GetMethod(nameof(JK.ApplyGravityBehaviour.ExecuteBehaviour));
            harmony.Patch(
                ExecuteBehaviour,
                transpiler: new HarmonyMethod(AccessTools.Method(typeof(ApplyGravityBehaviour), nameof(transpileExecuteBehaviour)))
            );
        }

        private static IEnumerable<CodeInstruction> transpileExecuteBehaviour(IEnumerable<CodeInstruction> instructions , ILGenerator generator) {
            CodeMatcher matcher = new CodeMatcher(instructions , generator);

            matcher.MatchStartForward(
                // bodyComp.Velocity.Y += num
                new CodeMatch(OpCodes.Ldloc_0),
                new CodeMatch(OpCodes.Ldflda, AccessTools.Field("JumpKing.Player.BodyComp:Velocity")),
                new CodeMatch(OpCodes.Ldflda, AccessTools.Field("Microsoft.Xna.Framework.Vector2:Y")),
                new CodeMatch(OpCodes.Dup),
                new CodeMatch(OpCodes.Ldind_R4),
                new CodeMatch(OpCodes.Ldloc_1),
                new CodeMatch(OpCodes.Add),
                new CodeMatch(OpCodes.Stind_R4)
            ).ThrowIfInvalid($"Cant find code in {nameof(ApplyGravityBehaviour)}");
            matcher.Advance(6);
            matcher.InsertAndAdvance(
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ApplyGravityBehaviour), nameof(negative)))
            );

            matcher.MatchStartForward(
                // bodyComp.Velocity.Y = Math.Min(bodyComp.Velocity.Y, PlayerValues.MAX_FALL);
                new CodeMatch(OpCodes.Ldloc_0),
                new CodeMatch(OpCodes.Ldflda, AccessTools.Field("JumpKing.Player.BodyComp:Velocity")),
                new CodeMatch(OpCodes.Ldloc_0),
                new CodeMatch(OpCodes.Ldflda, AccessTools.Field("JumpKing.Player.BodyComp:Velocity")),
                new CodeMatch(OpCodes.Ldfld, AccessTools.Field("Microsoft.Xna.Framework.Vector2:Y")),
                new CodeMatch(OpCodes.Call, AccessTools.Method("JumpKing.PlayerValues:get_MAX_FALL")),
                new CodeMatch(OpCodes.Call, AccessTools.Method("System.Math:Min", new Type[]{typeof(float), typeof(float)})),
                new CodeMatch(OpCodes.Stfld, AccessTools.Field("Microsoft.Xna.Framework.Vector2:Y"))
            ).ThrowIfInvalid($"Cant find code in {nameof(ApplyGravityBehaviour)}");
            matcher.Advance(6);
            matcher.RemoveInstruction().InsertAndAdvance(
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ApplyGravityBehaviour), nameof(capYVelocity)))
            );

            return matcher.Instructions();
        }

        private static float negative(float value) {
            return Utils.negative(value);
        }

        private static float capYVelocity(float y, float cap) {
            return ModEntry.IsUpsideDown() ? Math.Max(y, -cap) : Math.Min(y, cap);
        }
    }
}
