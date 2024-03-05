using EntityComponent;
using HarmonyLib;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.Mods;
using JumpKing.Player;
using System;
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
        public static void BeforeLevelLoad() => LevelManager.RegisterBlockFactory(new BlockFactory());

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

            PatchWithHarmony();
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            ICollisionQuery collisionQuery = LevelManager.Instance;

            if (player != null)
            {
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.HighGravity), new Behaviours.HighGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SlipperyIce), new Behaviours.SlipperyIce(player.m_body));
                //player.m_body.RegisterBlockBehaviour(typeof(Blocks.Quicksand), new Behaviours.Quicksand(collisionQuery));
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.Reflector), new Behaviours.Reflector());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.CopiedThinSnow), new Behaviours.CopiedThinSnow(player));
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
        private static void PatchWithHarmony()
        {
            var harmony = new Harmony("YutaGoto.JumpKing_Expansion_Blocks");
            harmony.PatchAll();

            var isOnBlockMethodBlock = typeof(BodyComp).GetMethod("IsOnBlock", new Type[] { typeof(Type) });
            var postfixMethod = typeof(ModEntry).GetMethod("IsOnBlockPostfix");
            originalIsOnBlock = harmony.Patch(isOnBlockMethodBlock);
            harmony.Patch(isOnBlockMethodBlock, postfix: new HarmonyMethod(postfixMethod));

            //var inOnBlockMethodSand = typeof(ICollisionQuery).GetMethod("IsOnBlock", new Type[] { typeof(Type) });
            //var postfixQueryMethod = typeof(ModEntry).GetMethod("IsOnBlockWithQueryPostfix");
            //originalIsOnBlock = harmony.Patch(inOnBlockMethodSand);
            //harmony.Patch(inOnBlockMethodSand, postfix: new HarmonyMethod(postfixQueryMethod));
        }


        /// <summary>
        /// Overwrite the IsOnBlock method to include SlipperyIce and IceBlock
        /// </summary>
        private static MethodInfo originalIsOnBlock;

        public static void IsOnBlockPostfix(object __instance, ref bool __result, Type __0)
        {
            if (__0 == typeof(IceBlock) && originalIsOnBlock != null)
            {
                // If IsOnBlock was called with an IceBlock, call the unpatched version of it with 
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(IceBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.SlipperyIce) });
            }
        }

        //public static void IsOnBlockWithQueryPostfix(ref bool __result, Type __0)
        //{
        //    if (__0 == typeof(SandBlock) && originalIsOnBlock != null)
        //    {
        //        // If IsOnBlock was called with an IceBlock, call the unpatched version of it with 
        //        __result = (bool)originalIsOnBlock.Invoke(null, new object[] { __0, typeof(SandBlock) }) ||
        //                   (bool)originalIsOnBlock.Invoke(null, new object[] { __0, typeof(Blocks.Quicksand) });
        //    }
        //}
    }
}
