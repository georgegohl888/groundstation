   M S B A M L   `   `   `  ����c  _GMap.NET.WindowsPresentation, Version=2.0.1.0, Culture=neutral, PublicKeyToken=b85b9027b614afefqPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentationGMap.NET.WindowsPresentation  5   	   H DGroundStation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:GroundStationGroundStation 5   	      GroundStation.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   bgmapsPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentation     5   	   -localclr-namespace:GroundStation     5   	     � Title$  BEANS Ground Station��5	   	   $��688.5��6&   $��1050.5��65   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��410 �6   �� 5      $
��109* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��360 �6   �� 5      .+�5   
      GroundStation.UserControl1  -   5          Name$	 PFD��5         GroundStation.UserControl1   Loaded$ 
PFD_Loaded��6K   }�. $��1��6>   $��0,0,0,0q�6_   �� 5   
   -   5        $��ConnectDisconnect��5      $��Connect��6*   $��Right=�6<   $��0,0,0,26q�6X   $	��126��6j   $��1��6�   �� 5   
   -   5        $��	PortsList��5      $��Right=�6$   $	��126��6@   $��1��6L   $��26��6\   $��Bottom=�6h   �� 5   
   -   5        $��LogData��5      $��1��6%   $
��Left=�65   $��	10,10,0,0q�6P   $��2��6c   $	��Top=�6p   $��15��6�   $��64��6�   �� 5   
   $��2��6   .��5      �� -   5         ��Name$ DebugLog��5      5      �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��Current status��6C   $	��Top=�6Y   $��1��6q   $��46,0,0,0q�6�   $��34��6�   $	��151��6�   $��22(�6�   $
��Bold"�6�   �� 5   
   -   5        $ 	StatusBox��5      $��1��6'   $
��Left=�67   $��	52,23,0,0q�6R   $
��Wrap=�6e   $	��Top=�6�    J� RenderTransformOrigin$ -0.025,2.062&�6�   $��27��6�   $	��145��6�   
���  ��$��20(�6�   ��6y   �� *FlightStatus+6�   .   (GMap.NET.WindowsPresentation.GMapControl  5   
   -   5          Name$ mapView��5      $��1��6E   $��1��6R   �� 5   
   $
��Left=�6   $��36��6+   $��	10,10,0,0q�67   $	��Top=�6J   $��34��6b   $��Images/Satellite icon.png��6m   �� 5    
   $
��Left=�6   $��19��6-   $��	49,10,0,0q�69    �� TextWrapping$
 Wrap=�6L   $��6 SAT��6`   $	��Top=�6m   $��39��6�   ��6�   Z� ��6�   Z� �� 5!   
   $
��Left=�6   $��19��6-   $��	49,27,0,0q�69   $
 Wrap=�6L   $��GPS LOCK��6`   $	��Top=�6p   $��63��6�   ��6�   Z� ��6�   Z� �� 5"   
   $
��Left=�6   $��46��6+   $��
117,-1,0,0q�67   $	��Top=�6K   $��51��6c   $��Images/Battery icon 1.png��6n   �� 5#   
   $
��Left=�6   $��19��6-   $��
168,10,0,0q�69   $
 Wrap=�6M   $	��Top=�6�   $��53��6�   ��6a   �� *
BattVolt+	 �� StringFormat$	 {0} V����6�   Z� ��6�   Z� �� 5$   
   $
��Left=�6   $��19��6-   $��
168,26,0,0q�69   $
 Wrap=�6M   $��--.-- A��6a   $	��Top=�6p   $��53��6�   ��6�   Z� ��6�   Z� �� 5%   
   -   5        
 ��Name$
 FD_Enable_CheckBox��5%       d� 	IsChecked$
 trueY�6/   $��1��6@   $��	92,10,0,0q�6P   $��2��6c   $��Toggle Flight Director��6p   $��15��6�   $	��Top=�6�   $
��Left=�6  $	��137��6   � DataContext 5&        5'      5)      5*      5+      