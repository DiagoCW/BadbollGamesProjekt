INCLUDE globalsmainstory.INK
VAR intro = false
-> Intro
=== Intro ===
{ intro: -> Question }
Nice driving, asshole!
    * [Thanks.]
        Yeah, real nice... Business is already bad as it is, a totaled car right behind the shop is going to do wonders for the customers.
        -> IntroCont
    * [That wasn't me, it was the other guy.]
        Oh, the other guy. Of course. The one standing here talking to me right now about the other guy. THAT guy. Him.
        ** [That's him.]
            I know, that's what I just said. You should fuck off to go find him and have him fix this mess.
            *** [Me, as in... Him?]
                You, as in.. Erm... He...
                **** [Him is me?]
                **** [Who is him?]
                **** [Me momo hihi :)]
                    ----I don't, I... What is this? What's going on?
                    Just get lost. You're cramping my style.
                    And get rid of that fucking car.
                    ~ intro = true
                -> END
        ** [Alright, I did it...]
            Really? I never would've guessed. I could've sworn it was that other guy! 
            -> IntroCont
-> END

= IntroCont
~ intro = true
{&So, what are we gonna do about it? | You're sorry for what? }
    //* [Let me get back to you on that later. (LEAVE)]
        //-> Intro
    * [I said I was sorry.]
        You didn't, actually. 
        ** [Yeah.]
            -> Apologize
    * [{I'm sorry. | I'm sorry for crashing my car behind your shop epic style.}]
        Thanks bro(: that's all i wanted. lmk if you need anything else
        -> END
-> END

= Apologize // loopar igenom tills spelaren ber om ursäkt
{ &You still haven't. | Still waiting. | What was that? }
* [Ok, I'm sorry.]
    -> IntroCont
+ [{&Cool. | Right on. | Yeah. | That's nice.}]
    -> Apologize

=== Question ===

-> END