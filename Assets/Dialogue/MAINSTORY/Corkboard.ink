INCLUDE globalsmainstory.INK
Your trusty corkboard. It's been with you for many years of service, helping you make sense of the clutter of clues and drawing conclusions in the case.
Skip tutorial?
    * [Yes]
        -> END
    * [No]
        -> Tutorial
= Tutorial
The top row will present you with your current set of suspects. It will update the more suspects you find.
{ LIST_COUNT(Suspects) > 1: It looks like you already found some suspects! You can try dragging a thread from their portrait now. | <> Right now, you don't have any suspects.} 
The bottom grid is for dragging in any clues you have in your inventory at the moment. 
Once you have dragged in at least one clue and have one suspect available, you can click and drag that suspect to form a connection to that clue.
Only one suspect per clue is permitted. This means that even if one clue seems to point in the direction of several suspects, it can only belong to one of them. 
It is your job through deduction to figure that out. A suspect can have many clues connected to them however.
You will not be notified of a wrong connection between a suspect and a clue, only when you've successfully made a connection.
glhf:)

-> END