INCLUDE globalsmainstory.INK
~ startMovementBlocking("Walking")
~ focusCamera("Cashier")
Hey, what are you doing?! #speaker: Store Clerk
Am I not supposed to go back here? #speaker: Player
Employees only! #speaker: Store Clerk
I really got to pee, c'mon! #speaker: Player
Toilets are out back! #speaker: Store Clerk
But what if I wanna piss in here? #speaker: Player
HELL NO! #speaker: Store Clerk
~ startMovement("Walking")
~ resetCamera()
<i>If I want to get in here, I'll have to find another way in.</i> #speaker:
~ canEnterStorage = true
-> END