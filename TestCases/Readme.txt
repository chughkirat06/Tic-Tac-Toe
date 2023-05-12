Simple Test Case
-----------------
Game1-USER_VS_USER --> Continue playing user vs user mode (Expected - User is able to continue playing user vs user game)
Game1-USER_VS_COMPUTER --> Continue playing user vs computer mode (Expected - User is able to continue playing user vs computer game)
Game1-COMPUTER_VS_COMPUTER --> Continue playing computer vs computer mode (Expected - Continued execution for computer vs computer game)


Edge Test Case
---------------
Game2-USER_VS_USER --> Incorrect Coordinates (Expected - Will skip the move on the board)
Game3-USER_VS_COMPUTER --> Empty File (Expected - A new game can be played)


Complex Test Case
-----------------
abc --> Incorrect filename but content is correct (Expected - )
Game1-COMPUTER_VS_COMPUTER --> Filename correct but content is incorrect (Expected - )