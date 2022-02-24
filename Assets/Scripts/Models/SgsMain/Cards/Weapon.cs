using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Model
{
    public class Weapon : Equipage
    {
        public override async Task AddEquipage(Player owner)
        {
            owner.AttackRange += range - 1;
            await base.AddEquipage(owner);
        }
        public override void RemoveEquipage()
        {
            Owner.AttackRange -= range - 1;
            base.RemoveEquipage();
        }
        protected int range;
    }

    public class QingLongYanYueDao : Weapon
    {
        public QingLongYanYueDao()
        {
            range = 3;
        }

        // public Sha sha { get; set; }

        public async Task Skill(Player dest)
        {
            TimerTask.Instance.Hint = "是否发动青龙偃月刀？";
            TimerTask.Instance.GivenDest = dest;
            TimerTask.Instance.GivenCard = new List<string> { "杀", "雷杀", "火杀" };
            bool result = await TimerTask.Instance.Run(Owner, TimerType.CallSkill);

            if (!result) return;

            SkillView();
            await TimerTask.Instance.Cards[0].UseCard(Owner, new List<Player> { dest });
        }
    }

    public class QiLinGong : Weapon
    {
        public QiLinGong()
        {
            range = 5;
        }

        public async Task Skill(Player dest)
        {
            if (dest.plusHorse is null && dest.subHorse is null) return;

            TimerTask.Instance.Hint = "是否发动麒麟弓？";
            TimerTask.Instance.GivenDest = dest;
            bool result = await TimerTask.Instance.Run(Owner, TimerType.CallSkill, 0);

            if (!result) return;

            SkillView();
            CardPanel.Instance.Title = "麒麟弓";
            result = await CardPanel.Instance.Run(Owner, dest, TimerType.QLG);

            Equipage horse;
            if (result) horse = (Equipage)CardPanel.Instance.Cards[0];
            else horse = dest.plusHorse is null ? dest.subHorse : dest.plusHorse;

            await new Discard(dest, null, new List<Equipage> { horse }).Execute();
        }
    }

    public class CiXiongShuangGuJian : Weapon
    {
        public CiXiongShuangGuJian()
        {
            range = 2;
        }
    }

    public class QingGangJian : Weapon
    {
        public QingGangJian()
        {
            range = 2;
        }

        public bool Skill(Sha sha)
        {
            SkillView();
            bool result = false;
            foreach (var dest in sha.Dests)
            {
                if (dest.armor != null)
                {
                    ((Armor)dest.armor).enable = false;
                    result = true;
                }
            }
            return result;
        }

        public void ResetArmor(Sha sha)
        {
            foreach (var dest in sha.Dests) if (dest.armor != null) ((Armor)dest.armor).enable = false;
        }
    }

    public class ZhangBaSheMao : Weapon
    {
        public ZhangBaSheMao()
        {
            range = 3;
        }
    }

    public class ZhuGeLianNu : Weapon
    {
        public ZhuGeLianNu()
        {
            range = 1;
        }
    }

    public class GuanShiFu : Weapon
    {
        public GuanShiFu()
        {
            range = 3;
        }

        public async Task<bool> Skill()
        {
            TimerTask.Instance.Hint = "是否发动贯石斧？";
            bool result = await TimerTask.Instance.Run(Owner, TimerType.SelectCard, 2);
            if (result)
            {
                await new Discard(Owner, TimerTask.Instance.Cards, TimerTask.Instance.Equipages).Execute();
                return true;
            }
            else return false;
        }
    }

    public class FangTianHuaJi : Weapon
    {
        public FangTianHuaJi()
        {
            range = 4;
        }
    }
}