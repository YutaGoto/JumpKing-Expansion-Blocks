using EntityComponent;
using HarmonyLib;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.Mods;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Reflection;

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
            Harmony harmony = new Harmony("YutaGoto.JumpKing_Expansion_Blocks");
            PatchWithHarmony(harmony);

            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            ICollisionQuery collisionQuery = LevelManager.Instance;
            
            if (player != null)
            {
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.HighGravity), new Behaviours.HighGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SlipperyIce), new Behaviours.SlipperyIce());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.ZeroFriction), new Behaviours.ZeroFriction());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Quicksand), new Behaviours.Quicksand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SideSand), new Behaviours.SideSand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.MagicSand), new Behaviours.MagicSand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.CursedIce), new Behaviours.CursedIce(player.m_body));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Reflector), new Behaviours.Reflector());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.CopiedThinSnow), new Behaviours.CopiedThinSnow(player));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Conveyor), new Behaviours.Conveyor(player.m_body));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.DeepWater), new Behaviours.DeepWater());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Accelerate), new Behaviours.Accelerate());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.InfinityJump), new Behaviours.InfinityJump(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.WallJump), new Behaviours.WallJump(collisionQuery));
            }
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() {
            Harmony harmony = new Harmony("YutaGoto.JumpKing_Expansion_Blocks");
            // Unpatch all patches for touching Babes
            harmony.UnpatchAll();
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

            MethodInfo isWearingSkin = AccessTools.TypeByName("SkinManager").GetMethod("IsWearingSkin", new Type[] {typeof(Items)});
            MethodInfo postfixIsWearingSkin = typeof(ModEntry).GetMethod("IsWearingSkinPostfix");
            harmony.Patch(isWearingSkin, postfix: new HarmonyMethod(postfixIsWearingSkin));
        }

        private static MethodInfo originalIsOnBlock;

        public static void IsOnBlockPostfix(object __instance, ref bool __result, Type __0)
        {
            if (__0 == typeof(IceBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(IceBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.SlipperyIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.ZeroFriction) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.CursedIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.InfinityJump) });
            }

            if (__0 == typeof(WaterBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(WaterBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.DeepWater) });
            }

            if (__0 == typeof(SnowBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(SnowBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.WallJump) });
            }

            if (__0 == typeof(SandBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(SandBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.Quicksand) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.SideSand) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.MagicSand) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.InfinityJump) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.WallJump) });
            }
        }

        /// <summary>
        /// for DeepWater and Accelerate block. patch for GetMultipliers method that is attatch to BodyComp to modify the player's speed
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="__instance"></param>
        public static void GetMultipliersPostfix(ref float __result, BodyComp __instance)
        {
            ICollisionQuery collisionQuery = LevelManager.Instance;
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            Rectangle hitbox = player.m_body.GetHitbox();
            collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            if (info.IsCollidingWith<Blocks.DeepWater>())
            {
                __result *= 0.25f;
            }
            else if (info.IsCollidingWith<Blocks.Accelerate>())
            {
                __result *= 2f;
            }
        }

        /// <summary>
        /// for CursedIce block. patch for IsWearingSkin method that is attatch to Skin to wearing GiantBoots
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="p_item"></param>
        public static void IsWearingSkinPostfix(ref bool __result, Items p_item)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            if (player.m_body.IsOnBlock<Blocks.CursedIce>() && p_item == Items.GiantBoots)
            {
                __result = true;
            }
        }
    }
}
