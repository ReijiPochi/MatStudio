   M S B A M L   `   `   `  ����A  =MatGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MatGUIMatGUI  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollBarButton  �� 
TargetType �� Property �� Value  MatBackGroundBrush �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value	 �� 
TargetName  MatBackGroundBrush_T  ScrollBarPageButton  ScrollBarThumb
 �Name ��Name  MatBorderBrush  MatFocusBrush_T  MatScrollBarStyle J� SnapsToDevicePixels � RowDefinitions �� LineUpCommand  MatInactiveForeGroundBrush �� Stretch ]�Name J� IsMouseOver ]� DecreaseRepeatButton �� PageUpCommand ]� IncreaseRepeatButton �� PageDownCommand ]� Thumb �� LineDownCommand �� Triggers � ColumnDefinitions �� LineLeftCommand �� PageLeftCommand �� PageRightCommand �� LineRightCommand 	 GridViewScrollViewerStyle 
 MatWorkAreaBrush �� Columns �� Path  �� RelativeSource!! �� ColumnHeaderContainerStyle" �� ColumnHeaderTemplate## �� ColumnHeaderTemplateSelector$ �� AllowsColumnReorder% �� ColumnHeaderContextMenu& �� ColumnHeaderToolTip-   'System.Windows.Input.KeyboardNavigation'    DirectionalNavigation( �� CanHorizontallyScroll) �� CanVerticallyScroll* �� CanContentScroll+ %� IsVirtualizing, %� VirtualizationMode- �� Orientation. �� ScrollableWidth/ � Maximum0 �� ViewportWidth1 �� ViewportSize2 �� HorizontalOffset3 � Value,4 �� %ComputedHorizontalScrollBarVisibility5 �� ScrollableHeight6 �� ViewportHeight7 �� VerticalOffset*8 �� #ComputedVerticalScrollBarVisibility $  GridViewColumnHeaderGripperStyle9 �� Padding   MatGridViewColumnHeaderStyle  MatForeGroundBrush: n�Name  MatListViewStyle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:MatGUI  5      %�  &	        &	 �    &	 �    &	 5    7   7  7  7  7   7   7  7  7  7   &		 ,    &	 �    &	 �    &	 �    �� 5      "  ��6$   .�5   
   ��  �@�OverridesDefaultStyle . 62   �� 5   
    � |� .  6&   �� 5   
    � �� .  6&   �� 5	   
    � ��# �  6'   �� 5
   
    � �� 5      �� 5      " ��6"   .R�5      � #��z��6   .+�5      �� $ Border��6!   �� 5      $ ContentPresenter��6H   $��Center=�6+   $��Center=�6b   #��z��6}   #��z��6�   #��z��6�   5       5      P� 5       � �� . . �5      �� $	 Border��6;   # �  6O    � ��5      P� 5       � {� .  . �5      ��  � y�$ Hidden=�6;   5      5      5      5      �� 5      "  ��6(   .�5   
   ��  �@�OverridesDefaultStyle . 62   �� 5    
    � ��
 ���� 6'   �� 5!   
    � |� .  6&   �� 5"   
    � �� .  6&   �� 5#   
    � �� 5$      �� 5%      " ��6"   .R�5&      �� #��z��6    #��z��6D   #��z��6f   5'      5)      5*      �� 5,      "  n�6#   .�5-   
   ��  �@�OverridesDefaultStyle . 62   �� 5.   
    � �� .  6&   �� 5/   
    � �� 50      �� 51      " n�6"   .R�52      � $

 Grid��6,   $��0,0,0,0q�6   .+�53      �� $ 	Rectangle��6B   $��Stretch=�6$   $��Stretch=�6U   $
��Auto��6q   $
��Auto��6~   #���  54   $   55       56      P� 57       �@J�IsMouseOver . . �58      �� $	 	Rectangle��65   # �  6L    � ��59      P� 5:       �@n�
IsDragging . . �5;      �� $	 	Rectangle��65   # �  6L    � ��5<      5>      5@      5A      �� 5C      "  ��6&   .�5D   
   ��  � ��# �  6'   �� 5E   
    �@��IsPressAndHoldEnabled .  69   �� 5F   
    �@��IsFlicksEnabled .  63   �� 5G   
    � ��$ 15��6"   �� 5H   
    � �� 5I      �� 5J      " ��6"   .R�5K      � $
 Bg��6   #��z��6'    .  5L      �� 5M      $��23��6,   �� 5N      $��0.00001* �6,   �� 5O      $��23��6,   .+�5Q      �� #��Z 6'   8��  ��5R      F� 5S   "   "�����r����- �@9��@9���    ��- 	#���  6A   $��8��6u   $
 Fill=�6�   $��0��6�   $��8��6�   5U      ]� 5V      $ 
PART_Track��6    ��. #{�z 6O   $��1��6y    5W      �� 5X   "   #��Z 6/   8�� 6\    5Z      �� 5[   "   #��Z 6/   8�� 6^    5]      n� 5^   "   8�� 6(   5`      �� 5a      #��Z 6'   $��2��6V   8�� ��5b      F� 5c   "   "�����r��@9���@9����- �    @9��	#���  6B   $��8��6v   $
 Fill=�6�   $��0��6�   $��8��6�   5e      5f      5g      5i       5j   
   P� 5k       �@��Orientation$ 
