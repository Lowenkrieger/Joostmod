using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace JoostMod.NPCs
{
    public class SandGolem : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Golem");
            Main.npcFrameCount[npc.type] = 14;
        }
        public override void SetDefaults()
        {
            npc.width = 52;
            npc.height = 104;
            npc.damage = 60;
            npc.defense = 32;
            npc.lifeMax = 2000;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.knockBackResist = 0.05f;
            npc.aiStyle = -1;
            banner = npc.type;
            bannerItem = mod.ItemType("DesertGolemBanner");
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SandBlock, 100 + Main.rand.Next(151));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DesertCore"));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SandGolem1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SandGolem2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SandGolem2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SandGolem3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SandGolem3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SandGolem4"), 1f);
            }
            if (npc.ai[3] <= 0)
            {
                npc.ai[0]++;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.playerInTown && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.spawnTileY < Main.rockLayer && spawnInfo.player.ZoneDesert && Main.hardMode ? 0.02f : 0f;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (npc.ai[0] > 700)
            {
                npc.ai[0] = 0;
            }
            if (npc.ai[1] > 700)
            {
                npc.ai[1] = 0;
                npc.direction *= -1;
                npc.velocity.X += npc.direction * 0.09f;
            }
            if (Collision.CanHitLine(npc.Center, 1, 1, P.position, P.width, P.height) || (npc.ai[0] > 0 && npc.ai[3] <= 0))
            {
                npc.ai[0]++;
            }

            if (npc.ai[0] <= 0)
            {
                npc.ai[1]++;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                if (npc.velocity.Y == 0 && npc.velocity.X == 0 && npc.ai[1] <= 550)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        npc.direction *= -1;
                    }
                    else
                    {
                        npc.velocity.Y = -6;
                    }
                }
                if (npc.velocity.X * npc.direction < 1.3f)
                {
                    npc.velocity.X += npc.direction * 0.09f;
                }
                if (npc.velocity.X > 1.3f && npc.velocity.Y == 0)
                {
                    npc.velocity.X = 1.3f;
                }
                if (npc.velocity.X < -1.3f && npc.velocity.Y == 0)
                {
                    npc.velocity.X = -1.3f;
                }
                if (npc.ai[1] > 550)
                {
                    npc.velocity.X = 0;
                }
            }
            else
            {
                if (npc.velocity.Y == 0 && npc.velocity.X == 0 && npc.ai[3] <= 0)
                {
                    npc.velocity.Y = -6;
                }
                if (npc.ai[3] < 2)
                {
                    npc.direction = (P.Center.X < npc.Center.X ? -1 : 1);
                }
                npc.ai[1]++;
                if (npc.life < npc.lifeMax / 2)
                {
                    npc.ai[1]++;
                    if (npc.velocity.X * npc.direction < 3f)
                    {
                        npc.velocity.X += npc.direction * 0.09f;
                    }
                    if (npc.velocity.X > 3f && npc.velocity.Y == 0)
                    {
                        npc.velocity.X = 3f;
                    }
                    if (npc.velocity.X < -3f && npc.velocity.Y == 0)
                    {
                        npc.velocity.X = -3f;
                    }
                }
                else
                {
                    if (npc.velocity.X * npc.direction < 1.6f)
                    {
                        npc.velocity.X += npc.direction * 0.09f;
                    }
                    if (npc.velocity.X > 1.6f && npc.velocity.Y == 0)
                    {
                        npc.velocity.X = 1.6f;
                    }
                    if (npc.velocity.X < -1.6f && npc.velocity.Y == 0)
                    {
                        npc.velocity.X = -1.6f;
                    }
                }
                if (npc.ai[1] >= 300)
                {
                    npc.ai[3] = Main.rand.Next(2) + 1;
                    npc.ai[1] = 0;
                }
                if (npc.ai[3] == 1)
                {
                    npc.ai[2]++;
                    npc.velocity.X = 0;
                }
            }

            if (npc.ai[3] == 1)
            {
                if (npc.ai[1] == 14)
                {
                    float Speed = 9f;
                    Vector2 vector8 = new Vector2(npc.Center.X, npc.position.Y + 26);
                    int damage = 30;
                    int type = mod.ProjectileType("SandBall");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
                }
                if (npc.ai[1] >= 20)
                {
                    npc.ai[1] = 0;
                }
            }

            if (npc.ai[3] == 2)
            {
                if (npc.ai[2] < 20 && npc.velocity.Y == 0)
                {
                    npc.direction = (P.Center.X < npc.Center.X ? -1 : 1);
                    npc.velocity.X = npc.direction * 6;
                    npc.velocity.Y = -6;
                    npc.ai[2] = 20;
                }
                if (npc.ai[2] == 20 && npc.velocity.Y == 0)
                {
                    npc.velocity.X = 0;
                    Vector2 pos = new Vector2(npc.Center.X, npc.position.Y + 26);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(pos.X, pos.Y, 4f * npc.direction, 0, mod.ProjectileType("ShockWave"), 75, 0f, Main.myPlayer);
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        int dustType = 32;
                        int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.velocity.X = dust.velocity.X + Main.rand.Next(-20, 20);
                        dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-20, -5);
                    }
                    npc.ai[2] += 3;
                }
                if (npc.ai[2] > 20)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;
                    npc.ai[2] += 3;
                }
            }
            if (npc.ai[2] >= 120)
            {
                npc.ai[3] = 0;
                npc.ai[2] = 0;
                npc.ai[1] = 0;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter += Math.Abs(npc.velocity.X);
            if (npc.ai[3] <= 0)
            {
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 104);
                }
                if (npc.frame.Y >= (104 * 7))
                {
                    npc.frame.Y = 104;
                }
                if (npc.velocity.X == 0)
                {
                    npc.frame.Y = 0;
                }
            }
            if (npc.ai[3] == 1)
            {
                if (npc.ai[1] < 7)
                {
                    npc.frame.Y = 104 * 7;
                }
                else if (npc.ai[1] < 14)
                {
                    npc.frame.Y = 104 * 8;
                }
                else if (npc.ai[1] < 20)
                {
                    npc.frame.Y = 104 * 9;
                }
            }
            if (npc.ai[3] == 2)
            {
                if (npc.ai[2] < 20)
                {
                    npc.frame.Y = 104 * 10;
                }
                if (npc.ai[2] == 20)
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 104 * 11;
                    }
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 104 * 12;
                    }
                }
                if (npc.ai[2] >= 20 && npc.velocity.Y == 0)
                {
                    npc.frame.Y = 104 * 13;
                }
            }
        }
    }
}

