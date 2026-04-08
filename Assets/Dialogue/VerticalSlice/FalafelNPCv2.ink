INCLUDE globalsmain.ink

{ startInvestigation: -> Receipt | -> Intro }

=== Intro ===
Intro
~ startInvestigation = true
~ dvisionTutorialTrigger = true
-> DONE

=== Receipt ===
{ foundReceipt: -> ContinueInvestigation | Receipt }
-> END

=== ContinueInvestigation ===
{ foundCulprit: -> FinishInvestigation | ContinueInvestigation }
-> END

=== FinishInvestigation ===
FinishInvestigation
->END