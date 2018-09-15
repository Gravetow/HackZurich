using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class OpenBankingAPIUtils
{
    public static IEnumerable<Transaction> keepWithDate(Transaction[] txs, DateTime date)
    {
        var formattedDate = String.Format("{0:yyyy-MM-dd}", date.Date);
        return txs.Where(tx => tx.valuedate.Equals(formattedDate));
    }

    public static double spendingOnDate(Transaction[] txs, DateTime date)
    {
        return keepWithDate(txs, date).Select(tx => tx.amount).Sum();
    }
}
