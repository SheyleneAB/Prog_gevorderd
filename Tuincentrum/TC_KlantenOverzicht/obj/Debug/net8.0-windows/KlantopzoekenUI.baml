   M S B A M L   `   `   `  ����N  JTC_KlantenOverzicht, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TC_KlantenOverzichtTC_KlantenOverzicht  5   	   )    #TC_KlantenOverzicht.KlantopzoekenUI   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:TC_KlantenOverzicht     5   	     � Title$  KlantopzoekenUI��5   	   $	��700��6!   $	��500��6.   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��2��6   $��Klantengegevens��61   $��16(�6K   $
��Bold"�6Y   $��Center=�6k   $��0,0,0,10q�6�   �� 5   
   $��1��6   $��0��6   $��Klant naam:��6-   $��10q�6C   $
��Bold"�6O   �� 5   
   $��1��6   $��1��6   $��10q�6L   ��6-   �� *
Klant.Naam+6L   �� 5   
   $��2��6   $��0��6   $��	Klant Id:��6-   $��10q�6A   $
��Bold"�6M   �� 5   
   $��2��6   $��1��6   $��10q�6J   ��6-   �� *
Klant.Id+6J   �� 5!   
   $��3��6   $��0��6   $��Klant Adres:��6-   $��10q�6D   $
��Bold"�6P   �� 5"   
   $��3��6   $��1��6   $��10q�6M   ��6-   �� *Klant.Adres+6M   �� 5$   
   $��4��6   $��0��6   $��Alle offertes:��6-   $��10q�6F   $
��Bold"�6R   &   System.Windows.Controls.DataGrid  5%   
   -   5        $��StatistiekenDataGrid2��5%   4   $��5��6   $��2��6       
IsReadOnly$
 True��6Q      AutoGenerateColumns$ False��6c   $��5q�6   $	��200��5&         Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5)      $ Prijs��6%   $ * 6Y    64   �� *Berekenendeprijs+6Y   5+      �� 5-   
   $��6��6   $��0��6"   $��2��62   	 �� Orientation$	 
Horizontal=�6F   $
��Left=�6_   $��0,10,0,0q�6z   .+�5.      �� $��Totaal Prijs:��6   $��10q�6,   $
��Bold"�68   �� 5/      -   5        $��Totaalprijs��5/      $��10q�6'   50      �� 52   
   $��7��6   $��0��6"   $��2��62   $	 
Horizontal=�6F   $
��Left=�6_   .+�53      �� $��Aantal offertes:��6   $��10q�6/   $
��Bold"�6;   �� 54      -   5        $��aantaloffertes��54      $��10q�6*   55      56      57      