   M S B A M L   `   `   `  ����J  FBore  adventure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Labirintus
Labirintus  5   	       Labirintus.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Labirintus     5   	   -   5          � Title$  Bore Adventure��5   	   .��5      �  � 	Resources 5   
    �� 
TargetType �� Property �� Value��%T   '��       �� 5      " ��6   .�5      ��  � ��$
 Bold"�6/   5      5       � ColumnDefinitions 5   
   �� 5      �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   � 5   
   -   5         �Name$ 	kartaGrid��5      |�.   � RowDefinitions 5      �� 5      �� 5      $
��auto �6    .+�5      (�  � Cursor$
 Hand��6   .��5       �� -   5        	 ��Name$	 ic��5       ��6+   �� *
game.karta+��5!      �� 5"      
 �� ImageSource
 6%   �� *currentSkin.Bkg+��5$      �� 5%      .R�5&      �� ��6+   ��  � FocusVisualStyle 6C   Z� ��5'   "   �� 5(   &   .R�5)   *   ��  �� Orientation$ 
Horizontal=�65   5*   '   ��5,   "   �� 5-   &    �� DataType  Labirintus.Cell"  63   .R�5.   *   �� $��30��61   $��30��6<   ��6H   �� *Bkg+._�5/   .   �� ��64   �� *File+50   +   51   '   53      54      56      57      �� 59      -   5         ��Name$ pauseButton��59      $��Center=�6C   $��Center=�6`   ��5:      �� 5;      " ��6    �� Triggers 5<      �� 5=       �� Binding 6*   �� *
IsPaused+ �� Value	 False6G   .a�5>   "   ��  � y�$ 	Collapsed=�6?   5?      �� 5@       6*   �� *
IsPaused+ True6G   .a�5A   "   ��  � y�$ Visible=�6?   5B      �� 5C       6*   �� *	IsLoading+ True6H   .a�5D   "   ��  � ��$&  Загрузка уровня...��6<   �� 5E   "    � {� .  6>   5F      �� 5G       6*   �� *	IsLoading+	 False6H   .a�5H   "   ��  � ��$5 /Нажмите для продолжения...��6<   �� 5I   "    � {� . 6>   5J      5L      5N      � 5P      $��1��6   .+�5Q      �� -   5        $��	LabelText��5Q      $��0��6)   $I��CДоведите грустного Киану до выхода...��63   $
��Bold"�6c   
������$��20(�6�   6�   5R      5S      � 5U   
   -   5        $��levelListGrid��5U      $��2��6$   $��4q�64   |�.   5V      �� 5W      $
��auto �6    �� 5X      �� 5Y      $
��auto �6    �� 5Z      .+�5]      �� $��Уровни��6   �� 5_      $��1��6   ��.  $��-1��6W   ��6#   �� *	levelList+��5`      �� 5a        Labirintus.Level"  6#   .R�5b      �� ��6$   �� *	shortName+5c      ��5e      �� 5f      " ��6   .�5g      ��  �@��
IsSelected 67   �� *
IsSelected+�� 5h       �@�ToolTip 5i      �� 5j   "   ��6(   �� *
fullPath+ � LayoutTransform 5k   &   �� 5l   *    �� ScaleX$ 5Q�69    �� ScaleY$ 5Q�6D   5n   #   5p      5q      5s      �� 5u      $��2��6   $��
Стили��6%   �� 5w      $��3��6   ��.  $��-1��6\   ��6#   �� *Skins.skinList+��5x      �� 5y        Labirintus.Skin"  6#   .R�5z      �� ��6$   �� *	shortName+5{      ��5}      �� 5~      " ��6   .�5      ��  �@��
IsSelected 67   �� *
IsSelected+5�      5�      5�      5�      5�      