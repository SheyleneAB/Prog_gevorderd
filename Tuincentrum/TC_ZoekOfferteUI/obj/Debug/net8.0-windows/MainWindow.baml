   M S B A M L   `   `   `  ����K  GTC_ZoekOfferteUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TC_ZoekOfferteUITC_ZoekOfferteUI  5   	   !    TC_ZoekOfferteUI.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:TC_ZoekOfferteUI     5   	   -   5          � Title$  Zoek Een Offerte��5   	   $	��300��6"   $	��400��6/   .��5
      �  � RowDefinitions 5   
   �� 5   
   $
��Auto �6   �� 5   
   $
��Auto �6   �� 5   
   $
��Auto �6   .+�5      �� $��0��6   $��Zoek Offerte ��6   $��20(�62   $��Center=�5      $��Center=�6*   $��10q�6E   �� 5      $��1��6   $	��250��6   .+�5   
   �� $��
Offerte ID��6   �� 5   
   -   5        $��OfferteIdTextBox��5      6*   �� 5   
   $��Klant Naam:��6   $��0,10,0,0q�6'   �� 5   
   -   5        $��
cboKlanten��5      $
��Left=�6%   $	��240��6@   $��0,0,10,0q�6L   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��0,0,10,0q�6:   ��6$   �� *Naam+6:   �� 5      ��6$   �� *Adres+5      5       5"      �� 5$   
   $��Datum��6   (  "System.Windows.Controls.DatePicker  5%   
   -   5        $��DatumPicker��5%      6(   5&      �� 5(      -   5        $��SearchButton��5(      $��2��6!   $��Search��6.   $��20q�6Z   $	��250��6f   5)      5*      