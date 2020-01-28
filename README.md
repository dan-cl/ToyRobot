# ToyRobot

Console app that controls a toy robot on a table. Initially the robot needs to be placed on the table with a position and heading.
After the robot has been placed it can be moved around the table and rotated. It can be re-positioned anytime and the current position and heading of the robot can be recalled at anytime. The robot cannot be positioned or moved outside the bounds of the table.

This app was created as an MVP to meet specific requirements, therefore the size of the table is fixed to 5x5, there is no graphical representation of the robot and no other objects on the table for the robot to collide with.

## Controls


`PLACE <xCoordinate>,<yCoordinate>,<heading>`

Place robot on table with coordinates and heading.

e.g. PLACE 3,1,NORTH


`MOVE`

Move robot one unit in currect direction.

`LEFT`

Rotate robot 90 deg anticlockwise.

`RIGHT`

Rotate robot 90 deg clockwise.

`REPORT`

Report robot's current position and heading.

`HELP`

Show instuctions.

`EXIT`

Exit application.

## How to Run

Built on .netcore 3.1.

Run "dotnet build" from the root directory of the solution to restore packages and build solution, then execute ".\ToyRobot.App\bin\Debug\netcoreapp3.1\ToyRobot.App.exe".

Run "dotnet test" from solution root to run tests.
