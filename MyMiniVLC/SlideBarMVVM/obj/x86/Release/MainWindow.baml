   M S B A M L   `   `   `  ���� G  CSlideBarMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SlideBarMVVMSlideBarMVVM      SlideBarMVVM.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,localclr-namespace:SlideBarMVVM     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions    � Title$  
MainWindow��$	��350��$	��525�� � 	Resources '   !SlideBarMVVM.MediaPlayerViewModel   ViewModelDataSource 0 ,HorizontalSliderThumbHoverBackgroundInverted �� EndPoint �� 
StartPoint � Color � Offset $  HorizontalSliderThumbHoverBorder �� Color 2 .HorizontalSliderThumbPressedBackgroundInverted & "HorizontalSliderThumbPressedBorder  SliderThumbDisabledGeometry & "SliderPointedThumbDisabledGeometry ) %SliderPointedThumbOuterBorderGeometry 1 -HorizontalSliderThumbNormalBackgroundInverted *	 &SliderPointedThumbMiddleBorderGeometry  
 HorizontalSliderUpThumbStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType J� SnapsToDevicePixels �� Angle T� X T� Y F�Name �� Triggers P� Property P� Value �� 
TargetName ( $HorizontalSliderThumbHoverBackground * &HorizontalSliderThumbPressedBackground ) %HorizontalSliderThumbNormalBackground " HorizontalSliderDownThumbStyle % !HorizontalSliderTrackNormalBorder ) %HorizontalSliderTrackNormalBackground  SliderRepeatButtonStyle " SliderThumbOuterBorderGeometry # SliderThumbMiddleBorderGeometry  HorizontalSliderThumbStyle ��Name J� RenderTransformOrigin'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� TargetProperty �� 
TargetName R� KeyTime  System.Windows.Visibility   Visible R� Value8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value  A�Name! �� Left
" �� Top# K� ZIndex$ �� ElementName% �� Mode & "VerticalSliderThumbHoverBackground ( $VerticalSliderThumbPressedBackground ' #VerticalSliderThumbNormalBackground   VerticalSliderLeftThumbStyle . *VerticalSliderThumbHoverBackgroundInverted 0 ,VerticalSliderThumbPressedBackgroundInverted / +VerticalSliderThumbNormalBackgroundInverted ! VerticalSliderRightThumbStyle # VerticalSliderTrackNormalBorder  VerticalSliderThumbStyle  SliderStyle& � RowDefinitions' m�Name( m� Fill) m� 	Placement* ��Name+ �� CornerRadius, ��Name- ]�Name. J� OpacityMask/ ]� DecreaseRepeatButton0 �� DecreaseLarge   RepeatButtonStyle1 ]� IncreaseRepeatButton2 �� IncreaseLarge3 ]� Thumb4 n�Name5 �� Triggers6 � ColumnDefinitions ! 	PathStyle��%�+  &	        &	      &	 r     &	 �     &	 �     &	 
    &	 =    &	 �    &	      &		 n    &	
 �    7  7  7 	 7  7  7  7  7  7  7  7  7  &	 @    &	 �    &	     &	 �    7  7  7 	 7  7  7  7  7  7  7  7  7  &	     &	 Z    &	 o    &	     &	 �    &	 �    &	 �    &	 �    &	 _    &	 �    7  7  7 	 7  7  7  7  7  7  7  7  &	 =    &	 �    &	     &	 �    7  7  7 	 7  7  7  7  7  7  7  7  &	 �    &	 P    7  7  7  7  7  7  7  7  7  7  7  &	 �    7  7  7  7  7 
 7  7  7  7  7  7  7  7  &	  j(    &	! )      �� $	 0,0&�$	 0,1&�.��� $ white��$ 0Q�� $ #B8E2F9��$	 0.5Q�� $ #B0DFF8��$ 1Q��� $ #3C7FB1���� $	 0,0&�$	 0,1&�.��� $ #B8E4FA��$
 0.25Q�� $ #5EB4E3��$	 0.5Q�� $ #4C8BB4��$ 1Q��� $ #2C628B���-)M -5,-10.5 L 5,-10.5 L 5,10.5 L -5,10.5 Z��FBM 4.5,-8.5 L -4.5,-8.5 L -4.5,4.5 L -0.5,8.5 L 0.5,8.5 L 4.5,4.5 Z��qmM 4.5,-7.5 A 1 1 0 0 0 3.5,-8.5 L -3.5,-8.5 A 1 1 0 0 0 -4.5,-7.5 L -4.5,4.5 L -0.5,8.5 L 0.5,8.5 L 4.5,4.5 Z��� $	 0,0&�$	 0,1&�.��� $ White��$ 0Q�� $ #F0EFEF��$	 0.4Q�� $ #D6D5D5��$ .8Q��73M 3.5,-7.5 L -3.5,-7.5 L -3.5,4.5 L 0,8 L 3.5,4.5 Z��� " n�.���  � |�	 .  ��  �@�OverridesDefaultStyle	 . ��  � ��$	 18����  � ��$	 10����  � ��
	 �������  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $	 180Q�T� $ 5Q�$ 9Q�.+�F� $ 
Background��8��  8�� F� $ InnerBorder��8�� 
�������$��1��F� $ OuterBorder��8�� ���	#FF929292$��1�� P�  �@J�IsMouseOver . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  � ��
 ��  �. ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  �@n�
IsDragging . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	 	  � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF4F4F4�� $ InnerBorder��	 Z�  � ���� $ OuterBorder��8	 
  � ���� $ OuterBorder�� � ��	 �	#FFAEB1AF�� $ OuterBorder��8	   � ���� $	 0,1&�$	 0,0&�.��� $ white��$ 0Q�� $ #B8E2F9��$	 0.5Q�� $ #B0DFF8��$ 1Q��� $	 0,1&�$	 0,0&�.��� $ #B8E4FA��$
 0.25Q�� $ #5EB4E3��$	 0.5Q�� $ #4C8BB4��$ 1Q��� $	 0,1&�$	 0,0&�.��� $ White��$ 0Q�� $ #F0EFEF��$	 0.4Q�� $ #D6D5D5��$ .8Q��� " n�.���  � |�	 .  ��  �@�OverridesDefaultStyle	 . ��  � ��$	 18����  � ��$	 10����  � ��
	 �������  � ��	 �� "
 n�.R���  . z�T� $ 5Q�$ 9Q�.+�F� $ 
Background��8��  8�� F� $ InnerBorder��8�� 
�������$��1��F� $ OuterBorder��8�� ���	#FF929292$��1�� P�  �@J�IsMouseOver . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  � ��
 ��  �. ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  �@n�
IsDragging . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	 	  � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF4F4F4�� $ InnerBorder��	 Z�  � ���� $ OuterBorder��8	 
  � ���� $ OuterBorder�� � ��	 �	#FFAEB1AF�� $ OuterBorder��8	   � ���� $	 0,1&�$	 0,0&�.��� $ 	#FFAEB1AF��$	 0.1Q�� $ White��$ .9Q��� $ 	#FFE7EAEA���� " ��.���  �@�OverridesDefaultStyle	 . ��  � ��	 .  ��  � |�	 .  ��  � ��	 �� "
 ��.R��� 
������ $��Center=�$��Stretch=�$��3���yuM -5,-9.5 A 1 1 0 0 1 -4,-10.5 L 4,-10.5 A 1 1 0 0 1 5,-9.5 L 5,9.5 A 1 1 0 0 1 4,10.5 L -4,10.5 A 1 1 0 0 1 -5,9.5 Z��)%M -4,-9.5 L 4,-9.5 L 4,9.5 L -4,9.5 Z��� " n�.���  � |�	 .  ��  �@�OverridesDefaultStyle	 . ��  � ��$	 22����  � ��$	 11����  � ��
	 �������  � ��	 �� "
 n�.R��� $ canvas�� . $ 0.5,0.5&�z�V� .~��� �� �� T� $ 0Q�   CommonStates��.   
Normal��. �� .��T� $ (UIElement.Visibility)�$ ellipse��..�i� $ 0��# Z �� $H B(Shape.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Color)�$ ellipse��.g�  $ 0��$	 Red��  	MouseOver��  Pressed��  Disabled��.+�A� $  ellipse�����	#FF929292$��Stretch=�$��Stretch=�$��0q�$! 0��$" 6��$# 2������ *ActualWidth+$$ canvas��$% OneWay=����� *ActualWidth+$$ canvas��$% OneWay=����� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $	 Red��$ 1Q� P�  �@J�IsMouseOver . P�  � ��
 ��  �P�  �@n�
IsDragging . P�  � {� .  �� $	 1,0&�$	 0,0&�.��� $ white��$ 0Q�� $ #B8E2F9��$	 0.5Q�� $ #B0DFF8��$ 1Q��� $	 1,0&�$	 0,0&�.��� $ #B8E4FA��$
 0.25Q�� $ #5EB4E3��$	 0.5Q�� $ #4C8BB4��$ 1Q��� $	 1,0&�$	 0,0&�.��� $ White��$ 0Q�� $ #F0EFEF��$	 0.4Q�� $ #D6D5D5��$ .8Q��� " n�.���  � |�	 .  ��  �@�OverridesDefaultStyle	 . ��  � ��$	 10����  � ��$	 18����  � ��
	 �������  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $ 90Q�T� $ 9Q�$ 5Q�.+�F� $ 
Background��8��  8�� F� $ InnerBorder��8�� 
�������$��1��F� $ OuterBorder��8�� ���	#FF929292$��1�� P�  �@J�IsMouseOver . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  � ��
 ��  �. ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  �@n�
IsDragging . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	 	  � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF4F4F4�� $ InnerBorder��	 Z�  � ���� $ OuterBorder��8	 
  � ���� $ OuterBorder�� � ��	 �	#FFAEB1AF�� $	 0,0&�$	 1,0&�.��� $ white��$ 0Q�� $ #B8E2F9��$	 0.5Q�� $ #B0DFF8��$ 1Q��� $	 0,0&�$	 1,0&�.��� $ #B8E4FA��$
 0.25Q�� $ #5EB4E3��$	 0.5Q�� $ #4C8BB4��$ 1Q��� $	 0,0&�$	 1,0&�.��� $ White��$ 0Q�� $ #F0EFEF��$	 0.4Q�� $ #D6D5D5��$ .8Q��� " n�.���  � |�	 .  ��  �@�OverridesDefaultStyle	 . ��  � ��$	 10����  � ��$	 18����  � ��
	 �������  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $	 -90Q�T� $ 9Q�$ 5Q�.+�F� $ 
Background��8��  8�� F� $ InnerBorder��8�� 
�������$��1��F� $ OuterBorder��8�� ���	#FF929292$��1�� P�  �@J�IsMouseOver . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  � ��
 ��  �. ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  �@n�
IsDragging . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	 	  � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF4F4F4�� $ InnerBorder��	 Z�  � ���� $ OuterBorder��8	 
  � ���� $ OuterBorder�� � ��	 �	#FFAEB1AF�� $	 1,0&�$	 0,0&�.��� $ 	#FFAEB1AF��$	 0.1Q�� $ White��$ .9Q��� " n�.���  � |�	 .  ��  �@�OverridesDefaultStyle	 . ��  � ��$	 11����  � ��$	 22����  � ��
	 �������  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $ 90Q�T� $ 11Q�$	 5.5Q�.+�F� $ 
Background��8��  8�� F� $ InnerBorder��8�� 
�������$��1��F� $ OuterBorder��8�� ���	#FF929292$��1�� P�  �@J�IsMouseOver . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  � ��
 ��  �. ��� $ 
Background��8	   � ���� $ OuterBorder��8	   � ��P�  �@n�
IsDragging . . ��� $ 
Background��8	   � ���� $ OuterBorder��8	 	  � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF4F4F4�� $ InnerBorder��	 Z�  � ���� $ OuterBorder��8	 
  � ���� $ OuterBorder�� � ��	 �	#FFAEB1AF�� " ��.���  �@��IsPressAndHoldEnabled	 .  ��  � ��
	 ���� ��  � ��	 �	#FFC4C4C4��  � ��	 �� "
 ��.R��� #��z��#��z��#��z�� . ._�� & �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $' TopTick��#( z��$��4��$	) Top=�$��0��$y�	Collapsed=�m� $' 
BottomTick��#( z��$��4��$) Bottom=�$��2��$y�	Collapsed=��� $* TrackBackground��8��  $��1q�8�� $+ 1��$	��4.0��$	��5,0q�$��1��$��center=�._��� $��-6,-1q�.+��� $, PART_SelectionRange��#��� ��$	��4.0��#��� ��$	��1.0��$y�Hidden=�]� $- 
PART_Track��$��1��
. �   �/ �� #��Z0 #���   1 �� #��Z2 8�� $��Stretch=�$
��Auto��3 n� $4 Thumb��8�� ���	#00FF0000���	#00808080� $��Right=�$	��2.4��$��0,0,-92.2,-198q�$��2��$��Bottom=�$	��3.2�� P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$	 Visible=��� $ Thumb��8	   � ���� $ TrackBackground�� � ��$	 5,2,5,0q�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$	 Visible=��� $ Thumb��8	   � ���� $ TrackBackground�� � ��$	 5,0,5,2q�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$	 Visible=��� $ 
BottomTick�� � y�$	 Visible=�P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$	 Visible=�P�  �@J�IsKeyboardFocused . . ��� $ Thumb�� � ��
	 ��  �5 P�  �@��Orientation$ Vertical=�. ���  � ��	 �� "
 ��.R��� #��z��#��z��#��z�� . ._�� 6 �� $
��Auto ��� #��z��$
��Auto ��� $
��Auto �.+�m� $' TopTick��$��0��#( z��$
) Left=�$y�	Collapsed=�$��4��m� $' 
BottomTick��$��2��#( z��$) Right=�$y�	Collapsed=�$��4���� $* TrackBackground��8�� $��1q�8�� $��1��$+ 1��$��center=�$	��0,5q�$	��4.0��._��� $��-1,-6q�.+��� $, PART_SelectionRange��#��� ��#��� ��$	��1.0��$y�Hidden=�$	��4.0��]� $- 
PART_Track��$��1��/ �� #��Z0 8�� 1 �� #��Z2 8��	 3 n� $4 Thumb��8��
  P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$	 Visible=��� $ Thumb��8	   � ���� $ TrackBackground�� � ��$	 2,5,0,5q�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$	 Visible=��� $ Thumb��8	   � ���� $ TrackBackground�� � ��$	 0,5,2,5q�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$	 Visible=��� $ 
BottomTick�� � y�$	 Visible=�P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$	 Visible=�P�  �@J�IsKeyboardFocused . . ��� $ Thumb�� � ��
	 ��  ��� " ��.���  �@�OverridesDefaultStyle	 . ��  � ��	 .  ��  � |�	 .  ��  � ��	 �� "
 ��.R��� $��Center=�$��3��
���  ��$��6,0,0,0q��� " F����� $ 0.5,1&�7 � MappingMode$7 RelativeToBoundingBox=�$ 0.5,0&�.��� $ Black��$ 1Q�� $ 	#FF0072F1��.��� 8 � DataContext8 �� 9 �� Source#9 [  .+�� $
��23.8��$��0,0,0,47.733q�$��Bottom=�.+�    SlideBarMVVM.SpecialSlider  -   :  Name$: slider��#���  ; �� IsMoveToPointEnabled; . < � Value< �� *Value+= � Maximum= �� *	Maximum+> � Minimum> �� *	Minimum+��Z� ��Z� � $��47.733��$��Bottom=�.+��� -   ? ��Name$? 
buttonPlay��$��122.667,0,75.733,0q����� *PlayPauseButtonText+���� *PlayPauseButton+�� -   @ ��Name$@ 	timeLabel��$
��Left=�$
��Wrap=�$��122.667��
�������A �� TextAlignment$A Center=����� *CurrentSourceMedia+�� -   $? 
openButton��$	��...��$��Right=�$��75.733������ *OpenDialogCommand+� $��0,-0.533,0,75.533q�.+�r� -   B r�Name$B Trou_de_balle��C r� LoadedBehavior$C Manual=�D r� SourceD �� *CurrentSourceMedia+���� 5 �� E �� BindingE �� *	PlayState+F �� ValueF Play.a��� '   !SlideBarMVVM.MediaElementBehavior �@ 	Playstate$
	 Play=��� E �� *	PlayState+	F Pause.a���  �@ 	Playstate$	 Pause=��� E �� *	PlayState+F Stop.a���  �@ 	Playstate$
	 Stop=