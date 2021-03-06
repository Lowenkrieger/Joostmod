using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SaplingFishHook : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BobberWooden);
            projectile.aiStyle = -1;
            projectile.bobber = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling Fish Hook");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.timeLeft = 60;
            if (player.HeldItem.fishingPole == 0 || player.CCed || player.noItems || player.pulley || player.dead || !player.active || !player.GetModPlayer<JoostPlayer>().fishingSapling)
            {
                projectile.Kill();
            }
            if (projectile.ai[1] > 0f && projectile.localAI[1] >= 0f)
            {
                projectile.localAI[1] = -1f;
                if (!projectile.lavaWet && !projectile.honeyWet)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        int num = Dust.NewDust(new Vector2(projectile.position.X - 6f, projectile.position.Y - 10f), projectile.width + 12, 24, Dust.dustWater(), 0f, 0f, 0, default(Color), 1f);
                        Dust dust = Main.dust[num];
                        dust.velocity.Y = dust.velocity.Y - 4f;
                        Dust dust2 = Main.dust[num];
                        dust2.velocity.X = dust2.velocity.X * 2.5f;
                        Main.dust[num].scale = 0.8f;
                        Main.dust[num].alpha = 100;
                        Main.dust[num].noGravity = true;
                    }
                    Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 0, 1f, 0f);
                }
            }
            if (projectile.ai[0] >= 1f)
            {
                if (projectile.ai[0] == 2f)
                {
                    projectile.ai[0] += 1f;
                    Main.PlaySound(SoundID.Item17, projectile.position);
                    if (!projectile.lavaWet && !projectile.honeyWet)
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            int num2 = Dust.NewDust(new Vector2(projectile.position.X - 6f, projectile.position.Y - 10f), projectile.width + 12, 24, Dust.dustWater(), 0f, 0f, 0, default(Color), 1f);
                            Dust dust3 = Main.dust[num2];
                            dust3.velocity.Y = dust3.velocity.Y - 4f;
                            Dust dust4 = Main.dust[num2];
                            dust4.velocity.X = dust4.velocity.X * 2.5f;
                            Main.dust[num2].scale = 0.8f;
                            Main.dust[num2].alpha = 100;
                            Main.dust[num2].noGravity = true;
                        }
                        Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 0, 1f, 0f);
                    }
                }
                if (projectile.localAI[0] < 100f)
                {
                    projectile.localAI[0] += 1f;
                }
                projectile.tileCollide = false;
                float num3 = 15.9f;
                int num4 = 10;
                float num5 = player.Center.X - projectile.Center.X;
                float num6 = player.Center.Y - projectile.Center.Y;
                float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
                if (num7 > 3000f)
                {
                    projectile.Kill();
                }
                num7 = num3 / num7;
                num5 *= num7;
                num6 *= num7;
                projectile.velocity.X = (projectile.velocity.X * (float)(num4 - 1) + num5) / (float)num4;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num4 - 1) + num6) / (float)num4;
                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
                    if (rectangle.Intersects(value))
                    {
                        if (projectile.ai[1] > 0f)
                        {
                            int num8 = (int)projectile.ai[1];
                            Item item = new Item();
                            item.SetDefaults(num8, false);
                            if (num8 == 3196)
                            {
                                int num9 = player.FishingLevel();
                                int minValue = (num9 / 20 + 3) / 2;
                                int num10 = (num9 / 10 + 6) / 2;
                                if (Main.rand.Next(50) < num9)
                                {
                                    num10++;
                                }
                                if (Main.rand.Next(100) < num9)
                                {
                                    num10++;
                                }
                                if (Main.rand.Next(150) < num9)
                                {
                                    num10++;
                                }
                                if (Main.rand.Next(200) < num9)
                                {
                                    num10++;
                                }
                                int stack = Main.rand.Next(minValue, num10 + 1);
                                item.stack = stack;
                            }
                            if (num8 == 3197)
                            {
                                int num11 = player.FishingLevel();
                                int minValue2 = (num11 / 4 + 15) / 2;
                                int num12 = (num11 / 2 + 30) / 2;
                                if (Main.rand.Next(50) < num11)
                                {
                                    num12 += 4;
                                }
                                if (Main.rand.Next(100) < num11)
                                {
                                    num12 += 4;
                                }
                                if (Main.rand.Next(150) < num11)
                                {
                                    num12 += 4;
                                }
                                if (Main.rand.Next(200) < num11)
                                {
                                    num12 += 4;
                                }
                                int stack2 = Main.rand.Next(minValue2, num12 + 1);
                                item.stack = stack2;
                            }
                            ItemLoader.CaughtFishStack(item);
                            item.newAndShiny = true;
                            Item item2 = player.GetItem(projectile.owner, item, false, false);
                            if (item2.stack > 0)
                            {
                                int number = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, num8, 1, false, 0, true, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            else
                            {
                                item.position.X = projectile.Center.X - (float)(item.width / 2);
                                item.position.Y = projectile.Center.Y - (float)(item.height / 2);
                                item.active = true;
                                ItemText.NewText(item, 0, false, false);
                            }
                        }
                        projectile.Kill();
                    }
                }
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                return;
            }
            bool flag = false;
            float num13 = player.Center.X - projectile.Center.X;
            float num14 = player.Center.Y - projectile.Center.Y;
            projectile.rotation = (float)Math.Atan2((double)num14, (double)num13) + 1.57f;
            float num15 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
            if (num15 > 900f)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.wet)
            {
                projectile.rotation = 0f;
                projectile.velocity.X = projectile.velocity.X * 0.9f;
                int num16 = (int)(projectile.Center.X + (float)((projectile.width / 2 + 8) * projectile.direction)) / 16;
                int num17 = (int)(projectile.Center.Y / 16f);
                float num18 = projectile.position.Y / 16f;
                int num19 = (int)((projectile.position.Y + (float)projectile.height) / 16f);
                if (Main.tile[num16, num17] == null)
                {
                    Main.tile[num16, num17] = new Tile();
                }
                if (Main.tile[num16, num19] == null)
                {
                    Main.tile[num16, num19] = new Tile();
                }
                if (projectile.velocity.Y > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y * 0.5f;
                }
                num16 = (int)(projectile.Center.X / 16f);
                num17 = (int)(projectile.Center.Y / 16f);
                float num20 = projectile.position.Y + (float)projectile.height;
                if (Main.tile[num16, num17 - 1] == null)
                {
                    Main.tile[num16, num17 - 1] = new Tile();
                }
                if (Main.tile[num16, num17] == null)
                {
                    Main.tile[num16, num17] = new Tile();
                }
                if (Main.tile[num16, num17 + 1] == null)
                {
                    Main.tile[num16, num17 + 1] = new Tile();
                }
                if (Main.tile[num16, num17 - 1].liquid > 0)
                {
                    num20 = (float)(num17 * 16);
                    num20 -= (float)(Main.tile[num16, num17 - 1].liquid / 16);
                }
                else if (Main.tile[num16, num17].liquid > 0)
                {
                    num20 = (float)((num17 + 1) * 16);
                    num20 -= (float)(Main.tile[num16, num17].liquid / 16);
                }
                else if (Main.tile[num16, num17 + 1].liquid > 0)
                {
                    num20 = (float)((num17 + 2) * 16);
                    num20 -= (float)(Main.tile[num16, num17 + 1].liquid / 16);
                }
                if (projectile.Center.Y > num20)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                    if (projectile.velocity.Y < -8f)
                    {
                        projectile.velocity.Y = -8f;
                    }
                    if (projectile.Center.Y + projectile.velocity.Y < num20)
                    {
                        projectile.velocity.Y = num20 - projectile.Center.Y;
                    }
                }
                else
                {
                    projectile.velocity.Y = num20 - projectile.Center.Y;
                }
                if ((double)projectile.velocity.Y >= -0.01 && (double)projectile.velocity.Y <= 0.01)
                {
                    flag = true;
                }
            }
            else
            {
                if (projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.95f;
                }
                projectile.velocity.X = projectile.velocity.X * 0.98f;
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                if (projectile.velocity.Y > 15.9f)
                {
                    projectile.velocity.Y = 15.9f;
                }
            }
            if (Main.myPlayer == projectile.owner)
            {
                int num21 = player.FishingLevel();
                if (num21 < 0 && num21 == -1)
                {
                    player.displayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
                }
            }
            if (projectile.ai[1] != 0f)
            {
                flag = true;
            }
            if (flag)
            {
                if (projectile.ai[1] == 0f && Main.myPlayer == projectile.owner)
                {
                    int num22 = player.FishingLevel();
                    if (num22 == -9000)
                    {
                        projectile.localAI[1] += 5f;
                        projectile.localAI[1] += (float)Main.rand.Next(1, 3);
                        if (projectile.localAI[1] > 660f)
                        {
                            projectile.localAI[1] = 0f;
                            projectile.FishingCheck();
                            return;
                        }
                    }
                    else
                    {
                        if (Main.rand.Next(300) < num22)
                        {
                            projectile.localAI[1] += (float)Main.rand.Next(1, 3);
                        }
                        projectile.localAI[1] += (float)(num22 / 30);
                        projectile.localAI[1] += (float)Main.rand.Next(1, 3);
                        if (Main.rand.Next(60) == 0)
                        {
                            projectile.localAI[1] += 60f;
                        }
                        if (projectile.localAI[1] > 660f)
                        {
                            projectile.localAI[1] = 0f;
                            projectile.FishingCheck();
                            return;
                        }
                    }
                }
                else if (projectile.ai[1] < 0f)
                {
                    if (projectile.velocity.Y == 0f || (projectile.honeyWet && (double)projectile.velocity.Y >= -0.01 && (double)projectile.velocity.Y <= 0.01))
                    {
                        projectile.velocity.Y = (float)Main.rand.Next(100, 500) * 0.015f;
                        projectile.velocity.X = (float)Main.rand.Next(-100, 101) * 0.015f;
                        projectile.wet = false;
                        projectile.lavaWet = false;
                        projectile.honeyWet = false;
                    }
                    projectile.ai[1] += (float)Main.rand.Next(1, 5);
                    if (projectile.ai[1] >= 0f)
                    {
                        projectile.ai[1] = 0f;
                        projectile.localAI[1] = 0f;
                        projectile.netUpdate = true;
                    }
                }
            }
            AutoFish();
            return;
        }
        private void AutoFish()
        {
            Player player = Main.player[projectile.owner];
            if (player.whoAmI == Main.myPlayer && projectile.ai[0] == 0f && projectile.ai[1] < 0f && projectile.localAI[1] != 0f)
            {
                projectile.ai[0] = 1f;
                float num12 = -10f;
                if (projectile.wet && projectile.velocity.Y > num12)
                {
                    projectile.velocity.Y = num12;
                }
                projectile.netUpdate2 = true;
                bool flag4 = false;
                int num13 = 0;
                for (int num14 = 0; num14 < 58; num14++)
                {
                    if (player.inventory[num14].stack > 0 && player.inventory[num14].bait > 0)
                    {
                        bool flag5 = false;
                        int num15 = 1 + player.inventory[num14].bait / 5;
                        if (num15 < 1)
                        {
                            num15 = 1;
                        }
                        if (player.accTackleBox)
                        {
                            num15++;
                        }
                        if (Main.rand.Next(num15) == 0)
                        {
                            flag5 = true;
                        }
                        if (projectile.localAI[1] < 0f)
                        {
                            flag5 = true;
                        }
                        if (projectile.localAI[1] > 0f)
                        {
                            Item item3 = new Item();
                            item3.SetDefaults((int)projectile.localAI[1], false);
                            if (item3.rare < 0)
                            {
                                flag5 = false;
                            }
                        }
                        if (flag5)
                        {
                            num13 = player.inventory[num14].type;
                            if (ItemLoader.ConsumeItem(player.inventory[num14], player))
                            {
                                Item item2 = player.inventory[num14];
                                Item arg_BFD_0 = item2;
                                arg_BFD_0.stack = item2.stack - 1;
                            }
                            if (player.inventory[num14].stack <= 0)
                            {
                                player.inventory[num14].SetDefaults(0, false);
                            }
                        }
                        flag4 = true;
                        break;
                    }
                }
                if (flag4)
                {
                    if (num13 == 2673)
                    {
                        if (Main.netMode != 1)
                        {
                            NPC.SpawnOnPlayer(player.whoAmI, 370);
                        }
                        else
                        {
                            NetMessage.SendData(61, -1, -1, null, player.whoAmI, 370f, 0f, 0f, 0, 0, 0);
                        }
                        projectile.ai[0] = 2f;
                    }
                    else if (Main.rand.Next(7) == 0 && !player.accFishingLine)
                    {
                        projectile.ai[0] = 2f;
                    }
                    else
                    {
                        projectile.ai[1] = projectile.localAI[1];
                    }
                    projectile.netUpdate = true;
                }

            }
        }
        public override bool PreDrawExtras(SpriteBatch spriteBatch)     //projectile draws the fishing line correctly
        {
            Player player = Main.player[projectile.owner];
            float pPosX = player.MountedCenter.X;
            float pPosY = player.MountedCenter.Y;
            pPosY += player.gfxOffY;
            float gravDir = player.gravDir;
            pPosX -= (float)(24 * player.direction);
            pPosY -= 4 * player.gravDir;
            if (player.gravDir < 0)
            {
                pPosY -= 12;
            }
            if (player.direction < 0)
            {
                pPosX -= 12;
            }

            Vector2 value = new Vector2(pPosX, pPosY);
            value = player.RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
            float projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
            float projPosY = projectile.position.Y + (float)projectile.height * 0.5f - value.Y;
            Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
            float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
            bool flag2 = true;
            if (projPosX == 0f && projPosY == 0f)
            {
                flag2 = false;
            }
            else
            {
                float projPosXY = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                projPosXY = 12f / projPosXY;
                projPosX *= projPosXY;
                projPosY *= projPosXY;
                value.X -= projPosX;
                value.Y -= projPosY;
                projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
                projPosY = projectile.position.Y + (float)projectile.height * 0.5f - value.Y;
            }
            while (flag2)
            {
                float num = 12f;
                float num2 = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                float num3 = num2;
                if (float.IsNaN(num2) || float.IsNaN(num3))
                {
                    flag2 = false;
                }
                else
                {
                    if (num2 < 20f)
                    {
                        num = num2 - 8f;
                        flag2 = false;
                    }
                    num2 = 12f / num2;
                    projPosX *= num2;
                    projPosY *= num2;
                    value.X += projPosX;
                    value.Y += projPosY;
                    projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
                    projPosY = projectile.position.Y + (float)projectile.height * 0.1f - value.Y;
                    if (num3 > 12f)
                    {
                        float num4 = 0.3f;
                        float num5 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                        if (num5 > 16f)
                        {
                            num5 = 16f;
                        }
                        num5 = 1f - num5 / 16f;
                        num4 *= num5;
                        num5 = num3 / 80f;
                        if (num5 > 1f)
                        {
                            num5 = 1f;
                        }
                        num4 *= num5;
                        if (num4 < 0f)
                        {
                            num4 = 0f;
                        }
                        num5 = 1f - projectile.localAI[0] / 100f;
                        num4 *= num5;
                        if (projPosY > 0f)
                        {
                            projPosY *= 1f + num4;
                            projPosX *= 1f - num4;
                        }
                        else
                        {
                            num5 = Math.Abs(projectile.velocity.X) / 3f;
                            if (num5 > 1f)
                            {
                                num5 = 1f;
                            }
                            num5 -= 0.5f;
                            num4 *= num5;
                            if (num4 > 0f)
                            {
                                num4 *= 2f;
                            }
                            projPosY *= 1f + num4;
                            projPosX *= 1f - num4;
                        }
                    }
                    rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
                    Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
                    Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num)), color2, rotation2, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                }
            }
            return false;
        }
    }
}