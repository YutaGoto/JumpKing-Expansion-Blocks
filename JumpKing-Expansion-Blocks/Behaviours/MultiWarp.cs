using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class MultiWarp : IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public MultiWarp()
        {
        }
        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.MultiWarp>();
            }

            if (behaviourContext != null && behaviourContext.LastFrameCollisionInfo?.PreResolutionCollisionInfo != null)
            {
                BodyComp m_bodyComp = behaviourContext.BodyComp;
                Point center = m_bodyComp.GetHitbox().Center;
                int num2 = -1;
                int numOffset = 0;
				float num = 0f;
                Blocks.MultiWarp multiWarp = behaviourContext.LastFrameCollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.MultiWarp>().FirstOrDefault() as Blocks.MultiWarp;

                if (IsPlayerOnBlock && multiWarp != null && (center.X <= 0 || center.X >= 480.0))
                {
                    if (center.X <= 0)
                    {
                        num = 479.8f;
                    }
                    else if (center.X >= 480)
                    {
                        num = -479.8f;
                    }

                    m_bodyComp.Position.X += num;
                    numOffset = multiWarp.Offset;
					num2 = multiWarp.ToScreenNo + (numOffset * 255);

					if (num2 == -1)
                    {
                        throw new InvalidOperationException($"Cannot teleport: Player Position '({m_bodyComp.Position.X},{m_bodyComp.Position.Y})' " + "however no valid Teleport Link was found!");
                    }

                    int num4 = (int)((0f - (m_bodyComp.Position.Y - 360f)) / 360f);
                    int num5 = num2 - num4 - 1;
                    int num6 = 360 * num5;
                    float num7 = (int)m_bodyComp.Position.Y;
                    _ = m_bodyComp.Position;
                    float y = num7 - (float)num6;
                    m_bodyComp.Position.Y = y;
                    center.Y -= num6;
                    Camera.UpdateCamera(center);
                }
            }

            return true;
        }
    }
}
