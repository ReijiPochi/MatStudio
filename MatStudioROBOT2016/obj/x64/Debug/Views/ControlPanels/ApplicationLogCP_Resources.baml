   M S B A M L   `   `   `  ����M  IMatStudioROBOT2016, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MatStudioROBOT2016.Views.ControlPanels&MatStudioROBOT2016.Views.ControlPanels  5      G CMatFramework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS9clr-namespace:MatFramework.Controls;assembly=MatFrameworkMatFramework.Controls 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ListViewItemStyleForLog  �� 
TargetType �� Property �� Value  MatForeGroundBrush  MatItemsBackGroundBrush �� 
TargetType ��Name �� Padding J� SnapsToDevicePixels �� Triggers P� Property	 P� Value  MatBackGroundBrush
 ]� 
Conditions �� Property �� Value  MatFocusBorderBrush  LogConditionCell1   +MatFramework.Controls.LogConditionIndicator    	Condition  MessageCell ^� ShowDuration � ToolTip  DescriptionCell�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      >local4clr-namespace:MatStudioROBOT2016.Views.ControlPanels  5      Bmatf9clr-namespace:MatFramework.Controls;assembly=MatFramework  5      %)  &	        &	 �    &	 �    &	 j    �� 5      "  ��6,   .�5   
   ��  �@J�SnapsToDevicePixels . 60   �� 5   
    � ��# �  6'   �� 5	   
    � ��# �  6'   �� 5
   
    � ��$ 30��6#   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ Border��6   $ 2q�6-    . #��z��5      #��z��6G   ._�5      �� #��z��6/   5       5      P� 5       �@��
IsSelected	 . . �5      �� # �  6;    � ��5      ]� 5      
 5      �� 5   "    �@J�	IsFocused . 6A   �� 5   "    �@��
IsSelected . 6B   /�5      �� 5   "   # �  6?    � ��5!      5#      5%      5&      �� 5(      .R�5)   
       6%   �� *	Condition+5*      �� 5,      .R�5-   
   �� $ 10000��6I   $��CharacterEllipsis=�6m   ��6   �� *	Message+ 6-   �� *	Message+6m   5.      �� 50      .R�51   
   �� $ 20000��6Q   $��CharacterEllipsis=�6u   ��6   �� *Description+ 61   �� *Description+6u   52      54      