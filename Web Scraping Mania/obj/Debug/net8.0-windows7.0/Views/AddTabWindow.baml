   M S B A M L   `   `   `  ����M  IWeb Scraping Mania, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Web_Scraping_Mania.ViewsWeb_Scraping_Mania.Views  5   	   M+clr-namespace:Web_Scraping_Mania.ViewModelsWeb_Scraping_Mania.ViewModels  5   	   +    %Web_Scraping_Mania.Views.AddTabWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Web_Scraping_Mania.Views     5   	   :VW+clr-namespace:Web_Scraping_Mania.ViewModels     5   	     � Title$!  Додати вкладку��5	   	   $	��450��6    $	��552��6-    � 
ResizeMode$ CanMinimize=�69   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��275* �6   �� 5      $
��243* �6    � RowDefinitions 5   
   �� 5      �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��0��6   $��
10,23,0,24q�6H   ��6/   �� *	TabName+{�6\   �� *EnableTitle+�� 5   
   $��1��6   $��1��6!   $.��(Введіть назву вкладки��61   $
��Wrap=�6N   $��17(�6b   $��Center=�6p   $��23��6�   $��8,0,0,0q�6�   {�6�   �� *EnableTitle+6�   �� 5   
   $��2��6   $��0��6   $��
10,23,0,23q�6H   ��6/   �� *	TabLink+6H   �� 5   
   $��3��6   $��1��6!   $9��3Введіть шлях для збереження��61   $
��Wrap=�6T   $��17(�6h   $��Center=�6v   $��23��6�   $��8,0,0,0q�6�   {�6�   �� *EnableTitle+6�   �� 5   
   $��3��6   $��0��6   $��
10,23,0,23q�6I   ��6/   �� *
FilePath+{�6]   �� *EnableDowmload+�� 5   
   $��2��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   $��0,0,10,0q�6g   .+�5      �� $6��0Введіть URL-адресу сторінки��6   $
��Wrap=�6;   $��17(�6O   $��Center=�6]   $��8,0,0,0q�6x   $��22��6�   �� 5      $��*��6   $��25(�6!   $��Center=�6/   
���  ��$��34��6[   $��5,0,0,0q�6g   5      �� 5!   
   $��4��6   $��1��6   $��50��6.   $!��Додати вкладку��6:   $��17(�6S   $	��200��6a   $��Center=�6m   $��Right=�6�   $��0,0,10,5q�6�   $��2��6�   ��6�   �� *AddTabCommand+6�   �� 5"   
   $��4��6   $��0��6"   $��Center=�62   $��0,0,0,10q�6M   .+�5#      �� $��Center=�6   $'��!Ввести ім'я вручну��62   $��	10,10,0,0q�6O   $��15��6�    d� 	IsChecked 6b   �� *EnableTitle+6�   	� 5$      $��Center=�6   $+��%Завантажити ресурси��65   $��	10,10,0,0q�6S   $��15��6�    6f   �� *EnableDowmload+6�   	� 5%      $��Center=�6   $0��*Додати не завантажуючи��65   $��	10,10,0,0q�6V   $��15��6�    6i   �� *EnableBuffering+6�   5&      5(      5)      