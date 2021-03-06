using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace JoostMod.Tiles
{
	public class EvilStone : ModTile
	{
		public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.AnchorTop = AnchorData.Empty;
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Stone of Death");
			AddMapEntry(new Color(127, 0, 255), name);
			dustType = 14;
            disableSmartCursor = true;
        }

        public override void RightClick(int i, int j)
        {
            WorldGen.KillTile(i, j, false, false, false);
            if (Main.netMode == 1 && !Main.tile[i, j].active())
            {
                NetMessage.SendData(17, -1, -1, null, 4, (float)i, (float)j, 0f, 0, 0, 0);
            }
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = mod.ItemType("EvilStone");
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            drawColor = new Color(127, 0, Main.DiscoG);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.25f;
            g = 0;
            b = (255 - Main.DiscoG) / 510f;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 vector = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                vector = Vector2.Zero;
            }
            Color color = new Color(127, 0, (255 - Main.DiscoG));
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/EvilStonePupil"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + vector, new Rectangle(tile.frameX, tile.frameY, 16, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("EvilStone"));
		}
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Player player = Main.LocalPlayer;
                if (WorldGen.crimson)
                {
                    player.AddBuff(BuffID.Wrath, 30);
                }
                else
                {
                    player.AddBuff(BuffID.Rage, 30);
                }
            }
        }
    }
}