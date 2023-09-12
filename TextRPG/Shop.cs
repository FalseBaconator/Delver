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

        public Shop(InputManager inputManager) 
        {
            this.inputManager = inputManager;
        }

        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }

        public void StartShop()
        {
            Globals.shopping = true;
            hud.SetMessage("Welcome to my shop!");
            hud.OpenShopMenu();
        }

        public void Update()
        {
            if (!Globals.shopping) return;
            switch (inputManager.GetKey())
            {
                case ConsoleKey.E:
                    Globals.shopping = false;
                    break;
            }
        }
    }
}