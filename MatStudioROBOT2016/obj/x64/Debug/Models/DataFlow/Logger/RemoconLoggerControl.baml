   M S B A M L   `   `   `  ����M  IMatStudioROBOT2016, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:MatStudioROBOT2016.Models.DataFlow.Logger)MatStudioROBOT2016.Models.DataFlow.Logger  5      A =MatGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/$clr-namespace:MatGUI;assembly=MatGUIMatGUI 5      D    >MatStudioROBOT2016.Models.DataFlow.Logger.RemoconLoggerControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Ilocal7clr-namespace:MatStudioROBOT2016.Models.DataFlow.Logger     5      7matgui$clr-namespace:MatGUI;assembly=MatGUI     5      $	��400��5      $	��500��6     J� SnapsToDevicePixels  . .��5
      �  � RowDefinitions 5   
   �� 5      �� 5      $	��295 �6   $	��295��6)   .+�5   
     MatGUI.MatFocusableBG  $��2��6      MatGUI.MatScrollViewer  5   
   -   5          Name$ GraphScrollViewer��5   !   $��80,6,6,6q�5   !      ScrollHorizontal . $��Visible=�6K   $��Visible=�6s   .��5      �� -   5         ��Name$ GraphCanvas��5         MatWorkAreaBrush#���   6*   }�.  ��6o   �� *ActualHeight+ �� ElementName$ ListStackPanel�� �� Mode$ OneWay=�.+�5      �� -   5         ��Name$ TimeBar��5        MatThemeColorBrush#���  6(   	 �� Y2$
 1000��6V   5      5        5   
   -   5        $ ListScrollViewer��5   !   $
��Left=�6;   $��75��6V   $��5,6,6,6q�5   !   $��Visible=�62   $��Hidden=�6Z   .��5      �� -   5        	 ��Name$	 ListStackPanel��5        MatForeGroundBrush#���  61   5      �� 5   
     MatBorderBrush#���  6   $��Bottom=�6E   $��Stretch=�6`   $��6��6~     5   
   $��2��6!   $��0,0,0,35q�6.   .��5       � .+�5!      �� $
��Left=�6   $��75��68   .+�5"      �� $��Analog L��6    $��20��60   $
��0,55q�6<   #���  6J   $��Right=�6|   �� 5#      $��Analog R��6    $��20��60   $
��0,55q�6<   #���  6J   $��Right=�6|   5$      �� 5%      $
��80,0q�6   .+�5&        MatGUI.MatGraph  -   5        
  Name$
 AnalogLGraph��5&   &      Mode$ DistributionMap=�6<     MatBackGroundColor   BackgroundColor# �  6S   $	��128��6�   $	��0,1q�6�   ��6�   �� *ActualWidth+$ GraphScrollViewer��$ OneWay=�  5'      -   5        $
 AnalogRGraph��5'   &   $ DistributionMap=�6<   # �  6S   $	��128��6�   $	��0,1q�6�   ��6�   �� *ActualWidth+$ GraphScrollViewer��$ OneWay=�5(      5)      5*      �� 5,   
   $��Bottom=�6   $��Stretch=�60   $��25��6N   $��6q�6Z    �� Orientation$ 
Horizontal=�6e   $��1��6~   .+�5-      �� -   5         ��Name$ DataList��5-      $	��200��6)     MatGUI.MatButton  5.      -	   5          Name$ 	NewButton��5.      $��25��62   $��25��6>      MonoColorIcon .  $��Bottom=�6_   $
��Left=�6z   ��5/      �� 50        PlayIcon#���  6   $��25��6@   $��25��6K   52        53      -
   5        $ InportButton��53      $��25��65   $��25��6A    .  $��Bottom=�6b   $
��Left=�6}   ��54      �� 55      #���  6   $��25��6@   $��25��6K   57        58      -   5        $ SaveAsButton��58      $��25��65   $��25��6A    .  $��Bottom=�6b   $
��Left=�6}   ��59      �� 5:      #���  6   $��25��6@   $��25��6K   5<        5=      -   5        $ DeleteButton��5=      $��25��65   $��25��6A    .  $��Bottom=�6b   $
��Left=�6}   ��5>      �� 5?      #���  6   $��25��6@   $��25��6K   5A        5B      -   5        $ 
PlayButton��5B      $��25��63   $��25��6?    .  $��Bottom=�6`   $
��Left=�6{   ��5D      �� 5E      #���  6   $��25��6@   $��25��6K   5G        5H      -   5        $ PauseButton��5H      $��25��64   $��25��6@    .  $��Bottom=�6a   $
��Left=�6|   {�.  ��5J      �� 5K        	PauseIcon#���  6   $��25��6A   $��25��6L   5M        5N      -   5        $ 
StopButton��5N      $��25��63   $��25��6?    .  $��Bottom=�6`   $
��Left=�6{   {�.  ��5P      �� 5Q        StopIcon#���  6   $��25��6@   $��25��6K   5S        5T      -   5        $ 	RecButton��5T      $��25��62   $��25��6>    .  $��Bottom=�6_   $
��Left=�6z   ��5V      �� 5W        RecIcon#���  6   $��25��6?   $��25��6J   5Y      5Z      5[      5\      