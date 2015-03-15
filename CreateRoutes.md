# Introduction #

Use this menu item to create a canonical route from an activity. You can later apply this route to an activity without gps data (for example if you forgot your watch, or if you want to update your database of pre-gps workouts).


# Details #

Unfortunately, the ST Plugin API doesnt allow plugins to create new Routes, but it does allow them to update existing routes. So the first thing to do is to go to the Routes View (note: "Other Tasks -> Select View -> Routes", not the Routes panel on the the Daily Activity page, or the "AR - Routes" view), and create a new route (Routes -> Add Route).

At this point, if you're using ST 2.1 or later, you will get the opportunity to select an activity, and create a route from that. I dont recommend that because ST will throw away all time/pace and evelation data from the route.

Instead, stick with the default "New route" option.

Go back to the Daily activity, or Reports View, select your workout, and choose "Edit Activity -> AR Create Routes From Activities...". Your activity will appear, select it, and make sure its mapped to the correct Route (use the drop down). By default, AR will choose a route with no name, or the name "New Route".

Click OK, and your route will be updated (note that you can use the same procedure to update the route from another activity at a later date).