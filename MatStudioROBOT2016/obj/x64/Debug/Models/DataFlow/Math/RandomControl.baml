   M S B A M L   `   `   `  ����M  IMatStudioROBOT2016, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:MatStudioROBOT2016.Models.DataFlow.Math'MatStudioROBOT2016.Models.DataFlow.Math  5      A =MatGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/$clr-namespace:MatGUI;assembly=MatGUIMatGUI 5
      ;    5MatStudioROBOT2016.Models.DataFlow.Math.RandomControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:MatStudioROBOT2016.Models.DataFlow.Math     5      7matgui$clr-namespace:MatGUI;assembly=MatGUI     5
      $	��100��5	      $	��200��6   .��5      � .+�5   
     MatGUI.MatFocusableBG    MatGUI.MatGraph  5   
   -   5           Name$  	Histogram��5         MatBackGroundColor   BackgroundColor# �   6-   $��0,0,0,30q�6d      ZoomX$
 50.0Q�5         ZoomY$
 0.03Q�6'      OffsetX$
 -6.0Q�64      Mode$ Histgram=�6C   �� 5   
   $��分散��6     MatForeGroundBrush#���  6   $��Bottom=�6O   $
��Left=�6j   $��30��6�   $��50��6�   $��Center=�6�     MatGUI.MatDoubleBox  5   
   -   5          Name$ sigma��5         Minimum$	 0.1Q�6-      Maximum$	 1.0Q�6;   	   DoubleValue$		 1.0Q�6I   
   Format$
 f2��6[      Delta$	 0.1Q�6g   $��30��5      $��Bottom=�6*   $
��Left=�6E   $��50,0,0,0q�6`   $��50��6r     MatGUI.MatButton  5   
   -   5          Name$
 Calc��5      $��実行��6)   $��30��5      $��Bottom=�6'   $
��Left=�6B   $��	100,0,0,0q�6]   $��50��6p   6{   �� 5   
   -   5         ��Name$ ResultTb��5      $��Bottom=�6&   $��30��6A   #���  6M   $��	200,0,0,0q�6   5      5      