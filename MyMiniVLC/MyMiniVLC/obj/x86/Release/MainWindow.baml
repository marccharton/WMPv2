   M S B A M L   `   `   `  ���� D  @MyMiniVLC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyMiniVLC	MyMiniVLC      MyMiniVLC.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyMiniVLC     -     �Name$  window�� � Title$ 
MainWindow��$	��350��$	��525�� � 	Resources  "  ButtonControlTemplateMyMiniVLC �� 
TargetType �� CornerRadius �� EndPoint � MappingMode �� 
StartPoint � Color	 � Offset��%  &	        �� " ��.R��� $
��20,2q�$ 50��$
��Auto��$��	1,0,4.2,0q���Z� ���� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ Black��$	 1Q�� $ White��._�� ���	#00000000.+��� $��0q�$
��Wrap=�#��z��$��Center=�$��Center=����� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ Black��$	 1Q�� # � ��
 J� CommandBindings
 )  #System.Windows.Input.CommandBinding  -      MyMiniVLC.MainWindow   fileDialogRoutedCommand   Command# Z  J� InputBindings ��  �� Command# Z 
 �� Key$ O�� �� 	Modifiers$ Controlf�.��� $��0q�.+�� $��
0,0.8,0,74q�.+�r� -    r�Name$ videoPlayer�� r� LoadedBehavior$ Manual=�� $
��21.6��$��
0,0,0,51.2q�$��Bottom=�.+��� -    ��Name$ 
sliderTime����Z� � $
��51.2��$��Bottom=�.+�]� ��.  $��0q�$��Center=�.+��� -    ��Name$ 
buttonPlay��$
��Play��$��0q�$	��150���� -   $ buttonPause��$��Pause��$��0q�$	��150��