   M S B A M L   `   `   `  ���� J  FBore  adventure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Labirintus
Labirintus      Labirintus.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:Labirintus     -     � Title$  Bore Adventure��.��� 
������� � 	Resources  �� 
TargetType �� Property �� Value��%/   '��       �� " ��.���  � ��$
 Bold"� � ColumnDefinitions �� �� $
��auto ��� $
��auto �.+��� $��0��� -    �Name$ 	kartaGrid��|�.   � RowDefinitions �� �� $
��auto �.+�(�  � Cursor$
 Hand��.���� -   	 ��Name$	 ic������ *
game.karta+���� 
 �� ImageSource
 �� *currentSkin.Bkg+���� .R��� ����  � FocusVisualStyle Z� ���� .R���  �� Orientation$ 
Horizontal=�����  �� DataType  Labirintus.Cell"  .R��� $��30��$��30������ *Bkg+._��� ���� *File+�� -    ��Name$ pauseButton��$��Center=�$��Center=����� " �� �� Triggers ��  �� Binding �� *
IsPaused+ �� Value	 False.a���  � y�$ 	Collapsed=���  �� *
IsPaused+ True.a���  � y�$ Visible=���  �� *	IsLoading+ True.a���  � ��$&  Загрузка уровня...����  � {� .  ��  �� *	IsLoading+	 False.a���  � ��$5 /Нажмите для продолжения...����  � {� . � $��1��.+��� -   $��	LabelText��$��0��$I��CДоведите грустного Киану до выхода...��$
��Bold"�
������$��20(�� -   $��levelListGrid��$��2��$��4q�|�.   �� $
��auto ��� �� $
��auto ��� .+��� $��Уровни���� 
�������$��1����.  $��-1������ *	levelList+����   Labirintus.Level"  .R��� ���� *	shortName+���� " ��.���  �@��
IsSelected �� *
IsSelected+��  �@�ToolTip �� ���� *
fullPath+ � LayoutTransform ��  �� ScaleX$ 5Q� �� ScaleY$ 5Q��� $��2��$��
Стили���� 
�������$��3����.  $��-1������ *Skins.skinList+����   Labirintus.Skin"  .R��� ���� *	shortName+���� " ��.���  �@��
IsSelected �� *
IsSelected+