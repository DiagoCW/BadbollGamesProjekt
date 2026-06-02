INCLUDE globalsmainstory.INK
EXTERNAL FadeOut()
VAR intro = true
~ playAudio("cardoor-open-close")
<i>You slam the door shut, and off you go. You are en route to the scene of the crime.<i> #speaker:
~ playAmbience("highway-travel-interior")
~ playAmbience("PREMONITION-IN-THE-PARK")
<i>Your name is Justin Time. You're an old school detective. You've been cracking heads and cases as long as there's been heads and cases to crack. Which is a very long time to be cracking heads and cases. Except...</i>
This sucks. I haven't had a proper case to solve in, what... 20 years now? And they call me in to take a look at some low-life drunk they found dead in an alley? #speaker: Player
I used to solve the biggest cases. Everywhere I rolled up I would say my iconic catchphrase, <i>"Looks like I'm... <b>Justin Time</b>."</i>, and then I solved the case. And everyone loved me for it.
The officer that called me in didn't even consider the prospect that we could have a murder on our hands.
Are law enforcement so quick to rule out cold-blooded murder these days? As if there's not a crime to be solved?
<b>Bullshit artist.</b> There's always a crime to be solved. My instincts are screaming as much.
This will be my epic comeback. I'll prove it was a murder. They'll see.
<i>After driving for a while, you see the outskirts of the town you're headed towards. It's dark now, and it's started raining.</i> #speaker: 
<i>...a *lot*, actually. You suddenly start hydroplaning. This is <b>really bad.<b> You swerve uncontrollably.</i>
That's not good. But what if I... #speaker: Player
    * [<b>Step on the gas</b>]
        <i>You inexplicably start flooring it. The car speeds up.</i> #speaker: 
        Uh-oh. #speaker: Player
        <i>It felt like the right thing to do in the heat of the moment, but there's no going back now.</i> #speaker: 
        ~ steppedOnGas = true
    * [<b>Resign to my fate</b>]
        <i>You close your eyes and let go of the wheel. It's all in God's hands now.</i> #speaker: 
        This is fine. Soon it will all be over... #speaker: Player
        ~ resignedToFate = true
    * [<b>Start panicking</b>]
        <i>You scream like a little kid and start thrashing around, tugging at your seatbelt and kicking everything within reach.</i> #speaker: 
        AIIEHH!! I DON'T WANT TO DIE! SOMEBODY FUCKING SAVE ME!!!!!!!!! #speaker: Player
        <i>The swerving gets worse.</i> #speaker: 
        ~ panicked = true
- ~ stopAmbience("highway-travel-interior")
- ~ lowerPitch("PREMONITION-IN-THE-PARK")
- <i>As the car draws closer and closer to the town, time slows down exponentially until it stands completely still.</i>
~ playAudio("car-crash")
~ stopAmbience("PREMONITION-IN-THE-PARK")
</i>And then it doesn't. You reach an abrupt stop, and time resumes again.</i>
~ FadeOut()
~ playAmbience("ambiens-1")
<i>You've hit a tree in the middle of a park. Your Honda Civic '89 has been absolutely demolished. You exit the driver's seat, miracolously without a single scratch.</i>
My car... My lovely little car... A shame that there was no way to avoid this situation. #speaker: Player
~ startMovementBlocking("Walking")
~ focusCamera("Police Officer")
<i>You see the officer from across your shoulder, rushing towards you after he heard the crash.</i>
-> Intro

=== Intro ===
Jesus, are you alright?! What happened? #speaker: Police #anim: Talking
Looks like I'm... <i>Jus imhime...</i> time to... Justin... #speaker: Player
What? What are you talking about, did you have a concussion or something? #speaker: Police
{ panicked: -> Panicked }
{ resignedToFate: -> Fate }
{ steppedOnGas: -> Gas }
- (Panicked)
The car started swerving, so I panicked. So what? Big deal, happens to the best of us. I didn't cry like a baby or nothing. #speaker: Player
But you were barely going 20km an hour... #speaker: Police
<> -> introcont
- (Fate)
I left my fate in the hands of God, and he led me right where I needed to be. Praise his name. #speaker: Player
Yes, praise his name indeed... #speaker: Police
<> -> introcont 
- (Gas)
I started flooring it. Sadly, my foot got stuck on the pedal. Not much to be done about that. #speaker: Player
You shouldn't have floored it to begin with, but I guess you're right. #speaker: Police
<> -> introcont
- (introcont) Anyway, what took you so long? I've been waiting for quite a while. #speaker: Police
My car broke down on my way here, but it's alright. I fixed it using my <color=\#FFFF00>Detective Vision.</color> #speaker: Player
Uh... Ok. I don't know what that means. It sounds cool though, I'm happy for you. #speaker: Police #anim: Shake
Stop wasting my time, officer. Let's get on with the briefing. #speaker: Player 
~ startMovement("Walking")
Alright... Follow me, it's just over here. #speaker: Police
~ resetCamera()
~ talkToPolice = true
-> END
