public enum Phase
{
    Prepare,    // 准备阶段
    Judge,      // 判定阶段
    Get,        // 摸牌阶段
    Perform,    // 出牌阶段
    Discard,    // 弃牌阶段
    End         // 结束阶段
}

public enum TimerType
{
    PerformPhase,
    Discard,
    DiscardFromHand,
    UseCard,
    UseSha,
    UseWxkj,
    SelectHandCard,
    CallSkill,
    ZBSM,
    QlgPanel,
    RegionPanel,
    SSQY
}