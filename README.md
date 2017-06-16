# _Band Tracker_

#### _Allow users to track a band's concert venues_

#### By _**Kimlan Nguyen**_

## Description

_This is a prototype program that allow user to track bands. User will be able to perform the following actions:_

## Specifications

 | Behavior   |Input Example   | Output Example  |
 |---------------- |:----------: |:------------: |---------:|
 |Allow users to add a new band to database | add "Journey" | Display "journey" |
 |Allow users to add an new venue to database | add "Moda Center" | Display "Moda Center" |
 |Allow users to views bands in database | Retrieve all bands | Display all bands in database |
 |allow users to views venues in database | Retrieve all venues | Display all venues in database |
 |Allow users to view all bands that have concert at a venue | click on "Moda Center" | Display "Journey" |
 |Allow users to view all venues that a band has concert at | click on "Journey" | Journey performs at "Moda Center" |
 |Allow users to edit a venue | Change "Moda Center" to "Rose Quarter" | Display "Rose Quarter" |
 |Allow users to edit a band | Change "Journey" to " Reo SpeedWagon" | Display "Reo SpeedWagon" |
 |Allow users to remove a venue | remove "Rose Quarter" | "Rose Quarter" is removed from database
 |Allow users to remove a band | remove "Reo SpeedWagon" | "Reo SpeedWagon" is removed from database |



## Setup/Installation Requirements
* _This program requires installing C#, Git and asp.net5. Follow the instruction here https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c to install c# and asp.net5 on your computer._
* _Download or clone this file using Git._
* _To create a database on your computer, open Windows PowerShell. Type sqlcmd -S "(localdb)\mssqllocaldb"; CREATE DATABASE band_tracker; > GO > USE band_tracker; > GO > CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255), type VARCHAR(255)); > CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255), location VARCHAR(255), capacity INT); > GO_
* _In Windows PowerShell navigate to the BandTracker folder._
* _Type "dnu restore" in the console to compile Nancy, exclude ""._
* _Type "dnx kestrel" in the console run the program, exclude ""._
* _Paste this link http://localhost:5004/ onto your web browser._

## Known Bugs

_Create bands or venues with the same name as the other bands or venues will create duplicates._

## Support and contact details

_Kimlan1510@gmail.com_

## Technologies Used

* _HTML_
* _C#_
* _Nancy_
* _Razor_
*_SQL

### License

*This program is licensed under MIT License.*

Copyright (c) 2017 **_Kimlan Nguyen_**
