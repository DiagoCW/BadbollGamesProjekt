INCLUDE globalsmain.ink

/*
{ foundCulprit: -> Confront2 | -> Intro }
=== Intro === 
Hej hej hej hej hej!!!!! # speaker: Customer
<PLAYERCHOICE> #speaker: Player
    {seenBreath and foundReceipt:
    * [Angående din andedräkt...] -> Confront2
  - else:
    * [Hur är läget?]
        E du min mamma ->END
    * [Jag måste gå nu.]<>->END
    } 
=== Confront2 ===
#speaker: Customer
Ok du fångade mig bra jobbat!!!! #anim: ThumbsUp
~ foundCulprit = true
-> END
*/

{ foundCulprit: -> Confront2 | -> Intro }
=== Intro === 
Hej hej hej hej hej!!!!! # speaker: Customer
<PLAYERCHOICE> #speaker: Player
    {seenBreath and foundReceipt} * [Angående din andedräkt...] -> Confront2
    * [Hur är läget?]
        E du min mamma ->END
    * [Jag måste gå nu.]<>->END
    } 
=== Confront2 ===
#speaker: Customer
Ok du fångade mig bra jobbat!!!! #anim: ThumbsUp
~ foundCulprit = true
-> END