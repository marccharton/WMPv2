   M S B A M L   `   `   `  ����  MediaPlayer    MediaPlayer.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6?   -   5          �Name$  Window��5       � Title$ MyWindowsMediaPlayer v2��5      $	��640��5      $	��480��6   $	��120��6    � 	Resources 5	         ButtonPlayTemplate �� 
TargetType ��Name �� CornerRadius J� RenderTransformOrigin'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition	   GeneratedDuration	
   To   States   System.Windows.VisualState   
Storyboard �� TargetProperty �� 
TargetName8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value  ButtonNextTemplate F�Name �� Stretch �� StrokeDashArray��%^  &	        &	     �� 5
      " ��6/   .R�5      �� $ border��6   $��1,1,0,0q�6   $ 60��66   ���	#88FFFFFF$��0,-3.2,-3.2,0q�6_   
�������$ 0.5,0.5&�6�    5        5      CommonStates�� 5        5   	   $	 0:0:0.4D�6   $
 	MouseOver��66     5   	   $	 0:0:0.4D�6   $
 Normal��66   . 5        
Normal��6     5      	MouseOver��. 5   	   �� .��5   
   �� $0 *(Panel.Background).(SolidColorBrush.Color)�6'   $ border��6n   .g�5        $ 0��6   $ 	#CC4E92B0��6+   5      5   
   5   	     5      Pressed��6     5      Disabled��6   5      ._�5      � .+�5      �� $��0q�6   $
��Wrap=�6   #��z��60   $��Center=�6Q   $��Center=�6l   5       5!      5"      �� 5#      " ��6/   .R�5$      �  5%        5&      CommonStates�� 5'        5(   	   $	 0:0:0.4D�6   $
 Normal��66     5)   	   $	 0:0:0.4D�6   $
 	MouseOver��66   . 5+        
Normal��. 5,   	   �� .��5-   
   �� $* $(Shape.Fill).(SolidColorBrush.Color)�6'   $
 path��6h   .g�5.        $ 0��6   $ 	#99F4F4F5��6+   5/      50   
   51   	     52      	MouseOver��. 53   	   �� .��54   
   �� $* $(Shape.Fill).(SolidColorBrush.Color)�6'   $
 path��6h   .g�55        $ 0��6   $ 	#CC4E92B0��6+   56      57   
   58   	     59      Pressed��6     5:      Disabled��6   5;      .+�5=      F� $
 path��6   ����0�4��O�@8�N��?�Q8q(�!@�bk�s��?�v,׷� ��,<F� �	k-��0a���B`#@#�hY����@#��ch� 0�ed�� �R���#@�4��O�@8�N��?	���	#99F4F4F5$��0.425,-1.505,0.175,0.225q�6�   $
 Fill=�6   
�������$ 0.189,0.493&�6>  $	 1 0R�6b  $	��0.5��6x  �� 5>      $��9.9,11.5,4.775,12.8q�6   $
��Wrap=�6-   #��z��6A   5?      5@      5A      .��5C      � -   5         �Name$ 
LayoutRoot��5C      .+�5D      �� $��	0,1.4,0,0q�6   ���	#FF80CAE3.@�5E      �� -   5         ��Name$ 	PlayerTab��5E      $��Player��6    .��5F      � ��5G      
� 5H       
� GradientOrigin$	 0,1&�6    
� RadiusX$ 1Q�61    
� RadiusY$ 1Q�6=   .��5I   	   �  � Color$ 	#FF00A9E2��6    � Offset$ 0Q�6(   � 5J   	   $ 	#FF94E4FF��6   $ 1Q�6(   5K   	   .+�5M      � -   5        $ 	MediaGrid��5M      $��8,8,8,52q�6   
���   �61   �� 5N      -   5         ��Name$ ControlsGrid��5N      $��48��6(   $	��8,0q�64   $��Bottom=�6A    �� Orientation$ 
Horizontal=�6\   $��Center=�6u   .+�5O      �� -   5         ��Name$ 
PrevButton��5O      $��P��6#   $
��Left=�6/   #���  6J   $��27��6z   $��	0,-3,-7,0q�6�   $��37��6�   $ 0.5,0.5&�6�   $��1q�6�   z�5P   	   V� 5Q   
   .~�5R      �� �� 5S      �� 5T       �� Angle$	 180Q�6   T� 5U      5V      5X   	   �� 5Y      -   5        $ 
PlayButton��5Y      $��P��6#   $��0q�6/   #���   6:   $��Center=�6j   $��37��6�   $��37��6�   $��Center=�6�   �� 5Z      -   5        $ 
NextButton��5Z      $��N��6#   $��Right=�6/   $��37��6K   #���  6W   $��27��6�   $��-5,0,0,0q�6�   5[      5\      5]      �� 5^      -	   5        $ 
LibraryTab��5^      $��Playlist��6!   .��5_      � ���	#FFE5E5E56   5`      5a      5b      5c      