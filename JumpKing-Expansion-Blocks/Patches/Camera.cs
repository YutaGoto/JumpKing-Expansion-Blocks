using HarmonyLib;
using JK = JumpKing;

using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

using System;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Patches
{
    public class Camera
    {
        public Camera (Harmony harmony)
        {
            Type type = typeof(JK.Camera);
            Type[] parameterTypes = new Type[]
            {
                typeof(Point),
                typeof(Vector2),
                typeof(bool)
            };
            MethodInfo UpdateCameraWithVelocity = AccessTools.Method(type, "UpdateCameraWithVelocity", parameterTypes);
            harmony.Patch(
                UpdateCameraWithVelocity,
                transpiler: new HarmonyMethod(AccessTools.Method(typeof(Camera), nameof(transpileUpdateCameraWithVelocity)))
            );
        }

        private static IEnumerable<CodeInstruction> transpileUpdateCameraWithVelocity(IEnumerable<CodeInstruction> instructions , ILGenerator generator) {
            CodeMatcher matcher = new CodeMatcher(instructions , generator);

            matcher.MatchStartForward(
                    // p_velocity.Y < 0f
                    new CodeMatch(OpCodes.Ldarg_1),
                    new CodeMatch(OpCodes.Ldfld, AccessTools.Field("Microsoft.Xna.Framework.Vector2:Y")),
                    new CodeMatch(OpCodes.Ldc_R4, (float)0),
                    new CodeMatch(OpCodes.Bge_Un_S)
                )
                .ThrowIfInvalid($"Cant find code in {nameof(Camera)}");
            matcher.Advance(3);
            var label = matcher.Instruction.operand;
            matcher.RemoveInstruction();
            matcher.Insert(
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Camera), nameof(revGEff))),
                    new CodeInstruction(OpCodes.Brtrue_S, label)
                );

            return matcher.Instructions();
        }

        private static bool revGEff(float left, float right) {
            return Utils.reverseComparison(left, right, "ge");
        }
    }
}
