using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CrimtaneHatchet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimtane Hatchet");
			Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 3");
		}
		public override void SetDefaults()
		{
			item.damage = 16;
			item.thrown = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.width = 36;
			item.height = 28;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = 1000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("CrimtaneHatchet");
			item.shootSpeed = 12f;
		item.maxStack = 3;
		}
		public override bool CanUseItem(Player player)
        {
         if ((player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("CrimtaneHatchet2")]) >= item.stack) 
			           {
                    return false;
                }
            else return true;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 2);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

