   M S B A M L   `   `   `  ���� M  IWeb Scraping Mania, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Web_Scraping_ManiaWeb_Scraping_Mania  )    #Web_Scraping_Mania.Views.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:Web_Scraping_Mania       � Title$  Scraping Mania��$	��470��$	��876�� � WindowState$ 	Maximized=� � 	Resources    
UpdateIcon �� 	UriSource��%(   &	        �� $ Icon\update-12.png?�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� �� ��  � ColumnDefinitions �� $
��Auto ��� $��6* ��� $
��Auto ��� $��6* ��� $��5* ��� $��5* ��� $��5* ��� $��5* �.+�o� $��Center=�$��15(�$��8��$��22��.@�m� $��Файл��.@�m� $��Вкладки��.@�m� $!��Додати вкладку�� m� Command �� *AddTabCommand+m� $%��Видалити вкладку�� �� *RemoveTabCommand+m� $��Файл��.@�m� $��Відкрити файл�� �� *
OpenFile+ �� IsAsync$
 True��m� $+��%Зберегти проєкт як...�� �� *SaveProjectCommand+m� $+��%Зберегти обране як...�� �� *SaveSelectedFile+m� $��Викачати код�� �� *DownloadCode+m� $��Парсинг��.@�m� $$��Парсинг по тегам�� �� *GetByTagCommand+m� $*��$Парсинг усього коду�� �� *GetAllCommand+$
 True��m� $��Прев'ю�� �� *OpenPreviewWindow+m� $��Про автора�� �� *AboutAuthor+�� $(��"Поточна сторінка...��$��5,2,0,2q�$��17(�$��0��$��1���� $��1��$��1��$��5,2,0,2q�$��15(����� *
WebSites+ �� SelectedItem �� *SelectedItem+���� .R��� ���� *Title+�� $��2��$��1��$��30��$��25��$��Center=�$��5,2,5,2q�$��Center=����� *UpdateButton+���� $��Stretch=�$��Stretch=����� 	 �� Source#	 [  �� $��1��$��3��$��15(����� *
SearchText+�� $��4��$��1��$��Знайти в коді��$��15(����� *TextSelection+� $��5��$��1�� �� $
��0.8* ��� �� $
��0.8* �.+��� $��0��$��-��$��15(����� *MinusSelectionHtml+{��� 
 �� Path$
 IsEnabled[0]��� $��1�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��15(����� $
 DictCount[0][2]��� $��15(�$��/���� $��15(����� $
 DictCount[0][1]��� $��2��$��+��$��15(����� *PlusSelectionHtml+{��� $
 IsEnabled[0]�� $��6��$��1�� �� $
��0.8* ��� �� $
��0.8* �.+��� $��0��$��-��$��15(����� *MinusSelectionCss+{��� $
 IsEnabled[1]��� $��1��$ 
Horizontal=�$��Center=�.+��� $��15(����� *DictCount[1][2]+�� $��15(�$��/���� $��15(����� *DictCount[1][1]+�� $��2��$��+��$��15(����� *PlusSelectionCss+{��� $
 IsEnabled[1]�� $��7��$��1�� �� $
��0.8* ��� �� $
��0.8* �.+��� $��0��$��-��$��15(����� *MinusSelectionScript+{��� $
 IsEnabled[2]��� $��1��$ 
Horizontal=�$��Center=�.+��� $��15(����� *DictCount[2][2]+�� $��15(�$��/���� $��15(����� *DictCount[2][1]+�� $��2��$��+��$��15(����� *PlusSelectionScript+{��� $
 IsEnabled[2]��� -    ��Name$ HtmlCodeHandler��$��8��$��2������ *SelectedItem.HtmlFiles+ �� *SelectedHtmlTab+�� -   $ CssCodeHandler��$��8��$��3������ *SelectedItem.CssFiles+ �� *SelectedCssTab+�� -   $ ScriptsHandler��$��8��$��4������ *SelectedItem.Scripts+ �� *SelectedScriptTab+