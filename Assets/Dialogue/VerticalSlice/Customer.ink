INCLUDE globalsmain.ink

{ foundCulprit: -> Confront2 | -> Intro }
=== Intro === 
Hej hej hej hej hej!!!!! # speaker: Customer
<PLAYERCHOICE>
    {seenBreath:
    * [Angående din andedräkt...] -> Confront2
  - else:
    * [Hur är läget?]
        E du min mamma
        ->END
    * [Jag måste gå nu.]
->END
}

=== Confront ===
<PLAYERCHOICE>
    * [Angående din andedräkt...]
        Awkward!!!
        ~ foundCulprit = true
        ->END
    * [Jag måste gå nu.] 
        -> END
-> DONE

=== Confront2 ===
Ok du fångade mig!!!!
-> END