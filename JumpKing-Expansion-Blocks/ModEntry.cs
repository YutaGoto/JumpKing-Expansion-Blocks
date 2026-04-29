using EntityComponent;
using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.MiscEntities.WorldItems.Inventory;
using JumpKing.Mods;
using JumpKing.Player;
using JumpKing.Workshop;
using JumpKing_Expansion_Blocks.Patches;
using System;
using System.IO;
using System.Reflection;

namespace JumpKing_Expansion_Blocks
{
    [JumpKingMod("YutaGoto.JumpKing_Expansion_Blocks")]
    public static class ModEntry
    {
        private static readonly string harmonyId = "YutaGoto.JumpKing_Expansion_Blocks";
        public static readonly Harmony harmony = new Harmony(harmonyId);
        public static string[] Tags { get; set; } = new string[0];
        public static string AssemblyPath { get; set; }
        public static ulong LevelID { get; set; }
        public static PlayerEntity Player { get; set; }

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {
            //#if DEBUG
            //            Debug.WriteLine("-------");
            //            Debugger.Launch();
            //            Harmony.DEBUG = true;
            //#endif

            LevelManager.RegisterBlockFactory(new BlockFactory());
            PatchWithHarmony(harmony);
            AssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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
            Level level = Game1.instance.contentManager.level;

            if (level is null)
            {
                return;
            }
            LevelID = level.ID;
            Tags = XmlSerializerHelper.Deserialize<Level.LevelSettings>($"{Game1.instance.contentManager.root}\\{Level.FileName}").Tags;
            Player = EntityManager.instance.Find<PlayerEntity>();
            ICollisionQuery collisionQuery = LevelManager.Instance;

            if (Player != null && collisionQuery != null)
            {
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.HighGravity), new Behaviours.HighGravity());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.RainGravity), new Behaviours.RainGravity());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SpecialHighGravity), new Behaviours.SpecialHighGravity());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SuperLowGravity), new Behaviours.SuperLowGravity());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SlipperyIce), new Behaviours.SlipperyIce());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.ZeroFriction), new Behaviours.ZeroFriction());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.DiamondHandsIce), new Behaviours.DiamondHandsIce());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.OneWayIce), new Behaviours.OneWayIce());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.HeavyIce), new Behaviours.HeavyIce());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.Quicksand), new Behaviours.Quicksand(collisionQuery));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SideSand), new Behaviours.SideSand(collisionQuery));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.MagicSand), new Behaviours.MagicSand(collisionQuery));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.UpSand), new Behaviours.UpSand(collisionQuery));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.RestrainedIce), new Behaviours.RestrainedIce());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.CursedIce), new Behaviours.CursedIce());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.ReversedWalk), new Behaviours.ReversedWalk());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.ReversedCharge), new Behaviours.ReversedCharge());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SuperCharge), new Behaviours.SuperCharge());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.ReversedGravity), new Behaviours.ReversedGravity());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.Ascend), new Behaviours.Ascend());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.MoveUp), new Behaviours.MoveUp());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.Reflector), new Behaviours.Reflector());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.Trampoline), new Behaviours.Trampoline());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.Conveyor), new Behaviours.Conveyor());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.DeepWater), new Behaviours.DeepWater());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.Accelerate), new Behaviours.Accelerate());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SpeedUp), new Behaviours.SpeedUp());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.NoResetVelocity), new Behaviours.NoResetVelocity());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.MoreFallSpeed), new Behaviours.MoreFallSpeed());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.InfinityJump), new Behaviours.InfinityJump(collisionQuery));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.WallJump), new Behaviours.WallJump(collisionQuery));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.DoubleJump), new Behaviours.DoubleJump(Player));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.JumpStepHop), new Behaviours.JumpStepHop(Player));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.AutoJumpCharge), new Behaviours.AutoJumpCharge());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.AirJump), new Behaviours.AirJump(Player));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.AirDash), new Behaviours.AirDash(Player, collisionQuery));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.Flapping), new Behaviours.Flapping(Player));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.CloudJump), new Behaviours.CloudJump(Player));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.AerialJump), new Behaviours.AerialJump(Player));
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.ForceFramesJump), new Behaviours.ForceFramesJump());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.DisabledSmallJump), new Behaviours.DisabledSmallJump());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.RevokeJumpCharge), new Behaviours.RevokeJumpCharge());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.RevokeWalking), new Behaviours.RevokeWalking());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.ForceDirectionJump), new Behaviours.ForceDirectionJump());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.AntiGiantBoots), new Behaviours.AntiGiantBoots());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SideLock), new Behaviours.SideLock());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.MultiWarp), new Behaviours.MultiWarp());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.QuickMove), new Behaviours.QuickMove());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.SoftPlatform), new Behaviours.SoftPlatform());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.CeilingShift), new Behaviours.CeilingShift());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.CeilingShiftSolid), new Behaviours.CeilingShiftSolid());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.JkqPlatform), new Behaviours.JkqPlatform());
                Player.m_body.RegisterBlockBehaviour(typeof(Blocks.TrapHopping), new Behaviours.TrapHopping());
            }
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd()
        {
            Player = null;
        }

        /// <summary>
        /// Setups the Harmony patching
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Bug", "S1848:Objects should not be created to be dropped immediately without being used", Justification = "<保留中>")]
        private static void PatchWithHarmony(Harmony harmony)
        {
            new PatchedJumpState(harmony);
            new PatchedWalk(harmony);
            new PatchedApplyGravityBehaviour(harmony);

            // new DrawDebug(harmony);

            MethodInfo isOnBlockMethodBlock = typeof(BodyComp).GetMethod("IsOnBlock", new Type[] { typeof(Type) });
            MethodInfo postfixIsOnBlockPostfixMethod = typeof(ModEntry).GetMethod("IsOnBlockPostfix");
            originalIsOnBlock = harmony.Patch(isOnBlockMethodBlock);
            harmony.Patch(isOnBlockMethodBlock, postfix: new HarmonyMethod(postfixIsOnBlockPostfixMethod));

            MethodInfo isGetMultipliers = typeof(BodyComp).GetMethod("GetMultipliers");
            MethodInfo postfixGetMultipliers = typeof(PatchedBodyComp).GetMethod("GetMultipliersPostfix");
            harmony.Patch(isGetMultipliers, postfix: new HarmonyMethod(postfixGetMultipliers));

            MethodInfo isWearingSkin = AccessTools.TypeByName("SkinManager").GetMethod("IsWearingSkin", new Type[] { typeof(Items) });
            MethodInfo postfixIsWearingSkin = typeof(PatchedSkinManager).GetMethod("IsWearingSkinPostfix");
            harmony.Patch(isWearingSkin, postfix: new HarmonyMethod(postfixIsWearingSkin));

            MethodInfo hasItemEnabled = typeof(InventoryManager).GetMethod("HasItemEnabled", new Type[] { typeof(Items) });
            MethodInfo postfixHasItemEnabled = typeof(PatchedInventoryManager).GetMethod("HasItemEnabledPostfix");
            harmony.Patch(hasItemEnabled, postfix: new HarmonyMethod(postfixHasItemEnabled));

            MethodInfo resolveXCollisionBehaviour = typeof(ResolveXCollisionBehaviour).GetMethod("ExecuteBehaviour", new Type[] { typeof(BehaviourContext) });
            MethodInfo prefixResolveXCollisionBehaviour = typeof(PatchedResolveXCollisionBehaviour).GetMethod("UpdateXIfCompressedAgainstWall");
            harmony.Patch(resolveXCollisionBehaviour, prefix: new HarmonyMethod(prefixResolveXCollisionBehaviour));
        }

        private static MethodInfo originalIsOnBlock;

        public static void IsOnBlockPostfix(object __instance, ref bool __result, Type __0)
        {
            if (__0 == typeof(IceBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(IceBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.SlipperyIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.ZeroFriction) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.DiamondHandsIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.OneWayIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.HeavyIce) }) ||
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
