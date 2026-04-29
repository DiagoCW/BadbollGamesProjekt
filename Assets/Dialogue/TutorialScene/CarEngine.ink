INCLUDE GlobalsMain2.ink

{not carHood:
~carHood = true
}
Do you open the hood?
    *[Pop the hood]
    The hood of the car slides open.
    -> Examine
    *[Don't]
    You don't open the hood.
-> END

=== Examine ===
{not carEngine:
The cars engine looks like a jumbled mess of wires and metal if you were an enginear you could probably see the problem right away; you probbably need a more manual aproach.
~carEngine = true
}
What could the problem be?
	*[Check oil]
		You pull the dipstick out of its tube, methodically whipe it on your coat sleave and dip it once more; It's a bit low.
        -> Examine
    *[Check radiator fan]
        You peer down at the radiator. The fan is spinning, it sounds strained while trying to keep up with the heat.
        -> Examine
	*[Check coolant]
		You lean over the car and look at a translucent reservoir, the level sits below the minimum line; it's usually alot more blue.
        -> Examine
=== FoundProblem ===
-> END