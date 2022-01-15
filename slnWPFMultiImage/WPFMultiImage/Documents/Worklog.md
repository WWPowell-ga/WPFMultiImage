# Worklog

## 2022 Jan 3 
 Watched Tim Corey's Youtube video on the C# challenge. https://www.youtube.com/watch?v=Dn_xVbIn8iw

## 2022 Jan 4 
Joined Twitter so I can Tweet at #IAmTimCorey.

## 2022 Jan 5 
Joined GitHub and did initial commit of project after rewatching Tim Corey video.

## 2022 Jan 6
Changed Twitter handle  
Tweeted Markdown cheetsheet  
Rebuilt Worklog  
Completed initial roadmap - this is subject to change  

## 2022 Jan 7
Moved menu to a UserControl - so when people wnat to upgrade this app by adding menu items they should leave MainWindow alone and upgrade usMainMenu [probably]  
  
I've done several similar 'image apps' before in WinForms. Current job is a WPF shop so this is an opportunity to learn.  
  
The code in ucMainMenu then should fire code in MainCode. ucMainMenu should have VERY little code.
File->EXIT works.

## 2022 Jan 8
Stubbed in File->Open so I could see the child window I built with a 'child' menu.
Added WINDOWS to main menu
Went to study what a WPF FileOpenDialog looks like - I've done WinForms and assume this will be similar
Added ChildWindow, ucChildMenu, ChildCode
Started coding Open File dialog

## 2022 Jan 9
Open File Dialog working
Basic stats working
Image loading

TO DO:
- [ ] Open File dialog should CenterScreen
- [ ] Statistics grid spaces out rows way too far
- [ ] Scrollviewer and Imag don't yet co-operate fully

I've decided I don't like the 'child window approach'. If you open many images the screen gets too cluttered to work on.  
I'll keep it for now and explore some better alternatives later

## 2022 Jan 10
Scrollviewer and Img are more co-operative. Statistics grid has better spacing. SO many options in WPF.  
Statistics from bitmap as well as file
Started breaking down the bitmap metadata
Started minimizing, maximizing, normalizing, and closing all windows.  
Extent of child area of main window is wrong!

## 2022 Jan 11
I'm starting to write Windows inside a window inside a window - messy.
Also the MDI 'style' is shunned these days. Who knew. The hazards of being old...
Going to start moving to a Navigation-style window I think.

## 2022 Jan 12

## 2022 Jan 13
Created robust ImagesClass
Started creating CarouselClass

## 2022 Jan 14
Getting closer
BitmapImage is not being returned??? Maybe should be BitmapSource???

## 2022 Jan 15
Stupid bug with BitmapImage fixed
ImageGrid displaying images
Stats complete but not Palette
Not System.Windows.Media.PixelFormats contains good info but is deep.
I'll have to write a BIG switch to support it. Later.


