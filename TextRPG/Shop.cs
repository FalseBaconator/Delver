using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shop
    {
        private Hud hud;
        InputManager inputManager;
        private Player player;
        public EventHandler ItemBought;

        public Shop(InputManager inputManager, Player player) 
        {
            this.inputManager = inputManager;
            this.player = player;
            player.SetShop(this);
        }

        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }

        public void StartShop()
        {
            Globals.shopping = true;
            hud.SetMessage("Welcome to my shop!");
        }

        public void Update()
        {
            if (!Globals.shopping) return;
            switch (inputManager.GetKey())
            {
                case ConsoleKey.E:
                    Globals.shopping = false;
                    break;
                case ConsoleKey.D1:
                    BuyHPPotion();
                    break;
                case ConsoleKey.D2:
                    BuySHLDRepair();
                    break;
                case ConsoleKey.D3:
                    BuyATKBuff();
                    break;
            }
        }

        private void BuyHPPotion()
        {
            if (player.GetGold() < Constants.healthPotionCost) return;
            player.giveGold(-Constants.healthPotionCost);
            player.Heal(Constants.healAmount);
        }
        private void BuySHLDRepair()
        {
            if (player.GetGold() < Constants.shieldRepairCost) return;
            player.giveGold(-Constants.shieldRepairCost);
            player.RestoreShield(Constants.shieldRepairAmount);
        }
        private void BuyATKBuff()
        {
            if (player.GetGold() < Constants.ATKBuffCost) return;
            player.giveGold(-Constants.ATKBuffCost);
            player.RaiseATK(Constants.ATKBuffAmount);
        }

        protected virtual void OnItemBought()
        {
            if (ItemBought != null)
                ItemBought(this, EventArgs.Empty);
        }
    }
}