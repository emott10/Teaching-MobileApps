# Image Manipulator
For my app, Image Manipulator, the goal was to create a working image manipulator that can change the effects of a picture. When using my app you first get two opitions, either take a new photo or pick one from the gallery. After the user has a photo picked out you can pick from nine different image effects that you can choice from to apply to the photo, you can also apply more then one to a photo. If you don't like the photo effects applied you can use the revert to original button to start over with the effects.After you found the effects that you like you can then save the photo to your phones gallery and start off with a new photo. This app was made to meet the homework requirements but also to serve as a good app to show to future employers so they can see an example of my work(Still needs work done for me to be able to confidently show them). 


## System Design 
For my image manipulator the system requirements that I was able to run were Android 7.0 and 7.1. Other then that I didn't have the ability to test any other Android OS. For a walk through of how to use the app, check out the usage section below. For scenarios, this app would work great for anyone that likes to take pictures and apply effects to them. 

## Usage
Step 1: Right when you open the app you will be prompted with two buttons, one of them takes you to the camera app so you can take a new         photo to edit. The other will take you to the gallary to pick a photo that the user has already captured. Although, I was not           able to get this button working, so it just gives you a toast saying that the button is still in development.
        Check out the picture below to see the screen I am talking about.
https://lh3.googleusercontent.com/6rRfQlP-yQ-HeQLIjdd3hCIrIojy0GtI_BncZBMCyoNh2ixCpQSgY0t5F50iRNkIaFRevKb-CM4bPzQjUnJC=w1920-h925-rw

Step 2: Since, the gallery button does not work (yet) the only opition you have is to take a picture. This will bring up the camera app         and you can take a picture of whatever or whoever you like. After taking the picture you will get a prompt to either accept the         picture or take a new one. If you accept the app will move to the next layout where you can see the picture you took and pick           which effect you would like.
        Check out the picture below to see the screen I am talking about.
https://lh3.googleusercontent.com/WNx97xOwYuKNhTxwu0xRvIo7J17ALnf-MCAWNJF5Ewn_UQnwMc66tjRVe4eS2TYib0gaiTZnXDbIo8Y-QeCU=w1920-h925-rw

Step 3: From this screen you can chose one or more effects to apply. Please keep in mind that the effects may take a few seconds to             render after you push the button and the app will freeze for that amount of time but the app didn't crash it is just changing           all of the bytes in the picture which can be a lot.

Step 4: If you end up not liking an effect that you applied then you can use the revert to original button to go back to the original           picture.

Step 5: If you like the picture and want to save it, scroll to the bottom of the effects and this is were you can save the photo. 
        SideNote: This only worked the first time I tried to save the picture after that the picture didn't show up in the gallery.

Step 6: If you want to start over at ANY time just simple push the back button on your android device and it will take you make to the           first screen, where you can take a photo or pick on from the gallery.
