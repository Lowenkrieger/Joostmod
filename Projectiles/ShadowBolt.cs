using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ShadowBolt : ModProjectile
	{public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow bolt");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 50;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frame = (projectile.frame + 1) % 4;
				projectile.frameCounter = 0;
			}
		}
	}
}

