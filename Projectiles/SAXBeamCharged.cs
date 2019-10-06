using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SAXBeamCharged : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1800;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.light = 0.75f;
            projectile.coldDamage = true;
            aiType = ProjectileID.Bullet;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                if (!target.HasBuff(BuffID.Frozen))
                {
                    target.AddBuff(BuffID.Frozen, 100, true);
                }
            }
            target.AddBuff(BuffID.Frostburn, 600, true);
            target.AddBuff(BuffID.Chilled, 200, true);

        }
        public override void AI()
        {
            if (projectile.ai[0] != 0)
            {
                if (projectile.localAI[0] == 0)
                {
                    projectile.localAI[0] = projectile.position.X;
                }
                if (projectile.localAI[1] == 0)
                {
                    projectile.localAI[1] = projectile.position.Y;
                }
                float freq = 0.15f;
                float mag = 40f;
                int time = 1800 - projectile.timeLeft;
                Vector2 pos = new Vector2(projectile.localAI[0], projectile.localAI[1]);
                Vector2 dir = projectile.velocity;
                dir.Normalize();
                Vector2 axis = dir.RotatedBy(90 * projectile.ai[0] * 0.0174f);
                Vector2 wave = axis * (float)Math.Sin(time * freq) * mag;
                projectile.position = pos + wave;
                projectile.localAI[0] = projectile.position.X - wave.X + projectile.velocity.X;
                projectile.localAI[1] = projectile.position.Y - wave.Y + projectile.velocity.Y;
            }
        }
    }
}
