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
        SoundManager soundManager;

        public Quests(Hud hud, SoundManager soundManager)
        {
            this.hud = hud;
            this.soundManager = soundManager;
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

        public void GrantQuest()
        {
            Globals.questCompleted = false;

            if (Globals.currentFloor == Constants.BossFloor)
            {
                currentQuest = QuestList.killBoss;
                Globals.questString = Constants.killBossString;
                hud.SetMessage("Quest granted: " + Constants.killBossString + "!", true);
                return;
            }

            Random random = new Random();
            do
            {
                Array values = Enum.GetValues(typeof(QuestList));
                QuestList randomQuest = (QuestList)values.GetValue(random.Next(values.Length));
                currentQuest = randomQuest;
            }
            while (currentQuest == QuestList.killBoss);

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
            hud.SetMessage("Quest granted: " + Globals.questString + "!", true);
        }

        //Event subscribers

        public void OnEnemyKilled(object source, KilledEnemyEventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.killEnemies) return;

            if (e.enemiesKilled >= Constants.enemiesToKill)
            {
                Globals.questCompleted = true;
                soundManager.Play(SoundManager.Noise.quest);
                hud.SetMessage("Quest completed!", true);
            }
        }
        public void OnItemPickedUp(object source, ItemPickedUpEventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.pickUpItems) return;

            if (e.itemsPickedUp >= Constants.itemsToGet)
            {
                Globals.questCompleted = true;
                soundManager.Play(SoundManager.Noise.quest);
                hud.SetMessage("Quest completed!", true);
            }
        }
        public void OnItemBought(object source, EventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.buyFromShop) return;

            Globals.questCompleted = true;
            soundManager.Play(SoundManager.Noise.quest);
            hud.SetMessage("Quest completed!", true);
        }
        public void OnShieldLost(object source, EventArgs e)
        {
            if (Globals.questCompleted || currentQuest != QuestList.loseShield) return;

            Globals.questCompleted = true;
            soundManager.Play(SoundManager.Noise.quest);
            hud.SetMessage("Quest completed!", true);
        }
    }
}