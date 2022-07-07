# Project Review

## Julian Müller-Huschke

---

<!-- Your review goes here -->
<!-- Explain why you did the things that way or any snippet that is word mentioning -->
<!-- If you had any issue and how you resolved them -->

Overall, I was only able to spend limited time (about 6 hours) on this coding challenge, thus my project focussed on fulfilling the basic requirements well rather than all the bonus requirements. It looks and plays very bland, which hurts me a bit to submit it in this basic state. Still all systems are present and the code base forms a solid enough foundation to build an actually nice game from this barebones game.

Following parts are worth mentioning in the code and my implementation work:
1. I switched the player collision handling to a Unity Build in `CharacterController`, because using a rigid body for the player almost never results in a nice controllable player character. This `CharacterController` however does not like the world moving around him, so collision handling is not perfect. Still it works good enough. One proper way to solve this would be by writing custom version of Unity's `CharacterController`, however that would take a few extra hours. Another way would be by writing a custom 2D collision engine for the game, which would probably be just as quick, since the collision shapes are extremly constrained anyway.
2. I used non-linear progression to keep the game a bit more interesting. This means, in the beginning it speeds up quite fast but later it speeds up less for each score point reached. This should delay the inevitable point where the game becomes unbeatable. Still lots of more balancing would be needed to get the progression curve just right. Also, I think both the platform gaps should progressively become smaller and the players movement speed should increase over time in order to keep up with the faster moving platforms later in the game.
3. I struggled with building a nice clean game state handling, since my previous Unity projects never really required a menu system. I think what I came up with works fine, but I think I should study some more example projects in order to learn about best practices.
4. I should have asked you at the beginning of the coding challenge whether using a newer Unity version is fine for you. This old version is amd64-only and runs very slow on the ARM-based laptop I used. Well, now it is to late and I used the old one from the README.md anyway.

I think the result overall is alright. Looking forward to discussing the project with you!