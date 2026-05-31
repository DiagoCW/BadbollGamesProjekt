INCLUDE globalsmainstory.INK
{ talkedToBartender: <>-> CanQuestion | -> Intro }
=== Intro() ===
{ What can I get you? | { ~Anything else? | What'll it be? } } #anim: Talking #speaker: Bartender
    * {debug} [LÅS UPP SOM SUSPECT]
        ~ addsuspect(bartender)
        ~ unlockSuspect(bartenderID)
        -> Intro
    * { items !? items.beerz } [A beer.]
        Coming right up. That'll be 80 spänn. #speaker: Bartender
        What a rip off... I can see why you're not getting any customers. #speaker: Player
        ~ getitem(items.beerz)
        -> Intro
    + { finishedCrimeScene } [Did you know Peter Grip?]
        { items !? items.beerz:
            The sooner you buy a beer, the sooner I'll tell ya. #speaker: Bartender
            -> Intro
        - else:
        -> StartQuestion("Did I. Save yourself the trouble of asking in the future by the way, everyone knew the guy.")
            }
    + [I gots to go, baby.]
        Don't let the door hit you. #speaker: Bartender
        -> DONE

=== CanQuestion ===
{ Suspects ? bartender and Suspects !? storeClerk: <i>The bartender has made a strong case for being the culprit. But I get the feeling I still have a few loose threads to look into...</i> -> END } #speaker:
Need another drink? I'd really prefer if you'd buy another beer. #speaker: Bartender
* { finishedCrimeScene } [I've got some questions for you.]
    -> StartQuestion("Alright. Shoot.") 
* [I gots to go now, baby.]
    -> END


// --- MAIN HUB ---
=== StartQuestion(msg) ===
~ talkedToBartender = true
{msg} #speaker: Bartender
-(opts)
* [About the victim...] -> KnowPeter
* { Suspects ? bossMan } [Ask about Boss Man] -> BossMan
* /*{ knowledge !? receiptsBelongToVictim}*/ [Last night?] -> LastNight
* { knowledge ? bartenderAlibi and Suspects !? bartender } [<color=\#FFFF00>About your alibi...</color>] -> Alibi
* [See you later.] -> END

= Alibi
Just to be clear, last night the victim and two other patreons were here until closing, correct? #speaker: Player
Uh... yeah. That's right. #speaker: Bartender
That's not what I heard. I have a credible source that you were alone with Peter up until you closed. #speaker: Player
Who told you that, him?! <i> He gestures towards the man still sleeping in the armchair.</i> He wasn't even awake during half the time! #speaker: Bartender
He's not the only one. These two patreons are confirmed to have left the bar a few hours before closing, leaving only you and the victim here. #speaker: Player
That's... alright, so what? Why does it matter? He was found dead way off from here, right? #speaker: Bartender
{ knowledge ? victimPoisoned:
-> AlibiContinued
- else:
<i>The victim is assumed to have died of 'natural' causes. There is nothing here to implicate the bartender.</i> #speaker:
I guess you're right. Sorry about that... #speaker: Player
-> StartQuestion("You better watch yourself, pysen...")
}

= AlibiContinued
He was poisoned. Not by alcohol, but something else entirely. It's possible that you poisoned him somehow during that time. #speaker: Player
Yeah? Got any proof of that? #speaker: Bartender
{ Clues ? ratPoison: 
-> AlibiEnd
- else:
<i>If the victim was poisoned, I should be able to find something around here that he could've used to do it, as well as figure out how he did it. But for the time being...</i> #speaker: Player
No, not really. I was winging it and hoped you would come clean...
-> StartQuestion("You've got some fucking nerve, kompis... <b>NO BEER FOR YOU!</b>")
-> END
}

= AlibiEnd
For starters, what's up with the excessive amount of rat poison all over the place? #speaker: Player
It's for rats. Obviously. #speaker: Bartender
And Peter, it would seem. When you came over to clean up his spot earlier, I managed to snag one of the cans. The beer smells really bad, even for that overpriced shitty beer you serve here. #speaker: Player
You lied about your alibi. You were alone with Peter for at least an hour, at which point you had ample opportunity to poison him. When he inevitably dropped dead, you had ample opportunity to loot his body.
I had no reason to kill him though, this is naught but the conjecture of a delusional detective! You have 0 proof! #speaker: Bartender
<i>This is it. I need one final push and I got him dead to rights. Here goes...</i> #speaker: 
{ knowledge ? knowAboutTrisslott: 
The trisslott. It all makes sense. #speaker: Player
You never mentioned that either, and that was to cover your tracks. But I know that the victim was flaunting it around last night, and you seemed <i>especially</i> interested in it.
With your business failing, and his tab racking up more and more debt... You saw an opportunity. You saw big money. 
You're... you're wrong. I didn't do it. I couldn't... #speaker: Bartender
~ addsuspect(bartender)
~ unlockSuspect(bartenderID)
<i>You have unlocked The Bartender as a suspect. You can speak with The Officer regarding this or consult your Field Manual for further information.</i>
-> END
- else:
I've got nothing. Sorry. Just keeping you on your toes! Got to go now baby. #speaker: Player
<i>Damn. You know you're on to something here, but there's one last crucial piece missing... but what?</i> #speaker:
<i>You should come back to this eventually. Right now you should keep investigating and ask around.</i>
-> END
}

= BossMan
~ temp m = "Under Construction. Här kan man få mer information efter att Boss Man lagts till som misstänkt."
-> StartQuestion(m)

= KnowPeter
I know that he died, yes. Awful way to go. #speaker: Bartender
What way would that be? #speaker: Player
Alcohol, as anyone else would tell you. I guess I feel somewhat responsible for it, being the proprietor and main supplier of his lifestyle... But he was a big boy, he could make his own decisions. #speaker: Bartender
Not a very empathetic guy, are you? #speaker: Player
I'm plenty empathic, kompis. All my friends tell me I'm an empath. #speaker: Bartender
Anyway, it was hard to appreciate the guy. 
He always got way too drunk and took over the karaoke machine. Do you know what it does to a man's psyche to hear that fool butcher ABBA's <i>Dancing Queen</i> four times a week?
-> StartQuestion("I'm sorry, but good riddance. What else can I tell you?")

= LastNight
He came here like any other night, business as usual. Well, not in the profitable sense, but something else. He never pays upfront, always tells me to "put it on his tab". #speaker: Bartender
I suppose there's nothing to be done about that now... At least he can't rack up anymore debt than he already had.
Come to think of it, he did pay last night. I had to do a double take when I heard him say "put it on my <b>card</b>" instead of "<b>tab</b>".
{ knowledge ? pocketsEmptied: 
    -> Wallet
- else:
    Not very frugal with his money, then. #speaker: Player
    <>-> LastNightCont
}

= Wallet
When the body was found, his pockets were emptied. He didn't have his wallet on him. #speaker: Player
    Then he lost it, simple as. He was prone to losing his wallet during his benders. 
    He would use it as an excuse to put off paying his tab, but he never had any money to begin with. #speaker: Bartender
* { Clues ? receipts and knowledge !? receiptsBelongToVictim} [<color=\#FFFF00><b>Show receipt from trash</b></color>] -> ShowReceipt
* { knowledge !? stoleWallet and Clues !? victimWallet } [Did he drop it here?]
    Nope, hasn't turned up here. He usually found it by himself somehow. Not that it matters. #speaker: Bartender
    <i>Nothing of note here. It's not hard to believe a drunk like him would lose his wallet constantly.</i> #speaker:
    <i>But he always got it back, one way or another. I need to find out more about this somehow.</i>
    -> LastNightCont
* { Clues ? victimWallet } [<color=\#FFFF00>Boss Man has his wallet.</color>]
    Huh, go figure. That's probably how he kept getting it back. He was as much of a regular there as he was here. #speaker: Bartender
    You don't think that Boss Man could have stolen it? #speaker: Player
    I don't think so. Unless you know something that I don't, of course. #speaker: Bartender
    -> LastNightCont
    
= LastNightCont
So, was Peter the only one here last night? #speaker: Player
No, no. Sleepyhead over there and loiter man was here all night. Then they all left shortly after another. #speaker: Bartender
It was another dull and uneventful night. Well, except for Peter's singing.
Anything else? Was he acting out of character, or did he tell you anything? #speaker: Player
No, not really. Business as usual. #speaker: Bartender
// * { knowledge ? bartenderAlibi } [<color=\#FFFF00>That ain't right...</color>] -> Alibi 
~ bartenderToldHisAlibi = true
Alright, that'll be all. #speaker: Player
-> StartQuestion("Anything else?")

= ShowReceipt
Wait, do you still have any of his receipts from last night? #speaker: Player
Sure, I think I have them here somewhere... #speaker: Bartender
Here you go. #anim: Talking
<i>You check the card code on the bottom of the receipt and compare it to the ones you found in the trash can earlier. <b>It's a match.</b></i> #speaker:
<i>All these receipts belong to the victim. This also confirms that he was at Boss Man last night and bought a falafelrulle.</i>
{ Clues ? victimWallet: 
    <i>...and Boss Man somehow has his wallet. I need to confront him about this.
- else:
    <i>...and then lost his wallet somehow. Could it have been stolen before his death? Or after his death...?</i>
    <i>I need to find that wallet.</i>
} 
~ gainknowledge(receiptsBelongToVictim)
Thank you. This has been a big help. #speaker: Player
* [Continue hearing the rest] -> LastNightCont
* [I have everything I need.]
    -> StartQuestion("Glad to be of service. Let me know if you need anything else.")
