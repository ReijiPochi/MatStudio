   M S B A M L   `   `   `  ����M  IMatStudioROBOT2016, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MatStudioROBOT2016MatStudioROBOT2016  5      A =MatGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/$clr-namespace:MatGUI;assembly=MatGUIMatGUI 5      I)clr-namespace:MatStudioROBOT2016.ControlsMatStudioROBOT2016.Controls  5      e7clr-namespace:MatStudioROBOT2016.Models.DataFlow.Logger)MatStudioROBOT2016.Models.DataFlow.Logger  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source9    3MatStudioROBOT2016.Controls.MatDataObjectsPresenter �� 
TargetType �� Property �� Value �� 
TargetType  MatGUI.MatScrollViewer  Name ��Name J� 	AllowDrop   MatEditableAreaBrush	 ��Name  MatFocusBrush_T
 K� ZIndex6   0MatStudioROBOT2016.Controls.MatDataObjectControl  MatBorderBrush  MatInactiveForeGroundBrush � ColumnDefinitions  MatBackGroundBrush ��Name n�Name ��Name �� Orientation �� Triggers P� 
SourceName P� Property P� Value �� 
TargetName � DataContext8   2MatStudioROBOT2016.Controls.MatDataObjectControlVM  MatGUI.MatMenuItem m� Command �� Mode m� CommandParameter �� RelativeSource:   4MatStudioROBOT2016.Controls.MatDataOutputPortControl  MatForeGroundBrush ��Name �� CornerRadius �� Path9   3MatStudioROBOT2016.Controls.MatDataInputPortControlA   ;MatStudioROBOT2016.Models.DataFlow.Logger.RemoconButtonItem ��Name � UseLayoutRounding�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      *local clr-namespace:MatStudioROBOT2016  5      /matgui$clr-namespace:MatGUI;assembly=MatGUI  5      3ctrls)clr-namespace:MatStudioROBOT2016.Controls  5      Blogger7clr-namespace:MatStudioROBOT2016.Models.DataFlow.Logger  5        5	      �� 5
   
   $N H/MatStudioROBOT2016;component/Views/ControlPanels/TerminalCP_Design.xaml?�6   �� 5   
   $T N/MatStudioROBOT2016;component/Views/ControlPanels/ApplicationLogCP_Design.xaml?�6   �� 5   
   $O I/MatStudioROBOT2016;component/Views/ControlPanels/RobotCoreCP_Design.xaml?�6   �� 5   
   $S M/MatStudioROBOT2016;component/Views/ControlPanels/RobotCoreMainCP_Design.xaml?�6   �� 5   
   $N H/MatStudioROBOT2016;component/Views/ControlPanels/DataFlowCP_Design.xaml?�6   �� 5   
   $O I/MatStudioROBOT2016;component/Views/ControlPanels/InterfaceCP_Design.xaml?�6   �� 5   
   $O I/MatStudioROBOT2016;component/Views/ControlPanels/GameMakerCP_Design.xaml?�6   %2  '         '  �    '  �    '  �    '  h
    �� 5      "   6   .�5   
   ��  � �� 5      �� 5      "   6"   .R�5      � .+�5        $ PART_Viewer��61   $��Visible=�5   1   $��Visible=�6Y   .��5      �� $ PART_Canvas��6%    . $
��2000��6K   $
��2000��6X   #���   6f   .+�5   "   �� $	 	PART_Line��6'   #���  6:   $��2��6e   $
 114514��6y   5       5!      5"      5#      5%      5&      �� 5(      "  6   .�5)   
   ��  � ��$	 150��6"   �� 5*   
    � ��$ 60��6#   �� 5+   
    � ��# �  6'   �� 5,   
    � ��# �  6(   �� 5-   
    � �� 5.      �� 5/      "  6"   .R�50      �� #��z��6   #��z��6G   $��1q�6s   ._�51      �  52      �� 53   "   �� 54   "   .+�57      �� $��17��6(   #���  64   $	��Top=�6`   $��2��6x   �� 58      $ NameTb��6(   $��17��68   $	��Top=�6D   $��Center=�6]   $��2��6z   {�59   (   �� *IsHardwareActivated+��6R   �� *Name+n� 5:      $ 
PART_Thumb��6$   #��z��68   #��z��6b   $��2��6�   ��5;   "   �� 5<   &   " n�66   .R�5=   *   �� 
������ 61   5>   '   5@      �� 5B      $ PART_Inputs��6)   $ Vertical=�6>   $��0��6U   $��0,19,0,0q�6e   5D      �� 5F      $ PART_Outputs��6)   $ Vertical=�6?   $��1��6V   $��0,19,0,0q�6f   5H      5I      5J       5K      P� 5L      $ NameTb��6E    � {� .  . �5M      �� # �  6;   $ NameTb��6p    � ��5N      5P      5R      �� 5S   
    �@�ContextMenu 5T      �� 5U       5V        5W      .@�5Y        $��Remove��6)    69   �� *CloseCommand+$ OneWay=� 6g   ��  �� *Self+5Z      5\      5]      �� 5_      "  6   .�5`   
   ��  � ��$ 17��6#   �� 5a   
    � ��# �  6'   �� 5b   
    � �� 5c      �� 5d      "  6"   .R�5e      � .+�5f      �� $ PART_Bd��6!   #���   62   $ 6��5g   !   $��12��62   $��12��6=   $��0,0,2,0q�6I   $��Right=�6Z   �� 5h      $��0,0,18,0q�6z   $��Right=�6�   ��6$   ��  �� *TemplatedParent+$ OutputPort.Name�6�   5i      5j      5l      5m      �� 5o      "  6   .�5p   
   ��  � ��$ 17��6#   �� 5q   
    � ��# �  6'   �� 5r   
    � �� 5s      �� 5t      "  6"   .R�5u      � .+�5v      �� $ PART_Bd��6!   #���   62   #���   6f   $��1q�6�   $ 6��5w   !   $��12��62   $��12��6=   $��2,0,0,0q�6I   $
��Left=�6Z   �� 5x      $��18,0,0,0q�6y   $
��Left=�6�   ��6$   ��  �� *TemplatedParent+$ InputPort.Name�6�   5y      5z      5|      5}      �� 5�      "  6   .�5�   
   ��  � ��$ 23��6#   �� 5�   
    � ��$ 23��6"   �� 5�   
    � �� 5�      �� 5�      "  6"   .R�5�      � .+�5�      �� $ Bd��6!   #���  6-   #���  6_   $��2q�5�   !   $ 9��65   �� 5�      $ Cp��6+   $��Center=�67   $��Center=�6T   $��Consolas)�6o   #���  5�   +    . 6g   5�      5�      5�      5�      5�      