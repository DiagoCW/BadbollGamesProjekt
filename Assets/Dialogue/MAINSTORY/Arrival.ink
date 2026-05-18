INCLUDE globalsmainstory.INK
EXTERNAL FadeOut()
VAR intro = true
<i>As you hit the long loansome road, perched back in your reclined leather seat, you feel at ease. You are en route to the scene of the crime.<i> #speaker:
<i>Your name is Justin Time. You're a real cool detective. You've been cracking heads and cases as long as there's been heads and cases to crack. Which is a very long time to be cracking heads and cases.</i>
<i>You've been called to a little known suburb outside of Malmö to investigate a murder that happened last night.</i>
<i>Or, you hope it's a murder. Because that's what you do, you solve murders.</i>
//<i>Your attention shifts to your stereo. It's been playing the same song on repeat, over and over again. You've lost count how many times now, maybe 6 or 7?</i>
//<i>It's not a particularly good song either, but it's all you've got to keep you company.</i>
<i>The calling officer didn't seem thrilled about having you come over to investigate. You have a penchant for drama, and you often try to uncover crimes where there are none.</i>
<i>He made it a point to mention that there's really nothing of interest here, but they need you to at least take a look at the scene and take some statements since they don't have any other available units.</i>
<i><b>Bullshit artist.</b> There's always a crime to be uncovered. Your instincts are screaming as much.</i>
<i>After driving for a while, you see the outskirts of the town you're headed towards. It's dark now, and it's started raining.</i>
<i>...a *lot*, actually. You suddenly start hydroplaning. This is <b>really bad.<b> You start swerving uncontrollably.</i>
    * [<b>Step on the gas</b>]
        <i>You inexplicably start flooring it.</i>
        <i>It felt like the right thing to do in the heat of the moment, but there's no going back now. You've comitted to this.</i>
        ~ steppedOnGas = true
    * [<b>Resign to your fate</b>]
        <i>You close your eyes and let go of the wheel. You won't open them again until you either see God or have reached your destination.</i> 
        ~ resignedToFate = true
    * [<b>Start panicking</b>]
        <i>You scream like a little kid and start thrashing around, tugging at your seatbelt and kicking everything within reach. The swerving gets worse.</i>
        ~ panicked = true
- <i>As the car draws closer and closer to the town, time slows down exponentially. Until it doesn't. You reach an abrupt stop, and time resumes again.</i>
~ FadeOut()
<i>You've hit a tree in the middle of a park. Your Honda Civic '89 has been absolutely demolished. You exit the driver's seat, miracolously without a single scratch. It all happened so fast, you think.</i>
<i>When in reality, you never went above 20km/h, and this whole situation was easily avoidable. The damage to the car is not proportional to the impact, since it was kind of a piece of shit to begin with.</i>
<i>Your car doesn't care though, as it's still absolutely fucked. You're not going anywhere anytime soon. Not that you need to either, you're right where you belong.</i>
<i>You seem to have crashed in the middle of the park. You see the officer from across your shoulder, he's rushed towards you after he heard the crash. Best not keep him waiting any longer.</i>
<i>You also hear someone shouting by the street behind you somewhere...</i>
-> END
