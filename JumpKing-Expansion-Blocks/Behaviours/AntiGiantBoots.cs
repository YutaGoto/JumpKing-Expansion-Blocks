using HarmonyLib;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using System.Reflection;
using System;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.MiscEntities.WorldItems.Inventory;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class AntiGiantBoots: IBlockBehaviour
    {
        public float BlockPriority => 2.0f;

        public bool IsPlayerOnBlock { get; set; }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext) => inputYVelocity;

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext) => inputGravity;

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.AntiGiantBoots>();
            }

            if (IsPlayerOnBlock && InventoryManager.HasItemEnabled(Items.GiantBoots))
            {
                MethodInfo disabledSkin = AccessTools.TypeByName("SkinManager").GetMethod("DisableSkin", new Type[] { typeof(Items) });
                disabledSkin.Invoke(null, new object[] { Items.GiantBoots });
            }

            return true;
        }
    }
}
