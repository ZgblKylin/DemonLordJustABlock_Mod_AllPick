using System.Collections.Generic;
using UnityEngine;

namespace AllPickMod
{
    [DefaultExecutionOrder(10000)]
    public class Main : SimpleModBehaviour
    {
        private const string ModVersion = "0.1.0";

        public override void OnModLoaded()
        {
            Log("V" + ModVersion + " 已加载：宝箱/铁匠/选卡全选、商店/奇遇免消耗。");
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
