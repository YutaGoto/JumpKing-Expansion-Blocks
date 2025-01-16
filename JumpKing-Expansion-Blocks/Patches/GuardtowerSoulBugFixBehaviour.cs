using HarmonyLib;
using JK = JumpKing.BodyCompBehaviours;

using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

using System;

namespace JumpKing_Expansion_Blocks.Patches
{
    public class GuardtowerSoulBugFixBehaviour
    {
        public GuardtowerSoulBugFixBehaviour (Harmony harmony)
        {
            Type type = typeof(JK.GuardtowerSoulBugFixBehaviour);
            MethodInfo ExecuteBehaviour = type.GetMethod(nameof(JK.GuardtowerSoulBugFixBehaviour.ExecuteBehaviour));
            harmony.Patch(
                ExecuteBehaviour,
                transpiler: new HarmonyMethod(AccessTools.Method(typeof(GuardtowerSoulBugFixBehaviour), nameof(transpileExecuteBehaviour)))
            );
        }

        private static IEnumerable<CodeInstruction> transpileExecuteBehaviour(IEnumerable<CodeInstruction> instructions , ILGenerator generator) {
            CodeMatcher matcher = new CodeMatcher(instructions , generator);

            matcher.MatchStartForward(
                // (int)behaviourContext["YStep"]) in `int num2 = (flag2 ? ((int)behaviourContext["YStep"]) : (-1));`
                new CodeMatch(OpCodes.Ldarg_1),
                new CodeMatch(OpCodes.Ldstr, "YStep"),
                new CodeMatch(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Dictionary<string, object>), "Item")),
                new CodeMatch(OpCodes.Unbox_Any, typeof(int))
            ).ThrowIfInvalid($"Cant find code in {nameof(GuardtowerSoulBugFixBehaviour)}");
            matcher.Advance(4);
            matcher.InsertAndAdvance(
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(GuardtowerSoulBugFixBehaviour), nameof(negative)))
            );

            return matcher.Instructions();
        }

        private static int negative(int value) {
            return (int)Utils.negative(value);
        }
    }
}
