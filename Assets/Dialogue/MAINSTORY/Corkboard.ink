INCLUDE globalsmainstory.INK
Your trusty corkboard. It's been with you for many years of service, helping you make sense of the clutter of clues and drawing conclusions in the case.
Skip tutorial?
    * [Yes]
        -> END
    * [No]
        -> Tutorial
= Tutorial
The top row will present you with your current set of suspects. It will update the more suspects you find.
{ LIST_COUNT(Suspects) > 1: It looks like you already found some suspects! <> | <> Right now, you don't have any suspects.} 
The bottom grid is for any clues you have in your inventory at the moment. Open your inventory and drag them onto the board.
Once you have dragged in at least one clue and have one suspect available, you can click and drag that suspect to form a connection to that clue. However, only one clue is not likely to lead to any meaningful conclusions.
Only one suspect per clue is permitted. This means that even if one clue seems to point in the direction of several suspects, it can only belong to one of them. 
It is your job through deduction to figure that out. A suspect can have many clues connected to them, and you can drag a thread from a suspect to upwards of 6 clues.
If you have made a wrongful connection, you will lose a point. Lose all your points and the investigation is finished, and the crime will <i>never</i> be solved, just another one for the books. Make the right connection, however...
Think over all clues and information you have before trying to make a final conclusion. Always assume that there are still clues left to find.
-> END