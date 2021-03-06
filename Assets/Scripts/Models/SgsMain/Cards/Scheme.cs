using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// 无懈可击
    /// </summary>
    public class 无懈可击 : Card
    {
        public 无懈可击()
        {
            Type = "锦囊牌";
            Name = "无懈可击";
        }

        public static async Task<bool> Call(Card card, Player dest)
        {
            string hint = dest != null ? "对" + (dest.Position + 1).ToString() + "号位" : "";
            TimerTask.Instance.Hint = card.Name + "即将" + hint + "生效，是否使用无懈可击？";

            bool result = await TimerTask.Instance.RunWxkj();
            if (result)
            {
                Debug.Log(TimerTask.Instance.Cards[0].Name);
                var wxkj = (无懈可击)TimerTask.Instance.Cards[0];
                await wxkj.UseCard(TimerTask.Instance.player);
                if (!wxkj.isCountered) return true;
            }

            return false;

        }

        public override async Task UseCard(Player src, List<Player> dests = null)
        {
            await base.UseCard(src, dests);

            isCountered = await Call(this, null);
        }

        private bool isCountered;
    }

    /// <summary>
    /// 过河拆桥
    /// </summary>
    public class 过河拆桥 : Card
    {
        public 过河拆桥()
        {
            Type = "锦囊牌";
            Name = "过河拆桥";
        }

        public override async Task UseCard(Player src, List<Player> dests)
        {
            await base.UseCard(src, dests);
            foreach (var dest in Dests)
            {
                if (await 无懈可击.Call(this, dest)) continue;

                CardPanel.Instance.Title = "过河拆桥";
                bool result = await CardPanel.Instance.Run(Src, dest, TimerType.RegionPanel);

                Card card;
                if (!result)
                {
                    if (dest.armor != null) card = dest.armor;
                    else if (dest.plusHorse != null) card = dest.plusHorse;
                    else if (dest.weapon != null) card = dest.weapon;
                    else if (dest.subHorse != null) card = dest.subHorse;
                    else if (dest.HandCardCount != 0) card = dest.HandCards[0];
                    else card = dest.JudgeArea[0];
                }
                else card = CardPanel.Instance.Cards[0];

                if (card is DelayScheme && dest.JudgeArea.Contains((DelayScheme)card))
                {
                    ((DelayScheme)card).RemoveToJudgeArea();
                    CardPile.Instance.AddToDiscard(card);
                }
                else await new Discard(dest, new List<Card> { card }).Execute();
            }
        }
    }

    /// <summary>
    /// 顺手牵羊
    /// </summary>
    public class 顺手牵羊 : Card
    {
        public 顺手牵羊()
        {
            Type = "锦囊牌";
            Name = "顺手牵羊";
        }

        public override async Task UseCard(Player src, List<Player> dests)
        {
            await base.UseCard(src, dests);
            foreach (var dest in Dests)
            {
                if (await 无懈可击.Call(this, dest)) continue;

                CardPanel.Instance.Title = "顺手牵羊";
                bool result = await CardPanel.Instance.Run(Src, dest, TimerType.RegionPanel);

                Card card;
                if (!result)
                {
                    if (dest.armor != null) card = dest.armor;
                    else if (dest.plusHorse != null) card = dest.plusHorse;
                    else if (dest.weapon != null) card = dest.weapon;
                    else if (dest.subHorse != null) card = dest.subHorse;
                    else if (dest.HandCardCount != 0) card = dest.HandCards[0];
                    else card = dest.JudgeArea[0];
                }
                else card = CardPanel.Instance.Cards[0];

                if (card is DelayScheme && dest.JudgeArea.Contains((DelayScheme)card))
                {
                    ((DelayScheme)card).RemoveToJudgeArea();
                    await new GetCard(src, new List<Card> { card }).Execute();
                }
                else await new GetCardFromElse(src, dest, new List<Card> { card }).Execute();
            }
        }
    }

    /// <summary>
    /// 决斗
    /// </summary>
    public class 决斗 : Card
    {
        public 决斗()
        {
            Type = "锦囊牌";
            Name = "决斗";
        }

        public override async Task UseCard(Player src, List<Player> dests)
        {
            await base.UseCard(src, dests);

            foreach (var dest in Dests)
            {
                if (await 无懈可击.Call(this, dest)) continue;

                var player = dest;
                bool done = false;
                while (!done)
                {
                    done = !await 杀.Call(player);
                    if (done) await new Damaged(player, 1, player == dest ? src : dest, this).Execute();
                    else player = player == dest ? src : dest;
                }
            }
        }
    }

    /// <summary>
    /// 南蛮入侵
    /// </summary>
    public class 南蛮入侵 : Card
    {
        public 南蛮入侵()
        {
            Type = "锦囊牌";
            Name = "南蛮入侵";
        }

        public override async Task UseCard(Player src, List<Player> dests = null)
        {
            dests = new List<Player>();
            foreach (var i in SgsMain.Instance.AlivePlayers) dests.Add(i);
            dests.Remove(src);

            await base.UseCard(src, dests);

            foreach (var dest in Dests)
            {
                if (await 无懈可击.Call(this, dest)) continue;

                if (!await 杀.Call(dest)) await new Damaged(dest, 1, src, this).Execute();
            }
        }
    }

    /// <summary>
    /// 万箭齐发
    /// </summary>
    public class 万箭齐发 : Card
    {
        public 万箭齐发()
        {
            Type = "锦囊牌";
            Name = "万箭齐发";
        }

        public override async Task UseCard(Player src, List<Player> dests = null)
        {
            dests = new List<Player>();
            foreach (var i in SgsMain.Instance.AlivePlayers) dests.Add(i);
            dests.Remove(src);

            await base.UseCard(src, dests);

            foreach (var dest in Dests)
            {
                if (await 无懈可击.Call(this, dest)) continue;

                if (!await 闪.Call(dest)) await new Damaged(dest, 1, src, this).Execute();
            }
        }
    }

    /// <summary>
    /// 桃园结义
    /// </summary>
    public class 桃园结义 : Card
    {
        public 桃园结义()
        {
            Type = "锦囊牌";
            Name = "桃园结义";
        }

        public override async Task UseCard(Player src, List<Player> dests = null)
        {
            dests = new List<Player>();
            foreach (var i in SgsMain.Instance.AlivePlayers) dests.Add(i);

            await base.UseCard(src, dests);

            foreach (var dest in Dests)
            {
                if (dest.Hp >= dest.HpLimit) continue;
                if (await 无懈可击.Call(this, dest)) continue;

                await new Recover(dest).Execute();
            }
        }
    }

    /// <summary>
    /// 无中生有
    /// </summary>
    public class 无中生有 : Card
    {
        public 无中生有()
        {
            Type = "锦囊牌";
            Name = "无中生有";
        }

        public override async Task UseCard(Player src, List<Player> dests = null)
        {
            // 默认将目标设为使用者
            if (dests is null || dests.Count == 0) dests = new List<Player> { src };

            await base.UseCard(src, dests);

            foreach (var dest in Dests)
            {
                if (await 无懈可击.Call(this, dest)) continue;

                await new GetCardFromPile(dest, 2).Execute();
            }
        }
    }
}
