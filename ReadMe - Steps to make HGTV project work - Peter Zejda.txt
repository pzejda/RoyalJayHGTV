1. make sure to have firefox browser (it is called explicitly in the
test methods.
2. one the .css file is inside a project in visual studio right
click the refernces list item in the solution explorer
3. choose manage NuGet Packages
4. type webdriver into the search
5. to get it to work I downloaded (maybe online the first 2 dll.s are needed?)
- selenium.webdriver
- selenium.firefox.webdriver
- selenium.webdriver.iedriver
- selenium.webdriver.chromedriver
- selenium.phantomjs.webdriver
6. run the tests in visual studio, 
7. DreamHomeGiveAwayReminderSignup - should pass (unless if their web page
changes before you guys check)
VerifyDreamHomeReminderRegistrationViaEmail - should fail on the assert
because HGTV doesn't send an email when someone signs up for notifications

* note I didn't use selenium's software for recording the screen to write the
tests but I did use their library for navigating a web page
