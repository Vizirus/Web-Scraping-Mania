   M S B A M L   `   `   `  ����M  IWeb Scraping Mania, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Web_Scraping_Mania.ViewsWeb_Scraping_Mania.Views  5   	   M+clr-namespace:Web_Scraping_Mania.ViewModelsWeb_Scraping_Mania.ViewModels  5   	   )    #Web_Scraping_Mania.Views.ParseByTag   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Web_Scraping_Mania.Views     5   	   :VW+clr-namespace:Web_Scraping_Mania.ViewModels     5   	     � Title$$  Парсинг по тегам��5	   	   $	��400��6"   $	��400��6/    � 
ResizeMode$ CanMinimize=�6;   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $%��Введіть вираз XPath��6   $��15(�60   $��0��6>   $��1��6N   $��Center=�6[   $��5,0,0,0q�6v   �� 5   
   $��1��6   $��1��6   $��Center=�6/   $��5,0,5,0q�6J   $��15(�6[   $��30��6   ��6i   �� *Data+6   �� 5   
   $2��,Виберіть режим парсингу��6   $��15(�64   $��0��6B   $��2��6R   $��Center=�6_   $��5,0,0,0q�6z   �� 5   
   $��2��6   $��1��6    $��30��60   $��5,0,5,0q�6<   $��Center=�6N   $��15(�6�   ��6i   �� *ParseProperty+ �� SelectedItem 6�   �� *SelectedItem+��5      �� 5      .R�5      �� ��6    �� *	ComboText+5      5!      �� 5#   
   $	��100��6   $��30��6   $��3��6)   $��1��66   $��Bottom=�6F   $��Right=�6a   $��5,0,5,0q�6}   $��Спарсити��6�   $��15(�6�   ��6�   �� *	Command+ �� IsAsync$
 True��6�   �� 5%   
   $��3��6   $D��>Підказка (з'явиться при навединні)��6!   $
��Bold"�6K   $��11(�6]   $��5,0,5,0q�6�    � ToolTip 6k   �� *ToolTipText+6�   �� 5&   
   $��3��6   $��30��6   $
��Bold"�6*   $��Довідка по XPATH��6<   $
��Left=�6W   
������ 
������ $��5,0,5,0q�6�   ��6�   �� *LinkCommand+6�   5'      5(      