using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class TerraSpearBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Spear");
        }
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 54;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 300;
            projectile.alpha = 55;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            projectile.tileCollide = true;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }
        public override void AI()
        {
            if ((projectile.timeLeft % 2) == 0)
            {
                //say you wanted to add particles that stay mostly still to leave a trail behind a projectile
                int num1 = Dust.NewDust(
                         projectile.position,
                         projectile.width,
                         projectile.height,
                         74, //Dust ID
                         projectile.velocity.X,
                         projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default(Color),
                         1f
                         );

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(
                         projectile.position,
                         projectile.width,
                         projectile.height,
                         74, //Dust ID
                         projectile.velocity.X,
                         projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default(Color),
                         1f
                         );

                Main.dust[dust].noGravity = true;
            }
        }

    }
}
