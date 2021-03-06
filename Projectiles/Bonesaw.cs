using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Bonesaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bonesaw");
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = 20;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true; 
            projectile.melee = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override void ModifyHitNPC(NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (npc.HitSound == SoundID.NPCHit2 || npc.HitSound == SoundID.DD2_SkeletonHurt)
            {
                crit = true;
            }
        }
        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < npc.width/12; i++)
                {
                    for (int j = 0; j < npc.height/12; j++)
                    {
                        Vector2 pos = npc.position + new Vector2(i * 12, j * 12);
                        //Vector2 dir = pos - npc.Center;
                        //dir.Normalize();
                        Vector2 vel = new Vector2(Main.rand.Next(9) - 4, Main.rand.Next(9) - 4);
                        Projectile.NewProjectile(pos, vel, ProjectileID.Bone, projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
            }
        }

    }
}