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

// { foundCulprit: -> Confront2 | -> Intro }
-> Intro

=== Confront ===
#speaker: Customer
Oroa dig inte... Jag har inte gjort något fel... #anim: ThumbsUp
-> END

=== Intro === 
Tjena, slå dig ner! # speaker: Customer
    * {seenBreath} [Angående din andedräkt...] -> Confront
    * [Hur är läget?]
        Jodå, det funkar. Men bartendern har sneglat på mig ända sen jag kom in hit. #speaker: Customer
        Hur länge har du suttit här? #speaker: Player
        Sen igår kväll. Jag vill inte gå hem... Det som väntar på mig där hemma är hemskt. #speaker: Customer
        ** [Vad är det som väntar på dig hemma?]
            Jag vet inte. Så jag sitter kvar här så länge! #speaker: Customer
        ** [Jag måste gå nu.]
        
    * [Jag måste gå nu.]
    -->END
    } 
    
