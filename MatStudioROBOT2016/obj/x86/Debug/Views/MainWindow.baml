   M S B A M L   `   `   `  ����M  IMatStudioROBOT2016, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MatStudioROBOT2016.ViewsMatStudioROBOT2016.Views  5   	   M+clr-namespace:MatStudioROBOT2016.ViewModelsMatStudioROBOT2016.ViewModels  5   	   A =MatGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/$clr-namespace:MatGUI;assembly=MatGUIMatGUI 5   	   )    #MatStudioROBOT2016.Views.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5   	   L HLivet, Version=1.3.0.0, Culture=neutral, PublicKeyToken=b0b1d3f711ef38cb.l&http://schemas.livet-mvvm.net/2011/wpf  5   	   .v&clr-namespace:MatStudioROBOT2016.Views  5   	   4vm+clr-namespace:MatStudioROBOT2016.ViewModels  5   	   1matgui$clr-namespace:MatGUI;assembly=MatGUI  5   	   -   5          � Title$  
MainWindow��5	   	   $	��600��6   $
��1000��6)    � UseLayoutRounding .    MatBackGroundBrush#���   5   	    � WindowState$ 	Maximized=�5   `    � DataContext 5      0   *MatStudioROBOT2016.ViewModels.MainWindowVM  5   
   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ ContentRendered��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      +  %Livet.Behaviors.LivetCallMethodAction     
MethodName$ 
Initialize��6?      MethodTarget 6&   �� 6?   5        5    
   $ Closed��6   . 5!      .  (Livet.Behaviors.DataContextDisposeAction  5"      .��5(      � 	 � RowDefinitions	 5)   
   �� 5*      $��26 �6   �� 5+      $��1* �6   .+�5.   
   o� $��0��6   $
��Left=�62   ��6   Z� .@�5/        MatGUI.MatMenuItem  $��	パネル��6!    50      2   ,MatStudioROBOT2016.ViewModels.ControlPanelVM  51      .@�53        $��ターミナル��6%     Terminal
 m� CommandParameter#
 �  6m    m� Command 64   �� *ShowControlPanelCommand+ �� Mode$ OneWay=�6m     54      $$��アプリケーションログ��6%     ApplicationLog#
 �  6r    69   �� *ShowControlPanelCommand+$ OneWay=�6r     55      $��データフロー��6%     DataFlow#
 �  6n    65   �� *ShowControlPanelCommand+$ OneWay=�6n     56      $��ロボットコア��6%   .@�57        $��設定��6)     	RobotCore#
 �  6n    65   �� *ShowControlPanelCommand+$ OneWay=�6n     58      $��インターフェイス��6)     	Interface#
 �  6t    6;   �� *ShowControlPanelCommand+$ OneWay=�6t     59      $+��%ロボットコア コントロール��6)     RobotCoreMain#
 �  6x    6@   �� *ShowControlPanelCommand+$ OneWay=�6x   5:        5;      $��ゲームメーカー��6%     	GameMaker#
 �  6o    66   �� *ShowControlPanelCommand+$ OneWay=�6o   5<        5>      $��ワークスペース��6!   .@�5?        -   5        $��読み込み��5?   %   63     5@      -   5        $��名前をつけて保存��5@   %   67   	  MatGUI.MatMenuSeparator	  5A        5B      -   5        $!��初期設定として保存��5B   %   68   5C      5D      
  MatGUI.MatWorkspace
  5F   
   $��1��6   .��5G      � .+�5H      � .+�5I      %  MatGUI.PhantasmagoriaTabControl  5K      5L      5M      5N      5P      5Q      