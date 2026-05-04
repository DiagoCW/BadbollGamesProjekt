INCLUDE globalsmainstory.INK
VAR confused = false
{ talkedToBossMan: -> CanQuestion|-> Intro }
#speaker: Boss Man
=== Intro ===
{ !Nice driving, asshole! | Like I said... Nice driving, asshole. } #anim: Talking
    { talkToPolice: -> CanQuestion.Question } 
    * [Thanks.]
        Yeah, real nice... Business is already bad as it is, a totaled car right behind the shop is going to do wonders for the customers.
        -> IntroCont
    * [That wasn't me, it was the other guy.]
        Oh, the other guy. Of course. The one standing here talking to me right now about the other guy. THAT guy. Him.
        ** [That's him.]
            I know, that's what I just said. You should fuck off to go find him and have him fix this mess.
            *** [Me, as in...]
                You, as in...
                **** [Him is me?]
                **** [Who is him?]
                **** [Jak się masz?]
                    ---- 
                    ~ confused = true
                    ~ talkedToBossMan = true
                    I don't, I... What is this? What's going on?
                    Just get lost. You're cramping my style.
                    And get rid of that fucking car.
                    -> DONE
        ** [Alright, I did it...]
            Really? I never would've guessed. I could've sworn you said it was the other guy! 
            -> IntroCont
    * [I gots to go, baby.]
        Go fucking where? Your car is impaled through that tree!
        -> DONE
    

= IntroCont
{!So, what are we gonna do about it? | You're sorry for what? }
    * [I said I was sorry.]
        You didn't, actually. 
        ** [Yeah.]
            -> Apologize
    * [{I'm sorry. | For crashing my car behind your shop epic style.}]
        That's all I wanted. I guess the car is kind of a selling point, now that I think about it. 
        People will be intrigued by the commotion, and flock towards my shop... Yeah...
        ~ talkedToBossMan = true
        -> DONE

= Apologize // loopar igenom tills spelaren ber om ursäkt
{ &You still haven't. | Still waiting. | What was that? }
* [Ok, I'm sorry.]
    -> IntroCont
+ [{&Cool. | Right on. | Yeah. | You know...}]
    -> Apologize

=== CanQuestion
{confused:
You're weird. You're making this weird. I don't really feel like talking to you. #anim: Shake
    { talkToPolice: -> Question | Forget about this bozo. I think I have a body to inspect somewhere. -> END }
- else:
How's it going?
    { talkToPolice: -> Question | Don't I have something better to do? Like, inspect a body? -> END } 
}

= Question
* [I've got some questions for you?]
    -> StartQuestion("What about?") 
* [I gots to go now, baby.]
    -> END

// This is the main hub for interrogating the suspect - More options
// will become available as you talk to people and find more clues
=== StartQuestion(msg) ===
~ talkedToBossMan = true
{msg}
* { Clues ? victimWallet } [Victim's wallet] -> Wallet
* { Suspects ? bossMan } [Did you make the call?] -> Call
+ [Did you know the victim?] -> Relation
* [See you later.] -> END

= Relation
Yeah, <>
-> StartQuestion("Anything else?")

= Wallet
-> END

= Call
-> END
