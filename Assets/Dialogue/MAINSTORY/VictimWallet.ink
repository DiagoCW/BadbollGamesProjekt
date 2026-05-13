INCLUDE globalsmainstory.INK
{ Clues ? victimWallet: The victim's wallet. Boss Man's got some explaining to do... -> END }

<i>A stray wallet just laying there. You could take a peek inside...</i>
* [Do it!]
* [I shouldn't...]
    You do it anyway. <>
- Inside is a credit card belonging to... <i>Peter Grip?</i> The victim?
Why is this here?
{ knowledge ? receiptsBelongToVictim: The number on the card matches that on the receipts you found earlier, which was confirmed by the bartender. }
Boss Man's got some explaining to do...
~ getclue(victimWallet)
-> END
