VAR seenDialogue = false
{seenDialogue: -> Finish | -> GenericDialogue}
=== GenericDialogue ===
Have you seen anything unusual lately? # speaker: Player
Can’t say that I have, but I’ll keep my eyes peeled… Just in case. # speaker: Some Guy
Thanks, but it’s Time. # speaker: Player
What? # speaker: Some Guy
Justin Time. # speaker: Player
-> Finish

=== Finish ===
~seenDialogue = true 
... Yeah, as I said, I’ll let you know. # speaker: Some guy
->END