using EntityComponent;
using HarmonyLib;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.MiscEntities.WorldItems.Inventory;
using JumpKing.Mods;
using JumpKing.Player;
using JumpKing_Expansion_Blocks.Patches;
using System;
using System.Reflection;

namespace JumpKing_Expansion_Blocks
{
    [JumpKingMod("YutaGoto.JumpKing_Expansion_Blocks")]
    public static class ModEntry
    {
        private static readonly string harmonyId = "YutaGoto.JumpKing_Expansion_Blocks";
        public static readonly Harmony harmony = new Harmony(harmonyId);
        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {

            LevelManager.RegisterBlockFactory(new BlockFactory());
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

            if (player != null && collisionQuery != null)
            {
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.HighGravity), new Behaviours.HighGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.RainGravity), new Behaviours.RainGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SpecialHighGravity), new Behaviours.SpecialHighGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SuperLowGravity), new Behaviours.SuperLowGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SlipperyIce), new Behaviours.SlipperyIce());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.ZeroFriction), new Behaviours.ZeroFriction());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.OneWayIce), new Behaviours.OneWayIce());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Quicksand), new Behaviours.Quicksand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SideSand), new Behaviours.SideSand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.MagicSand), new Behaviours.MagicSand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.UpSand), new Behaviours.UpSand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.RestrainedIce), new Behaviours.RestrainedIce());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.CursedIce), new Behaviours.CursedIce());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.ReversedWalk), new Behaviours.ReversedWalk());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.ReversedCharge), new Behaviours.ReversedCharge());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Reflector), new Behaviours.Reflector());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Trampoline), new Behaviours.Trampoline());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Conveyor), new Behaviours.Conveyor());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.DeepWater), new Behaviours.DeepWater());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Accelerate), new Behaviours.Accelerate());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.InfinityJump), new Behaviours.InfinityJump(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.WallJump), new Behaviours.WallJump(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.DoubleJump), new Behaviours.DoubleJump(player));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.CloudJump), new Behaviours.CloudJump(player));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.DisabledJump), new Behaviours.DisabledJump());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.MultiWarp), new Behaviours.MultiWarp());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.QuickMove), new Behaviours.QuickMove());
            }
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() { }

        /// <summary>
        /// Setups the Harmony patching
        /// </summary>
        private static void PatchWithHarmony(Harmony harmony)
        {
            new PatchedJumpState(harmony);

            MethodInfo isOnBlockMethodBlock = typeof(BodyComp).GetMethod("IsOnBlock", new Type[] { typeof(Type) });
            MethodInfo postfixIsOnBlockPostfixMethod = typeof(ModEntry).GetMethod("IsOnBlockPostfix");
            originalIsOnBlock = harmony.Patch(isOnBlockMethodBlock);
            harmony.Patch(isOnBlockMethodBlock, postfix: new HarmonyMethod(postfixIsOnBlockPostfixMethod));

            MethodInfo isGetMultipliers = typeof(BodyComp).GetMethod("GetMultipliers");
            MethodInfo postfixGetMultipliers = typeof(PatchedBodyComp).GetMethod("GetMultipliersPostfix");
            harmony.Patch(isGetMultipliers, postfix: new HarmonyMethod(postfixGetMultipliers));

            MethodInfo isWearingSkin = AccessTools.TypeByName("SkinManager").GetMethod("IsWearingSkin", new Type[] {typeof(Items)});
            MethodInfo postfixIsWearingSkin = typeof(PatchedSkinManager).GetMethod("IsWearingSkinPostfix");
            harmony.Patch(isWearingSkin, postfix: new HarmonyMethod(postfixIsWearingSkin));

            MethodInfo hasItemEnabled = typeof(InventoryManager).GetMethod("HasItemEnabled", new Type[] { typeof(Items) });
            MethodInfo postfixHasItemEnabled = typeof(PatchedInventoryManager).GetMethod("HasItemEnabledPostfix");
            harmony.Patch(hasItemEnabled, postfix: new HarmonyMethod(postfixHasItemEnabled));
        }

        private static MethodInfo originalIsOnBlock;

        public static void IsOnBlockPostfix(object __instance, ref bool __result, Type __0)
        {
            if (__0 == typeof(IceBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(IceBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.SlipperyIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.ZeroFriction) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.OneWayIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.RestrainedIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.CursedIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.Trampoline) }) ||
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
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.UpSand) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.InfinityJump) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.WallJump) });
            }
        }
    }
}
