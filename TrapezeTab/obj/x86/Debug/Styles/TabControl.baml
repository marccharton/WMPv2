   M S B A M L   `   `   `  ����H  DTrapezoidTabs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TrapezoidTabsTrapezoidTabs  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   BorderBrush1  �� Color  ForegroundBrush  
HoverBrush  TabControlBackgroundBrush �� EndPoint �� 
StartPoint � Color � Offset  TabItemPathBrush  TabControlStyle �� 
TargetType �� Property �� Value �� 
TargetType	 � RowDefinitions
 K� ZIndex �� CornerRadius �� Orientation K� IsItemsHost �Name F�Name �� ElementName,    &TrapezoidTabs.ContentToMarginConverter    Value �� 	Converter E� IsClosed E� 
StartPoint*   $TrapezoidTabs.ContentToPathConverter   Value � LayoutTransform �� ScaleY ��Name �� Path ��Name �� Triggers P� 
SourceName P� Property P� Value �� 
TargetName  F� 	Direction! F� Opacity" F� ShadowDepth# F� Softness�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      !cclr-namespace:TrapezoidTabs  5      %}	  &	        &	 #     &	 F     &	 i     &	 j    &	     7   7  7   '��     7   7  7   7  7  �� 5      $  	#FFFFFFFF��6+   �� 5      $  	#FF000000��6.   �� 5      $  	#FFFF4500��6)   �� 5	      $ 0.5,0&�6<   $ 0.5,1&�6M   .��5
   
   � $ 	#FFa9cde7��6   $ 0Q�6)   � 5   
   $ 	#FFe7f4fc��6   $	 0.3Q�6)   � 5   
   $ 	#FFf2fafd��6   $
 0.85Q�6)   � 5   
   $ 	#FFe4f6fa��6   $ 1Q�6)   5      �� 5      $	 0,0&�63   $	 0,1&�6D   .��5   
   � $ 	#FF3164a5��6   $ 0Q�6)   � 5   
   $ 	#FFe4f6fa��6   $ 1Q�6)   5      �� 5      " ��6$   .�5   
   ��  � ��$ 1q�6,   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      � 	 5      �� 5      $
��Auto �6,   �� 5      $��* �6,   .+�5      �� $��1��6!   $��2,0,2,2q�6.   $
 2��6H   $ 0,0,2,2��6Y   8��  8�� ._�5"      �� $��SelectedContent��6/   5#      �� 5%      $ 
Horizontal=�6%   $��0��6>   $
 1��6K    . 6\   �� 5&      $��0��6$   $��2��61   $��Bottom=�6<   8�� 5'   $   5(      5)      5+      5,      �� 5.      " ��6%   .�5/   
   ��  �@J�SnapsToDevicePixels . 60   �� 50   
    � �� 51      �� 52      " ��6"   .R�53      � $	 grd��6   .+�54      F� $ TabPath��6   $��2��60   8��  8�� ��55      �� $ TabItemContent��# Z ��58      B� 59   "   .��5:   &   E�  .  $	 1,0&�6B   ��5;   1   �� $ TabItemContent��# Z 5<   '   5=   #    5?      �� 5A   "   $ -1Q�61   5C      �� 5D      $ TabItemTopBorder��6$   $��2��6>   $y�Visible=�6I   $��Bottom=�5E   $   8�� ��5F   $   �� $ TabPath��$ Margin��� 5G      $ TabItemContent��6+   $��Header��6C   $��	10,2,10,2q�5H   +   $��Center=�6>   8�� 5I   +   5J       5K      P� 5L      $	 grd��6F    �@J�IsMouseOver . . �5M      �� 8  $ TabPath��6[    � ��5N      P� 5O       �@��
IsSelected . . �5P      �� $ TabPath��65    � �� 5Q   "   �� 5R   &   $  	#FFe4f6fa��66   5T      �� 5U       �@J�BitmapEffect 5V   "   F� 5W   &   $	  302Q�6=   $	! 0.4Q�6M   $" 2Q�5X   =   $	# 0.5Q�6M   5Z      �� 5[       �@K�ZIndex$ 2��6=   �� 5\      $ TabItemTopBorder��6J    � y�$ Hidden=�6;   6J   5]      5_      5a      5b      5d      