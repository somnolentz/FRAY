INCLUDE Globals.ink


-> introNPC

=== introNPC ===
Do you want to go to the test level? 
+ [Yes]
    okay!
 ~ firstlevelaccepted = true
    ->DONE
    
+ [No] 
okay loser.
    ->IntroNPCExhaust


=== IntroNPCExhaust ===
I'm here if you need me.
->DONE

