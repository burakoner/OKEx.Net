using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotBillTypeConverter : BaseConverter<OkexSpotBillType>
    {
        public SpotBillTypeConverter() : this(true) { }
        public SpotBillTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotBillType, string>> Mapping => new List<KeyValuePair<OkexSpotBillType, string>>
        {
            new KeyValuePair<OkexSpotBillType, string>(OkexSpotBillType.Transfer, "transfer"),
            new KeyValuePair<OkexSpotBillType, string>(OkexSpotBillType.Trade, "trade"),
            new KeyValuePair<OkexSpotBillType, string>(OkexSpotBillType.Rebate, "rebate"),
            new KeyValuePair<OkexSpotBillType, string>(OkexSpotBillType.Fee, "fee"),
            /*
            new KeyValuePair<OkexBillType, string>(OkexBillType.Deposit, "1"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.Withdraw, "2"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.Buy, "7"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.Sell, "8"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.BeginnersTask, "9"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.InviteFriendsToCompleteBeginnersTask, "10"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.DeductionOfTaskReward, "11"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.InvitationBonus, "12"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.CanceledWithdrawal, "13"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.DeductedForEvents, "14"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.ReceivedFromEvents, "15"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromFutures, "18"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToFutures, "19"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransactionFeeRebate, "22"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.ReceiveRedPacket, "23"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.SendRedPacket, "24"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.C2CBuy, "25"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.C2CSell, "26"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.Deduct, "27"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.Convert, "28"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToAssetsAccount, "29"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromAssetsAccount, "30"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToC2CAccount, "31"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromC2CAccount, "32"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToMarginAccount, "33"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromMarginAccount, "34"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.Borrow, "35"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.Repay, "36"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.MarketMakerBonus, "38"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.MarketMakerRebate, "39"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.FeeSettledWithLP, "41"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.PurchaseLoyaltyPoints, "42"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferLoyaltyPoints, "43"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.MMProgramBonus, "44"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.MMProgramRebate, "45"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromSpotAccount, "46"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToSpotAccount, "47"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToETT, "48"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromETT, "49"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.DeductedForMining, "50"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.GainFromMining, "51"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.ExtraYield, "52"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.IncentiveBonusDistribution, "53"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromOKPiggyBank, "55"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToOKPiggyBank, "56"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferFromSwapAccount, "57"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.TransferToSwapAccount, "58"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.RepayBonus, "59"),
            new KeyValuePair<OkexBillType, string>(OkexBillType.MarginFeeSettledWithLP, "60"),
            */
        };
    }
}
