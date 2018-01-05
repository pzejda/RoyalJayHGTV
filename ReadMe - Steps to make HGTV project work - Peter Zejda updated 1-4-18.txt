updated 1-4-18

-To easily get the packages follow steps 2-5 and use search term selenium.

-The big test that registers for dream home giveaway should be stepped through line by line :(  If you run it fast
it will probably fail, I was going to fix that but then the project took up two evenings for me plus some.  When I finally
got the input to all be correct on the big test and clicked the submit button it didn't like the data in the 
3 dropdown boxes that I entered headlessly.  This could be fixed by using headed keyboard presses (but the selenium
keyboard presses didn't work seem to be working for me, and I liked my headless solution).  I analyzed the html with
a diff tool and saw some minor differences, like some classes were taken away on the month data for example.  I figured
when I had all the month data correct in the html via javascript calls it shouldn't complain.  Anyway, you can see what
I am talking about if you step through the big test.  In the end it complains about the 3 dropdown entries.  Overall,
this was a good learning experience for me.

------------------------------------------------------------------------------
1. make sure to have firefox browser (it is called explicitly in the
test methods.
2. one the .css file is inside a project in visual studio right
click the refernces list item in the solution explorer
3. choose manage NuGet Packages
4. type webdriver into the search
5. to get it to work I downloaded (maybe online the first 2 dll.s are needed?)
- selenium.webdriver
- selenium.firefox.webdriver
- selenium.phantomjs.webdriver
6. run the tests in visual studio, 
7. DreamHomeGiveAwayReminderSignup - should pass (unless if their web page
changes before you guys check)
VerifyDreamHomeReminderRegistrationViaEmail - should fail on the assert
because HGTV doesn't send an email when someone signs up for notifications

* note I didn't use selenium's software for recording the screen to write the
tests but I did use their library for navigating a web page
