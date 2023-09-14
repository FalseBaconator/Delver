using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Quests
    {
        Hud hud;

        public Quests(Hud hud)
        {
            this.hud = hud;
        }

        public enum QuestList
        {
            killEnemies,
            buyFromShop,
            pickUpItems,
            loseShield,
            killBoss
        }

        private QuestList currentQuest;

        public void GrantQuest(bool isBossFloor = false)
        {
            Globals.questCompleted = false;

            if (isBossFloor)
            {
                currentQuest = QuestList.killBoss;
                Globals.questString = Constants.killBossString;
                hud.SetMessage("Quest granted: " + Constants.killBossString + "!");
                return;
            }

            Random random = new Random();
            while (currentQuest != QuestList.killBoss)
            {
                Array values = Enum.GetValues(typeof(QuestList));
                QuestList randomQuest = (QuestList)values.GetValue(random.Next(values.Length));
                currentQuest = randomQuest;
                switch (currentQuest)
                {
                    case QuestList.killEnemies:
                        Globals.questString = Constants.killEnemiesString;
                        break;
                    case QuestList.buyFromShop:
                        Globals.questString = Constants.buyFromShopString;
                        break;
                    case QuestList.pickUpItems:
                        Globals.questString = Constants.pickUpItemsString;
                        break;
                    case QuestList.loseShield:
                        Globals.questString = Constants.loseShieldString;
                        break;
                }
                hud.SetMessage("Quest granted: " + Globals.questString + "!");
            }
        }

        //Event subscribers

        public void OnEnemyKilled(object source, KilledEnemyEventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.killEnemies) return;

            if (e.enemiesKilled >= Constants.enemiesToKill)
            {
                Globals.questCompleted = true;
                hud.SetMessage("Quest completed!");
            }
        }
        public void OnItemPickedUp(object source, ItemPickedUpEventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.pickUpItems) return;

            if (e.itemsPickedUp >= Constants.itemsToGet)
            {
                Globals.questCompleted = true;
                hud.SetMessage("Quest completed!");
            }
        }
        public void OnItemBought(object source, EventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.buyFromShop) return;

            Globals.questCompleted = true;
            hud.SetMessage("Quest completed!");
        }
        public void OnShieldLost(object source, EventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.loseShield) return;

            Globals.questCompleted = true;
            hud.SetMessage("Quest completed!");
        }
    }
}