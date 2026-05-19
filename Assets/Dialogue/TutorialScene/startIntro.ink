INCLUDE GlobalsMain2.INK
/*
#speaker: Player
A wave of cold air hits you. The day's last sunlight penetrates the tree tops, and the dulcet tones of the forest are drowned out by the low pitched growl of the passing traffic. #speaker: Player
Your car, a Honda Civic '89', has ceased functioning on the side of the road. Not exactly an uncommon occurrence considering what it’s been through. 
	* [For fuck's sake…] #speaker:Player
		You should make a mental note to put a crown or two in the swear jar. #speaker:
			** [No, I think I deserve this one actually.] #speaker:Player
			** [It's barely a swear]”fucks sake” Is barely a swear, I could have said something much worse. #speaker:Player
			** [Shit, I'm losing control of myself] #speaker:Player
	* At least I managed to pull over[.], you think. <> 
		Neither your insurance nor your body could withstand a high speed car crash. 
			** [I'd take that shit like a champ.] <> #speaker:Player
			    I'll hold you to that.
			** [Yes. I would die without question.] <> #speaker:Player
	* [I wonder where the closest bus station is from here?] #speaker:Player
		Buses probably only run hourly this late.
			** [I hate Skånetrafiken.] #speaker:Player
			** [I can wait.] #speaker: Player
			    No, you really can't. Get this thing running again, or resign from your department effective immediately.
- No matter. You have somewhere important to be so you should probably pop the hood and have a look. #speaker:
-> END
*/

<i>A wave of cold air hits you. The day's last sunlight penetrates the tree tops, and the dulcet tones of the forest are drowned out by the low pitched growl of the passing traffic.</i> #speaker:
<i>Your Honda Civic '89 has ceased functioning on the side of the road. Not exactly an uncommon occurrence considering all it’s been through.</i>
    * [At least I managed to pull over.]
        <i>At least I managed to pull over, you think. Neither my insurance nor body could withstand a high speed car crash.</i>
	* [Jävla skrothög!]
	    <i>Your frustration does not help. The car is still not working.</i>
-
* [I should call a tow truck...]
    <i>That could take a few hours. You need to get to that crime scene immediately. You can try fixing the problem yourself.</i>
        **[That's fine. I can wait.]
            <i>No, you really can't. Get this thing running again, or resign from your department effective immediately from pure shame.</i>
        **[<b>Check under the hood</b>]
            <i>That's the spirit!</i> <>
    - <i>Pop that hood and have a look, it can't be that difficult.</i>
-> END
	    