Horizontal=�6-   . �5l      ��  � ��$
 Auto��6*   �� 5m       � ��$ 0��6-   �� 5n       � ��$ 15��6+   �� 5o       � ��# �  6/   �� 5p       � �� 5q      �� 5r      " ��6*   .R�5s      � $
 Bg��6#   #��z��6/    .  5t   "   �� 5u   &   $��23��67   �� 5v   &   $��0.00001* �67   �� 5w   &   $��23��67   .+�5y   "   �� #��Z 6/   8�� ��5z   &   F� 5{   *   "����@9��    ���- @9�����- ��- 	#���  6H   $��8��6|   $
 Fill=�6�   $��0��6�   $��8��6�   5}   #   ]� 5~   "   $ 
PART_Track��6(   $��1��6<   #{�z 6L    5   &   �� 5�   *   #��Z 67   8�� 6f    5�   &   �� 5�   *   #��Z 67   8�� 6g    5�   &   n� 5�   *   8�� 60   5�   #   �� 5�   "   $��2��6/   #��Z 6?   8��	 ��5�   &   F� 5�   *   "������-     �@9��@9���@9����- 	#���  6I   $��8��6}   $
 Fill=�6�   $��0��6�   $��8��6�   5�   #   5�      5�      5�      5�      5�      �� 5�      "  ��6.   .�5�   
   ��  � ��# � 
 6'   �� 5�   
    � �� .  6-   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      � #��z��6    5�      �� 5�      $��* �6/   �� 5�      $
��Auto �6/    5�      �� 5�      $��* �6,   �� 5�      $
��Auto �6,   .+�5�      ]� #��z��6$   .+�5�      �� $	��Top=�6+   $��Hidden=�6@   $��Hidden=�6g   |�.  #��z��6�   .��5�   "   �� $��2,0,2,0q�6=   # z 5�   +    5�   N   �� $" TemplatedParent.View.Columns�  �� *TemplatedParent+! 5�   +   �� $5 /TemplatedParent.View.ColumnHeaderContainerStyle�  �� *TemplatedParent+" 5�   +   �� $/ )TemplatedParent.View.ColumnHeaderTemplate�  �� *TemplatedParent+# 5�   +   �� $7 1TemplatedParent.View.ColumnHeaderTemplateSelector�  �� *TemplatedParent+$ 5�   +   �� $. (TemplatedParent.View.AllowsColumnReorder�  �� *TemplatedParent+% 5�   +   �� $2 ,TemplatedParent.View.ColumnHeaderContextMenu�  �� *TemplatedParent+& 5�   +   �� $. (TemplatedParent.View.ColumnHeaderToolTip�  �� *TemplatedParent+5�   +   5�      �� 5�      $!��PART_ScrollContentPresenter��65   $' Local=�6X   ( .  ) .  $��Bottom=�5�   o   * . + . $, 	Recycling=�5�   u   5�      �� 5�      $��PART_HorizontalScrollBar��6$   $- 
Horizontal=�5�   $   $��1��6=   #/ z. 6J   #1 z0 6v   #3 z2 5�   $   #y�z4 6O   #���  5�   $   �� 5�      $��PART_VerticalScrollBar��6$   $��1��5�   $   #/ z5 64   #1 z6 6a   #3 z7 5�   $   #y�z8 6M   #���  5�   $   5�      5�      5�      5�      �� 5�      "  n�65   .�5�   
   ��  � ��$ 18��6"   �� 5�   
    � �� 5�      �� 5�      " n�6"   .R�5�      �� #9 z��6   
������ ._�5�      �� $��Center=�6$   $��1��6A   #���  6K   5�      5�      5�      5�      �� 5�      "  ��61   .�5�   
   ��  � ��$ Center=�67   �� 5�   
    � ��$ Center=�65   �� 5�   
    � ��# �  6'   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      � .+�5�      �� $ HeaderBorder��6!   $��0,0,0,1q�67   #���  6Q   ._�5�      �� $ HeaderContent��6/   ��. $��Center=�6a   $��Center=�6|   5�      n� 5�      $: PART_HeaderGripper��6    $��Right=�6<   $��0,0,-9,0q�6X   #���  6j   5�      5�      5�      5�      �� 5�      "  ��6%   .�5�   
   ��  � �� 5�      �� 5�      " ��6"   .R�5�      �� $��Border��6   $��0q�6+   ._�5�      �� #��� 	 6'   .��5�      �� 5�      5�      5�      5�      5�      5�      