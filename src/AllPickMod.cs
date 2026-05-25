using System.Collections.Generic;
using UnityEngine;

namespace AllPickMod
{
    [DefaultExecutionOrder(10000)]
    public class Main : SimpleModBehaviour
    {
        private const string ModVersion = "0.1.2";

        public override void OnModLoaded()
        {
            Log("V" + ModVersion + " 已加载：宝箱/铁匠/选卡全选、商店/奇遇/抽奖/斗蛐蛐免消耗、弩箭移除装填。");
        }

        public override void OnModUnloaded()
        {
            Log("已卸载。");
        }

        private void Update()
        {
            BattleObject bo = SingletonData<BattleObject>.Instance;
            if (bo == null || !bo.isInBattle)
            {
                return;
            }

            if (!bo.treasureAllGet)
            {
                bo.treasureAllGet = true;
            }

            if (bo.playPrice > 0)
            {
                bo.playPrice = 0;
            }

            if (!bo.moneyCanFu)
            {
                bo.moneyCanFu = true;
            }

            if (bo.reloadingNum > 0)
            {
                bo.reloadingNum = 0;
            }

            UnitObjectPlayer player = bo.playerObject;
            if (player != null && !player.hasDead)
            {
                if (player.reLoading)
                {
                    player.reLoading = false;
                }

                if (player.arrowNumCount <= 0)
                {
                    player.arrowNumCount = bo.arrowNum;
                }
            }

            ProcessUnitList(bo.chooseObjects);
            ProcessUnitList(bo.otherObjects);
        }

        private static void ProcessUnitList(List<UnitObject> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                UnitObject unit = units[i];
                if (unit == null || unit.hasDead)
                {
                    continue;
                }

                if (!unit.dontDestroyOther)
                {
                    unit.dontDestroyOther = true;
                }

                UnitObjectOther unitOther = unit as UnitObjectOther;
                if (unitOther != null)
                {
                    if (unitOther.price > 0)
                    {
                        unitOther.price = 0;
                    }

                    if (unitOther.hpPrice > 0)
                    {
                        unitOther.hpPrice = 0;
                    }
                }
            }
        }
    }
}
