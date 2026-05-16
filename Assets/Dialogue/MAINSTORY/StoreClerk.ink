INCLUDE globalsmainstory.INK
VAR askedQuestion = false
#speaker: Store Clerk #anim: Talking
{ askedQuestion: Welcome back! Hope you didn't have any more questions, because I won't answer them. | Welcome, welcome! What can I do for you today? }
-> Intro
=== Intro ===
* {finishedCrimeScene and not askedQuestion } [Did you know the victim?] -> Victim
* {finishedCrimeScene and not askedQuestion } [About last night...] -> LastNight
* {AltercationVictim} [What happened last night?] -> LastNightForReal
* [I gots to go, baby.] -> END

= Victim
I heard from some customers that they found someone, <i>dead</i>. Scary times we're living in. #anim: Shake
You don't know who it was, then? #speaker: Player
Sir, I'm going on the 18th hour of my 30 hour shift. I don't have time to worry about stuff like that. Wait until after my shift if you want to ask me about anything non-work related. #speaker: Store Clerk
-> Intro
= LastNight
{ Victim: As I said, sir, } I don't have time to answer any questions you might have for me. I'm on the clock here, I got work to do. #speaker: Store Clerk
C'mon. One question, please? #speaker: Player
Alright, <i>one question.</i> #speaker: Store Clerk
    * { knowledge ? receiptsBelongToVictim } [Did Peter Grip come here last night?] -> LastNightVictim
    * [Where were you last night?]
        Here, as usual. I started my shift 18 hours ago, and still have some hours to go. #speaker: Store Clerk
        ~ cashierToldHisAlibi = true
        And you've never left even once? #speaker: Player
        -> LastNightEnd
    * [How's it going?]
        Fine, thank you. #speaker: Store Clerk
        Busy day? #speaker: Player
        -> LastNightEnd
    * [-Come back later]
        Could I come back and ask about this later? #speaker: Player
        Absolutely, sir! That counts as a question though, so actually no. Thank you! 
        ~ askedQuestion = true
        -> END

= LastNightVictim
Ah, Peter... I believe so. #speaker: Store Clerk
You don't sound too thrilled to hear his name. Did something happen? #speaker: Store Clerk
-> LastNightEnd

= LastNightEnd
That's your second question, I'm afraid. As I said, I'm happy to answer any questions you might have once I get off my shift. #speaker: Store Clerk
{ LastNightVictim and (Suspects ? bossMan or talkedToSuspiciousGuy): 
    * [-Keep pressing him] -> AltercationVictim
- else:
    ~ askedQuestion = true
    <i>Damn, I didn't think he meant literally one question... Should I have asked something else?</i>
    -> END
}

= AltercationVictim
No, you will riddle me this. I have some people claiming that you got into some altercation with the victim last night, and I would like to know what that was. #speaker: Player
That's... it was nothing, he just flew off into some drunken stupor like he always did. I had to rough him up a bit, you know? #speaker: Store Clerk #Shake
<i>Did?</i> I never told you who the victim was, and you implied that you didn't know who it was, either. But you speak about him in the past tense. #speaker: Player
Oh... oh. #speaker: Store Clerk
Oh is right, buddy. So you knew who the victim was, and there are independent witnesses that claim something happened between you two yesterday. #speaker: Player
Alright, I'll tell you... 
-> Intro

= LastNightForReal

-> END