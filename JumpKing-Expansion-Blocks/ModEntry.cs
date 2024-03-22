using BehaviorTree;
using EntityComponent;
using HarmonyLib;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.Mods;
using JumpKing.Player;
using JumpKing_Expansion_Blocks.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JumpKing_Expansion_Blocks
{
    [JumpKingMod("YutaGoto.JumpKing_Expansion_Blocks")]
    public static class ModEntry
    {
        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {
            LevelManager.RegisterBlockFactory(new BlockFactory());
            Harmony harmony = new Harmony("YutaGoto.JumpKing_Expansion_Blocks");
            PatchWithHarmony(harmony);
        }

        /// <summary>
        /// Called by Jump King when the level unloads
        /// </summary>
        [OnLevelUnload]
        public static void OnLevelUnload() { }

        /// <summary>
        /// Called by Jump King when the Level Starts
        /// </summary>
        [OnLevelStart]
        public static void OnLevelStart()
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            ICollisionQuery collisionQuery = LevelManager.Instance;
            BTselector bTselector = new BTselector();
            
            if (player != null)
            {
                bTselector.AddChild(new IsOnQuickSand(player));
                bTselector.AddChild(new IsOnSideSand(player));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.HighGravity), new Behaviours.HighGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SlipperyIce), new Behaviours.SlipperyIce(player.m_body));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.ZeroFriction), new Behaviours.ZeroFriction(player.m_body));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Quicksand), new Behaviours.Quicksand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SideSand), new Behaviours.SideSand(collisionQuery));
                // player.m_body.RegisterBlockBehaviour(typeof(Blocks.CompactedSnowAndIce), new Behaviours.CompactedSnowAndIce(player.m_body, player));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Reflector), new Behaviours.Reflector());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.CopiedThinSnow), new Behaviours.CopiedThinSnow(player));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Conveyor), new Behaviours.Conveyor(player.m_body));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.DeepWater), new Behaviours.DeepWater());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Accelerate), new Behaviours.Accelerate());
            }
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() {
            
        }

        /// <summary>
        /// Setups the Harmony patching
        /// </summary>
        private static void PatchWithHarmony(Harmony harmony)
        {
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            MethodInfo isOnBlockMethodBlock = typeof(BodyComp).GetMethod("IsOnBlock", new Type[] { typeof(Type) });
            MethodInfo postfixIsOnBlockPostfixMethod = typeof(ModEntry).GetMethod("IsOnBlockPostfix");
            originalIsOnBlock = harmony.Patch(isOnBlockMethodBlock);
            harmony.Patch(isOnBlockMethodBlock, postfix: new HarmonyMethod(postfixIsOnBlockPostfixMethod));

            MethodInfo isGetMultipliers = typeof(BodyComp).GetMethod("GetMultipliers");
            MethodInfo postfixGetMultipliers = typeof(ModEntry).GetMethod("GetMultipliersPostfix");
            harmony.Patch(isGetMultipliers, postfix: new HarmonyMethod(postfixGetMultipliers));
        }

        private static MethodInfo originalIsOnBlock;

        public static void IsOnBlockPostfix(object __instance, ref bool __result, Type __0)
        {
            if (__0 == typeof(IceBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(IceBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.SlipperyIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.ZeroFriction) });

                return;
            }

            if (__0 == typeof(WaterBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(WaterBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.DeepWater) });

                return;
            }

            if (__0 == typeof(SandBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(SandBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.Quicksand) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.SideSand) });

                return;
            }
        }

        public static void GetMultipliersPostfix(ref float __result, BodyComp __instance)
        {
            ICollisionQuery collisionQuery = LevelManager.Instance;
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            Rectangle hitbox = player.m_body.GetHitbox();
            collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            if (info.IsCollidingWith<Blocks.DeepWater>())
            {
                __result = 0.25f;
            }
            else if (info.IsCollidingWith<Blocks.Accelerate>())
            {
                __result = 2f;
            }
        }
    }
}
