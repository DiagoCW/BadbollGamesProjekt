INCLUDE globalsmainstory.INK

// Check if we've met him before
{ talkedToBartender: -> CanQuestion | -> Intro }

#speaker: Bartender
=== Intro ===
{ !What can I get you? | So? Are you gonna buy something or just stand there? } #anim: Talking
    * [A beer.]
        Coming right up. That'll be fifty spänn.
        ~ getclue(Clues.beer)
        ~ talkedToBartender = true
        -> IntroCont
    + [I need information.]
        Information ain't free. Buy a drink or get out.
        -> Intro
    + [I gots to go, baby.] 
        Yeah, don't let the door hit you.
        -> DONE

=== IntroCont ===
#speaker: Bartender
So, what is it you want to know?
-> StartQuestion("Shoot.")

=== CanQuestion ===
#speaker: Bartender
Need another drink? Or just more talk?
* [I've got some questions for you.]
    -> StartQuestion("Shoot.") 
* [I gots to go now, baby.]
    -> END


// --- MAIN HUB ---
=== StartQuestion(msg) ===
#speaker: Bartender
{msg}

* [Let's talk about Peter Grip.] -> KnowPeter
* { Suspects ? bossMan } [Do you know the falafel guy across the street?] -> BossMan
+ [Did you see anything last night?] -> LastNight
* [See you later.] -> END


// --- PETER SUB-HUB ---
=== PeterQuestions(msg) ===
#speaker: Bartender
{msg}

* { Clues ? Clues.beer } [I found this beer bottle on his body.] -> BeerMiscommunication
* [Why didn't you just kick him out?] -> KickOut
+ [Actually, I have other questions.] -> StartQuestion("What else do you need?")

=== KnowPeter ===
#speaker: Player
Do you know a guy named Peter Grip?

#speaker: Bartender
Peter? Yeah. Fuck that guy. Honestly, I hope he is dead.

#speaker: Player
He is. Found him dead just now.

#speaker: Bartender
Oh, wow. That's crazy. Anyway, what else do you want?

#speaker: Player
You don't seem very broken up about it.

#speaker: Bartender
He got way too drunk, and always took over the karaoke machine. Do you know what it does to a man's psyche to hear that idiot butcher ABBA's Dancing Queen four times a week?
-> PeterQuestions("Anything else about him?")


=== BeerMiscommunication ===
#speaker: Player
I found this beer bottle on his body. It's from your bar.

#speaker: Bartender
#anim: Talking
The bartender becomes completely pale in the face. He looks left and right, lowering his voice. 
Hey man, look, let's not jump to conclusions. You can't prove anything.

#speaker: Player
This town is way too small for a Systembolaget, which makes you the sole supplier. You were the last one to see him. Then he ends up dead in the dirt.

#speaker: Bartender
Listen, whatever happened out there is on him! I told him he couldn't take it outside!

#speaker: Player
So things escalated? You tried to stop him, and it got out of hand?

#speaker: Bartender
Escalated? No! I just looked the other way! I was so desperate to get him out of here that I let him take the bottle to go! I'm a fraud!

#speaker: Player
You're confessing? Just like that?

#speaker: Bartender
Yes! I broke serveringslagen! Please, I'm begging you, don't report me to kommunen! Do you know how hard it is to keep a serveringstillstånd in Staffanstorp?! They'll ruin me!

#speaker: Player
...I am investigating a brutal and bloody murder. I am not some kommuntant.

#speaker: Bartender
#anim: Idle
He blinks. The panic instantly leaves his body.
Wait, you don't care about the beer?

#speaker: Player
No.

#speaker: Bartender
Oh, sweet. Yeah, no, I didn't kill him. But seriously, keep the beer thing off the record, okay?
-> PeterQuestions("More questions about Peter?")


=== KickOut ===
#speaker: Player
If he was so annoying, why didn't you just kick him out?

#speaker: Bartender
He gestures broadly around the bar. There is one guy almost passed out in the armchair.
Look around you, detective.

#speaker: Player
There's almost nobody here.

#speaker: Bartender
Exactly. And this is what I call a 'busy night'. Business is terrible right now. I need every paying customer I can get.
-> PeterQuestions("Anything else?")


=== BossMan ===
#speaker: Player
Do you know the guy who runs the falafel stand across the street?

#speaker: Bartender
Boss Man? Yeah, he comes in for a beer sometimes. Usually just complains about not having enough vitlökssås.
-> StartQuestion("More questions?")

=== LastNight ===
#speaker: Player
Did you see anything last night?

#speaker: Bartender
As a matter of fact I did, they played 'family guy funny compilation hashtag funny hashtag familyguyfunnymoments' on TV.
-> StartQuestion("Anything else?")